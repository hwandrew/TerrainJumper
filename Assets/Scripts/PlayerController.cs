using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Partially taken from Holistic3D on Youtube */

public class PlayerController : MonoBehaviour {

	public Text winText;
	public float speed = 2.0f;
	public float jumpHeight = 2.0f;

	GameManager instance;
    RaycastHit hit;
	Rigidbody rb;
	float groundDist = 0.5f;

	// Use this for initialization
	void Start () {
		instance = GameObject.Find("GameManager").GetComponent<GameManager>();
		rb = this.GetComponent<Rigidbody>();

        // get rid of the cursor
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        DetectBlocks();
		FallReset();

        // Debug.Log(transform.forward);
	}

	public void DisableInput()
	{
		this.enabled = false;
		// Cursor.lockState = CursorLockMode.Locked;
	}

	public void EnableInput()
	{
		this.enabled = true;
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.GetComponent<BlockInfo>() != null)
		{
			if (!collision.gameObject.GetComponent<BlockInfo>().isSafe)
			{
				// TODO: make nice death here
				instance.ResetLevel();
			}
		}
		else if (collision.gameObject.name == "Finish")
		{
			winText.text = "Nice!";
			if (instance.GetCurrentLevel() == "Level_6")
			{
				winText.text = "Congrats, you've won!";
			}
			StartCoroutine(LongReset());
		}
	}


    /*
     * Waits 2 seconds, then loads the next level
     * Function is called after player reaches the end of a level
     */
	public IEnumerator LongReset()
	{
		yield return new WaitForSeconds(2);
		instance.LoadNextLevel();
	}

    /*
     * Restarts the current level when a player falls off the blocks
     */
	private void FallReset()
	{
		if (transform.position.y < -30f)
		{
			instance.ResetLevel();
		}
	}

    /*
     * Uses build in inputManager to move player
     * Player can move left, right, forward, backwards, and up (jump)
     */
    private void MovePlayer()
    {
        float translation = Input.GetAxis("Vertical");
        float straffe = Input.GetAxis("Horizontal");

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe * speed, 0, translation * speed);

        // jumping...
        if (Input.GetButtonDown("Jump") && IsOnFloor())
		{
			rb.AddForce(Vector3.up * jumpHeight);
		}
    }

    /*
     * DetectBlocks() manages the point-and-click feature of the game
     * When a player looks straight at a block, the block changes color slightly to indicate
     *      that the block can be clicked on
     * Uses Physics.Raycast to check if there is a clickable block infront of the player
     */
    private void DetectBlocks()
    {
        // send the raycast out of the camera child object
        bool detected = Physics.Raycast(transform.position, transform.GetChild(0).transform.forward, out hit);

        if (detected && hit.transform.gameObject.CompareTag("Moveable"))
        {
            BlockInfo currentBlock = hit.transform.gameObject.GetComponent<BlockInfo>();
            currentBlock.HighlightBlock();
            
            if (Input.GetButtonDown("Fire1"))
            {
                currentBlock.StartMoveBlock();
            }
        }
    }

    /*
     * Checks if the player is touching the floor, returns true if touching and false if not touching
     */
	private bool IsOnFloor()
	{
		RaycastHit temp;
		return Physics.SphereCast(transform.position + Vector3.up, 1.0f, Vector3.down, out temp, groundDist + 0.05f);
	}
}

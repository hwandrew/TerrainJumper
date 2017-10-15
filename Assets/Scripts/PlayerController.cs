using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Partially taken from Holistic3D on Youtube    */

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
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        DetectBlocks();
		FallReset();

        // Debug.Log(transform.forward);

		// press escape to allow cursor to exit the game screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				Cursor.lockState = CursorLockMode.None;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
			}
        }
	}

	public void Reset()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
			StartCoroutine(LongReset());
		}
	}

	public IEnumerator LongReset()
	{
		yield return new WaitForSeconds(2);
		instance.LoadNextLevel();
	}

	private void FallReset()
	{
		if (transform.position.y < -30f)
		{
			instance.ResetLevel();
		}
	}

    private void MovePlayer()
    {
        float translation = Input.GetAxis("Vertical");
        float straffe = Input.GetAxis("Horizontal");

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe * speed, 0, translation * speed);

        if (Input.GetButtonDown("Jump") && IsOnFloor())
		{
			rb.AddForce(Vector3.up * jumpHeight);
		}
    }

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

	private bool IsOnFloor()
	{
		RaycastHit temp;
		return Physics.SphereCast(transform.position + Vector3.up, 1.0f, Vector3.down, out temp, groundDist + 0.05f);
	}
}

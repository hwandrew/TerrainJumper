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

    RaycastHit hit;
	Rigidbody rb;
	float groundDist = 0.5f;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        DetectBlocks();
		FallReset();

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
		if (collision.gameObject.CompareTag("Moveable") && collision.gameObject.GetComponent<BasicBlockMovement>().canStart)
		{
			// TODO: make nice death here
			Reset();
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
		Reset();
	}

	private void FallReset()
	{
		if (transform.position.y < -30f)
		{
			Reset();
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
			if (hit.transform.gameObject.GetComponent<BasicBlockMovement>() != null)
			{
				BasicBlockMovement curr = hit.transform.gameObject.GetComponent<BasicBlockMovement>();
				curr.isLooking = true;
				if (Input.GetButtonDown("Fire1"))
				{
					curr.StartMoveBlock();
				}
			}
			else if (hit.transform.gameObject.GetComponent<HorizontalBlockMovement>() != null)
			{
				HorizontalBlockMovement curr = hit.transform.gameObject.GetComponent<HorizontalBlockMovement>();
				curr.isLooking = true;
				if (Input.GetButtonDown("Fire1"))
				{
					curr.StartMoveBlock();
				}
			}
        }
    }

	private bool IsOnFloor()
	{
		RaycastHit temp;
		if (Physics.SphereCast(transform.position, 1.05f, Vector3.down, out temp, groundDist + 0.05f))
		{
			Debug.Log("Grounded!!!");
		}
		else
		{
			Debug.Log("Fuck...");
		}
		return Physics.SphereCast(transform.position, 1.0f, Vector3.down, out temp, groundDist + 0.05f);
	}
}

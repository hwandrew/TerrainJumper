﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Partially taken from Holistic3D on Youtube    */

public class PlayerController : MonoBehaviour {

	public float speed = 2.0f;
	public float jumpHeight = 2.0f;

    RaycastHit hit;
	Rigidbody rb;
	float groundDist = 0.5f;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        DetectBlocks();
		FallReset();
	}

	public void Reset()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.CompareTag("Moveable") && collision.gameObject.GetComponent<BlockMovement>().canStart)
		{
			// TODO: make nice death here
			Reset();
		}
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
            if (Input.GetButtonDown("Fire1"))
            {
                hit.transform.gameObject.GetComponent<BlockMovement>().StartMoveBlock();
            }
        }
    }

	private bool IsOnFloor()
	{
		return Physics.Raycast(transform.position, Vector3.down, groundDist + 0.05f);
	}
}

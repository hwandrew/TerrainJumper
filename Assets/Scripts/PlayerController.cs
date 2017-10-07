using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Partially taken from Holistic3D on Youtube    */

public class PlayerController : MonoBehaviour {

    RaycastHit hit;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        DetectBlocks();
	}

    void MovePlayer()
    {
        float translation = Input.GetAxisRaw("Vertical");
        float straffe = Input.GetAxisRaw("Horizontal");

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);
    }

    void DetectBlocks()
    {
        // send the raycast out of the camera child object
        bool detected = Physics.Raycast(transform.position, transform.GetChild(0).transform.forward, out hit);

        if (detected && hit.transform.gameObject.CompareTag("Moveable"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // TODO: use hit information to move block upwards
                Debug.Log("trying to move...");
                hit.transform.gameObject.GetComponent<BlockMovement>().StartMoveBlock();
            }
        }
    }
}

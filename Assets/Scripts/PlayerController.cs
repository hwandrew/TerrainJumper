using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Partially taken from Holistic3D on Youtube */

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float translation = Input.GetAxisRaw("Vertical");
        float straffe = Input.GetAxisRaw("Horizontal");

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);
	}
}

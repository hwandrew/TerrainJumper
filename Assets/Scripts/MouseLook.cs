﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* MouseLook from Holistic3D on YouTube */

public class MouseLook : MonoBehaviour {

    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    public float minY = -70f;
    public float maxY = 70f;

    Vector2 mouseLook;
    Vector2 smoothV;
    GameObject character;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, minY, maxY);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}

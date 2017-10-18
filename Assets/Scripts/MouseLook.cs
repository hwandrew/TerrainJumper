using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* MouseLook algorithm from Holistic3D on YouTube */

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

        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV * sensitivity;
        // prevent user from going upside down when rotating on x-axis (looking up/down)
        mouseLook.y = Mathf.Clamp(mouseLook.y, minY, maxY);

        // Quaternion.AngleAxis changes the local axis of object when assigned to transform.localRotation
        // allows transform.forward to be infront of the direction that the player is facing
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Green block moves from location A (startPos) to location B (endPos) when clicked. The block stays
 * at B until clicked again, in which it then goes back to location A
 * 
 * Block can be landed on anytime
 */

public class GreenBlockMovement : MonoBehaviour {

    // set in inspector
    public Vector3 endPos;
    public Color normColor = Color.green;
    public Color hoverColor = Color.green;
    public float speed = 4.0f;
    public float time = 1.0f;
    public bool canStart = true;
    public bool atStart = true;
    public bool isLooking = false;

    private Vector3 startPos;
    Renderer rend;

	// Use this for initialization
	void Start () {
        startPos = transform.position;

        rend = this.GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning("No component <renderer>");
        }
        rend.material.color = normColor;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLooking)
        {
            rend.material.color = normColor;
        }
    }

    private void LateUpdate()
    {
        if (isLooking && canStart)
        {
            rend.material.color = hoverColor;
            isLooking = false;
        }
    }

    /* ---------- MOVING BLOCK FUNCTIONS ----------*/

    /*
     * Moves block from startPos to endPos when clicked once
     * 
     * If the block is clicked again, the block is moved from endPos back to startPos
     */
    public IEnumerator MoveBlock()
    {
        if (atStart)
        {
            for (float i = 0.0f; i < time; i += Time.deltaTime * speed)
            {
                transform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
            transform.position = endPos;
            atStart = false;
            canStart = true;
        }
        else if (!atStart)
        {
            for (float i = 0.0f; i < time; i += Time.deltaTime * speed)
            {
                transform.position = Vector3.Lerp(endPos, startPos, i);
                yield return null;
            }
            transform.position = startPos;
            atStart = true;
            canStart = true;
        }

        yield return null;
    }
}

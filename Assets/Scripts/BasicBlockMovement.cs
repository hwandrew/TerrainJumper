using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: add different block interactions?

public class BasicBlockMovement : MonoBehaviour {

    public float speed = 4.0f;
    public float moveDist = 2f;
    public float timeUp = 1.0f;
    public float timeDown = 4.0f;
    public bool canStart = true;

    Renderer rend;
    Vector3 startPos;
    Vector3 endPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        endPos = new Vector3(startPos.x, startPos.y + moveDist, startPos.z);

        rend = this.GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning("No component <renderer>");
        }
        rend.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartMoveBlock()
    {
        if (canStart)
        {
            canStart = false;
            StartCoroutine(MoveBlock());
        }
    }

    public IEnumerator MoveBlock()
    {
        for (float i = 0.0f; i < timeUp; i += Time.deltaTime * speed)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            rend.material.color = Color.Lerp(Color.red, Color.white, i);
            yield return null;
        }
        transform.position = endPos;

        yield return new WaitForSeconds(2.0f);

        for (float i = 0.0f; i < timeDown; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(endPos, startPos, i / speed);
            rend.material.color = Color.Lerp(Color.white, Color.red, i / speed);
            yield return null;
        }
        transform.position = startPos;

        canStart = true;
        yield return null;
    }
}

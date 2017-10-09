using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBlockMovement : MonoBehaviour {

	public Color normColor = Color.blue;
	public Color hoverColor = Color.blue;
	public float speed = 4.0f;
	public float moveDist = 2f;
	public float timeUp = 1.0f;
	public float timeDown = 4.0f;
	public bool canStart = true;
	public bool isLooking = false;

	Renderer rend;
	Vector3 startPos;
	Vector3 endPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		endPos = new Vector3(startPos.x + moveDist, startPos.y, startPos.z);

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
			rend.material.color = Color.Lerp(normColor, Color.white, i);
			yield return null;
		}
		transform.position = endPos;
		rend.material.color = Color.white;

		yield return new WaitForSeconds(2.0f);

		for (float i = 0.0f; i < timeDown; i += Time.deltaTime)
		{
			transform.position = Vector3.Lerp(endPos, startPos, i / speed);
			rend.material.color = Color.Lerp(Color.white, normColor, i / speed);
			yield return null;
		}
		transform.position = startPos;
		rend.material.color = normColor;

		canStart = true;
		yield return null;
	}
}

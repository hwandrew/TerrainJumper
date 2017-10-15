using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject mousePlane;
    public Material mouse1;
    public Material mouse2;
    public GameObject wasd;
    public GameObject space;
    public GameObject player;

    private int frames = 0;
    private bool isMouse1 = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(DeleteInstructions());
	}
	
	// Update is called once per frame
	void Update () {
		if (frames % 20 == 0)
        {
            if (isMouse1)
            {
                mousePlane.GetComponent<Renderer>().material = mouse2;
            }
            else
            {
                mousePlane.GetComponent<Renderer>().material = mouse1;
            }
            isMouse1 = !isMouse1;
        }
        frames++;

        mousePlane.transform.rotation = Quaternion.LookRotation(-player.transform.forward, player.transform.up);
    }

    private IEnumerator DeleteInstructions()
    {
        yield return new WaitForSeconds(3);
        Destroy(wasd);
        Destroy(space);
    }
}

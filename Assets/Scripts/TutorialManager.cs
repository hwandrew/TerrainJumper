using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * In charge of any tutorial aspect. This script is only found in scene "Level1"
 */

public class TutorialManager : MonoBehaviour {

    public GameObject mousePlane;
    public Material mouse1;
    public Material mouse2;
    public GameObject wasd;
    public GameObject space;
	public GameObject pauseInfo;
    public GameObject player;

    private int frames = 0;
    private bool isMouse1 = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(DeleteInstructions());
	}
	
	// Update is called once per frame
	void Update () {
        // every 20 frames, change the image of the mouse back and forth to show clicking
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

        // here is an attempt to rotate the plane according to where the player position is
        mousePlane.transform.rotation = Quaternion.LookRotation(-player.transform.forward, player.transform.up);
    }

    // get rid of the mildly annoying placement and intrusion of my WASD and Space icons to show player controls
    private IEnumerator DeleteInstructions()
    {
        yield return new WaitForSeconds(5);
		Destroy(pauseInfo);
        Destroy(wasd);
        Destroy(space);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour {

    public bool isSafe;
    public string typeBlock;

    // add all types of blocks
    BasicBlockMovement type0;
    HorizontalBlockMovement type1;
    GreenBlockMovement type2;

	// Use this for initialization
	void Start () {
        isSafe = true;
        
        switch (typeBlock)
        {
            case "Basic":
                type0 = this.GetComponent<BasicBlockMovement>();
                isSafe = false;
                break;
            case "Horizontal":
                type1 = this.GetComponent<HorizontalBlockMovement>();
                break;
            case "Green":
                type2 = this.GetComponent<GreenBlockMovement>();
                break;
			case "Death":
				isSafe = false;
			this.GetComponent<Renderer>().material.color = new Color((219/255f), (74/255f), (74/255f));
				break;
            default:
                Debug.LogWarning("typeBlock is probably null!");
                break;
        }
	}

    private void Update()
    {
        switch (typeBlock)
        {
            case "Basic":
                isSafe = (type0.canStart) ? false : true;
                break;
            case "Horizontal":
                break;
            case "Green":
                break;
			case "Death":
				break;
            default:
                isSafe = true;
                break;
        }
    }

    public void HighlightBlock()
    {
        switch (typeBlock)
        {
            case "Basic":
                type0.isLooking = true;
                break;
            case "Horizontal":
                type1.isLooking = true;
                break;
            case "Green":
                type2.isLooking = true;
                break;
            default:
                isSafe = true;
                break;
        }
    }

    public void StartMoveBlock()
    {
        switch (typeBlock)
        {
            case "Basic":
                if (type0.canStart)
                {
                    type0.canStart = false;
                    StartCoroutine(type0.MoveBlock());
                }
                break;
            case "Horizontal":
                if (type1.canStart)
                {
                    type1.canStart = false;
                    StartCoroutine(type1.MoveBlock());
                }
                break;
            case "Green":
                if (type2.canStart)
                {
                    type2.canStart = false;
                    StartCoroutine(type2.MoveBlock());
                }
                break;
            default:
                Debug.LogWarning("No block type assigned in update!");
                break;
        }
    }
}

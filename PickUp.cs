using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    private bool isMouseOverObject = false;
    public int itemId;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isMouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Crafting.givePlayerItem(itemId, 1);
                Destroy(gameObject);
            }
        }
	}

    void OnMouseEnter()
    {
        isMouseOverObject = true;
    }

    void OnMouseExit()
    {
        isMouseOverObject = false;
    }
}

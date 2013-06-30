using UnityEngine;
using System.Collections;

public class InteractObject : MonoBehaviour {

    private bool isMouseOverObject = false;
    private static GameObject interactingGO;

    private int check = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isMouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                interactingGO = gameObject;
                if (gameObject.name.ToString() == "Large Fire Pit(Clone)" || gameObject.name.ToString() == "Small Fire Pit(Clone)")
                {
                    Debug.Log("Number of ovens: " + Oven.ovenList.Count);
                    for (int i = 0; i < Oven.ovenList.Count; i++)
                    {
                        if (Oven.ovenList[i].OvenGameObject.transform == gameObject.transform)
                        {
                            Oven.currentOven = Oven.ovenList[i];
                            Messenger.Broadcast("ToggleOven");
                            //Debug.Log("Open Oven " +i);
                        }
                        else
                        {
                            check++;
                        }
                    }
                    if (check == Oven.ovenList.Count)
                    {
                        Oven.currentOven = new Oven(gameObject);
                        Messenger.Broadcast("ToggleOven");
                        //Debug.Log("Open New Oven");
                        check = 0;
                    }
                    check = 0;
                }
                else
                {
                    Debug.Log("No Interaction");
                }
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

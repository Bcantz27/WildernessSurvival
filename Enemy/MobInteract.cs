using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Looting))]
public class MobInteract : MonoBehaviour {

    public Mob.MobName mobName;
    public Mob _mob;

    private bool isMouseOverObject = false;

    void Start()
    {
        //gameObject.GetComponent<Looting>().Populate(2, Crafting.ItemName.FoxFur);
        if (_mob == null)
        {
            Debug.Log("Set mob");
            _mob = Mob.getMob(mobName);
            _mob.Alive = false;
        }
    }
/*
    void Update()
    {
        if (isMouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clicked");
                if (!_mob.Alive)
                {
                    if (_mob.Skinable)
                    {
                        //Animation
                        //Send Mob Skining info to loot window
                        Debug.Log("Skinned");
                        gameObject.GetComponent<Looting>().Populate(2, Crafting.ItemName.FoxFur);
                        GameObject.Destroy(gameObject);
                    }
                }
            }
        }
    }
 * */

    void OnMouseEnter()
    {
        isMouseOverObject = true;
    }

    void OnMouseExit()
    {
        isMouseOverObject = false;
    }
}

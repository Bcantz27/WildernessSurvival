using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Oven : MonoBehaviour{

    private GameObject _ovenGO;

    public static List<Oven> ovenList = new List<Oven>();
    public static Oven currentOven;

    private Item fuelItem = null;
    private Item foodItem = null;
    private Item cookedItem = null;

    private int maxFuelStorage;

    public Oven(GameObject ovenObj)
    {
        _ovenGO = ovenObj;
        ovenList.Add(this);
        if (ovenObj.name.ToString() == "Large Fire Pit(Clone)")
        {
            maxFuelStorage = 40;
        }
        else if (ovenObj.name.ToString() == "Small Fire Pit(Clone)")
        {
            MaxFuelStorage = 10;
        }
    }

    public GameObject OvenGameObject
    {
        get { return _ovenGO; }
        set { _ovenGO = value; }
    }

    public Item FuelItem
    {
        get { return fuelItem; }
        set { fuelItem = value; }
    }

    public Item FoodItem
    {
        get { return foodItem; }
        set { foodItem = value; }
    }

    public Item CookedItem
    {
        get { return cookedItem; }
        set { cookedItem = value; }
    }

    public int MaxFuelStorage
    {
        get { return maxFuelStorage; }
        set { maxFuelStorage = value; }
    }

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }


    
}

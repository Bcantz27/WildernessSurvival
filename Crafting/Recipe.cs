using UnityEngine;
using System.Collections;

public class Recipe : MonoBehaviour {

    private Hashtable _components = new Hashtable();
    private string _name;
    private Item craftedItem;

    public Recipe()
    {
        _name = "No Name";
        _components.Add(Crafting.ItemName.WoodLog,1);
    }

    public Recipe(string name, Hashtable components,Item CraftedItem)
    {
        _name = name;
        _components = components;
        craftedItem = CraftedItem;
    }

    public Hashtable Components
    {
        get { return _components; }
        set { _components = value; }
    }

    public Item CraftedItem
    {
        get { return craftedItem; }
        set { craftedItem = value; }
    }

}

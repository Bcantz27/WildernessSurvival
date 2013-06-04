using UnityEngine;

public class Item {
	private string _name;
    private int _id;
	private Texture2D _icon;
    private ItemType _itemType;
    private bool _isStackable = false;
    private int _itemsInStack;
	
	public Item() 
    {
		_name = "No name";
        _itemType = ItemType.Wood;
        _itemsInStack = 1;
	}
	
	public Item(string name) 
    {
		_name = name;
	}
	
	public string Name
    {
		get { return _name; }
		set { _name = value;}
	}

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public bool Stackable
    {
        get { return _isStackable; }
        set { _isStackable = value; }
    }

    public int ItemsInStack
    {
        get { return _itemsInStack; }
        set
        {
            if (_isStackable)
                _itemsInStack = value;
            else
                Debug.Log("Not Stackable");
        }
    }
	
	public Texture2D Icon 
    {
		get { return _icon; }
		set { _icon = value;}
	}

    public ItemType Type
    {
        get { return _itemType; }
        set { _itemType = value; }
    }
	
	public virtual string ToolTip() {
		return Name + "\n";
	}

    public enum ItemType
    {
        //Weapons
        Sword,
        Axe,
        Spear,
        Dagger,
        Bow,
        Crossbow,
        Pistol,
        Rifle,
        //Armor
        Hat,
        Shirt,
        Pants,
        Gloves,
        Boots,
        //Accessories
        Neck,
        Back,
        Ring,
        Wrist,
        //Materials
        Wood,
        Stone,
        Vine
    }
}

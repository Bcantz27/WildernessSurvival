using UnityEngine;

public class Item {
	private string _name;
    private int _id;
	private Texture2D _icon;
    private ItemType _itemType;
    private bool _isStackable = false;
    private bool _isPlaceable = false;
    private bool _isWieldable = false;
    private bool _isCookable = false;
    private bool _isEatable = false;
    private int _itemsInStack;
	
	public Item() 
    {
		_name = "No name";
        _itemsInStack = 1;
        _itemType = ItemType.Material;
	}
	
	public Item(string name) 
    {
		_name = name;
        _itemType = ItemType.Material;
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

    public bool Placeable
    {
        get { return _isPlaceable; }
        set { _isPlaceable = value; }
    }

    public bool Wieldable
    {
        get { return _isWieldable; }
        set { _isWieldable = value; }
    }

    public bool Cookable
    {
        get { return _isCookable; }
        set { _isCookable = value; }
    }

    public bool Eatable
    {
        get { return _isEatable; }
        set { _isEatable = value; }
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
        OneHanded,
        TwoHanded,
        OffHand,
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
        Tool,
        Material,
        //Consumables
        Food
    }
}

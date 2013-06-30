using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter {
	public static GameObject[] _weaponMesh;

	private static List<Item> _inventory = new List<Item>();
	public static List<Item> Inventory{
		get{ return _inventory; }
	}

    private static float[] _nutrition = new float[3];
    // 1: Carbs
    // 2: Protien
    // 3: Fat
	
	private static Item _rightHand;
	private static Item _leftHand;
	private static Item _head;
	private static Item _legs;
	private static Item _chest;
	private static Item _feet;
	private static Item _hands;
	private static Item _ring;
	private static Item _neck;
	private static Item _wrist;
	private static Item _back;
	
	public static Item LeftHand {
		get{return _leftHand;}
		set{
			_leftHand = value;
			
			HideWeaponMeshes();
			
			if(_leftHand == null)
				return;
			
			switch(_leftHand.Name){
				case "Sword":
					_weaponMesh[0].active = true;
				break;
				case "Axe":
				break;
				case "Spear":
				break;
				case "Dagger":
				break;
				case "Crossbow":
				break;
				case "Pistol":
				break;
				case "Bow":
				break;
				case "Rifle":
				break;
			}
		}
	}
	public static Item RightHand {
		get{return _rightHand;}
		set{
			_rightHand = value;
			
			HideWeaponMeshes();
			
			if(_rightHand == null)
				return;
			
			switch(_rightHand.Name){
				case "Sword":
					_weaponMesh[0].active = true;
				break;
				case "Axe":
				break;
				case "Spear":
				break;
				case "Dagger":
				break;
				case "Crossbow":
				break;
				case "Pistol":
				break;
				case "Bow":
				break;
				case "Rifle":
				break;
			}
		}
	}
	public static Item Head {
		get{return _head;}
		set{
			_head = value;
			
			HideWeaponMeshes();
			
			if(_head == null)
				return;
			
			switch(_head.Name){
				case "Sword":
					_weaponMesh[0].active = true;
				break;
				case "Axe":
				break;
				case "Spear":
				break;
				case "Dagger":
				break;
				case "Crossbow":
				break;
				case "Pistol":
				break;
				case "Bow":
				break;
				case "Rifle":
				break;
			}
		}
	}
	public static Item Legs {
		get{return _legs;}
		set{
			_legs = value;
			
			HideWeaponMeshes();
			
			if(_legs == null)
				return;
			
			switch(_legs.Name){
				case "Sword":
					_weaponMesh[0].active = true;
				break;
				case "Axe":
				break;
				case "Spear":
				break;
				case "Dagger":
				break;
				case "Crossbow":
				break;
				case "Pistol":
				break;
				case "Bow":
				break;
				case "Rifle":
				break;
			}
		}
	}
	public static Item Chest {
		get{return _leftHand;}
		set{
			_chest = value;
			
			HideWeaponMeshes();
			
			if(_chest == null)
				return;
			
			switch(_chest.Name){
				case "Sword":
					_weaponMesh[0].active = true;
				break;
				case "Axe":
				break;
				case "Spear":
				break;
				case "Dagger":
				break;
				case "Crossbow":
				break;
				case "Pistol":
				break;
				case "Bow":
				break;
				case "Rifle":
				break;
			}
		}
	}
	public static Item Feet {
		get{return _feet;}
		set{
			_feet = value;
			
			HideWeaponMeshes();
			
			if(_feet == null)
				return;
			
			switch(_feet.Name){
				case "Sword":
					_weaponMesh[0].active = true;
				break;
				case "Axe":
				break;
				case "Spear":
				break;
				case "Dagger":
				break;
				case "Crossbow":
				break;
				case "Pistol":
				break;
				case "Bow":
				break;
				case "Rifle":
				break;
			}
		}
	}
	public static Item Hands {
		get{return _hands;}
		set{
			_hands = value;
			
			HideWeaponMeshes();
			
			if(_hands == null)
				return;
			
			switch(_hands.Name){
				case "Sword":
					_weaponMesh[0].active = true;
				break;
				case "Axe":
				break;
				case "Spear":
				break;
				case "Dagger":
				break;
				case "Crossbow":
				break;
				case "Pistol":
				break;
				case "Bow":
				break;
				case "Rifle":
				break;
			}
		}
	}
	public static Item Ring {
		get{return _ring;}
		set{_ring = value;}
	}
	public static Item Neck {
		get{return _neck;}
		set{_neck = value;}
	}
	public static Item Wrist {
		get{return _wrist;}
		set{_wrist = value;}
	}
	public static Item Back {
		get{return _back;}
		set{
			_back = value;
			
			HideWeaponMeshes();
			
			if(_back == null)
				return;
			
			switch(_back.Name){
				case "Sword":
					_weaponMesh[0].active = true;
				break;
				case "Axe":
				break;
				case "Spear":
				break;
				case "Dagger":
				break;
				case "Crossbow":
				break;
				case "Pistol":
				break;
				case "Bow":
				break;
				case "Rifle":
				break;
			}
		}
	}
	
	void Awake() {

        for (int i = 0; i < _nutrition.Length; i++)
        {
            _nutrition[i] = 0;
        }


		Transform weaponMount = transform.Find ("Right Hand Weapon Mount");
		if(weaponMount != null){
			int count = weaponMount.GetChildCount();
			
			_weaponMesh = new GameObject[count];
			for(int cnt = 0; cnt < count; cnt++){
				_weaponMesh[cnt] = weaponMount.GetChild(cnt).gameObject;
			}
			
			HideWeaponMeshes();
		}else{
			Debug.Log("Weapon mounting error!");	
		}
	}
	
	void Update() {

	}
	private static void HideWeaponMeshes(){
		/*
		for(int cnt = 0; cnt < _weaponMesh.Length; cnt++){
			_weaponMesh[cnt].active = false;
		}
		*/
	}

    public static void eatFood(Food food)
    {
        _nutrition[0] += food.Carbs;
        _nutrition[1] += food.Protein;
        _nutrition[2] += food.Fat;

        for (int i = 0; i < 4; i++)
        {
            if(food.getHealAtIndex(i) != 0)
                Messenger<int, float>.Broadcast("player hunger change", i, food.getHealAtIndex(i), MessengerMode.DONT_REQUIRE_LISTENER);
        }

        Debug.Log("Carbs: " + _nutrition[0] + "\n" + "Protein: " + _nutrition[1] + "\n" + "Fat: " + _nutrition[2]);
    }

    public static bool checkInventoryForItem(int id, int amount)
    {
        bool has = false;
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Id == id)
            {
                if (Inventory[i].Stackable)
                {
                    if (amount <= Inventory[i].ItemsInStack)
                    {
                        has = true;
                    }
                }
                else
                {
                    has = true;
                }
            }
        }


        return has;
    }

    public static void removeItemFromInventory(Item item, int amount)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Id == item.Id)
            {
                if (Inventory[i].Stackable)
                {
                    if (amount < Inventory[i].ItemsInStack)
                    {
                        Inventory[i].ItemsInStack -= amount;
                    }
                    else
                    {
                        Inventory.RemoveAt(i);
                    }
                }
                else
                {
                    Inventory.RemoveAt(i);
                }
            }
        }

    }
}

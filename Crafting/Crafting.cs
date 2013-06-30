using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crafting : Recipe {

    public static List<Recipe> recipeList;
    public static bool initialized = false;


	// Use this for initialization
	void Start () {

	}

    private static void initalizeRecipes()
    {
        Hashtable recipe = new Hashtable();
        recipeList = new List<Recipe>();

        recipe.Add(ItemName.TreeBranch, 2);
        recipe.Add(ItemName.SharpRock, 1);
        recipe.Add(ItemName.Vine, 1);
        recipeList.Add(new Recipe("Stone Axe", recipe, getItem(ItemName.StoneAxe)));
        recipe = new Hashtable();

        recipe.Add(ItemName.SmallRock, 3);
        recipeList.Add(new Recipe("Large Rock", recipe, getItem(ItemName.LargeRock)));
        recipe = new Hashtable();

        recipe.Add(ItemName.WoodLog, 5);
        recipe.Add(ItemName.SmallRock, 10);
        recipeList.Add(new Recipe("Small Fire Pit", recipe, getItem(ItemName.SmallFirePit)));
        recipe = new Hashtable();

        recipe.Add(ItemName.WoodLog, 10);
        recipe.Add(ItemName.LargeRock, 10);
        recipeList.Add(new Recipe("Large Fire Pit", recipe, getItem(ItemName.LargeFirePit)));
        recipe = new Hashtable();

        recipe.Add(ItemName.Feather, 1);
        recipe.Add(ItemName.SmallRock, 1);
        recipe.Add(ItemName.TreeBranch, 1);
        recipeList.Add(new Recipe("Arrow", recipe, getItem(ItemName.Arrow)));
        recipe = new Hashtable();

        recipe.Add(ItemName.String, 3);
        recipe.Add(ItemName.TreeBranch, 3);
        recipeList.Add(new Recipe("Bow", recipe, getItem(ItemName.Bow)));
        recipe = new Hashtable();

        recipe.Add(ItemName.String, 3);
        recipe.Add(ItemName.TreeBranch, 3);
        recipe.Add(ItemName.WoodLog, 1);
        recipe.Add(ItemName.Horn, 1);
        recipeList.Add(new Recipe("Cross Bow", recipe, getItem(ItemName.Crossbow)));
        recipe = new Hashtable();

        recipe.Add(ItemName.TreeBranch, 1);
        recipe.Add(ItemName.LargeRock, 1);
        recipe.Add(ItemName.Vine, 2);
        recipeList.Add(new Recipe("Stone Hammer", recipe, getItem(ItemName.StoneHammer)));
        recipe = new Hashtable();

        recipe.Add(ItemName.LargeRock, 2);
        recipeList.Add(new Recipe("Sharp Rock", recipe, getItem(ItemName.SharpRock)));
        recipe = new Hashtable();

        recipe.Add(ItemName.WoodPlank, 4);
        recipe.Add(ItemName.Vine, 4);
        recipeList.Add(new Recipe("Hide Rack", recipe, getItem(ItemName.HideRack)));
        recipe = new Hashtable();

    }

    public static List<Item> getCraftableItems(List<Item> inventoryItems)
    {
        if (!initialized)
        {
            initalizeRecipes();

            initialized = true;
        }
        List<Item> listofItems = new List<Item>();
        int  check = 0;
        for (int i = 0; i < recipeList.Count; i++)
        {
            foreach (DictionaryEntry pair in recipeList[i].Components)
            {
               // Debug.Log("Key : " + pair.Key.ToString() + " Value : " + pair.Value.ToString());
                if (PlayerCharacter.checkInventoryForItem((int)pair.Key, (int)pair.Value))
                {
                    check++;
                }
            }
            if (check == recipeList[i].Components.Count && check != 0)
            {
                listofItems.Add(recipeList[i].CraftedItem);
                //Debug.Log("Item Added to Crafting List : " + recipeList[i].CraftedItem.Name.ToString());
            }
            check = 0;
        }
        

        return listofItems;
    }

    public static void givePlayerItem(ItemName item, int amount)
    {
        Item wantedItem = new Item();
        bool added = false;

        wantedItem = getItem(item);
        if (wantedItem.Stackable && amount >= 1)
        {
            wantedItem.ItemsInStack = amount;
        }

        for(int i = 0; i < PlayerCharacter.Inventory.Count;i ++)
        {
            if (PlayerCharacter.Inventory[i].Id == wantedItem.Id && wantedItem.Stackable)
            {
                PlayerCharacter.Inventory[i].ItemsInStack += wantedItem.ItemsInStack;
                added = true;
            }
        }
        if (!added)
        {
            PlayerCharacter.Inventory.Add(wantedItem);
        }
    }

    public static void removePlayerItem(ItemName item, int amount)
    {
        Item wantedItem = new Item();
        wantedItem = getItem(item);

        for (int i = 0; i < PlayerCharacter.Inventory.Count; i++)
        {
            if (PlayerCharacter.Inventory[i].Id == wantedItem.Id)
            {
                if (wantedItem.Stackable)
                {
                    if (amount < PlayerCharacter.Inventory[i].ItemsInStack)
                    {
                        PlayerCharacter.Inventory[i].ItemsInStack -= amount;
                    }
                    else
                    {
                        PlayerCharacter.Inventory.RemoveAt(i);
                    }
                    
                }
                else
                {
                    PlayerCharacter.Inventory.RemoveAt(i);
                }
            }
        }
        
    }

    public static bool isItemATool(int id)
    {
        bool tool = false;

        if (id == 16 || id == 27)
        {
            tool = true;
        }

        return tool;
    }

    public static void getRecipeItemsAndRemove(Recipe recipe)
    {
        foreach (DictionaryEntry pair in recipe.Components)
        {
            if (!isItemATool((int)pair.Key))
            {
                PlayerCharacter.removeItemFromInventory(getItem((ItemName)pair.Key), (int)pair.Value);
            }
            else
            {
                //Take durability
            }

        }
    }


    public static Item getItem(ItemName itemname)
    {
        Item item = new Item();
        float[] temp = { 0, 0, 0, 0 };

        switch (itemname)
        {
            case ItemName.WoodLog:
                item.Name = "Wood Log";
                item.Stackable = true;
                break;
            case ItemName.TreeBranch:
                item.Name = "Tree Branch";
                item.Stackable = true;
                break;
            case ItemName.WoodPlank:
                item.Name = "Wood Plank";
                item.Stackable = true;
                break;
            case ItemName.LargeRock:
                item.Name = "Large Rock";
                item.Stackable = true;
                break;
            case ItemName.SmallRock:
                item.Name = "Small Rock";
                item.Stackable = true;
                break;
            case ItemName.Flint:
                item.Name = "Flint";
                item.Stackable = true;
                break;
            case ItemName.Vine:
                item.Name = "Vine";
                item.Icon = Resources.Load("ItemIcons/" + item.Name + ".jpg") as Texture2D;
                item.Stackable = true;
                break;
            case ItemName.RabbitFur:
                item.Name = "Rabbit Fur";
                item.Stackable = true;
                break;
            case ItemName.FoxFur:
                item.Name = "Fox Fur";
                item.Stackable = true;
                break;
            case ItemName.WolfFur:
                item.Name = "Wolf Fur";
                item.Stackable = true;
                break;
            case ItemName.BearFur:
                item.Name = "Bear Fur";
                item.Stackable = true;
                break;
            case ItemName.DeerHide:
                item.Name = "Deer Hide";
                item.Stackable = true;
                break;
            case ItemName.ElkHide:
                item.Name = "Elk Hide";
                item.Stackable = true;
                break;
            case ItemName.StoneAxe:
                item.Name = "Stone Axe";
                item.Stackable = false;
                item.Wieldable = true;
                item.Type = Item.ItemType.OneHanded;
                break;
            case ItemName.SmallFirePit:
                item.Name = "Small Fire Pit";
                item.Stackable = false;
                item.Placeable = true;
                break;
            case ItemName.LargeFirePit:
                item.Name = "Large Fire Pit";
                item.Stackable = false;
                item.Placeable = true;
                break;
            case ItemName.HideRack:
                item.Name = "Hide Rack";
                item.Stackable = false;
                item.Placeable = true;
                break;
            case ItemName.String:
                item.Name = "String";
                item.Stackable = true;
                break;
            case ItemName.Feather:
                item.Name = "Feather";
                item.Stackable = true;
                break;
            case ItemName.Bow:
                item.Name = "Bow";
                item.Stackable = false;
                item.Wieldable = true;
                item.Icon = Resources.Load("Item Icons/" + item.Name +".jpg") as Texture2D;
                item.Type = Item.ItemType.TwoHanded;
                break;
            case ItemName.Crossbow:
                item.Name = "Cross Bow";
                item.Stackable = false;
                item.Wieldable = true;
                item.Type = Item.ItemType.TwoHanded;
                break;
            case ItemName.Arrow:
                item.Name = "Arrow";
                item.Stackable = true;
                break;
            case ItemName.Horn:
                item.Name = "Horn";
                item.Stackable = true;
                break;
            case ItemName.Antler:
                item.Name = "Antler";
                item.Stackable = true;
                break;
            case ItemName.Tooth:
                item.Name = "Tooth";
                item.Stackable = true;
                break;
            case ItemName.SharpRock:
                item.Name = "Sharp Rock";
                item.Stackable = true;
                break;
            case ItemName.StoneHammer:
                item.Name = "Stone Hammer";
                item.Stackable = false;
                item.Wieldable = true;
                item.Type = Item.ItemType.OneHanded;
                break;
            case ItemName.SmallBone:
                item.Name = "Small Bone";
                item.Stackable = true;
                item.Wieldable = false;
                break;
            case ItemName.LargeBone:
                item.Name = "Large Bone";
                item.Stackable = true;
                item.Wieldable = false;
                break;
            case ItemName.Apple:
                temp = new float[] {10,2,5,0};              //{Health,Energy,Hunger,Thrist}
                item = new Food(25,0.5f,0.3f,temp);
                item.Type = Item.ItemType.Food;
                item.Name = "Apple";
                item.Stackable = true;
                item.Wieldable = false;
                break;
            case ItemName.Steak:
                temp = new float[] { 30, 20, 75, 0 };
                item = new Food(0, 40f, 60f, temp);
                item.Type = Item.ItemType.Food;
                item.Name = "Steak";
                item.Stackable = true;
                item.Wieldable = false;
                break;
            case ItemName.RawMeat:
                temp = new float[] { 1, 1, 1, 0 };
                item = new Food(0, 2f, 3f, temp);
                item.Type = Item.ItemType.Food;
                item.Name = "Raw Meat";
                item.Stackable = true;
                item.Wieldable = false;
                break;
            case ItemName.Berries:
                temp = new float[] { 2, 1, 5, 0 };           
                item = new Food(11, 1, 0, temp);
                item.Type = Item.ItemType.Food;
                item.Name = "Berries";
                item.Stackable = true;
                item.Wieldable = false;
                break;

        }

        item.Id = (int)itemname;

        return item;
    }

    public enum ItemName
    {
        WoodLog,
        WoodPlank,
        TreeBranch,
        LargeRock,
        SmallRock,
        SharpRock,
        Flint,
        Vine,
        RabbitFur,
        FoxFur,
        WolfFur,
        BearFur,
        DeerHide,
        ElkHide,
        StoneAxe,
        StoneHammer,
        SmallFirePit,
        LargeFirePit,
        HideRack,
        String,
        Feather,
        Bow,
        Crossbow,
        Arrow,
        Horn,
        Antler,
        Tooth,
        SmallBone,
        LargeBone,
        RawMeat,
        Steak,
        Apple,
        Berries
    }
	
}

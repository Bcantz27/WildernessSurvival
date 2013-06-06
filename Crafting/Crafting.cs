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

        recipe.Add(2, 2);
        recipe.Add(26, 1);
        recipe.Add(7, 1);
        recipeList.Add(new Recipe("Stone Axe", recipe, getItemById(16)));
        recipe = new Hashtable();

        recipe.Add(5, 3);
        recipeList.Add(new Recipe("Large Stone", recipe, getItemById(4)));
        recipe = new Hashtable();

        recipe.Add(1, 5);
        recipe.Add(5, 10);
        recipeList.Add(new Recipe("Small Fire Pit", recipe, getItemById(17)));
        recipe = new Hashtable();

        recipe.Add(1, 15);
        recipe.Add(4, 10);
        recipeList.Add(new Recipe("Large Fire Pit", recipe, getItemById(18)));
        recipe = new Hashtable();

        recipe.Add(21, 1);
        recipe.Add(5, 1);
        recipe.Add(2, 1);
        recipeList.Add(new Recipe("Arrow", recipe, getItemById(24)));
        recipe = new Hashtable();

        recipe.Add(20, 3);
        recipe.Add(2, 3);
        recipeList.Add(new Recipe("Bow", recipe, getItemById(22)));
        recipe = new Hashtable();

        recipe.Add(20, 3);
        recipe.Add(2, 3);
        recipe.Add(1, 1);
        recipe.Add(25, 1);
        recipeList.Add(new Recipe("Cross Bow", recipe, getItemById(23)));
        recipe = new Hashtable();

        recipe.Add(2, 2);
        recipe.Add(4, 1);
        recipe.Add(7, 1);
        recipeList.Add(new Recipe("Stone Hammer", recipe, getItemById(27)));
        recipe = new Hashtable();

        recipe.Add(4, 2);
        recipeList.Add(new Recipe("Sharp Rock", recipe, getItemById(26)));
        recipe = new Hashtable();

        recipe.Add(3, 4);
        recipe.Add(7, 4);
        recipeList.Add(new Recipe("Hide Rack", recipe, getItemById(19)));
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

    public static void givePlayerItem(int id, int amount)
    {
        Item wantedItem = new Item();
        bool added = false;

        wantedItem = getItemById(id);
        if (wantedItem.Stackable && amount > 1)
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

    public static void removePlayerItem(int id, int amount)
    {
        Item wantedItem = new Item();
        wantedItem = getItemById(id);

        for (int i = 0; i < PlayerCharacter.Inventory.Count; i++)
        {
            if (PlayerCharacter.Inventory[i].Id == wantedItem.Id)
            {
                if (amount < wantedItem.ItemsInStack && wantedItem.Stackable)
                {
                    wantedItem.ItemsInStack -= amount;
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
                PlayerCharacter.removeItemFromInventory(getItemById((int)pair.Key), (int)pair.Value);
            }
            else
            {
                //Take durability
            }

        }
    }


    public static Item getItemById(int id)
    {
        Item item = new Item();

        switch (id)
        {
            case 1:
                item.Name = "Wood Log";
                item.Id = 1;
                item.Stackable = true;
                break;
            case 2:
                item.Name = "Tree Branch";
                item.Id = 2;
                item.Stackable = true;
                break;
            case 3:
                item.Name = "Wood Plank";
                item.Id = 3;
                item.Stackable = true;
                break;
            case 4:
                item.Name = "Large Rock";
                item.Id = 4;
                item.Stackable = true;
                break;
            case 5:
                item.Name = "Small Rock";
                item.Id = 5;
                item.Stackable = true;
                item.Placeable = true;
                break;
            case 6:
                item.Name = "Flint";
                item.Id = 6;
                item.Stackable = true;
                break;
            case 7:
                item.Name = "Vine";
                item.Id = 7;
                item.Stackable = true;
                break;
            case 8:
                item.Name = "Rabbit Fur";
                item.Id = 8;
                item.Stackable = true;
                break;
            case 9:
                item.Name = "Fox Fur";
                item.Id = 9;
                item.Stackable = true;
                break;
            case 10:
                item.Name = "Cow Hide";
                item.Id = 10;
                item.Stackable = true;
                break;
            case 11:
                item.Name = "Buffalo Hide";
                item.Id = 11;
                item.Stackable = true;
                break;
            case 12:
                item.Name = "Deer Hide";
                item.Id = 12;
                item.Stackable = true;
                break;
            case 13:
                item.Name = "Elk Hide";
                item.Id = 13;
                item.Stackable = true;
                break;
            case 14:
                item.Name = "Alligator Hide";
                item.Id = 14;
                item.Stackable = true;
                break;
            case 15:
                item.Name = "Sheep Hide";
                item.Id = 15;
                item.Stackable = true;
                break;
            case 16:
                item.Name = "Stone Axe";
                item.Id = 16;
                item.Stackable = false;
                item.Wieldable = true;
                item.Type = Item.ItemType.OneHanded;
                break;
            case 17:
                item.Name = "Small Fire Pit";
                item.Id = 17;
                item.Stackable = false;
                item.Placeable = true;
                break;
            case 18:
                item.Name = "Large Fire Pit";
                item.Id = 18;
                item.Stackable = false;
                item.Placeable = true;
                break;
            case 19:
                item.Name = "Hide Rack";
                item.Id = 19;
                item.Stackable = false;
                item.Placeable = true;
                break;
            case 20:
                item.Name = "String";
                item.Id = 20;
                item.Stackable = true;
                break;
            case 21:
                item.Name = "Feather";
                item.Id = 21;
                item.Stackable = true;
                break;
            case 22:
                item.Name = "Bow";
                item.Id = 22;
                item.Stackable = false;
                item.Wieldable = true;
                item.Type = Item.ItemType.TwoHanded;
                break;
            case 23:
                item.Name = "Cross Bow";
                item.Id = 23;
                item.Stackable = false;
                item.Wieldable = true;
                item.Type = Item.ItemType.TwoHanded;
                break;
            case 24:
                item.Name = "Arrow";
                item.Id = 24;
                item.Stackable = true;
                break;
            case 25:
                item.Name = "Horn";
                item.Id = 25;
                item.Stackable = true;
                break;
            case 26:
                item.Name = "Sharp Rock";
                item.Id = 26;
                item.Stackable = true;
                break;
            case 27:
                item.Name = "Stone Hammer";
                item.Id = 27;
                item.Stackable = false;
                item.Wieldable = true;
                item.Type = Item.ItemType.OneHanded;
                break;

        }

        return item;
    }
	
}

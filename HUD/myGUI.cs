using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myGUI : MonoBehaviour {
	public GUISkin mySkin;
	
	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float closeButtonWidth = 20;
	public float closeButtonHeight = 20;

    private Vector2 mousePos;
	private float _offset = 10;
	//*****************************************//
	/*  Loot window Vars
	//*****************************************/
	private bool _displayLootWindow = false;
	private const int LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect = new Rect(0,0,0,0);
	private Vector2 _lootWindowSlider = Vector2.zero;
	public float _lootWindowHeight = 90;
    public static List<Item> loot = new List<Item>();
	
	private string _toolTip = "";
	
	//*****************************************//
	/*  Inventory Window Vars
	//*****************************************/
	private bool _displayInventoryWindow = false;
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect = new Rect(10,10,170,265);
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;
	
	private float _doubleClickTimer = 0;
	private const float DOUBLE_CLICK_TIMER_THRESHHOLD = .5f;
	private Item _selectedItem;


    //*****************************************//
    /*  Dragging Vars
    //*****************************************/
    private bool dragging = false;
    private GameObject placeableObject;
    private GameObject draggingObj;
    private Item placeableItem;
    private Item draggingItem;

    private RaycastHit hit;
    private Ray ray;

    //*****************************************//
    /*  Oven Window Vars
    //*****************************************/
    private bool _displayOvenWindow = false;
    private const int OVEN_WINDOW_ID = 4;
    private Rect _ovenWindowRect = new Rect(10, 10, 256, 256);

    private Rect fuelRect;
    private Rect foodRect;
    private Rect cookedRect;

    //*****************************************//
    /*  Crafting Window Vars
    //*****************************************/
    private bool _displayCraftingWindow = false;
    private const int CRAFTING_WINDOW_ID = 3;
    private Rect _craftingWindowRect = new Rect(10, 10, 170, 500);
    private List<Item> craftableItems;
    private Vector2 _craftingWindowSlider = Vector2.zero;
    private int _craftingButtonWidth = 150;
    private int _craftingButtonHeight = 40;

	
	//*****************************************//
    /*  Character Window Vars
    //*****************************************/
	private bool _displayCharacterWindow = false;
	private const int CHARACTER_WINDOW_ID = 2;
	private Rect _characterWindowRect = new Rect(10,10,500,300);
	private int _characterPanel = 0;
	private string[] _characterPanelNames = new string[] {"Equiptment","Attributes","Skills"};


    public GUISkin GUISkin
    {
        get { return mySkin; }
        set { mySkin = value; }
    }

	// Use this for initialization
	void Start () {
        craftableItems = Crafting.getCraftableItems(PlayerCharacter.Inventory);
        fuelRect = new Rect(100, 50, buttonWidth, buttonHeight);
        foodRect = new Rect(50, 100, buttonWidth, buttonHeight);
        cookedRect = new Rect(150, 100, buttonWidth, buttonHeight);
	}
	
	private void OnEnable() {

		Messenger.AddListener("DisplayLoot",DisplayLoot);
		Messenger.AddListener("CloseChest",ClearWindow);
		Messenger.AddListener("ToggleInventory",ToggleInventoryWindow);
		Messenger.AddListener("ToggleCharacter",ToggleCharacterWindow);
        Messenger.AddListener("ToggleCrafting", ToggleCraftingWindow);
        Messenger.AddListener("ToggleOven", ToggleOvenWindow);
	}
	private void OnDisable() {

		Messenger.RemoveListener("DisplayLoot",DisplayLoot);
		Messenger.RemoveListener("CloseChest",ClearWindow);
		Messenger.RemoveListener("ToggleInventory",ToggleInventoryWindow);
		Messenger.RemoveListener("ToggleCharacter",ToggleCharacterWindow);
        Messenger.RemoveListener("ToggleCrafting", ToggleCraftingWindow);
        Messenger.RemoveListener("ToggleOven", ToggleOvenWindow);
	}
	
	// Update is called once per frame
	void Update () {
        if (dragging)
        {
            OnMouseDragItemPlace();
        }

        dragDetection();
	}
	
	void OnGUI(){
		GUI.skin = mySkin;
		
		if(_displayCharacterWindow)
			_characterWindowRect = GUI.Window(CHARACTER_WINDOW_ID, _characterWindowRect, CharacterWindow,"Character","Character Window");
		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset,Screen.height - (_offset + _lootWindowHeight),Screen.width - (_offset * 2),_lootWindowHeight), LootWindow,"Loot Window","Character Window");
		if(_displayInventoryWindow)
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow,"Inventory","Character Window");
        if (_displayCraftingWindow)
            _craftingWindowRect = GUI.Window(CRAFTING_WINDOW_ID, _craftingWindowRect, CraftingWindow, "Crafting", "Character Window");
        if (_displayOvenWindow)
            _ovenWindowRect = GUI.Window(OVEN_WINDOW_ID, _ovenWindowRect, OvenWindow, "", "Character Window");

		DisplayToolTip();
	}
	
	private void DisplayLoot(){
		_displayLootWindow = true;
	}
	
	private void ClearWindow(){
        Debug.Log("Closed Loot");
		_displayLootWindow = false;
	}
	
	private void LootWindow(int id) {
		GUI.skin = mySkin;
		
		if(GUI.Button(new Rect(_lootWindowRect.width - 22, 2, closeButtonWidth,closeButtonHeight),"","X Button")){
            _displayLootWindow = false;
		}
        if (loot == null)
			return;

        if (loot.Count == 0)
        {
            _displayLootWindow = false;
			return;
		}

        _lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * .5f, 15, _lootWindowRect.width - _offset, 70), _lootWindowSlider, new Rect(0, 0, (loot.Count * buttonWidth) + _offset, buttonHeight + _offset));

        for (int cnt = 0; cnt < loot.Count; cnt++)
        {
            if (GUI.Button(new Rect((buttonWidth * cnt) + 5, _offset, buttonWidth, buttonHeight), new GUIContent(loot[cnt].Icon, loot[cnt].ToolTip()), getSlotSkin()))
            {
                Debug.Log(loot[cnt].ToolTip());
                PlayerCharacter.Inventory.Add(loot[cnt]);
                loot.RemoveAt(cnt);
			}
            if (loot[cnt].Stackable)
            {
                GUI.Label(new Rect((buttonWidth * cnt) + 5, _offset, buttonWidth, buttonHeight), loot[cnt].ItemsInStack.ToString(), "Inventory Slot Empty");
            }
		}
		
		GUI.EndScrollView();
		
		SetToolTip();
	}

    private void CraftingWindow(int id)
    {

        if (GUI.Button(new Rect(_craftingWindowRect.width - 22, 2, closeButtonWidth, closeButtonHeight), "", "X Button"))
        {
            _displayCraftingWindow = false;
        }
        _craftingWindowSlider = GUI.BeginScrollView(new Rect(10, 30, _craftingWindowRect.width - 10, (_craftingButtonHeight * craftableItems.Count) + _craftingWindowRect.height), _craftingWindowSlider, new Rect(10, 40, _craftingButtonWidth - 20, _craftingWindowRect.height));


        for (int i = 0; i < craftableItems.Count; i++)
        {
            GUI.Label(new Rect(20, 45 + (32 * i), _craftingWindowRect.width - 30, 24), "Craft: " + craftableItems[i].Name.ToString());
            if (GUI.Button(new Rect(10, 40 + (32 * i), _craftingWindowRect.width - 30, 32), ""))
            {
                for (int j = 0; j < Crafting.recipeList.Count; j++)
                {
                    if (Crafting.recipeList[j].CraftedItem.Id == craftableItems[i].Id)
                    {
                        Crafting.getRecipeItemsAndRemove(Crafting.recipeList[j]);
                        Debug.Log("Removed Items");
                        Crafting.givePlayerItem((Crafting.ItemName)craftableItems[i].Id, 1);
                        craftableItems = Crafting.getCraftableItems(PlayerCharacter.Inventory);
                    }
                }
            }
        }
        GUI.EndScrollView();
        GUI.DragWindow();
    }


    public void dragDetection()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y); 
        if (Input.GetMouseButtonDown(0))
        {
            if (!dragging && _inventoryWindowRect.Contains(mousePos))
            {
                if (placeableItem != null)
                {
                    placeableObject = Resources.Load("Placeables/" + placeableItem.Name.ToString()) as GameObject;
                    draggingObj = GameObject.Instantiate(placeableObject) as GameObject;
                    Debug.Log("OBJ");
                }
                dragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (dragging)
            {
                if (_ovenWindowRect.Contains(mousePos) && _displayOvenWindow)
                {
                    Debug.Log("In Oven");
                    if (fuelRect.Contains(mousePos))
                    {
                        Debug.Log("Fuel");
                        if (Oven.currentOven.FuelItem == null)
                        {
                            if (draggingItem.ItemsInStack > Oven.currentOven.MaxFuelStorage)
                            {
                                Item temp = Crafting.getItem((Crafting.ItemName)draggingItem.Id);
                                temp.ItemsInStack = draggingItem.ItemsInStack - Oven.currentOven.MaxFuelStorage;
                                Crafting.removePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack);
                                Crafting.givePlayerItem((Crafting.ItemName)temp.Id, temp.ItemsInStack);
                                draggingItem.ItemsInStack = Oven.currentOven.MaxFuelStorage;
                                Oven.currentOven.FuelItem = draggingItem;
                            }
                            else
                            {
                                Oven.currentOven.FuelItem = draggingItem;
                                Crafting.removePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack);
                                //Debug.Log("Dragging Item Name: " + draggingItem.Name.ToString());
                            }
                        }
                        else
                        {
                            if (draggingItem.ItemsInStack > Oven.currentOven.MaxFuelStorage)
                            {
                                Crafting.removePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack);
                                Crafting.givePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack - Oven.currentOven.MaxFuelStorage);
                                Crafting.givePlayerItem((Crafting.ItemName)Oven.currentOven.FuelItem.Id, Oven.currentOven.FuelItem.ItemsInStack);
                                draggingItem.ItemsInStack = Oven.currentOven.MaxFuelStorage;
                                Oven.currentOven.FuelItem = draggingItem;
                            }
                            else
                            {
                                Crafting.removePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack);
                                Crafting.givePlayerItem((Crafting.ItemName)Oven.currentOven.FuelItem.Id, Oven.currentOven.FuelItem.ItemsInStack);
                                Oven.currentOven.FuelItem = draggingItem;
                            }
                            //Debug.Log("Dragging Item Name: " + draggingItem.Name.ToString());
                            //Debug.Log("Fuel Item Name: " + Oven.currentOven.FuelItem.Name.ToString());
                        }
                    }
                    else if (foodRect.Contains(mousePos))
                    {
                        Debug.Log("Food");
                        if (Oven.currentOven.FoodItem == null)
                        {

                            Crafting.removePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack);
                            Oven.currentOven.FoodItem = draggingItem;
                            //Debug.Log("Dragging Item Name: " + draggingItem.Name.ToString());
                        }
                        else
                        {
                            //Debug.Log("Dragging Item Name: " + draggingItem.Name.ToString());
                            //Debug.Log("Fuel Item Name: " + Oven.currentOven.FoodItem.Name.ToString());
                            Crafting.removePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack);
                            Crafting.givePlayerItem((Crafting.ItemName)Oven.currentOven.FoodItem.Id, Oven.currentOven.FoodItem.ItemsInStack);
                            Oven.currentOven.FoodItem = draggingItem;
                        }
                    }
                    else if (cookedRect.Contains(mousePos))
                    {
                        Debug.Log("Cooked");
                        if (Oven.currentOven.CookedItem == null)
                        {

                            Crafting.removePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack);
                            Oven.currentOven.CookedItem = draggingItem;
                            //Debug.Log("Dragging Item Name: " + draggingItem.Name.ToString());
                        }
                        else
                        {
                            //Debug.Log("Dragging Item Name: " + draggingItem.Name.ToString());
                            //Debug.Log("Fuel Item Name: " + Oven.currentOven.CookedItem.Name.ToString());
                            Crafting.removePlayerItem((Crafting.ItemName)draggingItem.Id, draggingItem.ItemsInStack);
                            Crafting.givePlayerItem((Crafting.ItemName)Oven.currentOven.CookedItem.Id, Oven.currentOven.CookedItem.ItemsInStack);
                            Oven.currentOven.CookedItem = draggingItem;
                        }
                    }
                }

                if (placeableItem != null)
                {
                    Crafting.removePlayerItem((Crafting.ItemName)placeableItem.Id, 1);
                    placeableItem = null;
                    if (_inventoryWindowRect.Contains(mousePos))
                    {
                        Destroy(draggingObj);
                        draggingObj = null;
                        //Do inventory item dragging here
                    }
                    else
                    {
                        draggingObj = null;
                        placeableObject = null;
                    }
                }
                draggingItem = null;
                dragging = false;
            }
        }
    }


    private void OnMouseDragItemPlace()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 25) && hit.collider.GetType() == typeof(TerrainCollider))
        {
            Debug.DrawLine(ray.origin, hit.point);
            if (draggingObj != null)
            {
                draggingObj.transform.up = hit.normal;
                draggingObj.transform.position = hit.point;
            }
        }
    }

    private void OvenWindow(int id)
    {
        GUI.skin = mySkin;

        if (GUI.Button(new Rect(_ovenWindowRect.width - 22, 2, closeButtonWidth, closeButtonHeight), "", "X Button"))
        {
            _displayOvenWindow = false;
        }

        if (Oven.currentOven.FuelItem == null)
        {
            GUI.Label(fuelRect, "", "Inventory Slot Empty");
        }
        else
        {
            if (GUI.Button(fuelRect, new GUIContent(Oven.currentOven.FuelItem.Icon, Oven.currentOven.FuelItem.ToolTip()), getSlotSkin()))
            {
                Debug.Log("Test");
            }
            if (Oven.currentOven.FuelItem.Stackable)
            {
                GUI.Label(fuelRect, Oven.currentOven.FuelItem.ItemsInStack.ToString(), "Inventory Slot Empty");
            }
        }

        if (Oven.currentOven.FoodItem == null)
        {
            GUI.Label(foodRect, "", "Inventory Slot Empty");
        }
        else
        {
            if (GUI.Button(foodRect, new GUIContent(Oven.currentOven.FoodItem.Icon, Oven.currentOven.FoodItem.ToolTip()), getSlotSkin()))
            {

            }
            if (Oven.currentOven.FoodItem.Stackable)
            {
                GUI.Label(foodRect, Oven.currentOven.FoodItem.ItemsInStack.ToString(), "Inventory Slot Empty");
            }
        }

        if (Oven.currentOven.CookedItem == null)
        {
            GUI.Label(cookedRect, "", "Inventory Slot Empty");
        }
        else
        {
            if (GUI.Button(cookedRect, new GUIContent(Oven.currentOven.CookedItem.Icon, Oven.currentOven.CookedItem.ToolTip()), getSlotSkin()))
            {

            }
            if (Oven.currentOven.CookedItem.Stackable)
            {
                GUI.Label(cookedRect, Oven.currentOven.CookedItem.ItemsInStack.ToString(), "Inventory Slot Empty");
            }
        }

        GUI.DragWindow();

    }

    public void InventoryWindow(int id)
    {
		GUI.skin = mySkin;
		
		int cnt = 0;
		
		for(int y = 0; y < _inventoryRows; y++){
			for(int x = 0; x < _inventoryCols; x++){
				if(cnt < PlayerCharacter.Inventory.Count){
                    Rect rect = new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight);
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        if (draggingItem == null)
                        {
                            draggingItem = PlayerCharacter.Inventory[cnt];
                        }
                        if (PlayerCharacter.Inventory[cnt].Placeable)
                        {
                            if (placeableItem == null)
                            {
                                Debug.Log("Place now");
                                placeableItem = PlayerCharacter.Inventory[cnt];
                            }
                        }
                    }

                    if (GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), new GUIContent(PlayerCharacter.Inventory[cnt].Icon, PlayerCharacter.Inventory[cnt].ToolTip()), getSlotSkin()))
                    {

						if(_doubleClickTimer != 0 && _selectedItem != null){
							if(Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHHOLD){
								#region DoubleClick Selectors
								switch(_selectedItem.Type){
                                    case Item.ItemType.Food:
                                        PlayerCharacter.eatFood((Food)PlayerCharacter.Inventory[cnt]);
                                        if (PlayerCharacter.Inventory[cnt].Stackable)
                                        {
                                            if (PlayerCharacter.Inventory[cnt].ItemsInStack > 1)
                                            {
                                                PlayerCharacter.Inventory[cnt].ItemsInStack--;
                                            }
                                            else
                                            {
                                                PlayerCharacter.Inventory.RemoveAt(cnt);
                                            }
                                        }
                                        else
                                        {
                                            PlayerCharacter.Inventory.RemoveAt(cnt);
                                        }
                                        break;
                                    case Item.ItemType.OneHanded:
										if(PlayerCharacter.RightHand == null){
											PlayerCharacter.RightHand = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.RightHand;
											PlayerCharacter.RightHand = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
                                    case Item.ItemType.TwoHanded:
										if(PlayerCharacter.RightHand == null && PlayerCharacter.LeftHand == null){
											PlayerCharacter.RightHand = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.LeftHand = new Item();
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											
										}
									break;
                                    case Item.ItemType.OffHand:
                                    if (PlayerCharacter.LeftHand == null)
                                    {
                                        PlayerCharacter.LeftHand = PlayerCharacter.Inventory[cnt];
                                        PlayerCharacter.Inventory.RemoveAt(cnt);
                                        _doubleClickTimer = 0;
                                        _selectedItem = null;
                                    }
                                    else
                                    {
                                        Item temp = PlayerCharacter.LeftHand;
                                        PlayerCharacter.LeftHand = PlayerCharacter.Inventory[cnt];
                                        PlayerCharacter.Inventory[cnt] = temp;
                                    }
                                    break;
									case Item.ItemType.Hat:
										if(PlayerCharacter.Head == null){
											PlayerCharacter.Head = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Head;
											PlayerCharacter.Head = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
                                    case Item.ItemType.Shirt:
										if(PlayerCharacter.Chest == null){
											PlayerCharacter.Chest = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Chest;
											PlayerCharacter.Chest = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
                                    case Item.ItemType.Neck:
										if(PlayerCharacter.Neck == null){
											PlayerCharacter.Neck = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Neck;
											PlayerCharacter.Neck = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
									case Item.ItemType.Pants:
										if(PlayerCharacter.Legs == null){
											PlayerCharacter.Legs = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Legs;
											PlayerCharacter.Legs = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
                                    case Item.ItemType.Gloves:
										if(PlayerCharacter.Hands == null){
											PlayerCharacter.Hands = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Hands;
											PlayerCharacter.Hands = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
                                    case Item.ItemType.Boots:
										if(PlayerCharacter.Feet == null){
											PlayerCharacter.Feet = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Feet;
											PlayerCharacter.Feet = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
                                    case Item.ItemType.Ring:
										if(PlayerCharacter.Ring == null){
											PlayerCharacter.Ring = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Ring;
											PlayerCharacter.Ring = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
                                    case Item.ItemType.Wrist:
										if(PlayerCharacter.Wrist == null){
											PlayerCharacter.Wrist = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Wrist;
											PlayerCharacter.Wrist = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;
									case Item.ItemType.Back:
										if(PlayerCharacter.Back == null){
											PlayerCharacter.Back = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory.RemoveAt(cnt);
											_doubleClickTimer = 0;
											_selectedItem = null;
										}else{
											Item temp = PlayerCharacter.Back;
											PlayerCharacter.Back = PlayerCharacter.Inventory[cnt];
											PlayerCharacter.Inventory[cnt] = temp;
										}
									break;

                                    default:
                                        Debug.Log("No Double Click");
                                        break;

									
								}
								#endregion
							}else{
								_doubleClickTimer = Time.time;
							}
						}else{
							_doubleClickTimer = Time.time;
							_selectedItem = PlayerCharacter.Inventory[cnt];
						}
					}
                    if (PlayerCharacter.Inventory[cnt].Stackable)
                    {
                        GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), PlayerCharacter.Inventory[cnt].ItemsInStack.ToString(), "Inventory Slot Empty");
                    }
				}else{
					GUI.Label(new Rect(5 +(x*buttonWidth), 20 + (y*buttonHeight),buttonWidth,buttonHeight),(x+y*_inventoryCols).ToString(), "Inventory Slot Empty");		
				}
				
				cnt++;
			}
		}
		
		GUI.DragWindow();
		SetToolTip();
	}
	
	public void ToggleInventoryWindow(){
		_displayInventoryWindow = !_displayInventoryWindow;
	}

    public void ToggleOvenWindow()
    {
        _displayOvenWindow = !_displayOvenWindow;
    }

    public void ToggleCraftingWindow()
    {
        Crafting.givePlayerItem(Crafting.ItemName.WoodLog, 20);
        Crafting.givePlayerItem(Crafting.ItemName.SmallRock, 20);
        Crafting.givePlayerItem(Crafting.ItemName.LargeRock, 20);
        Crafting.givePlayerItem(Crafting.ItemName.Vine, 20);

        if (_displayCraftingWindow)
        {
            craftableItems = Crafting.getCraftableItems(PlayerCharacter.Inventory);
        }
        _displayCraftingWindow = !_displayCraftingWindow;
    }
	
	public void CharacterWindow(int id){
		_characterPanel = GUI.Toolbar(new Rect(5,25,_characterWindowRect.width - 10,25),_characterPanel,_characterPanelNames);
		
		switch(_characterPanel){
		case 0:
			DisplayEquiptment();
			break;
		case 1:
			DisplayAttributes();
			break;
		case 2:
			DisplaySkills();
			break;
		}
		
		
		
		GUI.DragWindow();	
	}
	
	public void ToggleCharacterWindow(){
		_displayCharacterWindow = !_displayCharacterWindow;
	}
	
	private void SetToolTip() {
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip){
			if(_toolTip != ""){
				_toolTip = "";
			}
			
			if(GUI.tooltip != ""){
				_toolTip = GUI.tooltip;	
			}
		}
	}
	
	private void DisplayToolTip(){
		if(_toolTip != ""){
			if(Event.current.mousePosition.x <= Screen.width - 205){
                GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y - 100, 200, 100), _toolTip);
			}else{
                GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y - 100, 200, 100), _toolTip);
			}
		}
		
	}
	
	private void DisplayEquiptment(){
		
		if(PlayerCharacter.RightHand == null){
			GUI.Label(new Rect(190,160,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(190,160,40,40),new GUIContent(PlayerCharacter.RightHand.Icon, PlayerCharacter.RightHand.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.RightHand);
				PlayerCharacter.RightHand = null;
			}
		}
		
		if(PlayerCharacter.LeftHand == null){
			GUI.Label(new Rect(30,160,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(30,160,40,40),new GUIContent(PlayerCharacter.LeftHand.Icon, PlayerCharacter.LeftHand.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.LeftHand);
				PlayerCharacter.LeftHand = null;
			}
		}
		
		if(PlayerCharacter.Head == null){
			GUI.Label(new Rect(115,70,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(115,70,40,40),new GUIContent(PlayerCharacter.Head.Icon, PlayerCharacter.Head.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Head);
				PlayerCharacter.Head = null;
			}
		}
		if(PlayerCharacter.Chest == null){
			GUI.Label(new Rect(115,160,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(115,160,40,40),new GUIContent(PlayerCharacter.Chest.Icon, PlayerCharacter.Chest.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Chest);
				PlayerCharacter.Chest = null;
			}
		}
		if(PlayerCharacter.Hands == null){
			GUI.Label(new Rect(50,250,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(50,250,40,40),new GUIContent(PlayerCharacter.Hands.Icon, PlayerCharacter.Hands.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Hands);
				PlayerCharacter.Hands = null;
			}
		}
		if(PlayerCharacter.Legs == null){
			GUI.Label(new Rect(115,205,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(115,205,40,40),new GUIContent(PlayerCharacter.Legs.Icon, PlayerCharacter.Legs.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Legs);
				PlayerCharacter.Legs = null;
			}
		}
		if(PlayerCharacter.Feet == null){
			GUI.Label(new Rect(115,250,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(115,250,40,40),new GUIContent(PlayerCharacter.Feet.Icon, PlayerCharacter.Feet.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Feet);
				PlayerCharacter.Feet = null;
			}
		}
		
		if(PlayerCharacter.Neck == null){
			GUI.Label(new Rect(115,115,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(115,115,40,40),new GUIContent(PlayerCharacter.Neck.Icon, PlayerCharacter.Neck.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Neck);
				PlayerCharacter.Neck = null;
			}
		}
		if(PlayerCharacter.Wrist == null){
			GUI.Label(new Rect(10,250,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(10,250,40,40),new GUIContent(PlayerCharacter.Wrist.Icon, PlayerCharacter.Wrist.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Wrist);
				PlayerCharacter.Wrist = null;
			}
		}
		if(PlayerCharacter.Ring == null){
			GUI.Label(new Rect(190,250,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(190,250,40,40),new GUIContent(PlayerCharacter.Ring.Icon, PlayerCharacter.Ring.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Ring);
				PlayerCharacter.Ring = null;
			}
		}
		if(PlayerCharacter.Back == null){
			GUI.Label(new Rect(70,115,40,40),"","Inventory Slot Empty");
		}else{
			if(GUI.Button(new Rect(70,115,40,40),new GUIContent(PlayerCharacter.Back.Icon, PlayerCharacter.Back.ToolTip()),getSlotSkin())){
				PlayerCharacter.Inventory.Add(PlayerCharacter.Back);
				PlayerCharacter.Back = null;
			}
		}
		
		
		SetToolTip();
		
	}
	
	private void DisplayAttributes(){
		
	}
	
	private void DisplaySkills(){
		
	}
	
	private string getSlotSkin(){

			return "Inventory Slot Common";

	}

}

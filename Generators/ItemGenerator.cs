using UnityEngine;

public static class ItemGenerator {
	public const int BASE_MELEE_RANGE = 3;
	public const int BASE_RANGED_RANGE = 10;
	
	private const string MELEE_WEAPONS_AXES_PATH ="Item Icons/Weapons/Melee/Axes/";
	private const string MELEE_WEAPONS_SWORDS_PATH ="Item Icons/Weapons/Melee/Swords/";
	private const string MELEE_WEAPONS_SPEARS_PATH ="Item Icons/Weapons/Melee/Spears/";
	private const string MELEE_WEAPONS_DAGGERS_PATH ="Item Icons/Weapons/Melee/Daggers/";
	private const string RANGED_WEAPONS_GUN_PATH ="Item Icons/Weapons/Range/Guns/";
	private const string RANGED_WEAPONS_BOW_PATH ="Item Icons/Weapons/Range/Bows/";
	
	
	public static Item CreateRandomItem() {
		float selector = 0;
		selector = Random.Range(0,101); //TODO: Add Weights
		//decide what type of item to make
		if(selector <= 100 && selector > 85){
			Item item = CreateWeapon(); // Weapon
			return item;
		}else if(selector >= 45){
			Item item = CreateArmor(); // Armor
			return item;
		}else if(selector >= 35){
			Item item = CreateAccessory(); // Accessory
			return item;
		}else{
			Item item = new Item(); // Misc
			return item;
		}
		selector = 0;
	}
	
	private static Weapon CreateWeapon() {
		float selector = 0;
		selector = Random.Range(0,21); // TODO: Add weights
		//decide if we make a melee or ranged weapon
		if(selector <= 20 && selector > 10){
			Weapon weapon = CreateMeleeWeapon();
			return weapon;
		}else{
			Weapon weapon = CreateRangeWeapon();
			return weapon;
		}
	}
	
	private static Weapon CreateMeleeWeapon() {
		Weapon meleeWeapon = new Weapon();
		
		int selector = 0;
		selector = Random.Range(0,4);
		if(selector == 0){ //Sword
			meleeWeapon.Name = "Sword";
			meleeWeapon.Type = Item.ItemType.Sword;
			meleeWeapon.MaxDamage = Random.Range(5,11);
			meleeWeapon.DamageVariance = Random.Range(.2f,.76f);
			meleeWeapon.TypeofDamage = DamageType.Pierce;
			meleeWeapon.MaxRange = BASE_MELEE_RANGE;
			meleeWeapon.Icon = Resources.Load(MELEE_WEAPONS_SWORDS_PATH + getSwordIcon()) as Texture2D;
			
			return meleeWeapon;
		}else if(selector == 1){ //Axe
			meleeWeapon.Name = "Axe";
            meleeWeapon.Type = Item.ItemType.Axe;
			meleeWeapon.MaxDamage = Random.Range(5,11);
			meleeWeapon.DamageVariance = Random.Range(.2f,.76f);
			meleeWeapon.TypeofDamage = DamageType.Slash;
			meleeWeapon.MaxRange = BASE_MELEE_RANGE;
			meleeWeapon.Icon = Resources.Load(MELEE_WEAPONS_AXES_PATH + getAxeIcon()) as Texture2D;
			
			return meleeWeapon;
		}else if(selector == 2){ // Spear
			meleeWeapon.Name = "Spear";
            meleeWeapon.Type = Item.ItemType.Spear;
			meleeWeapon.MaxDamage = Random.Range(5,11);
			meleeWeapon.DamageVariance = Random.Range(.2f,.76f);
			meleeWeapon.TypeofDamage = DamageType.Pierce;
			meleeWeapon.MaxRange = BASE_MELEE_RANGE+.5f;
			meleeWeapon.Icon = Resources.Load(MELEE_WEAPONS_SPEARS_PATH + getSpearIcon()) as Texture2D;
			
			return meleeWeapon;
		}else if(selector == 3){ // Dagger
			meleeWeapon.Name = "Dagger";
            meleeWeapon.Type = Item.ItemType.Dagger;
			meleeWeapon.MaxDamage = Random.Range(5,11);
			meleeWeapon.DamageVariance = Random.Range(.2f,.76f);
			meleeWeapon.TypeofDamage = DamageType.Slash;
			meleeWeapon.MaxRange = BASE_MELEE_RANGE-.3f;
			meleeWeapon.Icon = Resources.Load(MELEE_WEAPONS_DAGGERS_PATH + getDaggerIcon()) as Texture2D;
			
			return meleeWeapon;
		}else{
			//fill in all of the values for that item type
			meleeWeapon.Name = "MW:"+ Random.Range(0,100);
			meleeWeapon.MaxDamage = Random.Range(5,11);
			meleeWeapon.DamageVariance = Random.Range(.2f,.76f);
			meleeWeapon.TypeofDamage = DamageType.Slash;
			meleeWeapon.MaxRange = BASE_MELEE_RANGE;
			
			return meleeWeapon;
		}
	}
	
	private static Weapon CreateRangeWeapon() {
		Weapon rangeWeapon = new Weapon();
		int selector = Random.Range(0,6);
		if(selector == 0 || selector == 1){ //Bow
			rangeWeapon.Name = "Bow";
            rangeWeapon.Type = Item.ItemType.Bow;
			rangeWeapon.MaxDamage = Random.Range(5,11);
			rangeWeapon.DamageVariance = Random.Range(.2f,.76f);
			rangeWeapon.TypeofDamage = DamageType.Ranged;
			rangeWeapon.MaxRange = BASE_RANGED_RANGE;
			rangeWeapon.Icon = Resources.Load(RANGED_WEAPONS_BOW_PATH + getBowIcon()) as Texture2D;
			
			return rangeWeapon;
		}else if(selector == 2 || selector == 3){ //Crossbow
			rangeWeapon.Name = "Crossbow";
            rangeWeapon.Type = Item.ItemType.Crossbow;
			rangeWeapon.MaxDamage = Random.Range(5,11);
			rangeWeapon.DamageVariance = Random.Range(.2f,.76f);
			rangeWeapon.TypeofDamage = DamageType.Ranged;
			rangeWeapon.MaxRange = BASE_RANGED_RANGE-.2f;
			rangeWeapon.Icon = Resources.Load(RANGED_WEAPONS_BOW_PATH + getCrossbowIcon()) as Texture2D;
			
			return rangeWeapon;
		}else if(selector == 4){ //Pistol
			rangeWeapon.Name = "Pistol";
            rangeWeapon.Type = Item.ItemType.Pistol;
			rangeWeapon.MaxDamage = Random.Range(5,11);
			rangeWeapon.DamageVariance = Random.Range(.2f,.76f);
			rangeWeapon.TypeofDamage = DamageType.RangedPierce;
			rangeWeapon.MaxRange = BASE_RANGED_RANGE+1;
			rangeWeapon.Icon = Resources.Load(RANGED_WEAPONS_GUN_PATH + getPistolIcon()) as Texture2D;
			
			return rangeWeapon;
		}else if(selector == 5){ // Rifle
			rangeWeapon.Name = "Rifle";
            rangeWeapon.Type = Item.ItemType.Rifle;
			rangeWeapon.MaxDamage = Random.Range(5,11);
			rangeWeapon.DamageVariance = Random.Range(.2f,.76f);
			rangeWeapon.TypeofDamage = DamageType.RangedPierce;
			rangeWeapon.MaxRange = BASE_RANGED_RANGE+3;
			rangeWeapon.Icon = Resources.Load(RANGED_WEAPONS_GUN_PATH + getRifleIcon()) as Texture2D;
			
			return rangeWeapon;
		}else{
			rangeWeapon.Name = "RW:"+ Random.Range(0,100);
			rangeWeapon.MaxDamage = Random.Range(5,11);
			rangeWeapon.DamageVariance = Random.Range(.2f,.76f);
			rangeWeapon.TypeofDamage = DamageType.Ranged;
			rangeWeapon.MaxRange = BASE_RANGED_RANGE;
			
			return rangeWeapon;
		}
	}
	
	private static Armor CreateArmor() {
		Armor armor = new Armor();
		// TO DO:
		// Decide which type fo armor to make. (chest,legs... etc);
		int selector = Random.Range(0,5);
		
		if(selector == 0){ // Head
            armor.Name = "Hat";
            armor.Type = Item.ItemType.Hat;
			armor.ArmorLevel = Random.Range(5,11);
			armor.MaxDurability = Random.Range(10,26);
			armor.CurrentDurability = armor.MaxDurability;
		}else if(selector == 1){ // Chest
            armor.Name = "Shirt";
            armor.Type = Item.ItemType.Shirt;
			armor.ArmorLevel = Random.Range(5,11);
			armor.MaxDurability = Random.Range(10,26);
			armor.CurrentDurability = armor.MaxDurability;
		}else if(selector == 2){ // Legs
            armor.Name = "Pants";
            armor.Type = Item.ItemType.Pants;
			armor.ArmorLevel = Random.Range(5,11);
			armor.MaxDurability = Random.Range(10,26);
			armor.CurrentDurability = armor.MaxDurability;
		}else if(selector == 3){ // Shoulders
            armor.Name = "Gloves";
            armor.Type = Item.ItemType.Gloves;
			armor.ArmorLevel = Random.Range(5,11);
			armor.MaxDurability = Random.Range(10,26);
			armor.CurrentDurability = armor.MaxDurability;
		}else if(selector == 4){ // Feet
            armor.Name = "Boots";
            armor.Type = Item.ItemType.Boots;
			armor.ArmorLevel = Random.Range(5,11);
			armor.MaxDurability = Random.Range(10,26);
			armor.CurrentDurability = armor.MaxDurability;
		}else{
			armor.Name = "AR:"+ Random.Range(0,100);
			armor.ArmorLevel = Random.Range(5,11);
			armor.MaxDurability = Random.Range(10,26);
			armor.CurrentDurability = armor.MaxDurability;
		}
		
		return armor;
	}
	
	private static Jewelry CreateAccessory() {
		Jewelry accessory = new Jewelry();
		// Decide which type of accessory to make. (neck,ring... etc);
		int selector = Random.Range(0,3);
		if(selector == 0){ //Neck
			accessory.Name = "Neck";
            accessory.Type = Item.ItemType.Neck;
			accessory.Slot = JewelrySlot.Necklace;
			
			return accessory;
		}else if(selector == 1){ // Ring
			accessory.Name = "Ring";
            accessory.Type = Item.ItemType.Ring;
			accessory.Slot = JewelrySlot.Rings;
		
			return accessory;
		}else if(selector == 2){ // Wrist
			accessory.Name = "Wrist";
            accessory.Type = Item.ItemType.Wrist;
			accessory.Slot = JewelrySlot.Bracelets;
			
			return accessory;
		}else{
			accessory.Name = "ACC:"+ Random.Range(0,100);
			
			return accessory;	
		}
	}

    public static Item CreateTreeLoot(CutTree.TreeType treeType,int amount)
    {
        Item Log = new Item();
        if (treeType == CutTree.TreeType.Oak)
        {
            Log.Name = "Oak Log";
            Log.Type = Item.ItemType.Wood;
            Log.Stackable = true;
            Log.ItemsInStack = amount;
            return Log;
        }
        else
        {
            Log.Name = "Oak Log";
            Log.Type = Item.ItemType.Wood;
            Log.Stackable = true;
            Log.ItemsInStack = amount;
            return Log;
        }
    }
		
	#region 	Get Icons
	//Melee Weapons
	private static string getSwordIcon(){
		int temp = Random.Range(0,5);
		if(temp > 9){
			return "Sword" + temp.ToString();
		}else{
			return "Sword0" + temp.ToString();
		}
	}
	private static string getAxeIcon(){
		int temp = Random.Range(0,3);
		if(temp > 9){
			return "Axe" + temp.ToString();
		}else{
			return "Axe0" + temp.ToString();
		}		
	}
	private static string getSpearIcon(){
		int temp = 0;
		if(temp > 9){
			return "Spear" + temp.ToString();
		}else{
			return "Spear0" + temp.ToString();
		}		
	}
	private static string getDaggerIcon(){
		int temp = Random.Range(0,4);
		if(temp > 9){
			return "Dagger" + temp.ToString();
		}else{
			return "Dagger0" + temp.ToString();
		}		
	}
	// End Melee Weapons
	////Ranged Weapons
	private static string getBowIcon(){
		int temp = 0;
		if(temp > 9){
			return "Bow" + temp.ToString();
		}else{
			return "Bow0" + temp.ToString();
		}		
	}
	private static string getCrossbowIcon(){
		int temp = 0;
		if(temp > 9){
			return "Crossbow" + temp.ToString();
		}else{
			return "Crossbow0" + temp.ToString();
		}		
	}
	private static string getPistolIcon(){
		int temp = 0;
		if(temp > 9){
			return "Pistol" + temp.ToString();
		}else{
			return "Pistol0" + temp.ToString();
		}		
	}
	private static string getRifleIcon(){
		int temp = 0;
		if(temp > 9){
			return "Rifle" + temp.ToString();
		}else{
			return "Rifle0" + temp.ToString();
		}		
	}
	//// End ranged Weapons
	#endregion
	
}

public enum ItemType {
	Armor,
	Weapon,
	Accessory, //neck rings...
	Materials // Logs, Rocks, Ores, etc...
}

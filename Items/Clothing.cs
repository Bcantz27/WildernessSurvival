using UnityEngine;

public class Clothing : BuffItem {
	private ArmorSlot _slot; // Store armor slots
	
	public Clothing() {
		_slot = ArmorSlot.Head;
	}
	
	public Clothing(ArmorSlot slot) {
		_slot = slot;
	}
	
	public ArmorSlot Slot {
		get{return _slot;}
		set{ _slot = value;}
	}
}

public enum ArmorSlot {
	Head,
	Shoulders,
	Chest,
	Legs,
	Back,
	Boots
}

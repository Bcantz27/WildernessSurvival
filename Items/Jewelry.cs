using UnityEngine;

public class Jewelry : BuffItem {
	private JewelrySlot _slot;  // Store slot
	
	public Jewelry() {
		_slot = JewelrySlot.Rings;	
	}
	
	public Jewelry(JewelrySlot slot) {
		_slot = slot;
	}
	
	public JewelrySlot Slot {
		get{ return _slot; }
		set{ _slot = value; }
	}
}

public enum JewelrySlot {
	Necklace,
	Bracelets,
	Rings
}

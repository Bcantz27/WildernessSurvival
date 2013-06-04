using UnityEngine;

public class Armor : Clothing {
	private int _armorLevel;  //the armor level of this armor
	private int _curDur;
	private int _maxDur;
	
	public Armor(){
		_armorLevel = 0;
		_maxDur = 100;
		_curDur = _maxDur;
	}
	
	public Armor(int al,int maxDur,int curDur) {
		_armorLevel = al;
		_maxDur = maxDur;
		_curDur = curDur;
	}
	
	public int ArmorLevel {
		get{return _armorLevel;}
		set{_armorLevel = value;}
	}
	
	public int MaxDurability {
		get { return _maxDur; }
		set { _maxDur = value;}
	}
	
	public int CurrentDurability {
		get { return _curDur; }
		set { _curDur = value;}
	}
}

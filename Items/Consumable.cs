using UnityEngine;

public class Consumable : BuffItem {
	private float[] _amountToHeal = {0,0,0,0}; 	//the amount to heal each vital
	
	private float _buffTime;		//how long the buff will last.
	
	public Consumable() {
        _buffTime = 0;
	}
	
	public Consumable(float[] amtHeal, float bTime){
		_amountToHeal = amtHeal;
		_buffTime = bTime;
	}
	
	public float getHealAtIndex(int index){
		if(index < _amountToHeal.Length && index > -1)
			return _amountToHeal[index];
		else
			return 0;
	}
	
	public void setHealAt(int index, float heal) {
		if(index < _amountToHeal.Length && index > -1)
			_amountToHeal[index] = heal;
	}
	
	public float BuffTime {
		get{ return _buffTime;}
		set{ _buffTime = value;}
	}
}

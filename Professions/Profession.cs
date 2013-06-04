using UnityEngine;
using System.Collections;

public class Profession {
	private string _name;
	private int _level;
	private int _expToLevel;
	private float _levelModifier;
	
	public string Name{
		get{return _name;}
		set{_name = value;}		
	}
	
	public int Level{
		get{return _level;}
		set{_level = value;}
	}
	
	public int ExpToLevel{
		get{return _expToLevel;}
		set{_expToLevel = value;}		
	}
	
	public float LevelMod{
		get{return _levelModifier;}
		set{_levelModifier = value;}		
	}
}

// Professions
// Cooking,Hunting,Skinning,Smithing,Mining,Shipcrafting,Woodcutting


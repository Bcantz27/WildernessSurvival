/// <summary>
/// Modified stat.cs
/// 
/// 
/// This is the base class for all stats that will be modifiable by attributes.
/// </summary>

using System.Collections.Generic;			

public class ModifiedStat : BaseStat {
	private List<ModifyingAttribute> _mods;  	// A List of attributes that modify this stat
	private int _modValue; 						// The amout added to the base value from the modifiers
	
	public ModifiedStat() {
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	/// <summary>
	/// Adds the modifying attribute to our list of mods for this modifiedstat
	/// </summary>
	/// <param name='mod'>
	/// Mod.
	/// </param>
	public void addModifier(ModifyingAttribute mod) {
		_mods.Add (mod);	
	}
	
	/// <summary>
	/// Reset _modvalue to 0 and then check to see if we have 1 modifyingattribute in our list of mods
	/// </summary>
	private void calculateModValue() {
		_modValue = 0;	
		
		if(_mods.Count > 0)
			foreach(ModifyingAttribute att in _mods)
				_modValue += (int)(att.attribute.adjustedBaseValue * att.ratio);
	}
	
	/// <summary>
	/// Calculate the adjustedbasevalue from the baseValue + buffValue + modvalue.
	/// This function is overriding the AdjustedBaseValue in the BaseStat class.
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public new int adjustedBaseValue {
		get{ return baseValue + buffValue + _modValue;	}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update() {
		calculateModValue();	
	}
	
	public string GetModifyingAttributeString() {
		string temp = "";
		for(int i = 0; i < _mods.Count; i++) {
			temp += _mods[i].attribute.Name;
			temp += "_";
			temp += _mods[i].ratio;
			
			if(i < _mods.Count - 1)
				temp += "|";
		}
		
		return temp;
	}
}

public struct ModifyingAttribute {
	public Attribute attribute;
	public float ratio;
	
	public ModifyingAttribute(Attribute att, float rat) {
		attribute = att;
		ratio = rat;
	}
}
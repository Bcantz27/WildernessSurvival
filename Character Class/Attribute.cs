/// <summary>
/// Attribute.cs
/// 
/// For all of the character attributs in the game.
/// </summary>
public class Attribute : BaseStat {
	new public const int STARTING_EXP_COST = 50;
	
	private string _name;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute() {
		_name = "";
		expToLevel = STARTING_EXP_COST;
		levelModifier =1.05f;
	}
	
	
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>
	/// The name.
	/// </value>
	public string Name {
		get {return _name; }
		set{ _name = value; }
	}
	
}

/// <summary>
/// List of Attribute names.
/// </summary>
public enum AttributeName {
	Vitality,
	Agility,
	Intelligence,
	Strength
}
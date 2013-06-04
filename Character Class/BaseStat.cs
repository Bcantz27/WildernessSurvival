/// <summary>
/// Base stat.cs
/// 
/// This is the base class for the stats in game.
/// </summary>

public class BaseStat{
	public const int STARTING_EXP_COST = 100; 		//public accessable value for all base stats to start at
	
	private int _baseValue; 						//the base value of this stat
	private int _buffValue; 						//the amount of the buff to this stat
	private int _expToLevel; 						//the total amount of exp needed to raise this skill
	private float _levelModifier; 					//the modifier applied to the exp needed to raise the skill
	
	private string _name;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat(){
		_baseValue = 0;
		_buffValue = 0;
		_name = "";
		_levelModifier = 1.1f;
		_expToLevel = STARTING_EXP_COST;
		
	}
	#region Basic Setters and Getters
	//Setters and Getters	
	public int baseValue{
		get{ return _baseValue; }
		set{ _baseValue = value; }
	}
	public string Name {
		get {return _name; }
		set{ _name = value; }
	}
	public int buffValue{
		get{ return _buffValue; }
		set{ _buffValue = value; }
	}
	public int expToLevel{
		get{ return _expToLevel; }
		set{ _expToLevel = value; }
	}
	public float levelModifier{
		get{ return _levelModifier; }
		set{ _levelModifier = value; }
	}
	#endregion
	
	/// <summary>
	/// Calculates the exp to level.
	/// </summary>
	/// <returns>
	/// The exp to level.
	/// </returns>
	private int calcExpToLevel() {
		return (int)(_expToLevel*_levelModifier);	
	}
	
	/// <summary>
	/// Levels up.
	/// </summary>
	public void levelUp() {
		_expToLevel = calcExpToLevel();
		_baseValue++;
	}
	
	/// <summary>
	/// Recalculates the adjusted base value and returns it
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public int adjustedBaseValue {
		get{ return _baseValue + _buffValue;	}
	}
	
}

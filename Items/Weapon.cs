public class Weapon : BuffItem {
	private int _maxDamage;
	private float _dmgVar;
    private string _type;
	private int _curDur;
	private int _maxDur;
	private float _maxRange; // Min range?
	private DamageType _dmgType;
	
	public Weapon() {
		_maxDamage = 0;
		_maxRange = 0;
		_dmgVar = 0;
		_dmgType = DamageType.Pierce;
		_maxDur = 100;
		_curDur = _maxDur;
	}
	
	public Weapon(int mDmg, float dmgV, float mRange, DamageType dt,int maxDur,int curDur){
		_maxDamage = mDmg;
		_maxRange = mRange;
		_dmgVar = dmgV;
		_dmgType = dt;
		_maxDur = maxDur;
		_curDur = curDur;
	}

	public int MaxDamage {
		get { return _maxDamage; }
		set { _maxDamage = value; }
	}
	public int MaxDurability {
		get { return _maxDur; }
		set { _maxDur = value;}
	}
	
	public int CurrentDurability {
		get { return _curDur; }
		set { _curDur = value;}
	}
	
	public float DamageVariance {
		get { return _dmgVar; }
		set { _dmgVar = value; }
	}
	
	public float MaxRange {
		get { return _maxRange; }
		set { _maxRange = value; }
	}
	
	public DamageType TypeofDamage {
		get { return _dmgType; }
		set { _dmgType = value; }
	}
	
	public override string ToolTip() {
		return Name + "\n" +
				"Durability " + CurrentDurability + "/" + MaxDurability + "\n" +
				(MaxDamage * DamageVariance) + " - " + MaxDamage;
	}
}

public enum DamageType {
	Slash,
	Pierce,
	Ranged,
    RangedPierce
}

public class Vital : ModifiedStat {
	private float _curValue;
    private float _maxValue;
	
	public Vital() {
        _maxValue = 100;
        _curValue = 100;	
		expToLevel = 40;
		levelModifier = 1.1f;
	}
	
	public float curValue {
		get{
			//if(_curValue > adjustedBaseValue)
			//	_curValue = adjustedBaseValue;
			
			return _curValue;
			
		}
		set{ _curValue = value; }
	}

    public float maxValue
    {
        get
        {
            return _maxValue;
        }
        set { _maxValue = value; }
    }
}

public enum VitalName {
	Health,
	Energy,
    Hunger,
    Thirst
}
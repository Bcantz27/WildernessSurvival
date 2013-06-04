public class Vital : ModifiedStat {
	private int _curValue;
	
	public Vital() {
		_curValue = 0;	
		expToLevel = 40;
		levelModifier = 1.1f;
	}
	
	public int curValue {
		get{
			if(_curValue > adjustedBaseValue)
				_curValue = adjustedBaseValue;
			
			
			return _curValue;
			
		}
		set{ _curValue = value; }
	}
}

public enum VitalName {
	Health,
	Energy
}
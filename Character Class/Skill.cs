public class Skill : ModifiedStat {
	private bool _known;
	
	public Skill() {
		_known = false;
		expToLevel = 25;
		levelModifier = 1.1f;
	}
	
	public bool Known {
		get { return _known; }
		set{ _known = value; }
	}
}

public enum SkillName {
	Hunting,
	Fishing,
	Cooking,
	Blacksmithing,
	Gunsmithing,
	Skinning,
	Leatherworking,
	Minning,
	Woodcutting,
	Construction
}

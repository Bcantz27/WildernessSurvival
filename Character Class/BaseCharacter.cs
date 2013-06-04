using UnityEngine;
using System.Collections;
using System; // added to access the enum class

public class BaseCharacter : MonoBehaviour {
	private string _name;
	private string _password;
	private string _email;
	private int _level;
	private uint _freeExp;
	
	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;
	
	private int hungerMax = 100;
	private int hungerCur = 100;
	private int temperature = 98; // F
	
	
	public int HungerMax {
		get {return hungerMax; }
		set{ hungerMax = value; }
	}
	
	public int HungerCur {
		get {return hungerCur; }
		set{ hungerCur = value; }
	}
	
	public int Temperature {
		get {return temperature; }
		set{ temperature = value; }
	}
	
	public void Awake() {
		_name = string.Empty;
		_email = string.Empty;
		_level = 0;
		_freeExp = 0;
		_password = string.Empty;
	
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		
		SetupPrimaryAttributes();
		SetupSkills();
		SetupVitals();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Setters and Getters
	public string Name{
		get{ return _name; }
		set{ _name = value; }
	}
	
	public string Email{
		get{ return _email; }
		set{ _email = value; }
	}
	
	public string Password{
		get{ return _password; }
		set{ _password = value; }
	}
	
	public int Level{
		get{ return _level; }
		set{ _level = value; }
	}
	
	public uint FreeExp{
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}
	
	public void AddExp(uint exp) {
			_freeExp += exp;
		
		CalculateLevel();
	}
	
	public void CalculateLevel() {
		//find a way to display level with all stats
	}
	
	private void SetupPrimaryAttributes() {
		for(int cnt = 0; cnt< _primaryAttribute.Length; cnt++) {
			_primaryAttribute[cnt] = new Attribute();
			_primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
		}
	}
	private void SetupVitals() {
		for(int cnt = 0; cnt< _vital.Length; cnt++) {
			_vital[cnt] = new Vital();
		}
		
		SetupVitalModifiers();
		
	}
	private void SetupSkills() {
		for(int cnt = 0; cnt< _skill.Length; cnt++) {
			_skill[cnt] = new Skill();
		}
		
		//SetupSkillModifiers();
		
	}
	
	public Attribute GetPrimaryAttribute(int index) {
		return _primaryAttribute[index];	
	}
	
	public Vital GetVital(int index) {
		return _vital[index];	
	}
	
	public Skill GetSkill(int index) {
		return _skill[index];	
	}
	
	private void SetupVitalModifiers() {
		//health
		GetVital((int)VitalName.Health).addModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Vitality), ratio = 2});
		GetVital((int)VitalName.Health).addModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Intelligence), ratio = .4f});
		//energy
		GetVital((int)VitalName.Energy).addModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Strength), ratio = 1});
		GetVital((int)VitalName.Energy).addModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Agility), ratio = .7f});

	}
	
	private void SetupSkillModifiers() { // FINISH
		
	}
	
	public void StatUpdate() {
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt].Update();
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt].Update();
	}
}

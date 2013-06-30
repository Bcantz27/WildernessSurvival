using UnityEngine;
using System.Collections;
using System; // added to access the enum class

public class BaseCharacter : MonoBehaviour {

	private string _name;
	private string _password;
	private string _email;
	private int _level;
	private uint _freeExp;
	
	public Attribute[] _primaryAttribute;
    public Vital[] _vital;
    public Skill[] _skill;

    private float[] _tickTimer = {0,0}; //0 is Hunger timer 1 is Thist timer
    private const float HUNGER_DECAY_THRESHHOLD = 10;
    private const float THIRST_DECAY_THRESHHOLD = 7;

	private int temperatureCur = 98; // F

    private void OnEnable()
    {
        Messenger<int, float>.AddListener("player hunger change", ChangeVital);
    }
    private void OnDisable()
    {
        Messenger<int, float>.RemoveListener("player hunger change", ChangeVital);
    }

    public void ChangeVital(int index, float amount)
    {
        if (_vital[index].curValue + amount < _vital[index].maxValue)
        {
            _vital[index].curValue += amount;
        }
        else
        {
            Debug.Log("" + _vital[index].Name + " is Full.");
            _vital[index].curValue = _vital[index].maxValue;
        }
    }

    //Setters and Getters
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public uint FreeExp
    {
        get { return _freeExp; }
        set { _freeExp = value; }
    }

    public void AddExp(uint exp)
    {
        _freeExp += exp;

        CalculateLevel();
    }
	
	public int Temperature {
        get { return temperatureCur; }
        set { temperatureCur = value; }
	}

    public Vital[] getVitalList()
    {
        return _vital;
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
        _tickTimer[0] += Time.deltaTime;
        _tickTimer[1] += Time.deltaTime;

        if (_tickTimer[0] >= HUNGER_DECAY_THRESHHOLD)
        {
            decayHunger();
            _tickTimer[0] = 0;
        }

        if (_tickTimer[1] >= THIRST_DECAY_THRESHHOLD)
        {
            decayThirst();
            _tickTimer[1] = 0;
        }

        Messenger<float, float>.Broadcast("player health update", GetVital(0).curValue, GetVital(0).maxValue, MessengerMode.DONT_REQUIRE_LISTENER);
        Messenger<float, float>.Broadcast("player energy update", GetVital(1).curValue, GetVital(1).maxValue, MessengerMode.DONT_REQUIRE_LISTENER);
        Messenger<float, float>.Broadcast("player hunger update", _vital[2].curValue, _vital[2].maxValue, MessengerMode.DONT_REQUIRE_LISTENER);
        Messenger<float, float>.Broadcast("player thirst update", _vital[3].curValue, _vital[3].maxValue, MessengerMode.DONT_REQUIRE_LISTENER);
	}

    public void decayHunger()
    {
        if (_vital[2].curValue - 1 >= 0)
        {
            _vital[2].curValue -= 1;
        }
        else
        {
            _vital[2].curValue = 0;
        }
        
        //Debug.Log("Hunger: " + hungerCur);
    }

    public void decayThirst()
    {
        if (_vital[3].curValue - 1 >= 0)
        {
            _vital[3].curValue -= 1;
        }
        else
        {
            _vital[3].curValue = 0;
        }
        
        //Debug.Log("Thirst: " + thirstCur);
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

    public Vital GetVital(int index)
    {
        return _vital[index];
    }

    public int GetVitalLength() {
		return _vital.Length;	
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

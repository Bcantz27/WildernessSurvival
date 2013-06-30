using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mob : MonoBehaviour {

    private string _name;
    private int _id;
    private float _healthCur;
    private float _healthMax;
    private float _defenseLevel;
    private float _attackLevel;
    private float _agiltityLevel;

    private bool _hostile;
    private bool _alive;
    private bool _skinable;

    private GameObject _mobGameObject;
    
    Mob()
    {
        _name = "No Name";
        _healthCur = 100;
        _healthMax = 100;
        _defenseLevel = 1;
        _attackLevel = 1;
        _hostile = false;
        _alive = true;
    }

    Mob(string name, float healthCur, float healthMax, bool hostile, bool canSkin)
    {
        _name = name;
        _healthCur = healthCur;
        _healthMax = healthMax;
        _hostile = hostile;
        _alive = true;
        _skinable = canSkin;
    }

    Mob(string name, float healthCur, float healthMax, bool hostile, bool canSkin, int defenseLevel, int attackLevel, int agiltityLevel)
    {
        _name = name;
        _healthCur = healthCur;
        _healthMax = healthMax;
        _defenseLevel = defenseLevel;
        _attackLevel = attackLevel;
        _hostile = hostile;
        _skinable = canSkin;
        _agiltityLevel = agiltityLevel;
        _alive = true;
    }

    void Awake()
    {
        if (name == "No Name")
        {
            _name = gameObject.transform.FindChild("MobName").GetComponent<TextMesh>().text;
        }
        _mobGameObject = gameObject;
    }

    //Setters and Getters
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public float CurrentHealth
    {
        get { return _healthCur; }
        set { _healthCur = value; }
    }

    public float MaxHealth
    {
        get { return _healthMax; }
        set { _healthMax = value; }
    }

    public float AttackLevel
    {
        get { return _attackLevel; }
        set { _attackLevel = value; }
    }

    public float DefenseLevel
    {
        get { return _defenseLevel; }
        set { _defenseLevel = value; }
    }

    public bool Hostility
    {
        get { return _hostile; }
        set { _hostile = value; }
    }

    public bool Alive
    {
        get { return _alive; }
        set { _alive = value; }
    }

    public bool Skinable
    {
        get { return _skinable; }
        set { _skinable = value; }
    }

    public GameObject GetGameObject
    {
        get { return _mobGameObject; }
        set { _mobGameObject = value; }
    }

    public static Mob getMob(MobName name)
    {
        Mob tempMob;
        switch (name)
        {
            case MobName.Wolf:
                tempMob = new Mob("Wolf", 65, 65, true, true,2,5,10);
                tempMob.Id = (int)MobName.Wolf;
                tempMob.GetGameObject = Resources.Load("Mobs/Wolf") as GameObject;
                break;
            case MobName.Bear:
                tempMob = new Mob("Bear", 300, 300, true, true, 19,13,7);
                tempMob.Id = (int)MobName.Bear;
                break;
            case MobName.Fox:
                tempMob = new Mob("Fox", 30, 30, false, true, 3, 3,9);
                tempMob.Id = (int)MobName.Fox;
                break;
            case MobName.Deer:
                tempMob = new Mob("Deer", 45, 45, false, true, 2, 2,7);
                tempMob.Id = (int)MobName.Deer;
                break;
            case MobName.Rabbit:
                tempMob = new Mob("Rabbit", 45, 45, false, true, 2, 2,4);
                tempMob.Id = (int)MobName.Rabbit;
                break;
            case MobName.Cougar:
                tempMob = new Mob("Cougar", 45, 45, true, true, 2, 2,15);
                tempMob.Id = (int)MobName.Cougar;
                break;

            default:
                tempMob = new Mob();
                break;
        }

        return tempMob;
    }

    public enum MobName
    {
        Wolf,
        Bear,
        Fox,
        Deer,
        Rabbit,
        Cougar
    }

}

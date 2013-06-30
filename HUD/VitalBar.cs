using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	public bool _isPlayerHealthBar; 			//Tells us if it is a player or mob
    public bool _isPlayerEnergyBar; 			//Tells us if it is a player or mob
    public bool _isPlayerHungerBar; 			//Tells us if it is a player or mob
    public bool _isPlayerThirstBar; 			//Tells us if it is a player or mob
	
	private float _maxHealthBarLength;
    private float _curHealthBarLength;

    private float _maxEnergyBarLength;
    private float _curEnergyBarLength;

    private float _maxHungerBarHeight;
    private float _curHungerBarHeight;

    private float _maxThirstBarHeight;
    private float _curThirstBarHeight;

	private GUITexture _display;
	
	void Awake() {
		_display = gameObject.GetComponent<GUITexture>();
	}
	
	// Use this for initialization
	void Start () {
		
		_display = gameObject.GetComponent<GUITexture>();

        _maxHealthBarLength = (_display.pixelInset.width);
        _maxEnergyBarLength = (_display.pixelInset.width);
        _maxHungerBarHeight = (_display.pixelInset.height);
        _maxThirstBarHeight = (_display.pixelInset.height);

		OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnEnable() {
		if(_isPlayerHealthBar) {
            Messenger<float, float>.AddListener("player health update", ChangeHealthBarSize);
        }else if (_isPlayerEnergyBar){
            Messenger<float, float>.AddListener("player energy update", ChangeEnergyBarSize);
        }else if (_isPlayerHungerBar){
            Messenger<float, float>.AddListener("player hunger update", ChangeHungerBarSize);
        }else if (_isPlayerThirstBar){
            Messenger<float, float>.AddListener("player thirst update", ChangeThirstBarSize);
		}else{
			ToggleDisplay(false);
            Messenger<float, float>.AddListener("mob health update", ChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalbar",ToggleDisplay);

		}
	}
	
	public void OnDisable() {
		if(_isPlayerHealthBar) {
            Messenger<float, float>.RemoveListener("player health update", ChangeHealthBarSize);
        }else if(_isPlayerEnergyBar){
            Messenger<float, float>.RemoveListener("player energy update", ChangeEnergyBarSize);
        }else if (_isPlayerHungerBar){
            Messenger<float, float>.RemoveListener("player hunger update", ChangeHungerBarSize);
        }else if (_isPlayerThirstBar){
            Messenger<float, float>.RemoveListener("player thirst update", ChangeThirstBarSize);
		}else{
            Messenger<float, float>.RemoveListener("mob health update", ChangeHealthBarSize);
			Messenger<bool>.RemoveListener("show mob vitalbar",ToggleDisplay);
		}
	}

    public void ChangeHealthBarSize(float curHealth, float maxHeath)
    {
        _curHealthBarLength = ((curHealth / maxHeath) * _maxHealthBarLength);

        _display.pixelInset = new Rect(_display.pixelInset.x, _display.pixelInset.y, _curHealthBarLength, _display.pixelInset.height);
	}

    public void ChangeEnergyBarSize(float curEnergy, float maxEnergy)
    {
        _curEnergyBarLength = ((curEnergy / maxEnergy) * _maxEnergyBarLength);

        _display.pixelInset = new Rect(_display.pixelInset.x, _display.pixelInset.y, _curEnergyBarLength, _display.pixelInset.height);
    }

    public void ChangeHungerBarSize(float curHunger, float maxHunger)
    {
        _curHungerBarHeight = ((curHunger / maxHunger) * _maxHungerBarHeight);

        _display.pixelInset = new Rect(_display.pixelInset.x, _display.pixelInset.y, _display.pixelInset.width, _curHungerBarHeight);
    }

    public void ChangeThirstBarSize(float curThirst, float maxThirst)
    {
        _curThirstBarHeight = ((curThirst / maxThirst) * _maxThirstBarHeight);
       
        _display.pixelInset = new Rect(_display.pixelInset.x, _display.pixelInset.y, _display.pixelInset.width, _curThirstBarHeight);
    }
	
	public void SetPlayerHealth(bool b) {
		_isPlayerHealthBar = b;
	}
	
	private void ToggleDisplay(bool show){
		_display.enabled = show;
		
	}
}

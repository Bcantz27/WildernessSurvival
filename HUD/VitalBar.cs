using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	public bool _isPlayerHealthBar; 			//Tells us if it is a player or mob
	
	private int _maxBarLength;
	private int _curBarLength;
	private GUITexture _display;
	
	void Awake() {
		_display = gameObject.GetComponent<GUITexture>();
	}
	
	// Use this for initialization
	void Start () {
		//_isPlayerHealthBar = true;
		
		_display = gameObject.GetComponent<GUITexture>();
		
		_maxBarLength = (int)(_display.pixelInset.width);
		OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnEnable() {
		if(_isPlayerHealthBar) {
			Messenger<int, int>.AddListener("player health update",ChangeHealthBarSize);
		}else{
			ToggleDisplay(false);
			Messenger<int, int>.AddListener("mob health update",ChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalbar",ToggleDisplay);

		}
	}
	
	public void OnDisable() {
		if(_isPlayerHealthBar) {
			Messenger<int, int>.RemoveListener("player health update",ChangeHealthBarSize);
		}else{
			Messenger<int, int>.RemoveListener("mob health update",ChangeHealthBarSize);
			Messenger<bool>.RemoveListener("show mob vitalbar",ToggleDisplay);
		}
	}
	
	public void ChangeHealthBarSize(int curHealth, int maxHeath) {
		_curBarLength = (int)((curHealth / (float)maxHeath) * _maxBarLength);
		
//		_display.pixelInset= new Rect(_display.pixelInset.x,_display.pixelInset.y,_curBarLength,_display.pixelInset.height);
	}
	
	public void SetPlayerHealth(bool b) {
		_isPlayerHealthBar = b;
	}
	
	private void ToggleDisplay(bool show){
		_display.enabled = show;
		
	}
}

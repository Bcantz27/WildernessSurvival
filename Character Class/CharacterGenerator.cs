using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour {
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 250;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 50;
	private int pointsLeft;
	
	private const int SCALE = 1;
	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 20;
	private const int STAT_LABEL_WIDTH = 300;
	private const int STAT_LABEL_HEIGHT = 40;
	private const int BASEVALUE_LABEL_WIDTH = 40*SCALE;
	private const int BUTTON_WIDTH = 50*SCALE;
	private const int BUTTON_HEIGHT = 21*SCALE;
	private const int CREATE_BUTTON_HEIGHT = 30*SCALE;
	private const int CREATE_BUTTON_WIDTH = 100*SCALE;
	

	
	public GUISkin mySkin;
	
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		pc.name = "pc";
		
		_toon = pc.GetComponent<PlayerCharacter>();
		
		pointsLeft = STARTING_POINTS;
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++){
			_toon.GetPrimaryAttribute(i).baseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE-MIN_STARTING_ATTRIBUTE_VALUE);
		}
		_toon.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.skin = mySkin;
		DisplayWindow();
		//GUI.skin = null;
		DisplayName();
		GUI.skin = mySkin;
		DisplayPointsLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();
		DisplayResetButton();
		if(_toon.Name == "" || pointsLeft > 0){
			DisplayCreateLabel();
		}
	}
	
	private void DisplayName() {
		GUI.Label(new Rect(Screen.width*2/3 - 25, Screen.height/4-25, 90, 25),"Username:", "TextField");
		_toon.Name = GUI.TextField(new Rect(Screen.width*2/3+65, Screen.height/4-25, 100, 25), _toon.Name);
		DisplayPassword();
	}
	private void DisplayPassword() {
		GUI.Label(new Rect(Screen.width*2/3 - 25, Screen.height/4, 90, 25),"Password:", "TextField");
		_toon.Password = GUI.PasswordField(new Rect(Screen.width*2/3+65, Screen.height/4, 100, 25),_toon.Password,"*"[0],25);
		GUI.Label(new Rect(Screen.width*2/3 - 25, Screen.height/4+25, 90, 25),"Email Address:", "TextField");
		_toon.Email = GUI.TextField(new Rect(Screen.width*2/3+65, Screen.height/4+25, 100, 25),_toon.Email);
	}
	
	private void DisplayAttributes() {
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++){
			GUI.Label(new Rect(Screen.width/8, Screen.height*3/5 + (i*LINE_HEIGHT), STAT_LABEL_WIDTH, LINE_HEIGHT), ((AttributeName)i).ToString());
			GUI.skin = null;
			GUI.Label(new Rect(Screen.width/8 + 200, Screen.height*3/5 + (i*LINE_HEIGHT), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _toon.GetPrimaryAttribute(i).adjustedBaseValue.ToString());
			GUI.skin = mySkin;
			if(GUI.RepeatButton(new Rect(Screen.width/8 + 290,Screen.height*3/5 + (i*LINE_HEIGHT)+ 2, BUTTON_WIDTH,BUTTON_HEIGHT), "-")){
				if(_toon.GetPrimaryAttribute(i).baseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(i).baseValue--;
					pointsLeft++;
					_toon.StatUpdate();
				}
			}
			if(GUI.RepeatButton(new Rect(Screen.width/8 + 320,Screen.height*3/5 + (i*LINE_HEIGHT)+2, BUTTON_WIDTH,BUTTON_HEIGHT), "+")){
				if(pointsLeft > 0) {
					_toon.GetPrimaryAttribute(i).baseValue++;
					pointsLeft--;
					_toon.StatUpdate();
				}
			}
			if(_toon.Name != "" && pointsLeft == 0){
			if(GUI.Button(new Rect(Screen.width-OFFSET*8-CREATE_BUTTON_WIDTH, Screen.height-OFFSET*10-CREATE_BUTTON_HEIGHT,CREATE_BUTTON_WIDTH, CREATE_BUTTON_HEIGHT), "Create")) {		
				GameSettings gsScript = GameObject.Find("_GameSettings").GetComponent<GameSettings>();
				
				//Change the cur value of the vitals to the max modified value of that vital
				UpdateCurVitalValues();
				
				
				gsScript.SaveCharacterData();
				Application.LoadLevel(2);
			}
			}
		}
	}
	
	private void DisplayVitals() {
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++){
			GUI.Label(new Rect(Screen.width/8,Screen.height*1/5 + (i*LINE_HEIGHT), STAT_LABEL_WIDTH, LINE_HEIGHT), ((VitalName)i).ToString());
			GUI.skin = null;
			GUI.Label(new Rect(Screen.width/8+200, Screen.height*1/5 + (i*LINE_HEIGHT), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _toon.GetVital(i).adjustedBaseValue.ToString());
			GUI.skin = mySkin;
		}
	}
	
	private void DisplaySkills() {
		for(int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++){
			GUI.Label(new Rect(Screen.width/8, Screen.height*2/5 - 35 + (i*LINE_HEIGHT), STAT_LABEL_WIDTH, LINE_HEIGHT), ((SkillName)i).ToString()+" ");
			GUI.skin = null;
			GUI.Label(new Rect(Screen.width/8 + 200, Screen.height*2/5 - 35 + (i*LINE_HEIGHT), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _toon.GetSkill(i).adjustedBaseValue.ToString());
			GUI.skin = mySkin;
		}
	}
	private void resetPoints(){
		int temp = 0;
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++){
			temp = _toon.GetPrimaryAttribute(i).baseValue - 50;
			pointsLeft += temp;
			_toon.GetPrimaryAttribute(i).baseValue = 50;
			_toon.StatUpdate();
			temp = 0;
		}		
	}
	
	private void DisplayPointsLeft() {
		GUI.Label(new Rect(Screen.width/8,Screen.height*3/5 + (6*BUTTON_HEIGHT)-5, STAT_LABEL_WIDTH, LINE_HEIGHT),"Points Left: "+ pointsLeft);
	}
	
	private void DisplayResetButton() {
		if(GUI.Button(new Rect(Screen.width/4 + 300,Screen.height*3/5 + (6*BUTTON_HEIGHT)- 2, BUTTON_WIDTH*2,BUTTON_HEIGHT), "Reset Points")){
			resetPoints ();
		}
	}
	
	private void DisplayCreateLabel() {
		GUI.Label(new Rect(Screen.width-OFFSET*8-CREATE_BUTTON_WIDTH, Screen.height-OFFSET*10-CREATE_BUTTON_HEIGHT,CREATE_BUTTON_WIDTH, CREATE_BUTTON_HEIGHT), "Creating...", "Button");
	}
	
	private void DisplayWindow() {
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),"", "Window");
	}
	
	private void UpdateCurVitalValues() {
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++){
			_toon.GetVital(i).curValue = _toon.GetVital(i).adjustedBaseValue;
		}
	}
}

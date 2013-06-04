using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public enum TimeOfDay {
		Idle,
		SunRise,
		SunSet
	}
	
	public Transform[] sun;
	private	Sun[] _sunScript;
	
	public float dayCycleInMinutes = 1;

	public float sunRise;
	public float sunSet;
	public float skyBoxBlendModifier;  // how fast to blend skybox.
	
	public Color ambLightMax;
	public Color ambLightMin;
	
	public float morningLight;  
	public float nightLight;
	private bool _isMorning = false;
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;
	
	private TimeOfDay _tod;
	private float _noonTime;
	
	private float _degreeRotation;
	private float _timeOfDay;
	private float _dayCycleInSeconds;
	private float _morningLength;
	private float _eveningLength;
	
	// Use this for initialization
	void Start () {
		_tod = TimeOfDay.Idle;
		_dayCycleInSeconds = dayCycleInMinutes * MINUTE;
		_sunScript = new Sun[sun.Length];
		
		RenderSettings.skybox.SetFloat("_Blend",0);
		
		for(int i = 0; i < sun.Length; i++){
			Sun temp = sun[i].GetComponent<Sun>();
			
			if(temp == null){
				Debug.LogWarning("Sun script not found. Adding it.");
				sun[i].gameObject.AddComponent<Sun>();
				temp = sun[i].GetComponent<Sun>();
			}
			_sunScript[i] = temp;
		}
		
		_timeOfDay = 0;
		_degreeRotation = DEGREES_PER_SECOND * DAY /_dayCycleInSeconds;
		
		sunRise *= _dayCycleInSeconds;
		sunSet *= _dayCycleInSeconds;
		_noonTime = _dayCycleInSeconds/2;
		_morningLength = _noonTime - sunRise;
		_eveningLength = sunSet - _noonTime;
		morningLight *= _dayCycleInSeconds;
		nightLight *= _dayCycleInSeconds;
		
		SetupLighting();
	}
	
	// Update is called once per frame
	void Update () {
		_timeOfDay += Time.deltaTime;
		
		
		if(_timeOfDay > _dayCycleInSeconds) {
			_timeOfDay -=_dayCycleInSeconds;	
		}
		
		if(!_isMorning && _timeOfDay > morningLight && _timeOfDay < nightLight) {
			_isMorning = true;
			Messenger<bool>.Broadcast("Morning Light Time", true);
		}else if(_isMorning && _timeOfDay > nightLight) {
			_isMorning = false;
			Messenger<bool>.Broadcast("Morning Light Time", false);
		}
		
		for(int i = 0; i < sun.Length; i++)
			sun[i].Rotate(new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);
		
		if(_timeOfDay > sunRise && _timeOfDay < _noonTime) {
			AdjustLighting(true);
		}else if(_timeOfDay > _noonTime && _timeOfDay < sunSet){
			AdjustLighting(false);
		}
		
		if(_timeOfDay > sunRise && _timeOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend") < 1) {
			_tod = GameTime.TimeOfDay.SunRise;
			BlendSkybox();
		}else if(_timeOfDay > sunSet && _timeOfDay < sunRise && RenderSettings.skybox.GetFloat("_Blend") > 0) {
			_tod = GameTime.TimeOfDay.SunSet;
			BlendSkybox();
		}else{
			_tod = GameTime.TimeOfDay.Idle;
		}
			
	}
	
	private void BlendSkybox(){
		float temp = 0;
		switch(_tod){
			case TimeOfDay.SunRise:
				temp = ((_timeOfDay - sunRise) / _dayCycleInSeconds) * skyBoxBlendModifier;
				break;
			case TimeOfDay.SunSet:
				temp = ((_timeOfDay - sunSet) / _dayCycleInSeconds) * skyBoxBlendModifier;
				temp = 1 - temp;
				break;
			case TimeOfDay.Idle:
				temp = temp;
				break;
		}
		
		
		if(temp > 1)
			temp = 1 - (temp - 1);
		
		RenderSettings.skybox.SetFloat("_Blend",temp);
	}
	
	private void SetupLighting() {
		RenderSettings.ambientLight = ambLightMin;
		
		for(int i = 0; i < _sunScript.Length; i++) {
			if(_sunScript[i].giveLight){
				sun[i].GetComponent<Light>().intensity = _sunScript[i]._minLightBrightness;	
			}
		}
	}
	
	private void AdjustLighting(bool brighten) {
		float pos = 0;
		if(brighten){
			pos = (_timeOfDay - sunRise) / _morningLength;
		}else{
			pos = (sunSet - _timeOfDay) / _eveningLength;
		}
		RenderSettings.ambientLight = new Color(ambLightMax.r + ambLightMax.r* pos,ambLightMax.g + ambLightMax.g* pos,ambLightMax.b + ambLightMax.b* pos);
		
		for(int i = 0; i < _sunScript.Length; i++) {
			if(_sunScript[i].giveLight) {
				_sunScript[i].GetComponent<Light>().intensity = _sunScript[i]._maxLightBrightness * pos;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

[AddComponentMenu("Environments/Sun")]
public class Sun : MonoBehaviour {
	public float _maxLightBrightness;
	public float _minLightBrightness;
	
	public float _maxFlareBrightness;
	public float _minFlareBrightness;
	
	public bool giveLight = false;
	
	void Start() {
		if(GetComponent<Light>() != null){
			
		}
	}
	
}

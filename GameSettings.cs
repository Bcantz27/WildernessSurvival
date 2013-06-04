using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {
	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";   //This is the name of the gameobject that player will spawn at.
	private string name;
	private int mode = 0;
	
	public int Mode {
		get {return mode; }
		set{ mode = value; }
	}
	
	public string Name {
		get {return name; }
		set{ name = value; }
	}
	
	
	void Awake() {
		DontDestroyOnLoad(this);
	}
	
	public void SaveCharacterData() {
		
	}
	
	public void LoadCharacterData() {
	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {
	public enum State{
		Idle,
		Initialize,
		Setup,
		SpawnMob
	}
	
	public GameObject[] mobPrefabs; //an array to hold all of mobs we want to spawn
	public GameObject[] spawnPoints; //this array will hold a reference to all of the spawn points
	
	public State state;  // this is our local variable that holds our current state.
	
	
	void Awake() {
		state = MobGenerator.State.Initialize;	
	}
	
	// Use this for initialization
	IEnumerator Start () {
		while(true) {
			switch(state) {
			case State.Initialize:
				 Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMob:
				SpawnMob();
				break;
			}
			
			yield return 0;
		}
	
	}
	
	//make sure that everthing is initialized before we go on to the next step
	private void Initialize() {
		
		if(!CheckForMobPrefabs())
			return;
		
		if(!CheckForSpawnPoints())
			return;
		
		state = MobGenerator.State.Setup;
	}
	
	//make sure that everything is setup before continuing.
	private void Setup() {
		
		
		state = MobGenerator.State.SpawnMob;
	}
	
	//spawn a mob if we have an open spawn point.
	private void SpawnMob() {
		
		GameObject[] gos = AvailableSpawnPoints();
		
		for(int i = 0; i < gos.Length; i++) {
			GameObject go = Instantiate(mobPrefabs[Random.Range(0,mobPrefabs.Length)], gos[i].transform.position,Quaternion.identity) as GameObject;
			go.transform.parent = gos[i].transform;
			
		}
		
		state = MobGenerator.State.Idle;
	}
	
	//check to see that we have at least one mob prefab to spawn.
	private bool CheckForMobPrefabs() {
		if(mobPrefabs.Length > 0)
			return true;
		else
			return false;
	}
	
	//check to see if we have aleast one spawn point to spawn mobs.
	private bool CheckForSpawnPoints(){
		if(spawnPoints.Length > 0)
			return true;
		else
			return false;
	}
	
	//generate a list of available spawnpoints that do not have any mobs childed to it
	private GameObject[] AvailableSpawnPoints() {
		List<GameObject> gos = new List<GameObject>();
		
		for(int i = 0;i < spawnPoints.Length; i++){
			if(spawnPoints[i].transform.childCount == 0) {
				gos.Add (spawnPoints[i]);
			}
			
		}
		return gos.ToArray();
	}
}

using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;
	public GameObject gameSettings;
	public Camera mainCamera;
	
	public float zOffset;
	public float yOffset;
	public float xRotOffset;
	
	private PlayerCharacter _pcScript;
	private GameObject _pc;
	
	private Vector3 _playerSpawnPointPos;     //Stores the position of the spawn point.

	void Awake () {
		_playerSpawnPointPos = new Vector3(1078,252,1055);
		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);
		
		if(go == null){
			Debug.LogWarning("Can not find player spawn point.");
			
			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
			Debug.Log("Created player spawn point.");
			go.transform.position = _playerSpawnPointPos;
		}
		
		_pc = Instantiate(playerCharacter, go.transform.position, Quaternion.identity) as GameObject;
		_pc.name = "pc";
		
		_pcScript = _pc.GetComponent<PlayerCharacter>();
		
		zOffset = -2.5f;
		yOffset = 2.5f;
		xRotOffset = 22.5f;
		
//		mainCamera.transform.position = new Vector3(_pc.transform.position.x,_pc.transform.position.y +yOffset,_pc.transform.position.z +zOffset);
//		mainCamera.transform.Rotate(xRotOffset, 0, 0);
		
		//LoadCharacter();
	}
	
	public void LoadCharacter() {
		GameObject gs = GameObject.Find ("_GameSettings");
		if(gs == null){
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "_GameSettings";
		}
		GameSettings gsScript = GameObject.Find("_GameSettings").GetComponent<GameSettings>();
		
		gsScript.LoadCharacterData();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

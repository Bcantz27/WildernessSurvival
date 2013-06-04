using UnityEngine;
using System.Collections;

public class ServerManager : MonoBehaviour
{
    public GameObject myPlayer;

    void OnServerInitialized()
    {
		
    }
	
	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
		if(GameObject.Find("_GameSettings").GetComponent<GameSettings>().Mode == 1){
			GameObject.Instantiate(myPlayer, transform.position, transform.rotation);
		}else if(GameObject.Find("_GameSettings").GetComponent<GameSettings>().Mode == 2){
			SpawnPlayer();
		}
	}

    void OnConnectedToServer()
    {
        SpawnPlayer();
		foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
        {
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SpawnPlayer()
    {
        Network.Instantiate(myPlayer, transform.position, transform.rotation, 0);
		Debug.Log("Spawned player");
        myPlayer.name = GameObject.Find("_GameSettings").GetComponent<GameSettings>().Name;
    }
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Network.RemoveRPCs(Network.player);
        Network.DestroyPlayerObjects(Network.player);
        Application.LoadLevel(Application.loadedLevel);
    }
    
	
}

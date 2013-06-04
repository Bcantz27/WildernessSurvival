using UnityEngine;
using System.Collections;
using System;

public class Login : MonoBehaviour {
	public string connectToIp = "127.0.0.1";
    public int connectPort = 25000;
    public bool useNAT = false;
    public string ipAddress = "";
    public string port = "";
    public string playerName = "<NAME ME>";
	
	private const int USERNAME_LABEL_WIDTH =250;
	private const int USERNAME_LABEL_HEIGHT = 30;
	
	private int stage = 0;

	public GUISkin mySkin;
	
	public GameObject settings;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("_GameSettings");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.skin = mySkin;
		
		switch(stage){
		case 0:
			DisplaySinglePlayer();
			DisplayMultiPlayer();
			break;
		case 1:
			break;
		case 2:
			DisplayJoinServer();
			DisplayHostServer();
			DisplayBack();
			break;
		case 3:
			DisplayConnectServer();
			DisplayBack();
			break;
		case 4:
			DisplayStartServer();
			DisplayBack();
			break;
			
		}
		
	}
	
	private void DisplaySinglePlayer() {
		if(GUI.Button(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2,USERNAME_LABEL_WIDTH,30),"Singleplayer")){
			Application.LoadLevel(1);
			settings.GetComponent<GameSettings>().Mode = 1;
		}
	}
	private void DisplayMultiPlayer() {
		if(GUI.Button(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 + 30,USERNAME_LABEL_WIDTH,30),"Multiplayer")){
			stage = 2;
		}
	}
	
	private void DisplayJoinServer() {
		if(GUI.Button(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 + 30,USERNAME_LABEL_WIDTH,30),"Join")){
			stage = 3;
		}
	}
	
	private void DisplayHostServer() {
		if(GUI.Button(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 + 60,USERNAME_LABEL_WIDTH,30),"Host")){
			stage = 4;
		}
	}
	
	private void DisplayStartServer() {
		if(GUI.Button(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 + 60,USERNAME_LABEL_WIDTH,30),"Start Server")){
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				if (playerName != "<NAME ME>")
                {
                    //Network.useNat = useNAT;
					Application.LoadLevel(1);
                    Network.InitializeServer(32, connectPort,useNAT);
					settings.GetComponent<GameSettings>().Mode = 2;

                    foreach(GameObject go in FindObjectsOfType(typeof(GameObject)))
                    {
                        go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
                    }

                    PlayerPrefs.SetString("playerName", playerName);

                }
			}
		}
		GUI.Label(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 - 30,USERNAME_LABEL_WIDTH/2,25),"Name : ");
		GUI.Label(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2,USERNAME_LABEL_WIDTH/2,25),"IP Address : ");
		GUI.Label(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 + 30,USERNAME_LABEL_WIDTH/2,25),"Port : ");
		
		playerName = GUI.TextField(new Rect(Screen.width/2,Screen.height/2 - 30,USERNAME_LABEL_WIDTH/2,25),playerName);
		settings.GetComponent<GameSettings>().Name = playerName;
        connectToIp = GUI.TextField(new Rect(Screen.width/2,Screen.height/2,USERNAME_LABEL_WIDTH/2,25),connectToIp);
        connectPort = Convert.ToInt32(GUI.TextField(new Rect(Screen.width/2,Screen.height/2 + 30,USERNAME_LABEL_WIDTH/2,25),connectPort.ToString()));
	}
	
	private void DisplayConnectServer() {
		if(GUI.Button(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 + 60,USERNAME_LABEL_WIDTH,30),"Connect")){
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				if (playerName != "<NAME ME>")
                {
                    //Network.useNat = useNAT
                    if(Network.Connect(connectToIp, connectPort) == NetworkConnectionError.NoError){
						Application.LoadLevel(1);
						  PlayerPrefs.SetString("playerName", playerName);
					}else{
						Debug.Log("Failed to connect");
					}
                }
			}
		}
		GUI.Label(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 - 30,USERNAME_LABEL_WIDTH/2,25),"Name : ");
		GUI.Label(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2,USERNAME_LABEL_WIDTH/2,25),"IP Address : ");
		GUI.Label(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 + 30,USERNAME_LABEL_WIDTH/2,25),"Port : "); 
		
		playerName = GUI.TextField(new Rect(Screen.width/2,Screen.height/2 - 30,USERNAME_LABEL_WIDTH/2,25),playerName);
		settings.GetComponent<GameSettings>().Name = playerName;
        connectToIp = GUI.TextField(new Rect(Screen.width/2,Screen.height/2,USERNAME_LABEL_WIDTH/2,25),connectToIp);
        connectPort = Convert.ToInt32(GUI.TextField(new Rect(Screen.width/2,Screen.height/2 + 30,USERNAME_LABEL_WIDTH/2,25),connectPort.ToString()));
	}
	
	private void DisplayBack() {
		if(GUI.Button(new Rect(Screen.width/2 - USERNAME_LABEL_WIDTH/2,Screen.height/2 + 90,USERNAME_LABEL_WIDTH,30),"Back")){
			if(stage == 2){
				stage = 0;	
			}else if(stage == 3 || stage == 4){
				stage = 2;
			}
		}
	}
}

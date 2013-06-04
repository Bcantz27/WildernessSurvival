using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Movement))]
public class PlayerInput : MonoBehaviour {
	bool singleplayer;
	// Use this for initialization
	void Start () {
		if(GameObject.Find ("_GameSettings").GetComponent<GameSettings>().Mode == 1){
			singleplayer = true;
		}else{
			singleplayer = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!singleplayer && networkView.isMine){
			if(Input.GetButtonUp("Toggle Inventory")){
				Messenger.Broadcast("ToggleInventory");
			}
			
			if(Input.GetButtonUp("Toggle Character Window")){
				Messenger.Broadcast("ToggleCharacter");
			}

            if (Input.GetButtonUp("Toggle Crafting Window"))
            {
                Messenger.Broadcast("ToggleCrafting");
            }
			
			if(Input.GetButton("Move Forward")){
				if(Input.GetAxis ("Move Forward") > 0) {
					SendMessage("MoveMeForward",Movement.Forward.forward);
				}else{
					SendMessage("MoveMeForward",Movement.Forward.back);
				}
			}

			if(Input.GetAxis("Mouse ScrollWheel") > 0){
				SendMessage("ZoomIn",SendMessageOptions.DontRequireReceiver);
			}else if(Input.GetAxis("Mouse ScrollWheel") < 0){
				SendMessage("ZoomOut",SendMessageOptions.DontRequireReceiver);
			}
			
			if(Input.GetButtonUp("Move Forward")){
				SendMessage("MoveMeForward",Movement.Forward.none);
			}
			
			
			if(Input.GetButton("Rotate Player")){
				if(Input.GetAxis ("Rotate Player") > 0) {
					SendMessage("RotateMe",Movement.Turn.right);
				}else{
					SendMessage("RotateMe",Movement.Turn.left);
				}
			}
			
			if(Input.GetButtonUp("Rotate Player")){
				SendMessage("RotateMe",Movement.Turn.none);
			}
			
			
			if(Input.GetButton("Strafe")){
				if(Input.GetAxis ("Strafe") > 0) {
					SendMessage("StrafeMe",Movement.Turn.right);
				}else{
					SendMessage("StrafeMe",Movement.Turn.left);
				}
			}
			
			if(Input.GetButtonUp("Strafe")){
				SendMessage("StrafeMe",Movement.Turn.none);
			}
			
			
			if(Input.GetButtonUp("Jump")) {
				SendMessage("JumpMe");
			}
			
			if(Input.GetButtonDown("Run")) {
				SendMessage("RunMe",true);
			}
			if(Input.GetButtonUp("Run")) {
				SendMessage("RunMe",false);
			}
		}else if(singleplayer){
			if(Input.GetButtonUp("Toggle Inventory")){
				Messenger.Broadcast("ToggleInventory");
			}
			
			if(Input.GetButtonUp("Toggle Character Window")){
				Messenger.Broadcast("ToggleCharacter");
			}

            if (Input.GetButtonUp("Toggle Crafting Window"))
            {
                Messenger.Broadcast("ToggleCrafting");
            }
			
			if(Input.GetButton("Move Forward")){
				if(Input.GetAxis ("Move Forward") > 0) {
					SendMessage("MoveMeForward",Movement.Forward.forward);
				}else{
					SendMessage("MoveMeForward",Movement.Forward.back);
				}
			}
			if(Input.GetAxis("Mouse ScrollWheel") > 0){
				SendMessage("ZoomIn",SendMessageOptions.DontRequireReceiver);
			}else if(Input.GetAxis("Mouse ScrollWheel") < 0){
				SendMessage("ZoomOut",SendMessageOptions.DontRequireReceiver);
			}
			
			if(Input.GetButtonUp("Move Forward")){
				SendMessage("MoveMeForward",Movement.Forward.none);
			}
			
			
			if(Input.GetButton("Rotate Player")){
				if(Input.GetAxis ("Rotate Player") > 0) {
					SendMessage("RotateMe",Movement.Turn.right);
				}else{
					SendMessage("RotateMe",Movement.Turn.left);
				}
			}
			
			if(Input.GetButtonUp("Rotate Player")){
				SendMessage("RotateMe",Movement.Turn.none);
			}
			
			
			if(Input.GetButton("Strafe")){
				if(Input.GetAxis ("Strafe") > 0) {
					SendMessage("StrafeMe",Movement.Turn.right);
				}else{
					SendMessage("StrafeMe",Movement.Turn.left);
				}
			}
			
			if(Input.GetButtonUp("Strafe")){
				SendMessage("StrafeMe",Movement.Turn.none);
			}
			
			
			if(Input.GetButtonUp("Jump")) {
				SendMessage("JumpMe");
			}
			
			if(Input.GetButtonDown("Run")) {
				SendMessage("RunMe",true);
			}
			if(Input.GetButtonUp("Run")) {
				SendMessage("RunMe",false);
			}
		}
	}
	/*
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Water"))
			SendMessage("IsSwimming",true);
	}
	public void OnTriggerExit(Collider other) {
		if(other.CompareTag("Water"))
			SendMessage("IsSwimming",false);
	}
	*/
}

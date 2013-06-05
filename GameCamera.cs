using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	public Transform target;
	public float curZoom;
	public float height;
	public float maxZoom = 10;
	public float minZoom = 3.6f;
	
	private Transform _myTransform;
	private float _x;
	private float _y;
	private bool camButtonDown = false;
	
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	bool singleplayer;
	
	void Awake() {
		_myTransform = transform;
		curZoom = 6;
		
		if(GameObject.Find ("_GameSettings").GetComponent<GameSettings>().Mode == 1){
			singleplayer = true;
		}else{
			singleplayer = false;
		}
	}
	
	// Use this for initialization
	void Start () {
		if(networkView.isMine){
	       gameObject.GetComponent<Camera>().enabled = true;
	    }
	    else{
	       gameObject.GetComponent<Camera>().enabled = false;
	    }
	}
	
	// Update is called once per frame
	void Update () {
		if(!singleplayer && networkView.isMine){
			if(Input.GetMouseButtonDown(1)) {  //Use the input manager to make this user selectable button
				camButtonDown = true;
			}
			if(Input.GetMouseButtonUp(1)) {
				camButtonDown = false;
			}
			if(Input.GetAxis("Mouse ScrollWheel") > 0){
				ZoomIn ();
			}else if(Input.GetAxis("Mouse ScrollWheel") < 0){
				ZoomOut ();
			}
		}else if(singleplayer){
			if(Input.GetMouseButtonDown(1)) {  //Use the input manager to make this user selectable button
				camButtonDown = true;
			}
			if(Input.GetMouseButtonUp(1)) {
				camButtonDown = false;
			}
			if(Input.GetAxis("Mouse ScrollWheel") > 0){
				ZoomIn ();
			}else if(Input.GetAxis("Mouse ScrollWheel") < 0){
				ZoomOut ();
			}
		}
	}
	
	void LateUpdate() {
		
		if(target != null){
		if(!singleplayer && networkView.isMine){
			if(camButtonDown) {  //Use the input manager to make this user selectable button
				_x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
		        _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
				Debug.Log (""+Input.GetAxis("Mouse X"));
		 		
		 		//y = ClampAngle(y, yMinLimit, yMaxLimit);
		 		       
		        Quaternion rotation = Quaternion.Euler(_y, _x, 0.0f);
		        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -curZoom) + target.position;
		        
		        _myTransform.rotation = rotation;
		        _myTransform.position = position;
			}else{
				_x = 0;
				_y = 0;
				
				// Calculate the current rotation angles
				float wantedRotationAngle = target.eulerAngles.y;
				float wantedHeight = target.position.y + height;
					
				float currentRotationAngle = _myTransform.eulerAngles.y;
				float currentHeight = _myTransform.position.y;
				
				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
				// Damp the height
				currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
				// Convert the angle into a rotation
				Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
				
				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				_myTransform.position = target.position;
				_myTransform.position -= currentRotation * Vector3.forward * curZoom;
			
				// Set the height of the camera
				_myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);
				
				// Always look at the target
				_myTransform.LookAt(target);
			}
		}else if(singleplayer){
			if(camButtonDown) {  //Use the input manager to make this user selectable button
				_x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
		        _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
		 		
		 		//y = ClampAngle(y, yMinLimit, yMaxLimit);
		 		       
		        Quaternion rotation = Quaternion.Euler(_y, _x, 0.0f);
		        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -curZoom) + target.position;
		        
		        _myTransform.rotation = rotation;
		        _myTransform.position = position;
			}else{
				_x = 0;
				_y = 0;
				
				// Calculate the current rotation angles
				float wantedRotationAngle = target.eulerAngles.y;
				float wantedHeight = target.position.y + height;
					
				float currentRotationAngle = _myTransform.eulerAngles.y;
				float currentHeight = _myTransform.position.y;
				
				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
				// Damp the height
				currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
				// Convert the angle into a rotation
				Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
				
				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				_myTransform.position = target.position;
				_myTransform.position -= currentRotation * Vector3.forward * curZoom;
			
				// Set the height of the camera
				_myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);
				
				// Always look at the target
				_myTransform.LookAt(target);
			}
		}
		}else{
			/*
			GameObject go = GameObject.Find("Camera");
			if(go == null) {
				Debug.LogError("Unable to find player to attach camera too.");
				return;
			}
			
			target = go.transform;
			*/
		}
		//_myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
		//_myTransform.LookAt(target);
	}
	
	public void CameraSetup() {
		_myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - curZoom);
		_myTransform.LookAt(target);
	}
	public void ZoomIn(){
		if(curZoom > minZoom){
			curZoom -= .4f;
		}
	}
	public void ZoomOut(){
		if(curZoom < maxZoom){
			curZoom+= .4f;
		}
	}
}

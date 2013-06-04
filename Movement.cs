/// <summary>
/// Movement.
/// </summary>
/// List of Need animations
/// Player:
/// walk
/// run
/// strafe
/// fall
/// idle
/// 
/// Mob:
/// run
/// jump
/// fall
/// idle
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour {
	public enum State{
		Idle,
		Init,
		Setup,
		Run
	}
	public enum Turn{
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward {
		back = -1,
		none = 0,
		forward = 1
	}
	public float rotateSpeed = 200;
	public float strafeSpeed = 3;
	public float walkSpeed = 3;
	public float runMultiplier = 2;
	public float gravity = 20;
	public float airTime = 0;
	public float fallTime = .5f;
	public float jumpHeight = 7;
	public float jumpTime = 1.5f;
	
	private Transform _myTransform;
	private CharacterController _controller;
	private Vector3 _moveDirection;
	public CollisionFlags _collisionFlags;
	private bool _run = false;
	private bool _jump = false;
	private bool _isSwimming = false;
	
	private Turn _turn;
	private Turn _strafe;
	private Forward _forward;
	private State _state;
	
	
	void Awake() {
		_myTransform = transform;
		_controller = GetComponent<CharacterController>();
		_state = State.Init;
	}

	// Use this for initialization
	IEnumerator Start () {
		while(true) {
			switch(_state) {
			case State.Init:
				Init();
				break;
			case State.Setup:
				Setup();
				break;	
			case State.Run:
				ActionPicker();
				break;
			}
			yield return null;
		}
	}
	
	private void Setup() {
		_moveDirection = Vector3.zero;
		_turn = Movement.Turn.none;
		_forward = Movement.Forward.none;
		_strafe = Movement.Turn.none;
		_run = false;
		_jump = false;
		_state = State.Run;
	}
	
	private void Init() {
		if(!GetComponent<CharacterController>()){
			Debug.LogWarning("No Character Controller on Player");
			return;
		}
		_state = State.Setup;
	}
	
	private void ActionPicker(){
		_myTransform.Rotate(0,(int)_turn * Time.deltaTime * rotateSpeed, 0);

		if(_controller.isGrounded || _isSwimming) {
			airTime = 0;
			_moveDirection = new Vector3((int)_strafe,0,(int)_forward);
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			
			
			if(_forward != Forward.none) {
				if(_isSwimming){
					//Swim ();
				}else{
					if(_run) {
						_moveDirection *= runMultiplier;
						//Run();
					}else{
						//Walk();
					}
				}
			}
			else if(_strafe != Movement.Turn.none){
				if(_strafe == Movement.Turn.left){
					//StrafeLeft();
				}else{
					//StrafeRight();
				}
			}
			else{
				if(_isSwimming){
					//swim idle
				}else{
					//Idle();	
				}
			}
			if(_jump){
				if(airTime < jumpTime) {
					if(_run || _forward != Forward.none){
						_jump = false;
						_moveDirection.y += jumpHeight;
						//RunJump();
					}else{
						_jump = false;
						_moveDirection.y += jumpHeight;
						//Jump();
					}
					Debug.Log(""+_jump);
				}
			}
		}else{
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0) {
				airTime += Time.deltaTime;
				if(airTime > fallTime) {
					//Fall();
				}
			}
		}
		if(!_isSwimming)
			_moveDirection.y -= gravity * Time.deltaTime;
		_collisionFlags = _controller.Move(_moveDirection * Time.deltaTime);
	}
	
	public void MoveMeForward(Forward z) {
		_forward = z;
	}
	
	public void RotateMe(Turn y) {
		_turn = y;
	}
	
	public void StrafeMe(Turn x) {
		_strafe = x;
	}
	
	public void JumpMe() {
		_jump = true;
	}
	
	public void RunMe(bool r) {
		_run = r;
	}
	
	public void ToggleRun() {
		_run = !_run;
	}
	
	public void IsSwimming(bool b) {
		_isSwimming = b;
	}
}

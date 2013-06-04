using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Movement))]
[RequireComponent (typeof(SphereCollider))]
public class AI : MonoBehaviour {
	private enum State {
		Idle,
		Init,
		Setup,
		Search,
		Attack,
		Retreat,
		Flee
	}
	public float baseMeleeRange = 3.5f;
	public Transform target;
	public float perceptionRadius = 10;
	
	private Transform _myTransform;
	
	private const float ROTATION_DAMP = .3f;
	private const float FORWARD_DAMP = .9f;
	
	private Transform _home;
	private State _state;
	private bool _alive = true;
	public SphereCollider _sphereCollider;
	
	void Start(){
		_state = State.Init;
		StartCoroutine("FSM");
	}
	
	private IEnumerator FSM(){
		while(_alive){
			switch(_state){
			case State.Init:
				Init ();
				break;
			case State.Setup:
				Setup();
				break;
			case State.Search:
				Search ();
				break;
			case State.Attack:
				Attack ();
				break;
			case State.Flee:
				Flee ();
				break;
			case State.Retreat:
				Retreat();
				break;
			}
			
			yield return null;
		}
	}
	
	private void Init() {
		Debug.Log("***Init***");
		_myTransform = transform;
		_home = transform.parent.transform;
		_sphereCollider = GetComponent<SphereCollider>();
		if(_sphereCollider == null){
			Debug.LogError("There is no spherecollider on a mob");
			return;
		}
		_state = State.Setup;
	}
	
	private void Setup() {
		Debug.Log("***Setup***");
		_sphereCollider.center = GetComponent<CharacterController>().center;
		_sphereCollider.radius = perceptionRadius;
		_sphereCollider.isTrigger = true;
		
		_state = State.Search;
		_alive = false;
	}
	private void Search() {
		Move();
		_state = State.Attack;
	}
	private void Attack() {
		Move();
		_state = State.Retreat;
	}
	private void Flee(){
		Move();
		_state = State.Search;
	}
	private void Retreat(){
		Move();
		_state = State.Search;
	}
	private void Move() {
		if(target){
			Vector3 dir = (target.position - _myTransform.position).normalized;
			float direction = Vector3.Dot(dir,_myTransform.forward);
			
			float dist = Vector3.Distance(target.position, _myTransform.position);
			
			if(direction > FORWARD_DAMP && dist > baseMeleeRange){
				SendMessage("MoveMeForward",Movement.Forward.forward);
			}
			else{
				SendMessage("MoveMeForward",Movement.Forward.none);
			}
			
			dir = (target.position - _myTransform.position).normalized;
			direction = Vector3.Dot(dir,_myTransform.right);
			
			if(direction > ROTATION_DAMP) {
				SendMessage("RotateMe",Movement.Turn.right);
			}else if(direction < -ROTATION_DAMP){
				SendMessage("RotateMe",Movement.Turn.left);
			}else{
				SendMessage("RotateMe",Movement.Turn.none);
			}
		}else{
			SendMessage("RotateMe",Movement.Turn.none);
			SendMessage("MoveMeForward",Movement.Forward.none);
		}
	}
	
	public void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			target = other.transform;
			_alive = true;
			StartCoroutine("FSM");
		}
	}
	public void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			target = _home;
			_myTransform.LookAt(target);
			//_alive = false;
		}
	}
}

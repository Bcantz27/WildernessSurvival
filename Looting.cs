using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Looting : MonoBehaviour {
	public enum State {
		open,
		close,
		inbetween
	}
	
	public AudioClip openSound;
	public AudioClip closeSound;
	public State state;
	public GameObject[] parts;
	private Color[] _defaultColors;
	private float _lootDelay = .5f;
	public float _lootDistance = 2;
	public int _amountOfLoot;
	public bool inUse = false;
	
	private GameObject _player;
	private bool _used = false;
	private Transform _myTransform;
	
	public List<Item> loot = new List<Item>();
	
	public static float defaultLifeTimer = 120;
	private float _lifeTimer = 0;

	// Use this for initialization
	void Start () {
		if(_amountOfLoot == null)
			_amountOfLoot = Random.Range(1,3);
		
		state = State.close;
		
		_myTransform = transform;
		
		_defaultColors = new Color[parts.Length];
		
		if(parts.Length > 0){
			for(int cnt = 0; cnt < _defaultColors.Length;cnt++){
				//_defaultColors[cnt] = parts[cnt].renderer.material.GetColor("_col");
			}
		}
	}
	void Update(){
		_lifeTimer += Time.deltaTime;
		
		if(_lifeTimer > defaultLifeTimer && state != Looting.State.close){
			DestroyChest();
		}
		
		if(_player == null || !inUse){
			return;	
		}
		
		if(Vector3.Distance(_myTransform.position, _player.transform.position) > _lootDistance){
			ForceClose();
		}
	}
	
	public void OnMouseEnter(){
		HighLight(true);
	}
	public void OnMouseExit(){
		HighLight(false);
	}
	public void OnMouseUp() {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		if(go == null)
			return;
		
		if(Vector3.Distance(_myTransform.position,go.transform.position) > _lootDistance && !inUse)
			return;
		
		switch(state){
		case State.open:
			state = State.inbetween;
			ForceClose();
			break;
		case State.close:
            if (myGUI.loot != null)
            {
                ForceClose();
			}
			state = State.inbetween;
			StartCoroutine("Open");
			break;
		}
		
		if(state == State.close){
			Open ();
		}else{
			Close();
		}
	}
	
	private IEnumerator Open(){
       // myGUI.lootItems = this;
		
		_player = GameObject.FindGameObjectWithTag("Player");
		inUse = true;
		
		if(!_used)
			PopulateChest(_amountOfLoot);
		
		switch(gameObject.tag){
		case "Chest":
			animation.Play("open");
			audio.PlayOneShot(openSound);
			yield return new WaitForSeconds(animation["open"].length);
			break;
		case "Corpse":
			yield return new WaitForSeconds(_lootDelay);
			break;
			
		default:
			break;
		}
		
		state = State.open;
		Messenger.Broadcast("DisplayLoot");
	}
	private IEnumerator Close(){
		_player = null;
		inUse = false;
		switch(gameObject.tag){
		case "Chest":
			animation.Play("close");
			audio.PlayOneShot(closeSound);
			
			float tempTimer = animation["close"].length;
			if(closeSound.length > tempTimer)
				tempTimer = closeSound.length;
			
			yield return new WaitForSeconds(tempTimer);
			break;
		case "Corpse":
			yield return new WaitForSeconds(_lootDelay);
			break;
			
		default:
			break;
		}
		state = State.close;
		
		if(loot.Count == 0 && gameObject.tag == "Corpse")
			DestroyChest();
	}
	
	private void DestroyChest(){
		loot = null;
		Destroy(gameObject);
	}
	
	public void ForceClose(){
		Messenger.Broadcast("CloseChest");
		StopCoroutine("Open");
		StartCoroutine("Close");
	}
	
	private void PopulateChest(int x){
		for(int cnt = 0; cnt < x; cnt++){
			loot.Add(ItemGenerator.CreateRandomItem());
		}
		_used = true;
	}
	
	private void HighLight(bool glow){
		if(glow){
			if(parts.Length > 0){
				for(int cnt = 0; cnt < _defaultColors.Length;cnt++){
					parts[cnt].renderer.material.SetColor("_Color",Color.yellow);
				}
			}
		}else{
			if(parts.Length > 0){
				for(int cnt = 0; cnt < _defaultColors.Length;cnt++){
					parts[cnt].renderer.material.SetColor("_Color",_defaultColors[cnt]);
				}
			}
		}
	}
}

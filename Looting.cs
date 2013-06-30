using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(AudioSource))]
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
    private bool _populated = false;
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
        if (!gameObject.GetComponent<MobInteract>()._mob.Alive)
		    HighLight(true);
	}
	public void OnMouseExit(){
        if (!gameObject.GetComponent<MobInteract>()._mob.Alive)
		    HighLight(false);
	}
	public void OnMouseUp() {
        if (!gameObject.GetComponent<MobInteract>()._mob.Alive)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");

            if (go == null)
                return;

            if (Vector3.Distance(_myTransform.position, go.transform.position) > _lootDistance && !inUse)
                return;

            switch (state)
            {
                case State.open:
                    state = State.inbetween;
                    ForceClose();
                    break;
                case State.close:
                    if (loot != null)
                    {
                        ForceClose();
                    }
                    if (loot.Count > 0)
                    {
                        state = State.inbetween;
                        StartCoroutine("Open");

                    }
                    else
                    {
                        if (!_populated)
                        {
                            PopulateByLootTable();
                        }
                    }
                    break;
            }

            /*
            if (state == State.close)
            {
                Open();
            }
            else
            {
                Close();
            }
             */
        }
	}
	
	private IEnumerator Open(){
       // myGUI.lootItems = this;
        Debug.Log("Opened");
		_player = GameObject.FindGameObjectWithTag("Player");
		inUse = true;

        if (!_used && !_populated && gameObject.tag != "Mob")
        {
            PopulateRandom(_amountOfLoot);
        }
		
		switch(gameObject.tag){
		case "Chest":
			animation.Play("open");
			audio.PlayOneShot(openSound);
			yield return new WaitForSeconds(animation["open"].length);
			break;
		case "Mob":
            if (!_populated)
            {
                PopulateByLootTable();
            }
			yield return new WaitForSeconds(_lootDelay);
			break;
			
		default:
			break;
		}
		
		state = State.open;
        myGUI.loot = loot;
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
		case "Mob":
			yield return new WaitForSeconds(_lootDelay);
			break;
			
		default:
			break;
		}
		state = State.close;

        if (loot.Count == 0)
        {
            Debug.Log("Cleared");
            loot = new List<Item>();
            if (!gameObject.GetComponent<MobInteract>()._mob.Skinable)
                DestroyChest();

        }

	}
	
	private void DestroyChest(){
        loot = new List<Item>();
		Destroy(gameObject);
	}
	
	public void ForceClose(){
		Messenger.Broadcast("CloseChest");
		StopCoroutine("Open");
		StartCoroutine("Close");
	}

    public void Populate(int amout, Crafting.ItemName item)
    {
        Item tempItem = Crafting.getItem(item);
        if (amout > 1)
        {
            if (tempItem.Stackable)
            {
                tempItem.ItemsInStack = amout;
            }
        }
        loot.Add(tempItem);
        Debug.Log("Item: " + tempItem.Name);

        _populated = true;
    }
	
	public void PopulateRandom(int x){
		for(int cnt = 0; cnt < x; cnt++){
			loot.Add(ItemGenerator.CreateRandomItem());
		}
		_used = true;
	}

    private void PopulateByLootTable()
    {
        loot = MobGenerator.getLootOfMob((Mob.MobName)gameObject.GetComponent<MobInteract>()._mob.Id);
        _populated = true;
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

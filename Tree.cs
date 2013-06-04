using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(AudioSource))]
public class CutTree : MonoBehaviour {
    
	private bool displayTooltip = false;
	public Transform _myTransform;
    public Transform _playerPos;
    public int toolTipViewDistace = 25;
    public int chopDistance = 10;
    public GameObject treeStump;
    public List<Item> loot = new List<Item>();
    public TreeType treeType;
    public int size;
    public int lootSize;

    void Start()
    {
        size = Random.Range(1,101);
        _myTransform = transform;
        if (treeType == null)
        {
            treeType = TreeType.Oak;
        }

        if (size <= 100 && size >= 90)
        {
            _myTransform.localScale = new Vector3(5,1.5f,5);
            lootSize = 20;
        }
        else if (size >= 75)
        {
            _myTransform.localScale = new Vector3(4, 1.3f, 4);
            lootSize = 15;
        }
        else if (size >= 55)
        {
            _myTransform.localScale = new Vector3(3, 1.1f, 3);
            lootSize = 10;
        }
        else if (size >= 35)
        {
            _myTransform.localScale = new Vector3(2, 1, 2);
            lootSize = 7;
        }
        else
        {
            _myTransform.localScale = new Vector3(1, 1, 1);
            lootSize = 5;
        }
    }
	
	public void OnMouseEnter(){
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        if (Vector3.Distance(_playerPos.position, transform.position) <= toolTipViewDistace)
        {
            displayTooltip = true;
        }
	}
	
	public void OnMouseExit(){
		displayTooltip = false;
        _playerPos = null;
	}

	void OnGUI(){
        if (_playerPos != null)
        {
            if (Input.GetMouseButtonUp(0) && Vector3.Distance(_playerPos.position, _myTransform.position) <= chopDistance && displayTooltip)
            {
                GameObject.Instantiate(treeStump, new Vector3(_myTransform.position.x, _myTransform.position.y, _myTransform.position.z), new Quaternion(_myTransform.rotation.x, _myTransform.rotation.y, _myTransform.rotation.z + 90, _myTransform.rotation.w));
                GameObject.Destroy(gameObject);
                displayTooltip = false;
                CreateTreeDrops();
            }
        }
		if(displayTooltip){
			DisplayToolTip();
		}
	}

    private void CreateTreeDrops()
    {
        int amount;
        if (lootSize == null)
        {
            lootSize = 1;
        }
        amount = Random.Range(0 + lootSize, 3 + lootSize);
        myGUI.loot.Add(ItemGenerator.CreateTreeLoot(treeType,amount));
    
        Messenger.Broadcast("DisplayLoot");
    }
	
	private void DisplayToolTip(){
		if(Input.mousePosition.x <= Screen.width - 205){
			GUI.Box(new Rect(Input.mousePosition.x-Screen.width/2+390, - (Input.mousePosition.y-Screen.height/2)+260, 100, 30), "Cut " + treeType + " Tree");
		}else{
			GUI.Box(new Rect(Input.mousePosition.x-Screen.width/2+280, - (Input.mousePosition.y-Screen.height/2)+260, 100, 30), "Cut " + treeType + "  Tree");
		}
	}

    public enum TreeType
    {
       Oak,
       Birch,
       Willow,
    }
}

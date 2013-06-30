using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {

	public enum State{
		Idle,
		Initialize,
		Setup,
		SpawnMob
	}

    public static List<Mob> CurrentlySpawnedMobs;

	public Mob[] spawningMobs; //an array to hold all of mobs we want to spawn
	public Vector3[] spawnPoints; //this array will hold a reference to all of the spawn points
	
	public State state;  // this is our local variable that holds our current state.

    private void OnEnable()
    {
        Messenger<int, int,int>.AddListener("spawn mob at loc", spawnMobAtCords);
    }
    private void OnDisable()
    {
        Messenger<int, int, int>.RemoveListener("spawn mob at loc", spawnMobAtCords);
    }
	
	
	void Awake() {
		state = MobGenerator.State.Initialize;	
	}
	
	// Use this for initialization
	IEnumerator Start () {
		while(true) {
			switch(state) {
			case State.Initialize:
				 Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMob:
				SpawnMob();
				break;
			}
			
			yield return 0;
		}
	
	}
	
	//make sure that everthing is initialized before we go on to the next step
	private void Initialize() {
		
		if(!CheckForMobPrefabs())
			return;
		
		if(!CheckForSpawnPoints())
			return;
		
		state = MobGenerator.State.Setup;
	}
	
	//make sure that everything is setup before continuing.
	private void Setup() {
		
		
		state = MobGenerator.State.SpawnMob;
	}
	
	//spawn a mob if we have an open spawn point.
	private void SpawnMob() {

        Vector3[] gos = AvailableSpawnPoints();
		
		for(int i = 0; i < gos.Length; i++) {
            int temp = Random.Range(0, spawningMobs.Length);
            GameObject go = Instantiate(spawningMobs[temp].GetGameObject, gos[i], Quaternion.identity) as GameObject;
            go.transform.position = gos[i];
            go.GetComponent<MobInteract>()._mob = spawningMobs[temp];
            CurrentlySpawnedMobs.Add(spawningMobs[temp]);
		}

        spawningMobs = null;
        spawnPoints = null;
		
		state = MobGenerator.State.Idle;
	}
	
	//check to see that we have at least one mob prefab to spawn.
	private bool CheckForMobPrefabs() {
        if (spawningMobs.Length > 0)
			return true;
		else
			return false;
	}
	
	//check to see if we have aleast one spawn point to spawn mobs.
	private bool CheckForSpawnPoints(){
		if(spawnPoints.Length > 0)
			return true;
		else
			return false;
	}
	
	//generate a list of available spawnpoints that do not have any mobs childed to it
    private Vector3[] AvailableSpawnPoints()
    {
        List<Vector3> gos = new List<Vector3>();
		
		for(int i = 0;i < spawnPoints.Length; i++)
        {
				gos.Add (spawnPoints[i]);
		}

		return gos.ToArray();
	}

    public void addMobToSpawnQueAtLoc(Mob mob, Vector3 loc)
    {
        List<Mob> tempMobList = new List<Mob>();
        tempMobList.Add(mob);
        List<Vector3> tempLocList = new List<Vector3>();
        tempLocList.Add(loc);

        spawningMobs = tempMobList.ToArray();

        spawnPoints = tempLocList.ToArray();

        state = MobGenerator.State.Initialize;
        StartCoroutine("Start");
    }

    public void spawnMobAtCords(int mobId, int x, int z)
    {
        
        Vector3 temp = new Vector3(x, 0, z);
        float temp2 = Terrain.activeTerrain.SampleHeight(temp);
        temp = new Vector3(x, temp2, z);
        addMobToSpawnQueAtLoc(Mob.getMob((Mob.MobName)mobId), temp);
    }

    public static List<Item> getLootOfMob(Mob.MobName mobName)
    {
        List<Item> loot = new List<Item>();
        Hashtable possibleLoot = new Hashtable(); // Item , Percentage
        int selector = 0;
        Item tempItem;

        switch (mobName)
        {
            case Mob.MobName.Wolf:
                tempItem = Crafting.getItem(Crafting.ItemName.SmallBone);
                tempItem.ItemsInStack = Random.Range(1, 4);
                possibleLoot.Add(tempItem,30);

                tempItem = Crafting.getItem(Crafting.ItemName.RawMeat);
                tempItem.ItemsInStack = Random.Range(1, 3);
                possibleLoot.Add(tempItem, 60);

                tempItem = Crafting.getItem(Crafting.ItemName.Tooth);
                tempItem.ItemsInStack = Random.Range(1, 3);
                possibleLoot.Add(tempItem, 20);
                break;

            case Mob.MobName.Bear:
                tempItem = Crafting.getItem(Crafting.ItemName.LargeBone);
                tempItem.ItemsInStack = Random.Range(1, 4);
                possibleLoot.Add(tempItem,30);

                tempItem = Crafting.getItem(Crafting.ItemName.RawMeat);
                tempItem.ItemsInStack = Random.Range(1, 6);
                possibleLoot.Add(tempItem, 65);
                break;

            case Mob.MobName.Cougar:

                break;

            case Mob.MobName.Deer:
                tempItem = Crafting.getItem(Crafting.ItemName.Antler);
                tempItem.ItemsInStack = 1;
                possibleLoot.Add(tempItem,40);

                tempItem = Crafting.getItem(Crafting.ItemName.RawMeat);
                tempItem.ItemsInStack = Random.Range(1, 4);
                possibleLoot.Add(tempItem, 65);
                break;

            case Mob.MobName.Fox:

                break;

            case Mob.MobName.Rabbit:

                break;
        }

        foreach (DictionaryEntry pair in possibleLoot)
        {
            selector = Random.Range(0, 101);
            if (selector <= (int)pair.Value)
            {
                loot.Add((Item)pair.Key);
            }
        }


        return loot;
    }
}

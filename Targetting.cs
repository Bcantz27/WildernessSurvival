using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour {
	public List<Transform> targets;
	private Transform selectedTarget;
	
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		myTransform = transform;
		
		addAllEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)) {
			targetEnemy();
		}
	}
	
	public void addAllEnemies() {
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject enemy in go)
			addTarget(enemy.transform);
	}
	private void sortTargetsByDist() {
		targets.Sort(delegate(Transform t1, Transform t2) {
			return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
			});
	}
	
	private void targetEnemy() {
		if(targets.Count == 0){
			addAllEnemies();	
		}
		if(targets.Count > 0) {
		if(selectedTarget == null)
		{
			sortTargetsByDist();	
			selectedTarget = targets[0];
		}else{
			int index = targets.IndexOf(selectedTarget);
			if(index < targets.Count - 1) {
				index++;
			}else{
				index = 0;	
			}
			deselectTarget();
			selectedTarget = targets[index];
		}
		selectTarget();
		}
	}
	
	private void selectTarget(){
		Transform name = selectedTarget.FindChild("MobName");
		
		if(name == null) {
			Debug.Log ("Could not find the name on " + selectedTarget.name);
			return;
		}
		
		name.GetComponent<TextMesh>().text = selectedTarget.GetComponent<Mob>().Name;
		name.GetComponent<MeshRenderer>().enabled = true;
		//selectedTarget.GetComponent<Mob>().DisplayHealth();
		
		Messenger<bool>.Broadcast("show mob vitalbar", true);

	}
	private void deselectTarget(){
		selectedTarget.FindChild("MobName").GetComponent<MeshRenderer>().enabled = false;
		selectedTarget = null;
		
		Messenger<bool>.Broadcast("show mob vitalbar", false);

	}
	
	public void addTarget(Transform enemy) {
		targets.Add(enemy);	
	}
}

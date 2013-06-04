using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public GameObject target;
	public float attackTimer;
	public float coolDown;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		attackTimer = 0;
		coolDown = 2.0f;
		target = go.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(attackTimer > 0)
			attackTimer -= Time.deltaTime;
		if(attackTimer < 0)
			attackTimer = 0;
		
		if(attackTimer == 0) {
				meleeAttack();
				attackTimer = coolDown;
		}
	}
	
	private void meleeAttack() {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot (dir,transform.forward);
		
		Debug.Log (distance);
		Debug.Log (direction);
		
		if(distance < 2.5f) {
			if(direction > 0) {
				PlayerHealth ph = (PlayerHealth)target.GetComponent("PlayerHealth");
				ph.adjustCurrentHealth(-10);
			}
		}
	}
}

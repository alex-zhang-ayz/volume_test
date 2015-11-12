using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	public float radiusOfAggro = 20f;
	public float moveSpeed = 4f;
	public float damageDelayTime = 2;
	private float drainAmount;
	private bool isFollowing;
	private Rigidbody rb;
	private Vector3 playerPos;
	private bool recentDamage;
	private float startTime;

	void Start () {
		startTime = Time.time;
		recentDamage = false;
		isFollowing = false;
		rb = this.GetComponent<Rigidbody> ();
	}

	public bool getRecentDamage(){
		return recentDamage;
	}

	public void startTimer(){
		if (!recentDamage) {
			recentDamage = true;
			startTime = Time.time;
		}
	}

	void Update () {
		if (recentDamage && Time.time - startTime > damageDelayTime) {
			recentDamage = false;
		}

		isFollowing = false;
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radiusOfAggro);
		for (int i=0; i < hitColliders.Length; i++){
			if (hitColliders[i].tag == "Player"){
				isFollowing = true;
				playerPos = hitColliders[i].transform.position;
			}
		}
		if (isFollowing) {
			Vector3 dir = (new Vector3 (playerPos.x - this.transform.position.x,
			                           0,
			                           playerPos.z - this.transform.position.z)).normalized;
			rb.velocity = new Vector3 (dir.x * moveSpeed,
			                           rb.velocity.y,
			                           dir.z * moveSpeed);
		} else {
			rb.velocity = new Vector3 (Random.value * 2 - 1, rb.velocity.y, Random.value * 2 - 1);
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	
	public float moveSpeed = 10;
	public float rotateSpeed = 1;
	public float jumpStrength = 5;
	public GameObject bcObj;
	public GameObject vcObj;
	private Rigidbody rb;
	private bool grounded = false;
	private BuildControls bc;
	private VolumeControl vc;


	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		bc = bcObj.GetComponent<BuildControls> ();
		vc = vcObj.GetComponent<VolumeControl> ();
	}

	void OnCollisionEnter(Collision c){
		if (c.collider.tag == "Stage" || c.collider.tag == "Solids") {
			grounded = true;
		}
		if (c.collider.tag == "Enemy") {
			EnemyBehaviour eb = c.collider.GetComponent<EnemyBehaviour>();
			if (eb != null && !eb.getRecentDamage()){
				eb.startTimer();
				doDamage();
			}
		}
	}

	private void doDamage(){
		float damage = vc.maxHeldVolume * 0.02f;
		vc.requestVolume (damage);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			bc.rotate();
		}
		if (Input.GetKeyDown(KeyCode.Minus)){
			bc.addToNewScaling(false);
			bc.scale();
		}
		if (Input.GetKeyDown(KeyCode.Equals)){
			bc.addToNewScaling(true);
			bc.scale();
		}
		float horz = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");
		float hop = Input.GetAxis ("Jump");

		rb.velocity = new Vector3 (Camera.main.transform.forward.x * vert * moveSpeed, 
		                          this.rb.velocity.y,
		                          Camera.main.transform.forward.z * vert * moveSpeed);
		rb.velocity += new Vector3 (Camera.main.transform.right.x * horz * moveSpeed,
		                            0,
		                            Camera.main.transform.right.z * horz * moveSpeed);
		/*
		rb.velocity = new Vector3 (this.transform.right.x * horz * moveSpeed,
		                           this.rb.velocity.y,
		                           this.transform.forward.z * vert * moveSpeed);
		*/

		Vector3 hMove = rb.velocity;
		hMove.y = 0;
		float dis = hMove.magnitude * Time.fixedDeltaTime;
		hMove.Normalize();
		RaycastHit hit;
		if (rb.SweepTest (hMove, out hit, dis)) {
			if (hit.collider.tag == "Stage" || hit.collider.tag == "Solids")
				rb.velocity = new Vector3(0, rb.velocity.y, 0);
		}
		//Sol: http://forum.unity3d.com/threads/object-wont-fall-when-i-apply-horizontal-velocity-and-is-colliding-with-wall.143698/

		if (hop > 0 && grounded) {
			rb.AddForce (Vector3.up * jumpStrength, ForceMode.Impulse);
			grounded = false;
		}
	}
}

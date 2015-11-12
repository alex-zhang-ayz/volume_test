using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {
	
	public float moveSpeed = 5;
	public float rotateSpeed = 100;
	public float jumpStrength = 1;
	private Rigidbody rb;
	private bool grounded = false;
	
	
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
	}
	
	void OnCollisionEnter(Collision c){
		if (c.collider.tag == "Stage" || c.collider.tag == "Solids") {
			grounded = true;
		}
	}
	
	void Update () {
		float horz = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");
		float hop = Input.GetAxis ("Jump");
		print (Vector3.forward * vert  * moveSpeed);
		transform.Translate (Vector3.forward * vert * Time.deltaTime * moveSpeed);
		transform.Rotate (Vector3.up * horz * Time.deltaTime * rotateSpeed);
		
		if (hop > 0 && grounded) {
			rb.AddForce (Vector3.up * jumpStrength, ForceMode.Impulse);
			grounded = false;
		}
	}
}

using UnityEngine;
using System.Collections;

public class BlockadeBehaviour : MonoBehaviour {

	public Vector3 dir;
	public bool pos;
	public Material[] materials; //0: locked, 1: unlocked
	private bool finished;
	private bool canDrain;
	private Renderer r;

	void Start () {
		r = this.GetComponent<Renderer> ();
		finished = false;
		canDrain = false;
	}

	void OnTriggerExit(Collider c){
		if (finished && c.gameObject.tag == "Player") {
			bool cando = false;
			if (dir.x > 0){
				if ((pos && transform.position.x < c.transform.position.x) || 
				    (!pos && transform.position.x > c.transform.position.x)){
					cando = true;
				}
			}else if (dir.y > 0){
				if ((pos && transform.position.y < c.transform.position.y) || 
				    (!pos && transform.position.y > c.transform.position.y)){
					cando = true;
				}
			}else{
				if ((pos && transform.position.z < c.transform.position.z) || 
				    (!pos && transform.position.z > c.transform.position.z)){
					cando = true;
				}
			}

			if (cando){
				r.material = materials[0];
				this.gameObject.layer = LayerMask.NameToLayer ("Stage");
				this.gameObject.tag = "Stage";
				this.GetComponent<BoxCollider> ().isTrigger = false;
				canDrain = true;
			}
		}
	}

	public bool getCanDrain(){
		return canDrain;
	}
	/*
	void OnCollisionExit(Collision c){
		if (finished && c.collider.gameObject.tag == "Player") {
			r.material = materials [0];
			this.gameObject.layer = LayerMask.NameToLayer ("Stage");
		}
	}
*/
	void Update () {
	
	}

	public bool getFinished(){
		return finished;
	}

	public void unlock(){
		finished = true;
		r.material = materials [1];
		this.gameObject.layer = LayerMask.NameToLayer ("Blueprints");
		this.gameObject.tag = "Blueprint";
		this.GetComponent<BoxCollider> ().isTrigger = true;
	}
}

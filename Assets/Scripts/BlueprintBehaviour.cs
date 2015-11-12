using UnityEngine;
using System.Collections;

public class BlueprintBehaviour : MonoBehaviour {

	public Material[] materials; //0: base one, 1: collision
	private int mode;
	private Renderer r;
	[HideInInspector]
	public static int baseMode = 0, collideMode = 1;

	public int getMode(){
		return this.mode;
	}

	void Start () {
		if (materials.Length > 0) {
			mode = 0;
		}
		r = this.GetComponent<Renderer> ();
	}

	public bool isColliding(){
		if (mode == 1) {
			return true;
		} else {
			return false;
		}
	}

	public void resetColor(){
		r.sharedMaterial = materials [baseMode];
		mode = baseMode;

	}

	void OnCollisionEnter(Collision c){
		if (mode != collideMode) {
			r.sharedMaterial = materials[collideMode];
			mode = collideMode;
		}
	}

	void OnCollisionExit(Collision c){
		if (mode != baseMode) {
			r.sharedMaterial = materials[baseMode];
			mode = baseMode;
		}

	}

	void Update () {
	
	}
}

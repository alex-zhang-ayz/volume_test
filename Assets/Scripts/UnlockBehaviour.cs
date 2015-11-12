using UnityEngine;
using System.Collections;

public class UnlockBehaviour : MonoBehaviour {

	private bool unlocked;
	private float score;

	public float getScore(){
		return score;
	}

	void OnCollisionEnter(Collision c){
		if (c.collider.gameObject.tag == "Solids") {
			//score = VolumeCalculator.getVolume(c.collider.gameObject);
			if (!unlocked){
				score = VolumeCalculator.newGetVolume(c.collider.gameObject);
				unlocked = true;
			}
			//change material
			Destroy(c.collider.gameObject);
		}
	}

	public bool getUnlocked(){
		return unlocked;
	}

	void Start () {
		unlocked = false;
	}

	void Update () {
	
	}
}

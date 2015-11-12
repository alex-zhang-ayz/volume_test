using UnityEngine;
using System.Collections;

public class SphereBehaviour : MonoBehaviour {

	public Material enemyHitMat;
	private Renderer r;
	private bool hitAlready;

	void OnCollisionEnter(Collision c){
		if (c.collider.gameObject.tag == "Enemy" && !hitAlready) {
			float myvol = VolumeCalculator.newGetVolume(this.gameObject);
			float enevol = VolumeCalculator.newGetVolume(c.collider.gameObject);
			if (myvol >= enevol){
				hitAlready = true;
				r.material = enemyHitMat;
				float newRad = Mathf.Pow(3*(myvol + enevol) / (4*Mathf.PI), 1/3f);
				this.transform.localScale = new Vector3(newRad *2, newRad*2, newRad*2);
				Destroy(c.collider.gameObject);
			}
		}
	}

	void Start () {
		r = this.GetComponent<Renderer> ();
		hitAlready = false;
	}

	void Update () {
	
	}
}

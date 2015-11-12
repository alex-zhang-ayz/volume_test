using UnityEngine;
using System.Collections;

public class SolidBehaviour : MonoBehaviour {
	
	public float shrinkSpeed = 0.7f;
	private static Vector3 targetScaling = new Vector3 (0.1f, 0.1f, 0.1f);
	public GameObject vcObj;
	private VolumeControl vc;
	private bool startShrink = false;
	private float solidVolume = 0;
	public Vector3 ratioSub;

	// Use this for initialization
	void Start () {
		vc = vcObj.GetComponent<VolumeControl> ();
		//solidVolume = VolumeCalculator.getVolume (this.gameObject);
		solidVolume = VolumeCalculator.newGetVolume (this.gameObject);
		float max = Mathf.Max (new float[3]{this.transform.localScale.x, this.transform.localScale.y,
			this.transform.localScale.z});
		ratioSub = new Vector3 (this.transform.localScale.x / max,
		                       this.transform.localScale.y / max,
		                       this.transform.localScale.z / max);
		shrinkSpeed *= this.transform.localScale.magnitude;
	}

	public void setStartShrink(bool b){
		startShrink = b;
	}

	// Update is called once per frame
	void Update () {
		if (this.transform.position.y < -500) {
			Destroy(this.gameObject);
		}
		if (startShrink) {
			Vector3 sub = this.transform.localScale - Vector3.one * Time.deltaTime * shrinkSpeed;
			if (!(sub.x < 0 || sub.y < 0 || sub.z < 0)){
				//float prevVolume = VolumeCalculator.getVolume(this.gameObject);
				float prevVolume = VolumeCalculator.newGetVolume(this.gameObject);
				//this.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
				this.transform.localScale -= ratioSub * Time.deltaTime * shrinkSpeed;
				//float diffVolume = prevVolume - VolumeCalculator.getVolume(this.gameObject);
				float diffVolume = prevVolume - VolumeCalculator.newGetVolume(this.gameObject);
				vc.addVolume(diffVolume);
			}
		}
		if (this.transform.localScale.magnitude <= targetScaling.magnitude) {
			print (solidVolume);
			//vc.addVolume(solidVolume);
			//vc.addVolume(VolumeCalculator.getVolume(this.gameObject));
			vc.addVolume(VolumeCalculator.newGetVolume(this.gameObject));
			Destroy (this.gameObject);
		}
	}
}

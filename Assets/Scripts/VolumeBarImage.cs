using UnityEngine;
using System.Collections;

public class VolumeBarImage : MonoBehaviour {

	public GameObject vcObj;
	private VolumeControl vc;
	private float yPos;
	private RectTransform rt;

	void Start () {
		vc = vcObj.GetComponent<VolumeControl> ();
		rt = this.GetComponent<RectTransform> ();
		yPos = (-1) * rt.rect.height;
	}
	

	void Update () {
		yPos = rt.rect.height * vc.getPercentVolume () - rt.rect.height;
		if (this.transform.localPosition.y != yPos) {
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x,
			                                            yPos,
			                                            this.transform.localPosition.z);
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaxVolumeTextScript : MonoBehaviour {

	public GameObject vcObj;
	public GameObject bcObj;
	private VolumeControl vc;
	private BuildControls bc;
	private Text t;

	void Start () {
		t = this.GetComponent<Text> ();
		vc = vcObj.GetComponent<VolumeControl> ();
		bc = bcObj.GetComponent<BuildControls> ();
	}

	void Update () {
		int mode = bc.getMode ();
		if (mode > 0 && t.text != vc.getPiMaxVolume()) {
			t.text = vc.getPiMaxVolume();
		} else if (mode == 0 && t.text != vc.getStrMaxVolume()) {
			t.text = vc.getStrMaxVolume();
		}
	}
}

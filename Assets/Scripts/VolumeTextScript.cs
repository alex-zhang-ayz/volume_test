using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeTextScript : MonoBehaviour {

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
		if (mode > 0 && t.text != vc.getPiVolume ()) {
			t.text = vc.getPiVolume ();
		} else if (mode == 0 && t.text != vc.getStrVolume ()) {
			t.text = vc.getStrVolume();
		}
	}
}

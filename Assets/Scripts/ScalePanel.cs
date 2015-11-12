using UnityEngine;
using System.Collections;

public class ScalePanel : MonoBehaviour {

	public GameObject[] children;
	public GameObject bcObj;
	private BuildControls bc;
	private int mode;

	void Start () {
		bc = bcObj.GetComponent<BuildControls> ();
		mode = bc.getMode ();
		toggleChildren ();
	}

	private void toggleChildren(){
		for(int i=0;i<children.Length;i++){
			if (i == this.mode){
				children[i].SetActive(true);
			}else{
				children[i].SetActive(false);
			}
		}
	}

	void Update () {
		if (mode != bc.getMode ()) {
			mode = bc.getMode();
			toggleChildren();
		}
	}
}

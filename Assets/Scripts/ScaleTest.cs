using UnityEngine;
using System.Collections;

public class ScaleTest : MonoBehaviour {

	public Vector3 scaling = Vector3.one;
	public GameObject bcg;
	private BuildControls bc;

	void Start () {
		bc = bcg.GetComponent<BuildControls> ();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.K)){
			//bc.increaseCubeScaling(scaling.x, scaling.y, scaling.z);
			bc.increaseXRotate();
		}
		if (Input.GetKeyDown(KeyCode.L)){
			//bc.increaseCubeScaling(scaling.x, scaling.y);
			bc.increaseYRotate();
		}
		if (Input.GetKeyDown(KeyCode.Semicolon)){
			//bc.increaseCubeScaling(scaling.x);
			bc.increaseZRotate();
		}
	}
}

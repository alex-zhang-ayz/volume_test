using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BuildControls : MonoBehaviour {

	public float speed = 4;
	public GameObject[] blueprints;
	public GameObject[] solids;
	private Vector3 mousePos, targetPos;
	private float distance = 10f;
	private int mode;
	private int scaleMode;
	private GameObject currSel;
	public bool buildMode = false; 	//Change to private later
	public GameObject volumeController;
	public GameObject alertObj;
	private VolumeControl vc;
	private Vector3 newScaling;
	private Vector3 newRotate;
	private AlertText at;
	
	private bool prevTabValue = false;
	private float prevFireValue = 0;
	
	void Start () {
		scaleMode = 0;
		newScaling = Vector3.one;
		newRotate = new Vector3 (90, 0, 0);
		vc = volumeController.GetComponent<VolumeControl> ();
		at = alertObj.GetComponent<AlertText> ();
		if (blueprints.Length > 0) {
			mode = 0;
			setCurrSel (mode);
		}
	}

	public void setScaleMode(int i){
		scaleMode = i;
	}

	public void addToNewScaling(bool pos){
		switch (scaleMode) {
		case 0:	//all
			if (pos){
				increaseScaling(1,1,1);
			}else{
				increaseScaling(-1,-1,-1);
			}
			break;
		case 1: //x
			if (pos){
				increaseScaling(1,0,0);
			}else{
				increaseScaling(-1,0,0);
			}
			break;
		case 2: //y or L
			if (pos){
				increaseScaling(0,1,0);
			}else{
				increaseScaling(0,-1,0);
			}
			break;
		case 3: //z
			if (pos){
				increaseScaling(0,0,1);
			}else{
				increaseScaling(0,0,-1);
			}
			break;
		case 4: //r
			if (pos){
				increaseScaling(1,0,1);
			}else{
				increaseScaling(-1,0,-1);
			}
			break;
		}
	}

	public void increaseScaling(float x, float y, float z){
		float newX = newScaling.x + x;
		float newY = newScaling.y + y;
		float newZ = newScaling.z + z;
		if (newX <= 0) {
			newX = 1;
		}
		if (newY <= 0) {
			newY = 1;
		}
		if (newZ <= 0) {
			newZ = 1;
		}
		newScaling = new Vector3 (newX, newY, newZ);
	}

	public void increaseXRotate(){
		newRotate = new Vector3 (90, 0, 0);
	}

	public void increaseYRotate(){
		newRotate = new Vector3 (0, 90, 0);
	}

	public void increaseZRotate(){
		newRotate = new Vector3 (0, 0, 90);
	}

	public void reset(){
		scaleMode = 0;
		distance = 5f;
		if (currSel != null) {
			BlueprintBehaviour bb = currSel.GetComponent<BlueprintBehaviour>();
			if (bb != null){
				bb.resetColor();
			}
			newScaling = Vector3.one;
			currSel.transform.localScale = newScaling;
		}
	}

	public int getMode(){
		return mode;
	}

	public void setCurrSel(int mode){
		if (currSel != null) {
			Destroy(currSel.gameObject);
		}
		this.mode = mode;
		scaleMode = 0;
		newScaling = Vector3.one;
		newRotate = new Vector3 (90, 0, 0);
		currSel = GameObject.Instantiate(blueprints[mode]) as GameObject;
		currSel.name = "CurrSel";
		currSel.transform.position = new Vector3 (0, 100, 0);
	}

	private void makeSolid(){
		if (currSel != null) {
			BlueprintBehaviour bb = currSel.GetComponent<BlueprintBehaviour>();
			if (!bb.isColliding()){
				//float volume = vc.requestVolume(VolumeCalculator.getVolume(currSel));
				float volume = vc.requestVolume(VolumeCalculator.newGetVolume(currSel));
				if (bb.getMode() == BlueprintBehaviour.baseMode && volume > 0){
					GameObject newSolid = GameObject.Instantiate(solids[this.mode]) as GameObject;
					newSolid.transform.position = currSel.transform.position;
					newSolid.transform.localRotation = currSel.transform.localRotation;
					newSolid.transform.localScale = currSel.transform.localScale;
					newSolid.GetComponent<SolidBehaviour>().vcObj =  volumeController;
				}else if (volume < 0){
					at.playAlert("Not enough volume");
				}
			}
		}
	}

	public void scale(){
		if (buildMode && currSel != null) {
			if (currSel.transform.localScale != newScaling) {
				currSel.transform.localScale = newScaling;
			}
		}
	}
	public void rotate(){
		if (buildMode && currSel != null) {
			currSel.transform.Rotate (newRotate);
		}
	}


	void Update () {
		if (buildMode && currSel != null) {
			currSel.SetActive(true);
			bool tabdown = Input.GetKeyDown (KeyCode.Tab);
			if (!prevTabValue && tabdown) {
				if (mode < 2) {
					mode++;
				} else {
					mode = 0;
				}
				setCurrSel (mode);
			}
			prevTabValue = Input.GetKeyDown (KeyCode.Tab);



			float fire = Input.GetAxis("Fire1");
			bool overUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ();
			if (!overUI && prevFireValue == 0 && fire > 0){
				makeSolid();
			}
			prevFireValue = fire;

			float mouseScroll = Input.GetAxis ("Mouse ScrollWheel");
			if (mouseScroll != 0) {
				distance += mouseScroll * speed;
			}
			mousePos = Input.mousePosition;
			targetPos = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, distance));
			if (currSel != null) {
				currSel.transform.position = targetPos;
			}
			/*
			if (currSel.transform.localScale != newScaling){
				currSel.transform.localScale = newScaling;
			}
			*/
		} else if (!buildMode && currSel != null && currSel.activeSelf) {
			currSel.SetActive(false);
		}
	}
}

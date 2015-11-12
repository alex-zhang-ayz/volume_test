using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerBehaviour : MonoBehaviour {

	public GameObject[] blueprints;
	public GameObject[] solids;
	public GameObject volCon;
	public bool deleteBP = true;
	public static float spawnTime = 10;
	public bool keepSpawning = false;
	private Dictionary<string, GameObject> bptosol;
	private GameObject[] instantiatedObjs;
	private float startTime;

	void Start () {
		bptosol = new Dictionary<string, GameObject>();
		for (int i=0; i<blueprints.Length;i++){
			bptosol.Add(blueprints[i].name, solids[i]);
		}
		instantiatedObjs = new GameObject[this.transform.childCount];
		createSolid ();
		startTime = Time.time;
	}

	private void createSolid(){
		foreach (Transform child in this.transform) {
			GameObject newSolid = GameObject.Instantiate(bptosol[child.name]) as GameObject;
			newSolid.transform.position = child.position;
			newSolid.transform.localRotation = child.localRotation;
			newSolid.transform.localScale = child.localScale;
			newSolid.GetComponent<SolidBehaviour>().vcObj = volCon;
			if (deleteBP){
				child.gameObject.SetActive(false);
			}
		}
	}

	void Update () {
		if (keepSpawning && Time.time - startTime > spawnTime) {
			createSolid();
			startTime = Time.time;
		}
	}
}

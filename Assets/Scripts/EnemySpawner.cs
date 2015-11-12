using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] blueprints;
	public GameObject[] solids;
	public bool deleteBP = true;
	public static float spawnTime = 10;
	public bool keepSpawning = false;
	private Dictionary<string, GameObject> bptoEne;
	private float startTime;

	void Start () {
		bptoEne = new Dictionary<string, GameObject>();
		for (int i=0; i<blueprints.Length;i++){
			bptoEne.Add(blueprints[i].name, solids[i]);
		}
		createEnemy ();
		startTime = Time.time;
	}

	private void createEnemy(){
		foreach (Transform child in this.transform) {
			GameObject newEnemy = GameObject.Instantiate(bptoEne[child.name]) as GameObject;
			newEnemy.transform.position = child.position;
			newEnemy.transform.localRotation = child.localRotation;
			newEnemy.transform.localScale = child.localScale;
			if (deleteBP){
				child.gameObject.SetActive(false);
			}
		}

	}

	void Update () {
		if (keepSpawning && Time.time - startTime > spawnTime) {
			createEnemy();
			startTime = Time.time;
		}
	}
}

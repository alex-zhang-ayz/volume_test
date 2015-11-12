using UnityEngine;
using System.Collections;

public class DestroyControls : MonoBehaviour {

	public GameObject player;
	public bool destroyMode = false;
	public GameObject laserPrefab;
	private GameObject currLaser;
	public GameObject volumeController;

	void Start () {
	}
	

	void Update () {
		if (destroyMode) {
			if (Input.GetMouseButton(0)) {
				CastRay();
				
			}else{
				if (currLaser != null){
					Destroy(currLaser.gameObject);
				}
			}

		}
	}
	private Vector3 getPlayerPos(){
		return player.transform.position;
	}
	/*
	private Vector3 getPlayerPos(){
		return player.transform.parent.position + player.transform.localPosition;
	}
	*/
	void drawLaserToObject(GameObject g){
		Vector3 playerPos = getPlayerPos ();
		Vector3 midpoint = new Vector3 ((playerPos.x + g.transform.position.x) / 2,
		                                (playerPos.y + g.transform.position.y) / 2,
		                                (playerPos.z + g.transform.position.z) / 2);
		Vector3 diff = new Vector3 (playerPos.x - g.transform.position.x,
		                           playerPos.y - g.transform.position.y,
		                           playerPos.z - g.transform.position.z);
		float length = diff.magnitude;

		if (currLaser != null && (currLaser.transform.localScale.y != length || currLaser.transform.position != midpoint)) {
			Destroy (currLaser.gameObject);
			currLaser = GameObject.Instantiate (laserPrefab) as GameObject;
			Quaternion facing = currLaser.transform.rotation;
			currLaser.transform.position = midpoint;

			currLaser.transform.rotation = Quaternion.LookRotation(diff.normalized) * facing;
			currLaser.transform.localScale = new Vector3 (currLaser.transform.localScale.x,
			                                              length/2,
			                                              currLaser.transform.localScale.z);

		} else if (currLaser == null){
			currLaser = GameObject.Instantiate (laserPrefab) as GameObject;
			Quaternion facing = currLaser.transform.rotation;
			currLaser.transform.position = midpoint;

			currLaser.transform.rotation = Quaternion.LookRotation(diff.normalized) * facing;
			currLaser.transform.localScale = new Vector3 (currLaser.transform.localScale.x,
			                                              length/2,
			                                              currLaser.transform.localScale.z);
		}
	}

	public void removeAll(){
		if (currLaser != null) {
			Destroy (currLaser.gameObject);
		}
	}

	void CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		//if the ray hit something (collision)
		if (Physics.Raycast (ray, out hit, 100)) {
			GameObject coled = hit.collider.gameObject;
			if (coled.tag == "Solids"){
				drawLaserToObject(coled);
				coled.GetComponent<SolidBehaviour>().setStartShrink(true);


			}
			//Debug.DrawLine (ray.origin, hit.point);
			//Debug.Log ("Hit object: " + hit.collider.gameObject.name);

		}

	}
}

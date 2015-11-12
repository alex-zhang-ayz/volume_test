using UnityEngine;
using System.Collections;

public class MouseTest : MonoBehaviour {

	public float speed = 4;
	private Vector3 mousePos, targetPos;
	private float distance = 10f;
	

	// Use this for initialization
	void Start () {
		//float volume = VolumeCalculator.getVolume (this.gameObject);
		float volume = VolumeCalculator.newGetVolume (this.gameObject);
		print (volume);
	}


	// Update is called once per frame
	void Update () {
		float mouseScroll = Input.GetAxis ("Mouse ScrollWheel");
		if (mouseScroll != 0) {
			distance += mouseScroll * speed;
		}
		mousePos = Input.mousePosition;
		targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance));
		this.transform.position = targetPos;

	}
}

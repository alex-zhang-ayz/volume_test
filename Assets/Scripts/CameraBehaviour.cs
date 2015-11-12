using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraBehaviour : MonoBehaviour {

	public GameObject player;

	public float turnSpeed = 2.5f;
	
	public float height = 3f;
	public float distance = 7f;
	private float length = 10f;
	private Vector3 offsetX;
	private string[] allowedObjs;

	void Start () {
		allowedObjs = new string[5]{"Player", "Ceiling", "Blueprint", "Solids", "Enemy"};
		offsetX = new Vector3(0, height,  (-1) * distance);
		length = Mathf.Sqrt (height * height + distance * distance);
	}

	void Update () {
		offsetX = Quaternion.AngleAxis (Input.GetAxis("Horizontal") * turnSpeed, Vector3.up) * offsetX;
		transform.position = player.transform.position + offsetX; 
		transform.LookAt(player.transform.position);
		Ray r = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (r, out hit, 100)) {
			if (Array.IndexOf(allowedObjs, hit.collider.gameObject.tag) < 0){
				//Vector3 closestPoint = hit.collider.ClosestPointOnBounds(player.transform.position);
				Vector3 closestPoint = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
				Vector3 midpoint = new Vector3 ( (player.transform.position.x + closestPoint.x)/2,
				                                (player.transform.position.y + closestPoint.y)/2,
				                                (player.transform.position.z + closestPoint.z)/2);
				float d = Vector3.Distance(player.transform.position, closestPoint) / 2;
				float newH = Mathf.Sqrt(length * length - d * d);
				Vector3 ffsetX = midpoint + new Vector3(0, newH, 0);
				//Vector3 ffsetX = player.transform.position + new Vector3(0, newH, (-1) * d);

				transform.position = ffsetX; 
				transform.LookAt(player.transform.position);
				Debug.DrawLine(player.transform.position, ffsetX);
				Debug.DrawLine(player.transform.position, closestPoint);
			}
		}
	}
}

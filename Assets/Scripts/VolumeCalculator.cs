﻿using UnityEngine;
using System.Collections;

public class VolumeCalculator : MonoBehaviour {

	//Volume of Mesh: http://answers.unity3d.com/questions/52664/how-would-one-calculate-a-3d-mesh-volume-in-unity.html

	public static float getVolume(GameObject g){
		MeshFilter mesh = g.GetComponent<MeshFilter>(); 
		if (mesh == null) {
			return -1f;
		}
		return VolumeOfMesh(mesh); 
	}

	public static float newGetVolume(GameObject g){
		if (g.name.Contains ("Cube")) {
			return g.transform.localScale.x * g.transform.localScale.y * g.transform.localScale.z;
		} else if (g.name.Contains ("Sphere")) {
			return 4 * Mathf.PI * Mathf.Pow(g.transform.localScale.x / 2, 3) / 3;
		} else {
			return Mathf.PI * Mathf.Pow(g.transform.localScale.x / 2, 2) * (g.transform.localScale.y /2); 
		}
	}

	void Awake(){
		DontDestroyOnLoad (this);
	}

	public static float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3) {
		var v321 = p3.x*p2.y*p1.z;
		var v231 = p2.x*p3.y*p1.z;
		var v312 = p3.x*p1.y*p2.z;
		var v132 = p1.x*p3.y*p2.z;
		var v213 = p2.x*p1.y*p3.z;
		var v123 = p1.x*p2.y*p3.z;
		return (1.0f/6.0f)*(-v321 + v231 + v312 - v132 - v213 + v123);
	}
	public static float VolumeOfMesh(MeshFilter meshFilter)
	{
		float volume = 0;
		Mesh mesh = meshFilter.sharedMesh;
		Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;
		
		for (int i = 0; i < mesh.triangles.Length; i += 3)
		{
			Vector3 p1 = vertices[triangles[i + 0]];
			Vector3 p2 = vertices[triangles[i + 1]];
			Vector3 p3 = vertices[triangles[i + 2]];
			volume += SignedVolumeOfTriangle(p1, p2, p3);
		}
		volume *= meshFilter.gameObject.transform.localScale.x * 
			meshFilter.gameObject.transform.localScale.y *
				meshFilter.gameObject.transform.localScale.z;
		
		return Mathf.Abs(volume);
	}
}

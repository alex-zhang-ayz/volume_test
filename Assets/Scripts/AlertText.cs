using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertText : MonoBehaviour {

	private bool play;
	private float startTime;
	private static float playTime = 1.5f;
	private Text t;

	public void playAlert(string s){
		play = true;
		t.text = s;
		startTime = Time.time;
	}

	void Start () {
		play = false;
		startTime = Time.time;
		t = this.GetComponent<Text>();
	}

	void Update () {
		if (play && Time.time - startTime > playTime) {
			t.text = "";
			play = false;
		}
	}
}

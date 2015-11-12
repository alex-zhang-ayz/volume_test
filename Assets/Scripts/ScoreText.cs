using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	public GameObject scoreController;
	private ScoreController sc;
	private Text t;

	void Start () {
		sc = scoreController.GetComponent<ScoreController> ();
		t = this.GetComponent<Text> ();
	}

	void Update () {
		if (t.text != sc.getScore ().ToString ("F2")) {
			t.text = sc.getScore().ToString("F2");
		}
	}
}

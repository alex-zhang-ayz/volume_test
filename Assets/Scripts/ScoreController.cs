using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	private float playerScore;
	
	void Start () {
		playerScore = 0;
	}

	void Update () {

	}

	public void addScore(float f){
		playerScore += f;
	}

	public float getScore(){
		return playerScore;
	}
}

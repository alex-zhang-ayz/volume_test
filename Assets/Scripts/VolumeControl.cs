using UnityEngine;
using System.Collections;

public class VolumeControl : MonoBehaviour {

	public float maxHeldVolume;
	private float totalVolume = 0;


	void Start () {
	
	}

	void Update () {
	
	}

	public void drain(){
		totalVolume = 0;
	}

	public string getStrMaxVolume(){
		return maxHeldVolume.ToString ();
	}

	public string getPiMaxVolume(){
		float pivolume = maxHeldVolume / Mathf.PI;
		string sPV = pivolume.ToString ("F1");
		return sPV + "π";
	}

	public float getPercentVolume(){
		return totalVolume / maxHeldVolume;
	}

	public string getStrVolume(){
		return totalVolume.ToString ("F1");
	}

	public string getPiVolume(){
		float pivolume = totalVolume / Mathf.PI;
		string sPV = pivolume.ToString ("F1");
		return sPV + "π";
	}

	public float getVolume(){
		return totalVolume;
	}
	
	public float requestVolume(float f){
		float ret;
		if (totalVolume - f < 0) {
			ret = -1;
		} else {
			totalVolume -= f;
			ret = f;
		}
		return ret;
	}


	public float addVolume (float f){ 
		float ret;
		if (totalVolume + f > maxHeldVolume) {
			float diff = (totalVolume + f) - maxHeldVolume;
			totalVolume = maxHeldVolume;
			ret = diff;
		} else {
			totalVolume += f;
			ret = -1;
		}
		return ret;
	}
}

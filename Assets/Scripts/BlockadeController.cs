using UnityEngine;
using System.Collections;

public class BlockadeController : MonoBehaviour {

	public string blockadeName;
	public string unlockName;
	public GameObject scoreObj;
	public GameObject volumeControlObj;
	public float newMaxVolume;
	private ScoreController sc;
	private GameObject blockade;
	private GameObject unlock;
	private UnlockBehaviour ub;
	private BlockadeBehaviour blb;
	private VolumeControl vc;
	private bool doOnce = true;

	void Start () {
		sc = scoreObj.GetComponent<ScoreController> ();
		vc = volumeControlObj.GetComponent<VolumeControl> ();
		foreach (Transform child in this.transform) {
			if (child.name == blockadeName){
				blockade = child.gameObject;
			}else if(child.name == unlockName){
				unlock = child.gameObject;
			}
		}
		ub = unlock.GetComponent<UnlockBehaviour> ();
		blb = blockade.GetComponent<BlockadeBehaviour> ();
	}

	void Update () {
		if (ub != null && blb != null && !blb.getFinished () && ub.getUnlocked ()) {
			sc.addScore(ub.getScore());
			blb.unlock();
		}
		if (doOnce && blb.getCanDrain()){
			doOnce = false;
			vc.drain();
			vc.maxHeldVolume = newMaxVolume;
		}
	}
}

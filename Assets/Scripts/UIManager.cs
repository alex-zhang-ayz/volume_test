using UnityEngine;
using System.Collections;
using System;

public class UIManager : MonoBehaviour {

	public GameObject[] panels; //0: destroy, 1: build, 2: hotkey, 3: rotate, 4: scale

	public void togglePanel(int i){
		panels[i].SetActive(!panels[i].activeSelf);
	}

	public void hideAllOthers(int panelnum){
		int[] mainPanels = new int[2] {0,1};
		for (int i=0; i< panels.Length; i++) {
			if (i != panelnum && Array.IndexOf(mainPanels, i) < 0){
				panels[i].SetActive(false);
			}
		}
	}


	public void showOnePanel(int panelnum){
		for (int i=0; i< panels.Length; i++) {
			if (i == panelnum){
				panels[i].SetActive(true);
			}else{
				panels[i].SetActive(false);
			}
		}
	}

	void Start () {
		for (int i=0; i< panels.Length; i++) {
			panels[i].SetActive(true);
		}
		showOnePanel (0);
		togglePanel (2);
	}

	void Update () {
	
	}
}

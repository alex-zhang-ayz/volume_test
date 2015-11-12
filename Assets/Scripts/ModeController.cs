using UnityEngine;
using System.Collections;

public class ModeController : MonoBehaviour {

	public Texture2D buildCursor;
	public Texture2D destroyCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public GameObject bmObj, dmObj, uiMObj;
	private BuildControls bc;
	private DestroyControls dc;
	private UIManager uim;
	private int mode = 1; //0: build, 1: destroy
	private float prevChangeMode = 1;

	void Start () {
		uim = uiMObj.GetComponent<UIManager> ();
		bc = bmObj.GetComponent<BuildControls> ();
		dc = dmObj.GetComponent<DestroyControls> ();
		changeMode (mode);
	}
	

	void Update () {
		float cmF = Input.GetAxis ("ChangeMode");
		if (cmF> 0 && prevChangeMode == 0) {
			if (mode == 0){
				uim.showOnePanel(0);
				changeMode(1);
			}else{
				uim.showOnePanel(1);
				changeMode(0);
			}
		}
		prevChangeMode = cmF;
	}

	public void changeMode(int i){
		switch (i) {
		case 0:
			bc.buildMode = true;
			dc.destroyMode = false;
			dc.removeAll();
			bc.reset();
			hotSpot = new Vector2 (buildCursor.width/2, buildCursor.height/2);
			Cursor.SetCursor(buildCursor, hotSpot, cursorMode);
			mode = 0;
			break;
		case 1:
			bc.buildMode = false;
			dc.destroyMode = true;
			hotSpot = new Vector2 (destroyCursor.width/2, destroyCursor.height/2);
			Cursor.SetCursor(destroyCursor, hotSpot, cursorMode);
			mode = 1;
			break;
		}
	}
}

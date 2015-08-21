using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_SelectMenu : MonoBehaviour {
	private int chosenChar;
	GameObject selected;
	Transform[] charTr;

	void Start(){
		selected = GameObject.Find ("Selected");
		SelectOff ();

		charTr = new Transform[8];
		GameObject chSel = GameObject.Find ("Character Select");
		for (int loop = 0; loop < 8; loop++) {
			charTr[loop] = chSel.transform.GetChild(loop);
		}
	}

	public void OnStartButtonClick(){
		if (chosenChar != -1) {
			GameManager.charChoice = chosenChar;
			Application.LoadLevel ("ScPlay");
		}
	}

	public void OnBackButtonClick(){
		Application.LoadLevel ("ScMenu");
	}
	
	public void OnCharClick(int index){
		selected.SetActive (true);
		selected.transform.position = charTr[index].position;
		chosenChar = index;
	}

	public void SelectOff(){
		selected.SetActive (false);
		chosenChar = -1;
	}
}

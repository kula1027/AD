using UnityEngine;
using System.Collections;

public class UI_SelectMenu : MonoBehaviour {

	public UI_Charics Charics;

	public void OnSelectButtonClick(){
		Application.LoadLevel ("ScPlay");
	}
	
	public void OnRightButtonClick(){
		Charics.Right();
	}
	
	public void OnLeftButtonClick(){
		Charics.Left();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel ("ScMenu");
		}
	}
}

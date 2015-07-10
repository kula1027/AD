using UnityEngine;
using System.Collections;

public class UI_StartMenu : MonoBehaviour {
	public void OnStartButtonClick(string sceneName){
		Application.LoadLevel (sceneName);
	}

	public void OnLoadButtonClick(){

	}

	public void OnOptionButtonClick(){
		
	}

	public void OnExitButtonClick(){
		Application.Quit ();
	}
}

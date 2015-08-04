using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_SelectMenu : MonoBehaviour {

	public Image CharicsImg;
	public Sprite[] sprites;
	private int charicNum = 0;

	public void OnStartButtonClick(){
		Application.LoadLevel ("ScPlay");
	}

	public void OnBackButtonClick(){
		Application.LoadLevel ("ScMenu");
	}
	
	public void OnCharictorClick(int num){
		CharicsImg.sprite = sprites[num];
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel ("ScMenu");
		}
	}
}

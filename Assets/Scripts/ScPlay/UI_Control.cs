using UnityEngine;
using System.Collections;

public class UI_Control : MonoBehaviour {
	private GameObject slot_drink;
	private GameObject slot_item;
	private GameObject slot_skill;
	private GameObject slot_status;
	public GameManager gameManager;

	private GameObject panel_menu;

	void Start(){
		SlotButtonInit ();
		InGameMenuInit ();
	}


	#region In Game Menu Buttons
	private void InGameMenuInit (){
		panel_menu = GameObject.Find ("Panel_Menu");
		panel_menu.SetActive (false);
	}

	public void onButtonMenuClicked(){
		panel_menu.SetActive (true);
	}

	public void onButtonContinueClicked(){
		panel_menu.SetActive (false);
	}

	public void onButtonOptionClicked(){
		//todo
	}

	public void onButtonToMainClicked(){
		//todo
	}

	public void onButtonSaveExitClicked(){
		//saving method
		Application.Quit ();
	}

	#endregion

	#region Input Buttons
	public void onButtonWClicked(){
<<<<<<< HEAD
		gameManager.PlayerMove(MoveFlag.LEFT);
	}

	public void onButtonEClicked(){
		gameManager.PlayerMove(MoveFlag.RIGHT);
	}

	public void onButtonNClicked(){
		gameManager.PlayerMove(MoveFlag.UP);
	}

	public void onButtonSClicked(){
		gameManager.PlayerMove(MoveFlag.DOWN);
	}

	public void onButtonSEClicked(){
		gameManager.PlayerMove(MoveFlag.RIGHTDOWN);
	}

	public void onButtonSWClicked(){
		gameManager.PlayerMove(MoveFlag.LEFTDOWN);
	}

	public void onButtonNWClicked(){
		gameManager.PlayerMove(MoveFlag.LEFTUP);
	}

	public void onButtonNEClicked(){
		gameManager.PlayerMove(MoveFlag.RIGHTUP);
=======
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.LEFT)) {
			Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.LEFT);
		} else {
			Debug.Log ("move!");
			gameManager.PlayerMove (Direction.LEFT);
		}
	}

	public void onButtonEClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.RIGHT)) {
			Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.RIGHT);
		} else {
			Debug.Log ("move!");
			gameManager.PlayerMove (Direction.RIGHT);
		}
	}

	public void onButtonNClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.UP)) {
			Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.UP);
		} else {
			Debug.Log ("move!");
			gameManager.PlayerMove (Direction.UP);
		}
	}

	public void onButtonSClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.DOWN)) {
			Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.DOWN);
		} else {
			Debug.Log ("move!");
			gameManager.PlayerMove (Direction.DOWN);
		}
	}

	public void onButtonSEClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.RIGHTDOWN)) {
			Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.RIGHTDOWN);
		} else {
			Debug.Log ("move!");
			gameManager.PlayerMove (Direction.RIGHTDOWN);
		}
	}

	public void onButtonSWClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.LEFTDOWN)) {
			Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.LEFTDOWN);
		} else {
			Debug.Log ("move!");
			gameManager.PlayerMove (Direction.LEFTDOWN);
		}
	}

	public void onButtonNWClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.LEFTUP)) {
			Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.LEFTUP);
		} else {
			Debug.Log ("move!");
			gameManager.PlayerMove (Direction.LEFTUP);
		}
	}

	public void onButtonNEClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.RIGHTUP)) {
			Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.RIGHTUP);
		} else {
			Debug.Log ("move!");
			gameManager.PlayerMove (Direction.RIGHTUP);
		}
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
	}

	#endregion


	#region Slot Buttons
	private void SlotButtonInit(){
		slot_drink = GameObject.Find ("Slot_Drink");
		slot_item = GameObject.Find ("Slot_Item");
		slot_skill = GameObject.Find ("Slot_Skill");
		slot_status = GameObject.Find ("Slot_Status");
		
		slot_item.SetActive (false);
		slot_skill.SetActive (false);
		slot_status.SetActive (false); 
	}

	public void onDrinkButtonClicked(){
		slot_drink.SetActive (true);
		slot_item.SetActive (false);
		slot_skill.SetActive (false);
		slot_status.SetActive (false);
	}

	public void onItemButtonClicked(){
		slot_drink.SetActive (false);
		slot_item.SetActive (true);
		slot_skill.SetActive (false);
		slot_status.SetActive (false);
	}

	public void onSkillButtonClicked(){
		slot_drink.SetActive (false);
		slot_item.SetActive (false);
		slot_skill.SetActive (true);
		slot_status.SetActive (false);
	}

	public void onStatusButtonClicked(){
		slot_drink.SetActive (false);
		slot_item.SetActive (false);
		slot_skill.SetActive (false);
		slot_status.SetActive (true);
	}
	#endregion
}

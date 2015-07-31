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

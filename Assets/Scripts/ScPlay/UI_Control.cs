using UnityEngine;
using System.Collections;

public class UI_Control : MonoBehaviour {
	GameObject slot_drink;
	GameObject slot_item;
	GameObject slot_skill;
	GameObject slot_status;

	void Start(){
		SlotButtonInit ();
	}





	/// <summary>
	/// Slot Button Manager	/////////////// </summary>
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
	/////////////////////////////////////////////////////
}

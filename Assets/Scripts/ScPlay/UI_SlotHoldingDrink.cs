using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_SlotHoldingDrink : UI_Slot {

	private Text[] countText;
	public Text GetText(int index){
		return countText [index];
	}


	public void Init_Drink(){
		Init ();

		countText = new Text[slotSize];
		for (int loop = 0; loop < slotSize; loop++) {
			countText[loop] = slotItem[loop].transform.GetChild(0).GetComponent<Text>();
		}
	}

	public new void AddToSlot(int code_, Sprite sprite_, int index){
		if (slotItem [index].IsFilled) {
			if(countText[index].text == "")
				countText[index].text = "2";
			else{
				countText[index].text = (System.Convert.ToInt32(countText[index].text) + 1).ToString();
			}
		} else {
			slotItem [index].IsFilled = true;
			slotItem [index].Code = code_;
			slotItem [index].SetSprite (sprite_);
		}
	}

	public void RemoveDrink(int howMany){
		if (selected_index == -1)
			return;

		if (howMany > player.GetDrink (selected_index).GetCount ()) {
			howMany = player.GetDrink (selected_index).GetCount();
		}
		if (selected.activeSelf) {
			player.GetDrink(selected_index).SetCount(player.GetDrink(selected_index).GetCount() - howMany);
			if(player.GetDrink(selected_index).GetCount() > 1)
				countText[selected_index].text = player.GetDrink(selected_index).GetCount().ToString();
			else if(player.GetDrink(selected_index).GetCount() == 0){
				slotItem [selected_index].IsFilled = false;
				slotItem [selected_index].SetSprite (Resources.Load<Sprite> ("UI/trans"));
				countText[selected_index].text = "";
				player.RemoveDrink(selected_index);
				SelectOff();
			}
			else if(player.GetDrink(selected_index).GetCount() == 1){
				countText[selected_index].text = "";
			}
		}
	}

	public void UseDrink(){
		if (selected_index == -1)
			return;

		if (selected.activeSelf) {
			if(player.GetDrink(selected_index).GetCount() == 1){
				slotItem [selected_index].IsFilled = false;
				slotItem [selected_index].SetSprite (Resources.Load<Sprite> ("UI/trans"));
				countText[selected_index].text = "";
				player.RemoveDrink(selected_index);
				SelectOff ();
			}else if(player.GetDrink(selected_index).GetCount() > 1){
				player.GetDrink(selected_index).SetCount(player.GetDrink(selected_index).GetCount() - 1);
				if(player.GetDrink(selected_index).GetCount() > 1)
					countText[selected_index].text = player.GetDrink(selected_index).GetCount().ToString();
				else{
					countText[selected_index].text = "";
				}
			}
		}
	}

	public void OnJunkBtnClicked(){
		RemoveDrink (1);
	}
	
	public void OnUseBtnClicked(){
		UseDrink ();
	}
}

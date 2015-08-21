using UnityEngine;
using System.Collections;
using System;

public class UI_SlotHoldingItem :UI_Slot {

	public void Init_Item(){
		Init ();
	}

	public void RemoveItem(){
		if (selected.activeSelf) {
			slotItem [selected_index].IsFilled = false;
			slotItem [selected_index].SetSprite (Resources.Load<Sprite> ("UI/trans"));
			player.DropItem(selected_index);
			player.RemoveItem(selected_index);
			SelectOff ();
		}
	}

	private void EquipItem(){
		if (selected_index < 0)
			return;

		if(player.GetItem(selected_index).GetItemCode() < 20){ //무기일 경우
			SwapItemUI(slotItem [selected_index].transform, slotItem[0].transform);
			player.SwapItem(0, selected_index);
			UI_Control.AddLog(player.GetItem(0).GetName() + "을(를) 장착 하였다.");
			player.equipWeapon = (Weapon)player.GetItem(0);
			SelectOff();
		}

		ui_control.gameManager.HoldTurn ();
	}
	
	public void OnJunkBtnClicked(){
		RemoveItem ();
	}

	public void OnEquipBtnClicked(){
		EquipItem ();
	}
}

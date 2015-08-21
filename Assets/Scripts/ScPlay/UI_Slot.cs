using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour {
	protected UI_Control ui_control;
	protected Player player;

	protected readonly int slotSize = Config.inventoryCol * Config.inventoryRow;
	protected UI_SlotItem[] slotItem;

	protected GameObject selected;
	protected int selected_index;
	public int GetSelectedIndex(){return selected_index;}

	protected float refitDis;

	public void Init(){
		ui_control = GameObject.Find ("UI Manager").GetComponent<UI_Control>();

		slotItem = new UI_SlotItem[slotSize];
		for (int loop = 0; loop < slotSize; loop++) {
			slotItem[loop] = transform.GetChild(loop + 1).gameObject.GetComponent<UI_SlotItem>();
			slotItem[loop].UI_SlotItem_Start(GetComponent<UI_Slot>(), loop);
		}

		selected = transform.GetChild (0).gameObject;
		selected.SetActive (false);
		selected_index = -1;
		player = ui_control.GetPlayer ();

		refitDis = Mathf.Pow(ui_control.BottomSlotLength * 0.12f ,2);
	}

	public void OnEndDrag(Transform tr){
		if(GetComponent<UI_SlotHoldingItem>() != null)
			DragSwapItem (tr);
		if (GetComponent<UI_SlotHoldingDrink> () != null)
			DragSwapDrink (tr);

		tr.position = tr.GetComponent<UI_SlotItem>().OriPosition;
	}

	public void AddToSlot(int code_, Sprite sprite_, int index){
		slotItem[index].IsFilled = true;
		slotItem[index].Code = code_;
		slotItem[index].SetSprite(sprite_);
	}

	private void DragSwapItem(Transform tr){
		int curIndex = tr.GetComponent<UI_SlotItem> ().Index;

		for (int loop = 0; loop < slotSize; loop++) {
			if(slotItem[loop].isGold || loop == curIndex)continue;
			if((loop == 1 || loop == 2 || loop == 3 || loop == 4) && player.GetItem(curIndex).GetItemCode() < 20)continue;
			if (Vector3.SqrMagnitude (tr.position - slotItem[loop].transform.position) < refitDis) {
				SwapItemUI(tr, slotItem[loop].transform);
				if(loop == 0 && player.GetItem(curIndex).GetItemCode() < 20){
					UI_Control.AddLog(player.GetItem(curIndex).GetName() + "을(를) 장착 하였다.");
					player.equipWeapon = (Weapon)player.GetItem(curIndex);
					ui_control.gameManager.HoldTurn ();
				}
				player.SwapItem(loop, curIndex);

				SelectOff();
				break;
			}
		}
	}

	private void DragSwapDrink(Transform tr){
		int curIndex = tr.GetComponent<UI_SlotItem> ().Index;
		
		for (int loop = 0; loop < slotSize; loop++) {
			if (slotItem [loop].isGold || loop == curIndex)continue;
			if(loop < 5 && slotItem[loop].IsFilled){continue;}
			if (Vector3.SqrMagnitude (tr.position - slotItem [loop].transform.position) < refitDis) {
				SwapItemUI (tr, slotItem [loop].transform);
				SwapText (curIndex, loop);
				player.SwapDrink (curIndex, loop);
				SelectOff ();
				break;
			}
		}
	}
	private void SwapText(int idx, int idx2){
		Text text1 = GetComponent<UI_SlotHoldingDrink> ().GetText (idx);
		Text text2 = GetComponent<UI_SlotHoldingDrink> ().GetText (idx2);
		string temp = text1.text;
		text1.text = text2.text;
		text2.text = temp;
	}
	
	protected void SwapItemUI(Transform item, Transform item1){
		Sprite tempSpr = item.GetComponent<Image> ().sprite;
		item.GetComponent<Image> ().sprite = item1.GetComponent<Image> ().sprite;
		item1.GetComponent<Image> ().sprite = tempSpr;

		if (item1.GetComponent<UI_SlotItem> ().IsFilled == false) {
			item.GetComponent<UI_SlotItem> ().IsFilled = false;
			item1.GetComponent<UI_SlotItem> ().IsFilled = true;
		}
	}
	
	public void OnItemClick(int btnNum){
		if(ui_control.IsAtTop == false && slotItem[btnNum].IsFilled == true)
			Debug.Log ("Use Item " + btnNum);
	}

	public void SelectOn(int index){
		if (!selected.activeSelf) {
			selected.SetActive (true);
		}
		selected.transform.position = slotItem [index].gameObject.transform.position;
		selected_index = index;
	}

	public void SelectOff(){
		selected.SetActive (false);
		selected_index = -1;
	}

	#region Getter Setters
	public UI_SlotItem[] SlotItem {
		get {
			return slotItem;
		}
		set {
			slotItem = value;
		}
	}


	#endregion
}

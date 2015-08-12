using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour {
	private const int slotSize = 7;
	private int totalItemNum;
	private GameObject[] slotItem;

	void Start () {
		slotItem = new GameObject[slotSize];

		for (int loop = 0; loop < slotSize; loop++) {
			slotItem[loop] = transform.GetChild(loop).gameObject;
			slotItem[loop].SetActive(false);
			slotItem[loop].GetComponent<UI_SlotItem>().Index = loop;
			slotItem[loop].GetComponent<UI_SlotItem>().IsFilled = false;
		}
		totalItemNum = 0;

		PutItem (); PutItem ();
		PutItem (); PutItem ();
	}
	
	public void PutItem(){
		for (int loop = 0; loop < slotSize; loop++) {
			if(!slotItem[loop].GetComponent<UI_SlotItem>().IsFilled){
				slotItem [loop].SetActive(true);
				slotItem[loop].GetComponent<UI_SlotItem>().IsFilled = true;
				break;
			}
		}
	}

	public void PullItem(){
		if (totalItemNum > 0) {
			slotItem [totalItemNum--].SetActive (false);
		}
	}

	public void OnEndDrag(Transform tr){
		for (int loop = 0; loop < slotSize; loop++) {
			if(loop == tr.GetComponent<UI_SlotItem>().Index)continue;
			if (Vector3.SqrMagnitude (tr.position - slotItem[loop].transform.position) < 3000) {
				SwapItem(tr, slotItem[loop].transform);
				break;
			}
		}
		
		tr.position = tr.GetComponent<UI_SlotItem>().OriPosition;
	}

	private void SwapItem(Transform item, Transform item1){
		Sprite tempSpr = item.GetComponent<Image> ().sprite;
		item.GetComponent<Image> ().sprite = item1.GetComponent<Image> ().sprite;
		item1.GetComponent<Image> ().sprite = tempSpr;
	
		if (item1.gameObject.activeSelf == false) {
			item.GetComponent<UI_SlotItem> ().IsFilled = false;
			item1.GetComponent<UI_SlotItem> ().IsFilled = true;

			item1.gameObject.SetActive (true);
			item.gameObject.SetActive (false);
		}
	}

		
	public void RefreshOriPositions(){
		for (int loop = 0; loop < slotSize; loop++) {
			slotItem[loop].GetComponent<UI_SlotItem>().RefreshOriPos();
		}
	}

	public GameObject[] SlotItem {
		get {
			return slotItem;
		}
		set {
			slotItem = value;
		}
	}

	public int SlotSize {
		get {
			return slotSize;
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DragnDrop : MonoBehaviour, IDragHandler, IEndDragHandler {
	private Transform itemBeingDragged;
	private float startY;
	private float paramY;
	private bool corouLock;
	private bool isAtTop;

	private UI_Slot drink_slot;
	private UI_Control ui_control;

	private GameObject[] sideBtn;

	void Start(){
		itemBeingDragged = GameObject.Find ("Panel_Slot").transform;
		startY = itemBeingDragged.position.y;

		paramY = GameObject.Find ("ParamY").transform.position.y;
		corouLock = true;
		isAtTop = false;

		drink_slot = GameObject.Find ("Slot_Drink").GetComponent<UI_Slot> ();
		ui_control = GameObject.Find ("UI Manager").GetComponent<UI_Control> ();

		sideBtn = new GameObject[2];
		sideBtn [0] = GameObject.Find ("Button_Investigate");
		sideBtn [1] = GameObject.Find ("Button_Hold");
	}

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		SideBtn_hide ();
		itemBeingDragged.position = new Vector2 (itemBeingDragged.position.x, Input.mousePosition.y);
	}	
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		if (isAtTop) {
			if (itemBeingDragged.position.y > paramY * 0.7f) {
				if (corouLock){
					StartCoroutine ("Relocater", "top");
				}

			} else {
				if (corouLock){
					StartCoroutine ("Relocater", "bottom");
				}
			}
		} else {
			if (itemBeingDragged.position.y > paramY * 0.3f) {
				if (corouLock){
					StartCoroutine ("Relocater", "top");
				}
				
			} else {
				if (corouLock){
					StartCoroutine ("Relocater", "bottom");
				}
			}
		}
	}

	#endregion

	private Vector2 vel = Vector2.zero;
	IEnumerator Relocater(string toWhere){
		corouLock = false;
		ui_control.ItemDragLock = false;
		if (toWhere.Equals ("top")) {
			while (itemBeingDragged.position.y <= paramY * 0.6f) {
				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
				                                                new Vector2 (itemBeingDragged.position.x, paramY),
				                                                ref vel, 
			    	                                            0.3f);
				yield return null;
			}
			while (itemBeingDragged.position.y >= paramY * 0.6f) {
				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
				                                                new Vector2 (itemBeingDragged.position.x, paramY * 0.5f),
				                                                ref vel, 
				                                                0.3f);
				yield return null;
			}
			isAtTop = true;
		} else {
			while (itemBeingDragged.position.y >= startY) {
				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
				                                                new Vector2 (itemBeingDragged.position.x, startY * 0.8f),
				                                                ref vel, 
				                                                0.3f);
				yield return null;
			}
			while (itemBeingDragged.position.y <= startY) {
				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
				                                                new Vector2 (itemBeingDragged.position.x, startY * 1.2f),
				                                             ref vel, 
				                                             0.3f);
				yield return null;
			}
			isAtTop = false;
			SideBtn_unhide ();
		}
		corouLock = true;
		ui_control.ItemDragLock = true;
		SlotOriRefresh();
		yield break;
	}

	private void SlotOriRefresh (){
		drink_slot.RefreshOriPositions ();
	}

	private void SideBtn_hide(){
		sideBtn [0].transform.SetSiblingIndex (1);
		sideBtn [1].transform.SetSiblingIndex (2);
	}
	private void SideBtn_unhide(){
		sideBtn [0].transform.SetSiblingIndex (6);
		sideBtn [1].transform.SetSiblingIndex (6);
	}
}

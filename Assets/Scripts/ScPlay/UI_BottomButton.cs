using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BottomButton : MonoBehaviour, IDragHandler, IEndDragHandler {
	private Transform itemBeingDragged;
	private float startY;
	private float maxY;
	private bool corouLock;
	
	private UI_Control ui_control;

	private GameObject[] sideBtn;

	void Start(){
		itemBeingDragged = GameObject.Find ("Panel_Slot").transform;

		ui_control = GameObject.Find ("UI Manager").GetComponent<UI_Control> ();
		startY = itemBeingDragged.position.y;
		maxY = startY - GameObject.Find ("ParamY_Slot").transform.position.y;
		corouLock = true;

		sideBtn = new GameObject[2];
		sideBtn [0] = GameObject.Find ("Button_Investigate");
		sideBtn [1] = GameObject.Find ("Button_Hold");
	}

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		SideBtn_hide ();
		PanelLog_hide ();

		if (itemBeingDragged.position.y - Input.mousePosition.y > 0) {
			itemBeingDragged.position = new Vector2 (itemBeingDragged.position.x, Input.mousePosition.y);
		} else {
			if (itemBeingDragged.position.y < maxY)
				itemBeingDragged.position = new Vector2 (itemBeingDragged.position.x, Input.mousePosition.y);
		}

	}	
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		if (ui_control.IsAtTop) {
			if (itemBeingDragged.position.y > maxY * 0.7f) {
				if (corouLock){
					StartCoroutine ("Relocater", "top");
				}

			} else {
				if (corouLock){
					StartCoroutine ("Relocater", "bottom");
				}
			}
		} else {
			if (itemBeingDragged.position.y > maxY * 0.3f) {
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
			while (itemBeingDragged.position.y <= maxY) {
				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
				                                                new Vector2 (itemBeingDragged.position.x, maxY * 1.2f),
				                                                ref vel, 
			    	                                            0.3f);
				yield return null;
			}
			while (itemBeingDragged.position.y >= maxY) {
				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
				                                                new Vector2 (itemBeingDragged.position.x, maxY * 0.8f),
				                                                ref vel, 
				                                                0.3f);
				yield return null;
			}
			ui_control.IsAtTop = true;
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
			ui_control.IsAtTop = false;
			SideBtn_unhide ();
			PanelLog_unhide();
		}
		corouLock = true;
		ui_control.ItemDragLock = true;
		yield break;
	}

	private void SideBtn_hide(){
		sideBtn [0].transform.SetSiblingIndex (1);
		sideBtn [1].transform.SetSiblingIndex (2);
	}
	private void SideBtn_unhide(){
		sideBtn [0].transform.SetSiblingIndex (6);
		sideBtn [1].transform.SetSiblingIndex (6);
	}

	private void PanelLog_hide(){
		GameObject.Find("Panel_Log").transform.SetSiblingIndex(1);
	}
	private void PanelLog_unhide(){
		GameObject.Find("Panel_Log").transform.SetSiblingIndex(4);
	}
}

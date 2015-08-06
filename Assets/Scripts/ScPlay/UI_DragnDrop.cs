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

	void Start(){
		itemBeingDragged = GameObject.Find ("Panel_Slot").transform;
		startY = itemBeingDragged.position.y;

		paramY = GameObject.Find ("ParamY").transform.position.y;
		corouLock = true;
		isAtTop = false;
	}

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		itemBeingDragged.position = new Vector2 (itemBeingDragged.position.x, Input.mousePosition.y);

	}	
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		if (isAtTop) {
			if (itemBeingDragged.position.y > paramY * 0.7f) {
				if (corouLock)
					StartCoroutine ("Relocater", "top");

			} else {
				if (corouLock)
					StartCoroutine ("Relocater", "bottom");
			}
		} else {
			if (itemBeingDragged.position.y > paramY * 0.3f) {
				if (corouLock)
					StartCoroutine ("Relocater", "top");
				
			} else {
				if (corouLock)
					StartCoroutine ("Relocater", "bottom");
			}
		}
	}

	#endregion

	private Vector2 vel = Vector2.zero;
	IEnumerator Relocater(string toWhere){
		corouLock = false;
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
		}
		corouLock = true;
		yield break;
	}
}

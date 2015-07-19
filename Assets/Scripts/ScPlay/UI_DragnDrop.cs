﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UI_DragnDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	private Transform itemBeingDragged;
	float startY;
	float cvHeight;
	RectTransform rt;

	void Start(){
		itemBeingDragged = GameObject.Find ("Panel_Slot").transform;
		cvHeight = GameObject.Find ("Canvas").GetComponent<RectTransform> ().sizeDelta.y;
		rt = GameObject.Find ("Panel_Slot").GetComponent<RectTransform>();
		startY = rt.anchoredPosition.y;
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{

	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if(rt.anchoredPosition.y <= cvHeight * 0.8f && rt.anchoredPosition.y >= startY - 0.1f)
			itemBeingDragged.position = new Vector2(itemBeingDragged.position.x, Input.mousePosition.y);
	}	
	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if (rt.anchoredPosition.y > cvHeight * 0.5f) {
			StartCoroutine("Relocater", "top");

		} else {
			StartCoroutine("Relocater", "bottom");
		}
	}

	#endregion
	private Vector2 vel = Vector2.zero;
	IEnumerator Relocater(string toWhere){
		if (toWhere.Equals ("top")) {
			Debug.Log ("top");
			while (rt.anchoredPosition.y <= cvHeight * 0.8f) {
				itemBeingDragged.position += new Vector3 (0, 0.01f, 0);
				yield return null;
			}
			//itemBeingDragged.position = new Vector2 (itemBeingDragged.position.x, 95);
		}
//		} else {
//			while (transform.position.y >= 90) {
//				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
//				                                                new Vector2 (itemBeingDragged.position.x, -200f),
//				                                                ref vel, 
//				                                                0.001f);
//				yield return null;
//			}
//			itemBeingDragged.position = new Vector2(itemBeingDragged.position.x, -190);
//		}

		yield break;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UI_DragnDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	private Transform itemBeingDragged;
	Vector3 startPos;

	void Start(){
		itemBeingDragged = GameObject.Find ("Panel_Slot").transform;
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{

	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if(transform.position.y <= 380 && transform.position.y >= 90)
			itemBeingDragged.position = new Vector2(itemBeingDragged.position.x, Input.mousePosition.y - 280);
	}	
	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if (transform.position.y > 200) {
			StartCoroutine("Relocater", "top");

		} else {
			StartCoroutine("Relocater", "bottom");
		}
	}

	#endregion
	private Vector2 vel = Vector2.zero;
	IEnumerator Relocater(string toWhere){
		if (toWhere.Equals ("top")) {
			while (transform.position.y <= 380) {
				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
				                                               new Vector2 (itemBeingDragged.position.x, 200f),
			    	                                           ref vel, 
				                                               0.001f);
				yield return null;
			}
			itemBeingDragged.position = new Vector2(itemBeingDragged.position.x, 95);
		} else {
			while (transform.position.y >= 90) {
				itemBeingDragged.position = Vector2.SmoothDamp (itemBeingDragged.position, 
				                                                new Vector2 (itemBeingDragged.position.x, -200f),
				                                                ref vel, 
				                                                0.001f);
				yield return null;
			}
			itemBeingDragged.position = new Vector2(itemBeingDragged.position.x, -190);
		}

		yield break;
	}
}

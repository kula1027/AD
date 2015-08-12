using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SlotItem : MonoBehaviour, IDragHandler, IEndDragHandler {
	private UI_Slot ui_slot;
	private UI_Control ui_control;
	private Vector3 oriPosition;
	private int index;
	private bool isFilled;

	void Start () {
		ui_slot = transform.parent.GetComponent<UI_Slot> ();
		ui_control = GameObject.Find ("UI Manager").GetComponent<UI_Control>();
		oriPosition = transform.position;
		isFilled = false;
	}

	public void RefreshOriPos(){
		oriPosition = transform.position;
	}
	
	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		transform.SetSiblingIndex (100);
		if(ui_control.ItemDragLock)
			transform.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
	}	
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		ui_slot.OnEndDrag (transform);
		transform.SetSiblingIndex (index);
	}
	
	#endregion

	public Vector3 OriPosition {
		get {
			return oriPosition;
		}
		set {
			oriPosition = value;
		}
	}

	public int Index {
		get {
			return index;
		}
		set {
			index = value;
		}
	}

	public bool IsFilled {
		get {
			return isFilled;
		}
		set {
			isFilled = value;
		}
	}
}

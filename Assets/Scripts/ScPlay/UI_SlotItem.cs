using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SlotItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
	public bool isGold;
	private UI_Slot ui_slot;
	private UI_Control ui_control;

	private Vector3 oriPosition;
	private Sprite image;

	private int index;
	public bool isFilled;
	private bool isDraging;

	private int code;

	public void UI_SlotItem_Start (UI_Slot _ui_slot, int _index) {
		ui_slot = _ui_slot;
		index = _index;
		ui_control = GameObject.Find ("UI Manager").GetComponent<UI_Control>();
		oriPosition = transform.position;

		isFilled = false;
		isDraging = false;
		code = 0;
	}

	public void RelocateOriPos(){
		oriPosition = transform.position;
	}

	public void DoSelect(){
		if (index == ui_slot.GetSelectedIndex ()) {
			ui_slot.SelectOff();
			return;
		}

		if (ui_control.IsAtTop && isFilled && !isDraging) {
			ui_slot.SelectOn (index);
		}
	}

	public void SetSprite(Sprite sprite_){
		GetComponent<Image> ().sprite = sprite_;
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		RelocateOriPos ();
	}

	#endregion

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		if (ui_control.ItemDragLock && isFilled && ui_control.IsAtTop && !isGold) {
			if(ui_slot.GetComponent<UI_SlotHoldingDrink>() != null && (index < 5)){}
			else{
				isDraging = true;
				transform.SetSiblingIndex (100);
				transform.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			}
		}
	}	
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		if (isFilled && ui_control.IsAtTop) {
			ui_slot.OnEndDrag (transform);
			transform.SetSiblingIndex (index + 1);
			isDraging = false;
		}

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

	public int Code {
		get {
			return code;
		}
		set {
			code = value;
		}
	}
}

using UnityEngine;
using System.Collections;

public class Gauge : MonoBehaviour {

	public RectTransform gauge;

	private int value;
	private int firstValue;
	private RectTransform rect;

	public float rate = 0;

	void Start(){
		rect = transform.GetComponent<RectTransform>();
		InitValue(100, 100);
	}

	private void CalRate(){
		rate = ((float)value)/((float)firstValue)*100;
	}

	private void PrintValue(){
		//gauge.sizeDelta = new Vector2(rect.sizeDelta.x*(rate/100), 0);
	}

	public void InitValue(int fullValue,int value){
		firstValue = fullValue;
		this.value = value;
		CalRate();
		PrintValue();
	}

	public int GetValue(){
		return value;
	}

	public int GetFirstValue(){
		return firstValue;
	}

	public void SetValue(int v){
		value = v;
		if(value<0){
			value = 0;
		}
		if(firstValue < value){
			value = firstValue;
		}
		CalRate();
		PrintValue();
	}

}

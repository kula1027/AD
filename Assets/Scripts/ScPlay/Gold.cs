using UnityEngine;
using System.Collections;

public class Gold : Item {

	private int value;		//돈의 양

	public Gold(){
	}

	public override void initChild(int value_, int uselessCode_){
		value = value_;
	}

	public int getValue(){
		return this.value;
	}

	public void incAmount(int amount){
		this.value += amount;
		if (value < 0) {
			Debug.Log ("Gold can't be negative value");
			value = 0;
		}
	}

}

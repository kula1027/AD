using UnityEngine;
using System.Collections;

public class CodeManager : MonoBehaviour {

	public static int FindParantCode_Player(int childCode){
		if (childCode < 7) {					//Alpha ver PlayerCharacter
			return childCode;
		}
		switch (childCode) {
		default:
			Debug.Log("Can't Find Code");
			return 0;
		}
	}

	public static int FindParentCode_Enemy(int childCode){
		if (childCode < 22) {
			return childCode + 7;
		}
		switch(childCode){
			default:
			Debug.Log("Can't Find Code");
			return 0;
		}
	}

	public static int FindChildCode_Entity(int parentCode){
		if (parentCode < 7) {					//Alpha ver PlayerCharacter
			return parentCode;
		} else if (parentCode < 29) {			//Alpha ver Enemy
			return parentCode - 7;
		}
		switch (parentCode) {
		default:
			Debug.Log("Can't Find Code");
			return 0;
		}
	}
}

using UnityEngine;
using System.Collections;

public class UI_Buff : MonoBehaviour {
	private GameObject[] buff = new GameObject[12];
	private int buffCount;

	void Start () {
		for (int loop = 0; loop < 12; loop++) {
			buff[loop] = GameObject.Find("Buff (" + loop + ")");
			buff[loop].SetActive(false);
		}

		buffCount = 0;
	}

	void AddBuff(){
		buff [buffCount].SetActive (true);
		buffCount++;
	}

	void DeleteBuff(int index){
		if (index > buffCount) {
			Debug.Log ("index exceed buffCount");
			return;
		} else {

		}
	}
}

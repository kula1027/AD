using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Log : MonoBehaviour {
	private GameObject invisiblePanel;
	private GameObject log;
	private bool logLock;
	private const float scaleSpeed = 60f;
	private GameObject[] text_log;

	private const int arrCount = 13;

	void Start(){
		invisiblePanel = GameObject.Find ("InvisiblePanel");
		log = GameObject.Find ("Log");

		invisiblePanel.SetActive (false);
		logLock = true;

		text_log = new GameObject[arrCount];
		for (int loop = 0; loop < arrCount; loop++) {
			text_log[loop] = log.transform.GetChild(loop).gameObject;
			if(loop > 1)text_log[loop].SetActive(false);
		}
	}

	public void AddLogText(string str_){
		for (int loop = arrCount - 2; loop >= 0; loop--) {
			text_log[loop + 1].GetComponent<Text>().text = text_log[loop].GetComponent<Text>().text;
		}

		text_log [0].GetComponent<Text> ().text = str_;
	}

	public void OnLogClickDown(){
		if (invisiblePanel.activeSelf && logLock) {
			invisiblePanel.SetActive (false);
			if(logLock)StartCoroutine("LogDown");
		}
		else {
			invisiblePanel.SetActive (true);
			if(logLock)StartCoroutine("LogUp");

		}
	}

	private IEnumerator LogUp(){
		logLock = false;
		RectTransform rt = log.GetComponent<RectTransform> ();

		while (rt.offsetMax.y < 1110) {
			float temp = rt.offsetMax.y + scaleSpeed;
			rt.offsetMax = new Vector2(rt.offsetMax.x, temp);
			yield return null;
		}
		Text_Enable ();
		logLock = true;
	}
	
	private IEnumerator LogDown(){
		Text_Disable ();
		logLock = false;
		RectTransform rt = log.GetComponent<RectTransform> ();
		
		while (rt.offsetMax.y > 30) {
			float temp = rt.offsetMax.y - scaleSpeed;
			rt.offsetMax = new Vector2(rt.offsetMax.x, temp);
			yield return null;
		}

		logLock = true;
	}

	private void Text_Enable(){
		for(int loop = 2; loop < arrCount; loop++){
			text_log[loop].SetActive(true);
		}
	}

	private void Text_Disable(){
		for(int loop = 2; loop < arrCount; loop++){
			text_log[loop].SetActive(false);
		}
	}
}

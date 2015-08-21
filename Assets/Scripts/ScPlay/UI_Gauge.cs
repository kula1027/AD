using UnityEngine;
using System.Collections;

public class UI_Gauge : MonoBehaviour {
	//private const float MAXGAUGE = 418.6f; 
	private static RectTransform hp;
	private static RectTransform mp;
	private static RectTransform exp;

	void Start () {
		hp = transform.FindChild ("Gauge_HP").transform.FindChild("Gauge").GetComponent<RectTransform>();
		mp = transform.FindChild ("Gauge_MP").transform.FindChild("Gauge").GetComponent<RectTransform>();
		exp = transform.FindChild ("Gauge_EXP").transform.FindChild("Gauge").GetComponent<RectTransform>();
	}

	public static void SetPlayerHP(float rate){
		hp.localScale = new Vector3 (rate,1,1);
	}

	public static void SetPlayerMP(float rate){
		mp.localScale = new Vector3 (rate, 1, 1);
	}

	public static void SetPlayerEXP(float rate){
		exp.localScale = new Vector3 (rate, 1, 1);
	}
}

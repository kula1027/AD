﻿using UnityEngine;
using System.Collections;

public class UI_Gauge : MonoBehaviour {
	private const float MAXGAUGE = 418.6f; 
	private RectTransform hp;
	private RectTransform mp;
	private RectTransform exp;

	void Start () {
		hp = transform.FindChild ("Gauge_HP").transform.FindChild("Gauge").GetComponent<RectTransform>();
		mp = transform.FindChild ("Gauge_MP").transform.FindChild("Gauge").GetComponent<RectTransform>();
		exp = transform.FindChild ("Gauge_EXP").transform.FindChild("Gauge").GetComponent<RectTransform>();
	}

	public void SetHP(int percent){
		float hpPercent = MAXGAUGE * percent * 0.01f;

		hp.sizeDelta = new Vector2 (hpPercent, 0);
	}

	public void SetMP(int percent){
		float mpPercent = MAXGAUGE * percent * 0.01f;
		
		mp.sizeDelta = new Vector2 (mpPercent, 0);
	}

	public void SetEXP(int percent){
		float expPercent = MAXGAUGE * percent * 0.01f;
		
		exp.sizeDelta = new Vector2 (expPercent, 0);
	}
}

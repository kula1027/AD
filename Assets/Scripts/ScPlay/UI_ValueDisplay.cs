using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_ValueDisplay : MonoBehaviour {
	
	public float speed = 0.005f;
	public float alpha = 0.03f;
	// Use this for initialization

	public void activeDisplay(){
		StartCoroutine ("upwardDamage");
		StartCoroutine ("disappearDamage");
	}

	// Update is called once per frame
	IEnumerator upwardDamage (){
		while (true) {
			Vector3 pos = transform.position;
			pos.y += speed;
			transform.position = pos;
			yield return null;
		}
	}
	IEnumerator disappearDamage(){
		Text text = transform.GetComponent<UnityEngine.UI.Text>();
		for(float a = 1; a > 0; a -= alpha)
		{
			text.color = text.color - new Color(0,0,0,alpha);
			yield return null;
		}

		Destroy(transform.parent.gameObject);
	}
}
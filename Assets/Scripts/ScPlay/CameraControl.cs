using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private GameObject CMtran;

	public void SetPlayer(GameObject player){
		CMtran = player;
	}
	
	Vector3 vel = Vector3.zero;
	void Update () {
		if(CMtran){
			Vector3 targetPos = CMtran.transform.position - new Vector3(0, 1, 10);
			transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, 0.1f);
		}
	}
}

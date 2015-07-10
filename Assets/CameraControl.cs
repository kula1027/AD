using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	Transform CMtran;
	void Start () {
		CMtran = GameObject.Find ("Player").transform;
	}
	
	Vector3 vel = Vector3.zero;
	void Update () {
		Vector3 targetPos = CMtran.position - new Vector3(0, 0, 10);
		transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, 0.1f);
	}
}

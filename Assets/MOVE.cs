using UnityEngine;
using System.Collections;

public class MOVE : MonoBehaviour {
	


	public int moveFlag = 3;

	private const int speed = 1;

	void Start(){
		StartCoroutine ("Move");
	}

	// Update is called oncer frame
	IEnumerator Move(){

		float x_point = gameObject.transform.position.x;
		float y_point = gameObject.transform.position.y;
		float init_x_point = gameObject.transform.position.x;
		float init_y_point = gameObject.transform.position.y;
		while (true) {

			switch (moveFlag) {
			case 0:
				yield break;
			case 1://left
				x_point -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);


				Debug.Log(init_x_point + " " + x_point);
				if(init_x_point-x_point>1){
					moveFlag = 0;};
				break;


			case 2: //lefttop
				x_point -= Time.deltaTime * speed;
				y_point += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(init_x_point-x_point>1){
					moveFlag = 0;};
				break;
			case 3: //top
				y_point += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(y_point-init_y_point>1){
					moveFlag = 0;};
				break;
			case 4: //righttop
				x_point += Time.deltaTime * speed;
				y_point += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(x_point-init_x_point>1){
					moveFlag = 0;};
				break;
			case 5: // right
				x_point += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(x_point-init_x_point>1){
					moveFlag = 0;};
				break;
			case 6: //rightbottom
				x_point += Time.deltaTime * speed;
				y_point -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(x_point-init_x_point>1){
					moveFlag = 0;};
				break;
			case 7: //bottom
				y_point -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(init_y_point-y_point>1){
					moveFlag = 0;};
				break;
			case 8: //leftbottom
				x_point -= Time.deltaTime * speed;
				y_point -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(init_x_point-x_point>1){
					moveFlag = 0;};
				break;
			}
			yield return null;
		}

	}

	}

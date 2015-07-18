using UnityEngine;
using System.Collections;

public class MOVE : MonoBehaviour {

	public int moveFlag = 3;

	private const int speed = 1;

	void Start(){
	}

	public void SetMove(int moveFlag){
		this.moveFlag = moveFlag;
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
			case MoveFlag.STAY:
				yield break;
			case MoveFlag.LEFT	:
				x_point -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(init_x_point-x_point>1){	
					moveFlag = MoveFlag.STAY;
					gameObject.transform.position = new Vector3 ((int)(x_point+0.1f), (int)(y_point+0.1f), 0);
				}
				break;
			case MoveFlag.LEFTUP	:
				x_point -= Time.deltaTime * speed;
				y_point += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(init_x_point-x_point>1){
					moveFlag = MoveFlag.STAY;	
					gameObject.transform.position = new Vector3 ((int)(x_point+0.1f), (int)(y_point+0.1f), 0);
				}
				break;
			case MoveFlag.UP	:
				y_point += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(y_point-init_y_point>1){	
					moveFlag = MoveFlag.STAY;	
					gameObject.transform.position = new Vector3 ((int)(x_point+0.1f), (int)(y_point+0.1f), 0);
				}
				break;
			case MoveFlag.RIGHTUP	:
				x_point += Time.deltaTime * speed;
				y_point += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(x_point-init_x_point>1){
					moveFlag = MoveFlag.STAY;	
					gameObject.transform.position = new Vector3 ((int)(x_point+0.1f), (int)(y_point+0.1f), 0);
				}
				break;
			case MoveFlag.RIGHT	:
				x_point += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(x_point-init_x_point>1){	
					moveFlag = MoveFlag.STAY;	
					gameObject.transform.position = new Vector3 ((int)(x_point+0.1f), (int)(y_point+0.1f), 0);
				}
				break;
			case MoveFlag.RIGHTDOWN		:
				x_point += Time.deltaTime * speed;
				y_point -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(x_point-init_x_point>1){
					moveFlag = MoveFlag.STAY;	
					gameObject.transform.position = new Vector3 ((int)(x_point+0.1f), (int)(y_point+0.1f), 0);
				}
				break;
			case MoveFlag.DOWN	:
				y_point -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(init_y_point-y_point>1){	
					moveFlag = MoveFlag.STAY;	
					gameObject.transform.position = new Vector3 ((int)(x_point+0.1f), (int)(y_point+0.1f), 0);
				}
				break;
			case MoveFlag.LEFTDOWN	:
				x_point -= Time.deltaTime * speed;
				y_point -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (x_point, y_point, 0);
				if(init_x_point-x_point>1){	
					moveFlag = MoveFlag.STAY;	
					gameObject.transform.position = new Vector3 ((int)(x_point+0.1f), (int)(y_point+0.1f), 0);
				}
				break;
			}
			yield return null;
		}
	}
}

public class MoveFlag{
	public const int STAY = 0;
	public const int LEFT = 1;
	public const int LEFTUP = 2;
	public const int UP = 3;
	public const int RIGHTUP = 4;
	public const int RIGHT = 5;
	public const int RIGHTDOWN = 6;
	public const int DOWN = 7;
	public const int LEFTDOWN = 8;
}

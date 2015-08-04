using UnityEngine;
using System.Collections;

public class MOVE : MonoBehaviour {

	public int moveFlag = 0;

	private const int speed = 5;

	void Start(){
	}

	public void SetMove(int moveFlag){
		this.moveFlag = moveFlag;
		StartCoroutine ("Move");
	}

	private void IncTurnCount(){
		int amount = 1;
<<<<<<< HEAD
		for(int i = 0; i<((float)(gameObject.GetComponent<Entity>().getDex())/30.0f);i++){
=======
		for(int i = 0; i < 3 - ((gameObject.GetComponent<Entity>().getDex())/30);i++){
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			amount*=2;
		}
		if(gameObject.GetComponent<Enemy>())gameObject.GetComponent<Enemy>().incTrunCount(amount);
		if(gameObject.GetComponent<Player>())gameObject.GetComponent<Player>().incTrunCount(amount);
	}
	
	// Update is called oncer frame
	IEnumerator Move(){
		Vector3 pos = gameObject.transform.position;
		Vector3 initPos = gameObject.transform.position;
		while (true) {
			switch (moveFlag) {
<<<<<<< HEAD
			case MoveFlag.STAY:
				yield break;
			case MoveFlag.LEFT	:
				pos.x -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){	
					moveFlag = MoveFlag.STAY;
=======
			case Direction.STAY:
				yield break;
			case Direction.LEFT	:
				pos.x -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){	
					moveFlag = Direction.STAY;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y+0.1f), 0);
					IncTurnCount();
				}
				break;
<<<<<<< HEAD
			case MoveFlag.LEFTUP	:
=======
			case Direction.LEFTUP	:
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				pos.x -= Time.deltaTime * speed;
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){
<<<<<<< HEAD
					moveFlag = MoveFlag.STAY;	
=======
					moveFlag = Direction.STAY;	
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y + 1 +0.1f), 0);
					IncTurnCount();
				}
				break;
<<<<<<< HEAD
			case MoveFlag.UP	:
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.y - initPos.y>1){	
					moveFlag = MoveFlag.STAY;	
=======
			case Direction.UP	:
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.y - initPos.y>1){	
					moveFlag = Direction.STAY;	
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
					gameObject.transform.position = new Vector3 ((int)(initPos.x+0.1f), (int)(initPos.y + 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
<<<<<<< HEAD
			case MoveFlag.RIGHTUP	:
=======
			case Direction.RIGHTUP	:
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				pos.x += Time.deltaTime * speed;
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x - initPos.x>1){
<<<<<<< HEAD
					moveFlag = MoveFlag.STAY;	
=======
					moveFlag = Direction.STAY;	
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y + 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
<<<<<<< HEAD
			case MoveFlag.RIGHT	:
				pos.x += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x-initPos.x>1){	
					moveFlag = MoveFlag.STAY;	
=======
			case Direction.RIGHT	:
				pos.x += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x-initPos.x>1){	
					moveFlag = Direction.STAY;	
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y + 0.1f), 0);
					IncTurnCount();
				}
				break;
<<<<<<< HEAD
			case MoveFlag.RIGHTDOWN		:
=======
			case Direction.RIGHTDOWN		:
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				pos.x += Time.deltaTime * speed;
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x - initPos.x>1){
<<<<<<< HEAD
					moveFlag = MoveFlag.STAY;
=======
					moveFlag = Direction.STAY;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y - 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
<<<<<<< HEAD
			case MoveFlag.DOWN	:
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.y - pos.y>1){	
					moveFlag = MoveFlag.STAY;
=======
			case Direction.DOWN	:
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.y - pos.y>1){	
					moveFlag = Direction.STAY;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
					gameObject.transform.position = new Vector3 ((int)(initPos.x+0.1f), (int)(initPos.y - 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
<<<<<<< HEAD
			case MoveFlag.LEFTDOWN	:
=======
			case Direction.LEFTDOWN	:
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				pos.x -= Time.deltaTime * speed;
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){	
<<<<<<< HEAD
					moveFlag = MoveFlag.STAY;
=======
					moveFlag = Direction.STAY;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y - 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
			}
			yield return null;
		}
	}
}

<<<<<<< HEAD
public class MoveFlag{
=======
public class Direction{
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
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

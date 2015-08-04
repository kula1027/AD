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
		for(int i = 0; i < 3 - ((gameObject.GetComponent<Entity>().getDex())/30);i++){
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
			case Direction.STAY:
				yield break;
			case Direction.LEFT	:
				pos.x -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){	
					moveFlag = Direction.STAY;
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y+0.1f), 0);
					IncTurnCount();
				}
				break;
			case Direction.LEFTUP	:
				pos.x -= Time.deltaTime * speed;
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){
					moveFlag = Direction.STAY;	
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y + 1 +0.1f), 0);
					IncTurnCount();
				}
				break;
			case Direction.UP	:
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.y - initPos.y>1){	
					moveFlag = Direction.STAY;	
					gameObject.transform.position = new Vector3 ((int)(initPos.x+0.1f), (int)(initPos.y + 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
			case Direction.RIGHTUP	:
				pos.x += Time.deltaTime * speed;
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x - initPos.x>1){
					moveFlag = Direction.STAY;	
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y + 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
			case Direction.RIGHT	:
				pos.x += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x-initPos.x>1){	
					moveFlag = Direction.STAY;	
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y + 0.1f), 0);
					IncTurnCount();
				}
				break;
			case Direction.RIGHTDOWN		:
				pos.x += Time.deltaTime * speed;
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x - initPos.x>1){
					moveFlag = Direction.STAY;
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y - 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
			case Direction.DOWN	:
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.y - pos.y>1){	
					moveFlag = Direction.STAY;
					gameObject.transform.position = new Vector3 ((int)(initPos.x+0.1f), (int)(initPos.y - 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
			case Direction.LEFTDOWN	:
				pos.x -= Time.deltaTime * speed;
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){	
					moveFlag = Direction.STAY;
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y - 1 + 0.1f), 0);
					IncTurnCount();
				}
				break;
			}
			yield return null;
		}
	}
}

public class Direction{
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

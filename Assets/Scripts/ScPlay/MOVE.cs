using UnityEngine;
using System.Collections;

public class MOVE : MonoBehaviour {

	public int moveFlag = 0;
	private int sortingOrder = 0;

	private const int speed = 5;

	void Start(){
	}

	public void SetMove(int moveFlag){
		this.moveFlag = moveFlag;
		gameObject.GetComponent<Entity>().incTurnCount();
		StartCoroutine ("Move");
	}
	
	// Update is called oncer frame
	IEnumerator Move(){
		Vector3 pos = gameObject.transform.position;
		Vector3 initPos = gameObject.transform.position;
		while (true) {
			switch (moveFlag) {
			case Direction.STAY:
				if(gameObject.GetComponent<Player>()){
					if(gameObject.GetComponent<Player>().getEntityCode() == 4){
						GameObject.Find("GameManager").GetComponent<GameManager>().take9Drink();	//투척병은 주변 3x3 습득
					}else{
						GameObject.Find("GameManager").GetComponent<GameManager>().takeDrink();		//그외는 자기 자리만 습득
					}
					GameObject.Find("GameManager").GetComponent<GameManager>().takeItem();
				}
				yield break;
			case Direction.LEFT	:
				pos.x -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){	
					moveFlag = Direction.STAY;
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y+0.1f), 0);
				}
				break;
			case Direction.LEFTUP	:
				pos.x -= Time.deltaTime * speed;
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){
					moveFlag = Direction.STAY;	
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y + 1 +0.1f), 0);
				}
				break;
			case Direction.UP	:
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.y - initPos.y>1){	
					moveFlag = Direction.STAY;	
					gameObject.transform.position = new Vector3 ((int)(initPos.x+0.1f), (int)(initPos.y + 1 + 0.1f), 0);
				}
				break;
			case Direction.RIGHTUP	:
				pos.x += Time.deltaTime * speed;
				pos.y += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x - initPos.x>1){
					moveFlag = Direction.STAY;	
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y + 1 + 0.1f), 0);
				}
				break;
			case Direction.RIGHT	:
				pos.x += Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x-initPos.x>1){	
					moveFlag = Direction.STAY;	
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y + 0.1f), 0);
				}
				break;
			case Direction.RIGHTDOWN		:
				pos.x += Time.deltaTime * speed;
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(pos.x - initPos.x>1){
					moveFlag = Direction.STAY;
					gameObject.transform.position = new Vector3 ((int)(initPos.x + 1 + 0.1f), (int)(initPos.y - 1 + 0.1f), 0);
				}
				break;
			case Direction.DOWN	:
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.y - pos.y>1){	
					moveFlag = Direction.STAY;
					gameObject.transform.position = new Vector3 ((int)(initPos.x+0.1f), (int)(initPos.y - 1 + 0.1f), 0);
				}
				break;
			case Direction.LEFTDOWN	:
				pos.x -= Time.deltaTime * speed;
				pos.y -= Time.deltaTime * speed;
				gameObject.transform.position = new Vector3 (pos.x, pos.y, 0);
				if(initPos.x - pos.x>1){	
					moveFlag = Direction.STAY;
					gameObject.transform.position = new Vector3 ((int)(initPos.x - 1 + 0.1f), (int)(initPos.y - 1 + 0.1f), 0);
				}
				break;
			}
			if(transform.position.y > (float)(((int)(transform.position.y))+0.5f)){
				transform.FindChild ("Image").GetComponent<SpriteRenderer> ().sortingOrder = (int)(51 - transform.position.y);
			}else{
				transform.FindChild ("Image").GetComponent<SpriteRenderer> ().sortingOrder = (int)(52 - transform.position.y);
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

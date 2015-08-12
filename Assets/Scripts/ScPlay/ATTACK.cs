using UnityEngine;
using System.Collections;

public class ATTACK : MonoBehaviour {

	public int attackFlag = 0;
	private Entity target;
	private float weaponRange = 1f;
	void Start(){

	}

	public bool attackable(int attackFlag){
		RaycastHit2D[] hit = new RaycastHit2D[0];
		switch (attackFlag) {
		case Direction.STAY:
			break;
		case Direction.LEFT	:
			hit = Physics2D.RaycastAll(transform.position, Vector2.left, weaponRange);
			//Debug.Log ("l");
			break;
		case Direction.LEFTUP:		
			hit = Physics2D.RaycastAll(transform.position, new Vector2(-1,1), weaponRange*1.414f);
			//Debug.Log ("lu");
			break;
		case Direction.UP:		
			hit = Physics2D.RaycastAll(transform.position, Vector2.up, weaponRange);
			//Debug.Log ("u");
			break;
		case Direction.RIGHTUP:		
			hit = Physics2D.RaycastAll(transform.position, new Vector2(1,1), weaponRange*1.414f);
			//Debug.Log ("ru");
			break;
		case Direction.RIGHT:;			
			hit = Physics2D.RaycastAll(transform.position, Vector2.right, weaponRange);
			//Debug.Log ("r");
			break;
		case Direction.RIGHTDOWN:		
			hit = Physics2D.RaycastAll(transform.position, new Vector2(1,-1), weaponRange*1.414f);
			//Debug.Log ("rd");
			break;
		case Direction.DOWN:
			hit = Physics2D.RaycastAll(transform.position, Vector2.down, weaponRange);
			//Debug.Log ("d");
			break;
		case Direction.LEFTDOWN:		
			hit = Physics2D.RaycastAll(transform.position, new Vector2(-1,-1), weaponRange*1.414f);
			break;
		}
		for(int loop = 0; loop < hit.Length; loop++){
			if(hit[loop].collider.transform.tag=="Enemy"){
				//Debug.Log (hit[loop].transform.position);
				gameObject.GetComponent<Entity>().attackable.Add(hit[loop].transform.gameObject);
			}
		}
		
		if(gameObject.GetComponent<Entity>().attackable.Count!=0){
			return true;
		}
		else{
			return false;
		}
	}
	
	public void SetAttack(int attackFlag){
		this.attackFlag = attackFlag;
		gameObject.GetComponent<Entity>().incTrunCount();
		StartCoroutine ("Attack");
	}

	// Update is called oncer frame
	IEnumerator Attack(){
		Vector3 pos = gameObject.transform.position;
		while (true) {
			switch (attackFlag) {
			case Direction.STAY:
				yield break;
			case Direction.LEFT	:
				if(true){//공격 애니메이션이 끝나면
					for(int i = 0; i < gameObject.GetComponent<Entity>().attackable.Count; i++){
						//Debug.Log (gameObject.GetComponent<Entity>().getStr());
						gameObject.GetComponent<Entity>().attackable[i].GetComponent<Entity>().Damage(gameObject.GetComponent<Entity>().getStr());
					}
					attackFlag = Direction.STAY;
					gameObject.GetComponent<Entity>().attackable.Clear();
					yield break;
				}
				break;
			case Direction.LEFTUP	:
				if(true){//공격 애니메이션이 끝나면
					for(int i = 0; i < gameObject.GetComponent<Entity>().attackable.Count; i++){
						gameObject.GetComponent<Entity>().attackable[i].GetComponent<Entity>().Damage(gameObject.GetComponent<Entity>().getStr());
					}
					attackFlag = Direction.STAY;
					gameObject.GetComponent<Entity>().attackable.Clear();
					yield break;
				}
				break;
			case Direction.UP	:
				if(true){//공격 애니메이션이 끝나면
					for(int i = 0; i < gameObject.GetComponent<Entity>().attackable.Count; i++){
						gameObject.GetComponent<Entity>().attackable[i].GetComponent<Entity>().Damage(gameObject.GetComponent<Entity>().getStr());
					}
					attackFlag = Direction.STAY;
					gameObject.GetComponent<Entity>().attackable.Clear();
					yield break;
				}
				break;
			case Direction.RIGHTUP	:
				if(true){//공격 애니메이션이 끝나면
					for(int i = 0; i < gameObject.GetComponent<Entity>().attackable.Count; i++){
						gameObject.GetComponent<Entity>().attackable[i].GetComponent<Entity>().Damage(gameObject.GetComponent<Entity>().getStr());
					}
					attackFlag = Direction.STAY;
					gameObject.GetComponent<Entity>().attackable.Clear();
					yield break;
				}
				break;
			case Direction.RIGHT	:
				if(true){//공격 애니메이션이 끝나면
					for(int i = 0; i < gameObject.GetComponent<Entity>().attackable.Count; i++){
						gameObject.GetComponent<Entity>().attackable[i].GetComponent<Entity>().Damage(gameObject.GetComponent<Entity>().getStr());
					}
					attackFlag = Direction.STAY;
					gameObject.GetComponent<Entity>().attackable.Clear();
					yield break;
				}
				break;
			case Direction.RIGHTDOWN		:
				if(true){//공격 애니메이션이 끝나면
					for(int i = 0; i < gameObject.GetComponent<Entity>().attackable.Count; i++){
						gameObject.GetComponent<Entity>().attackable[i].GetComponent<Entity>().Damage(gameObject.GetComponent<Entity>().getStr());
					}
					attackFlag = Direction.STAY;
					gameObject.GetComponent<Entity>().attackable.Clear();
					yield break;
				}
				break;
			case Direction.DOWN	:
				if(true){//공격 애니메이션이 끝나면
					for(int i = 0; i < gameObject.GetComponent<Entity>().attackable.Count; i++){
						gameObject.GetComponent<Entity>().attackable[i].GetComponent<Entity>().Damage(gameObject.GetComponent<Entity>().getStr());
					}
					attackFlag = Direction.STAY;
					gameObject.GetComponent<Entity>().attackable.Clear();
					yield break;
				}
				break;
			case Direction.LEFTDOWN	:
				if(true){//공격 애니메이션이 끝나면
					for(int i = 0; i < gameObject.GetComponent<Entity>().attackable.Count; i++){
						gameObject.GetComponent<Entity>().attackable[i].GetComponent<Entity>().Damage(gameObject.GetComponent<Entity>().getStr());
					}
					attackFlag = Direction.STAY;
					gameObject.GetComponent<Entity>().attackable.Clear();
					yield break;
				}
				break;
			}
			yield return null;
		}
	}
}

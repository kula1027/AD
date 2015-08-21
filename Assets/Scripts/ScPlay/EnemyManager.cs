using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {


	public GameManager gameManager;
	public BoardManager boardManager;

	private ArrayList enemys = new ArrayList();		//에네미의 실제 오브젝트.

	public EnemyManager(){
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		boardManager = gameManager.boardManager;
	}

	public void CreateEnemy(int stage, int enemyNums){
		int[] createEnemyCode = new int[enemyNums];								//생성할 Enemy들의 코드 배열
		RandomManager.randomizeEnemy (stage, createEnemyCode);					//생성한 Enemy들의 코드 랜더마이징

		boardManager._Stage[stage].SetEnemyPos(enemyNums);					//BoardManager에서 Enemy포지션 설정

		for(int i = 0 ; i < enemyNums ; i ++){
			GameObject e = Instantiate(Resources.Load("enemyDefault", typeof(GameObject))) as GameObject;				//기본 프리팹 생성
			if(Resources.Load<Sprite>("Image_Enemy/" + Config.entityResources[CodeManager.FindParentCode_Enemy(createEnemyCode[i])]) == null){
				//Debug.Log ("Missing Image");
			}else{
				e.transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite
					= Resources.Load<Sprite>("Image_Enemy/" + Config.entityResources[CodeManager.FindParentCode_Enemy(createEnemyCode[i])]);	//이미지 등록
			}
			Vector3 pos = boardManager._Stage[stage].enemyPos[i];
			e.transform.position = pos;																					//위치설정
			(boardManager._Stage[stage].get_tileInfo())[(int)pos.y,(int)pos.x].e = e.GetComponent<Enemy>();				//TileInfo에 등록
			e.GetComponent<Enemy>().SetEnemyManager(this);																//EnemyManager 등록
			e.GetComponent<Entity>().init (CodeManager.FindParentCode_Enemy(createEnemyCode[i]), createEnemyCode[i]);	//Randomize된 코드로 초기화
			enemys.Add(e.gameObject);																					//EnemyManager에 등록
		}

		//boardManager._Stage [stage].debugLog ();	//Enemy의 위치가 잘 등록되었는지 확인하기 위한 디버그전용 함수
	}

	public void decTurnCount(){
		for(int i = 0 ; i < enemys.Count ; i ++){
			((GameObject)(enemys[i])).GetComponent<Entity>().decTurnCount();
		}
	}

	public bool isAllNonZeroTurn(){
		for(int i = 0 ; i < enemys.Count ; i ++){
			if(((GameObject)(enemys[i])).GetComponent<Entity>().getTurnCount()<=0){
//				Debug.Log(((GameObject)(enemys[i])).GetComponent<Entity>().getTurnCount());
				return false;
			}
		}
		return true;
	}

	public bool IsAllStay(){
		for(int i = 0 ; i < enemys.Count ; i ++){
			if(((GameObject)(enemys[i])).GetComponent<MOVE>().moveFlag!=Direction.STAY){
				return false;
			}
			if(((GameObject)(enemys[i])).GetComponent<ATTACK>().attackFlag!=Direction.STAY){
				return false;
			}
		}
		return true;
	}

	public void EnemyAct(){
		//Debug.Log("EnemyAct() is called");
		for(int i = 0 ; i < enemys.Count ; i ++){
			if(((GameObject)(enemys[i])).GetComponent<Enemy>().getTurnCount()==0){
				//Debug.Log(((GameObject)(enemys[i])).GetComponent<Enemy>().getStr());
				((GameObject)(enemys[i])).GetComponent<Enemy>().Act();
			}else{
				//Debug.Log(i+" , "+((GameObject)(enemys[i])).GetComponent<Enemy>().getTurnCount());
			}
		}
	}

	public void EnemyGiveBuff(){
		for (int i = 0; i < enemys.Count; i ++) {
			((GameObject)(enemys [i])).GetComponent<Buffmanager> ().giveBuff ();
		}
	}

	public void EnemyTakeBuff(){
		for (int i = 0; i < enemys.Count; i ++) {
			((GameObject)(enemys [i])).GetComponent<Buffmanager> ().takeBuff ();
		}
	}

	public void IncAllTurnCount(){
		for(int i = 0 ; i < enemys.Count ; i ++){
			((GameObject)(enemys[i])).GetComponent<Enemy>().incTurnCount();
		}
	}

	public void RemoveSlave(GameObject obj){
		for(int i = 0 ; i < enemys.Count ; i ++){
			if(((GameObject)(enemys[i])) == obj){
				enemys.RemoveAt(i);
				break;
			}
		}
	}

}

using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public Transform[] enemy;
	public GameManager gameManager;
	public BoardManager boardManager;

	private ArrayList enemys = new ArrayList();

	public void CreateEnemy(){
		boardManager.SetEnemyTiles(3);
		int count = boardManager.enemyTiles.Length;
		for(int i = 0 ; i < count ; i ++){
			int num = Random.Range(0, enemy.Length-1);
			Transform e = (Transform)Instantiate(enemy[num], ((GameObject)(boardManager.enemyTiles[i])).transform.position, Quaternion.identity);
			e.GetComponent<Enemy>().SetEnemyManager(gameObject);
			enemys.Add(e.gameObject);
		}
	}

	public void decTurnCount(){
		for(int i = 0 ; i < enemys.Count ; i ++){
			((GameObject)(enemys[i])).GetComponent<Entity>().decTurnCount();
		}
	}

	public bool IsAllStop(){
		for(int i = 0 ; i < enemys.Count ; i ++){
			if(((GameObject)(enemys[i])).GetComponent<MOVE>().moveFlag!=MoveFlag.STAY){
				return false;
			}
		}
		return true;
	}

	public void EnemyAct(){
		for(int i = 0 ; i < enemys.Count ; i ++){
			if(((GameObject)(enemys[i])).GetComponent<Enemy>().getTurnCount()==0 && ((GameObject)(enemys[i])).GetComponent<MOVE>().moveFlag == MoveFlag.STAY){
				((GameObject)(enemys[i])).GetComponent<Enemy>().Act();
			}else{
				Debug.Log(((GameObject)(enemys[i])).GetComponent<Enemy>().getTurnCount());
			}
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

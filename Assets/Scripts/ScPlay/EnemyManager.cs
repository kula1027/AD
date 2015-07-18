using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public Transform[] enemy;
	public GameManager main;
	public BoardManager board;

	private ArrayList enemys = new ArrayList();

	public void CreateEnemy(){
		int count = board.enemyTiles.Length;
		for(int i = 0 ; i < count ; i ++){
			int num = Random.Range(0, enemy.Length-1);
			Transform e = (Transform)Instantiate(enemy[num], ((GameObject)(board.enemyTiles[i])).transform.position, Quaternion.identity);
			e.GetComponent<Enemy>().SetMaster(gameObject);
			enemys.Add(e.gameObject);
		}
	}

	public void EnemyAct(){
		for(int i = 0 ; i < enemys.Count ; i ++){
			((GameObject)(enemys[i])).GetComponent<Enemy>().Act();
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

<<<<<<< HEAD
﻿using UnityEngine;
=======
using UnityEngine;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
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
<<<<<<< HEAD
=======
			e.GetComponent<Entity>().init (IdInfo.DEBUG,IdInfo.DEBUG);
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
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
<<<<<<< HEAD
			if(((GameObject)(enemys[i])).GetComponent<MOVE>().moveFlag!=MoveFlag.STAY){
=======
			if(((GameObject)(enemys[i])).GetComponent<MOVE>().moveFlag!=Direction.STAY){
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				return false;
			}
		}
		return true;
	}

	public void EnemyAct(){
		for(int i = 0 ; i < enemys.Count ; i ++){
<<<<<<< HEAD
			if(((GameObject)(enemys[i])).GetComponent<Enemy>().getTurnCount()==0 && ((GameObject)(enemys[i])).GetComponent<MOVE>().moveFlag == MoveFlag.STAY){
				((GameObject)(enemys[i])).GetComponent<Enemy>().Act();
			}else{
				Debug.Log(((GameObject)(enemys[i])).GetComponent<Enemy>().getTurnCount());
=======
			if(((GameObject)(enemys[i])).GetComponent<Enemy>().getTurnCount()==0 && ((GameObject)(enemys[i])).GetComponent<MOVE>().moveFlag == Direction.STAY){
				((GameObject)(enemys[i])).GetComponent<Enemy>().Act();
			}else{
				//Debug.Log(((GameObject)(enemys[i])).GetComponent<Enemy>().getTurnCount());
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
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

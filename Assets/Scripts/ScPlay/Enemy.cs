<<<<<<< HEAD
﻿using UnityEngine;
=======
using UnityEngine;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
using System.Collections;

public class Enemy : Entity {

	public EnemyManager enemyManager;
	private Ai_Enemy ai;

	private int enemyCode;									//적 코드
	private int enemyLevel;									//적 레벨
	private int[] dropItem = new int[Config.dropItemLimit];	//몬스터 드롭템
	private int dropExp;									//몬스터 드롭 경험치

	public override void initChild(int enemyCode_){
		enemyCode = enemyCode_;
		enemyLevel = Config.enemyLevel[enemyCode];
		for(int i = 0 ; i< Config.dropItemLimit; i++)
		{
			dropItem[i] = 0;
		}
		dropExp = Config.dropExp[enemyCode];
	}

	public void SetEnemyManager(GameObject mas){
		enemyManager = mas.GetComponent<EnemyManager>();
		ai = transform.GetComponent<Ai_Enemy>();
		ai.setTileInfo (enemyManager.boardManager.get_tileInfo(0));
		ai.InitAI (transform.position, gameObject);
	}

	public void DestroyGameObject(){
		enemyManager.RemoveSlave(gameObject);
		Destroy(gameObject);
	}

	public void Act(){
		Move();
		//ai.setTileInfo (master.board.get_tileInfo(0));
		//ai.InitAI (transform.position, gameObject);
		//Move2(ai.get_Flag());
	}

	public void Move(){
		Vector3 playerPos = enemyManager.gameManager.player.transform.position;
		playerPos = playerPos - transform.position;
		if(0.5f<playerPos.x){
			if(0.5f<playerPos.y){
				if(rightUp == KindTag.empty || rightUp == KindTag.item){
<<<<<<< HEAD
					SetMove(MoveFlag.RIGHTUP);
				}
			}else if(playerPos.y<-0.5f){
				if(rightDown == KindTag.empty || rightDown == KindTag.item){
					SetMove(MoveFlag.RIGHTDOWN);
				}
			}else{
				if(right == KindTag.empty || right == KindTag.item){
					SetMove(MoveFlag.RIGHT);
=======
					SetMove(Direction.RIGHTUP);
				}
			}else if(playerPos.y<-0.5f){
				if(rightDown == KindTag.empty || rightDown == KindTag.item){
					SetMove(Direction.RIGHTDOWN);
				}
			}else{
				if(right == KindTag.empty || right == KindTag.item){
					SetMove(Direction.RIGHT);
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				}
			}
		}else if(playerPos.x<-0.5f){
			if(0.5f<playerPos.y){
				if(leftUp == KindTag.empty || leftUp == KindTag.item){
<<<<<<< HEAD
					SetMove(MoveFlag.LEFTUP);
				}
			}else if(playerPos.y<-0.5f){
				if(leftDown == KindTag.empty || leftDown == KindTag.item){
					SetMove(MoveFlag.LEFTDOWN);
				}
			}else{
				if(left == KindTag.empty || left == KindTag.item){
					SetMove(MoveFlag.LEFT);
=======
					SetMove(Direction.LEFTUP);
				}
			}else if(playerPos.y<-0.5f){
				if(leftDown == KindTag.empty || leftDown == KindTag.item){
					SetMove(Direction.LEFTDOWN);
				}
			}else{
				if(left == KindTag.empty || left == KindTag.item){
					SetMove(Direction.LEFT);
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				}
			}
		}else{
			if(0.5f<playerPos.y){
				if(up == KindTag.empty || up == KindTag.item){
<<<<<<< HEAD
					SetMove(MoveFlag.UP);
				}
			}else if(playerPos.y<-0.5f){
				if(down == KindTag.empty || down == KindTag.item){
					SetMove(MoveFlag.DOWN);
				}
			}else{
				{
					SetMove(MoveFlag.STAY);
=======
					SetMove(Direction.UP);
				}
			}else if(playerPos.y<-0.5f){
				if(down == KindTag.empty || down == KindTag.item){
					SetMove(Direction.DOWN);
				}
			}else{
				{
					SetMove(Direction.STAY);
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				}
			}
		}
	}

	public void Move2(int flag){
		
		SetMove(flag);
		
	}
}

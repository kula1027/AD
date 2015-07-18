using UnityEngine;
using System.Collections;

public class Enemy : Entity {

	public EnemyManager master;
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

	public void SetMaster(GameObject mas){
		master = mas.GetComponent<EnemyManager>();
		ai = transform.GetComponent<Ai_Enemy>();
		ai.setTileInfo (master.board.get_tileInfo(0));
		ai.InitAI (transform.position, gameObject);
	}

	public void DestroyGameObject(){
		master.RemoveSlave(gameObject);
		Destroy(gameObject);
	}

	public void Act(){
		//Move();
		ai.setTileInfo (master.board.get_tileInfo(0));
		Move2(ai.get_Flag());
	}

	public void Move(){
		Vector3 playerPos = master.main.player.transform.position;
		playerPos = playerPos - transform.position;

		if(0.5f<playerPos.x){
			if(0.5f<playerPos.y){
				if(rightUp == KindTag.empty || rightUp == KindTag.item){
					SetMove(MoveFlag.RIGHTUP);
				}
			}else if(playerPos.y<-0.5f){
				if(rightDown == KindTag.empty || rightDown == KindTag.item){
					SetMove(MoveFlag.RIGHTDOWN);
				}
			}else{
				if(right == KindTag.empty || right == KindTag.item){
					SetMove(MoveFlag.RIGHT);
				}
			}
		}else if(playerPos.x<-0.5f){
			if(0.5f<playerPos.y){
				if(leftUp == KindTag.empty || leftUp == KindTag.item){
					SetMove(MoveFlag.LEFTUP);
				}
			}else if(playerPos.y<-0.5f){
				if(leftDown == KindTag.empty || leftDown == KindTag.item){
					SetMove(MoveFlag.LEFTDOWN);
				}
			}else{
				if(left == KindTag.empty || left == KindTag.item){
					SetMove(MoveFlag.LEFT);
				}
			}
		}else{
			if(0.5f<playerPos.y){
				if(up == KindTag.empty || up == KindTag.item){
					SetMove(MoveFlag.UP);
				}
			}else if(playerPos.y<-0.5f){
				if(down == KindTag.empty || down == KindTag.item){
					SetMove(MoveFlag.DOWN);
				}
			}else{
				{
					SetMove(MoveFlag.STAY);
				}
			}
		}
	}

	public void Move2(int flag){
		
		SetMove(flag);
		
	}
}

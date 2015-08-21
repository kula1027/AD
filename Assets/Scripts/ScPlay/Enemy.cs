using UnityEngine;
using System.Collections;

public class Enemy : Entity {
	private int waitingFrame;
	
	public EnemyManager enemyManager;
	private Ai_Enemy ai;
	
	private int enemyCode;									//적 코드
	private int enemyLevel;									//적 레벨
	private bool isAlive;									//생물유무
	private bool attackFirst;								//선공여부
	private int moveType;									//이동방식유형
	private int appearFloorMin;								//출현 층 범위 최소값
	private int appearFloorMax;								//출현 층 범위 최대값
	private int appearFrequency;							//출현빈도
	private int[] dropItem = new int[Config.dropItemLimit];	//몬스터 드롭템
	private float dropExp;									//몬스터 드롭 경험치
	private int dropBottleNum;								//드랍하는 술병 최대 개수
	private int breakBottleNum;								//기본 공격 시 깨뜨리는 술병 수
	private int dropGold;									//드랍 골드량
	private bool isWaitingDead;								//사망대기일때는 Destroy를 시도하지않음
	
	public override void initChild(int enemyCode_){
		waitingFrame = 0;
		enemyCode = enemyCode_;
		enemyLevel = Config.enemyLevel[enemyCode];
		isAlive = Config.isAlive [enemyCode];
		attackFirst = Config.attackFirst [enemyCode];
		moveType = Config.moveType [enemyCode];
		appearFloorMin = Config.appearFloorMin [enemyCode];
		appearFloorMax = Config.appearFloorMax [enemyCode];
		appearFrequency = Config.appearFrequency [enemyCode];
		for(int i = 0 ; i< Config.dropItemLimit; i++)
		{
			dropItem[i] = 0;
		}
		dropExp = Config.dropExp[enemyCode];
		dropBottleNum = Config.dropBottleNum[enemyCode];
		breakBottleNum = Config.breakBottleNum [enemyCode];
		dropGold = Config.dropGold [enemyCode];
		isWaitingDead = false;
	}
	
	public void SetEnemyManager(EnemyManager e){
		enemyManager = e;
		ai = transform.GetComponent<Ai_Enemy>();
		//		ai.setTileInfo (enemyManager.boardManager.get_tileInfo(0));
		//		ai.InitAI (transform.position, gameObject);
	}

	public float GetDropExp(){
		return this.dropExp;
	}
	
	public void DestroyGameObject(){
		if (isWaitingDead) {
			return;
		}
		isWaitingDead = true;
		Vector3 pos = transform.position;

		//먼저 드랍된 아이템이 sorting Layer의 우선순위가 낮다. (나중에 드랍된 아이템이 위로 올라가게 보임.)

		if (dropGold != 0) {
			enemyManager.gameManager.makeItemGO (-dropGold, pos, "gold");									//골드를 드랍!
		}

		if (Random.Range (0, 2) < 1) {																		//50%확률
			if(Random.Range (0,100) < enemyManager.gameManager.playerScript.getRareItemRate100()){			//레어 아이템 드랍률
				enemyManager.gameManager.makeItemGO (1, pos, "ItemPrefab/" + Config.spriteName_Item[Random.Range(6,20)]);	//레어 아이템 드랍!

				GameObject canvas = Instantiate(Resources.Load("ValueDisplay", typeof(GameObject))) as GameObject;
				canvas.GetComponent<RectTransform>().localPosition = gameObject.transform.position;
				GameObject rareDropDisplay = Instantiate(Resources.Load("rareDropDisplay", typeof(GameObject))) as GameObject;
				rareDropDisplay.transform.SetParent(canvas.transform);
				rareDropDisplay.GetComponent<RectTransform> ().localPosition = new Vector3(0,1.5f,0);
				rareDropDisplay.GetComponent<UI_ValueDisplay> ().activeDisplay ();

				UI_Control.AddLog("레어 아이템 드랍!");
			}else{
				enemyManager.gameManager.makeItemGO (1, pos, "ItemPrefab/" + Config.spriteName_Item[Random.Range(0,6)]);	//일반 아이템 드랍!
			}
		}
		for(int i = 0; i < dropBottleNum ; i++){
			if(Random.Range (0, 2) < 1 || enemyManager.gameManager.playerScript.getEntityCode() == 4){		//50%확률 (투척병은 100%드랍)
				if(Random.Range (0,100) < enemyManager.gameManager.playerScript.getLabeledRate100()){		//라벨있는술 드랍확률
					//라벨있는 술을 드랍;
				}else{
					//라벨없는 술을 드랍;
				}
			}
		}

		(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ()) [((int)pos.y), ((int)pos.x)].e = null;		//Tile Info에서 등록 해제
		enemyManager.RemoveSlave(gameObject);
		StartCoroutine ("Die");
	}
	
	IEnumerator Die(){
		while (true) {
			if(waitingFrame > 1){
				Destroy(gameObject);
				yield break;
			}
			gameObject.transform.position = new Vector3(-100,-100,100);
			waitingFrame++;
			yield return null;
		}
	}
	
	public void Act(){
		if (isStun) {
			incTurnCount();
		} else if (isOoC) {
			incTurnCount();
			//todo
		} else {
			Vector3 playerPos = enemyManager.gameManager.player.transform.position;
			float distance = 0;
			distance = Vector3.Distance (playerPos, transform.position);
			//int sight = 4;	//sight라는 변수는 이미 Entity에 정의되어 있음.
			//Move ();
		
			if (distance < sight) {
				ai.setTileInfo (enemyManager.boardManager.get_tileInfo (0));
				ai.InitAI (transform.position, gameObject, sight);
				if(ai.get_Flag()<0){
					if(attackScript.attackable(-ai.get_Flag())){
						SetAttack(-ai.get_Flag());
					}
				}else{
					Move2 (ai.get_Flag ());
				}
			}
			else{
				incTurnCount();
			}
		}
		
	}
	
	public void Move(){
		Vector3 playerPos = enemyManager.gameManager.player.transform.position;
		playerPos = playerPos - transform.position;
		if(0.5f<playerPos.x){
			if(0.5f<playerPos.y){
				if(rightUp == KindTag.empty || rightUp == KindTag.item){
					SetMove(Direction.RIGHTUP);
				}
			}else if(playerPos.y<-0.5f){
				if(rightDown == KindTag.empty || rightDown == KindTag.item){
					SetMove(Direction.RIGHTDOWN);
				}
			}else{
				if(right == KindTag.empty || right == KindTag.item){
					SetMove(Direction.RIGHT);
				}
			}
		}else if(playerPos.x<-0.5f){
			if(0.5f<playerPos.y){
				if(leftUp == KindTag.empty || leftUp == KindTag.item){
					SetMove(Direction.LEFTUP);
				}
			}else if(playerPos.y<-0.5f){
				if(leftDown == KindTag.empty || leftDown == KindTag.item){
					SetMove(Direction.LEFTDOWN);
				}
			}else{
				if(left == KindTag.empty || left == KindTag.item){
					SetMove(Direction.LEFT);
				}
			}
		}else{
			if(0.5f<playerPos.y){
				if(up == KindTag.empty || up == KindTag.item){
					SetMove(Direction.UP);
				}
			}else if(playerPos.y<-0.5f){
				if(down == KindTag.empty || down == KindTag.item){
					SetMove(Direction.DOWN);
				}
			}else{
				{
					SetMove(Direction.STAY);
				}
			}
		}
	}
	
	public void Move2(int flag){
		//Tile info를 이동후 위치로 갱신한다.
		Vector3 pos = transform.position;
		(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[(int)pos.y,(int)pos.x].e=null;
		switch (flag) {
		case Direction.STAY:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[(int)pos.y,(int)pos.x].e=this;
			break;
		case Direction.LEFT		:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[(int)pos.y,((int)pos.x)-1].e=this;
			break;
		case Direction.LEFTUP	:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[((int)pos.y)+1,((int)pos.x)-1].e=this;
			break;
		case Direction.UP		:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[((int)pos.y)+1,(int)pos.x].e=this;
			break;
		case Direction.RIGHTUP	:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[((int)pos.y)+1,((int)pos.x)+1].e=this;
			break;
		case Direction.RIGHT	:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[(int)pos.y,((int)pos.x)+1].e=this;
			break;
		case Direction.RIGHTDOWN:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[((int)pos.y)-1,((int)pos.x)+1].e=this;
			break;
		case Direction.DOWN		:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[((int)pos.y)-1,(int)pos.x].e=this;
			break;
		case Direction.LEFTDOWN	:
			(enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].get_tileInfo ())[((int)pos.y)-1,((int)pos.x)-1].e=this;
			break;
		}
		//enemyManager.boardManager._Stage [enemyManager.gameManager.currStage].debugLog ();
		SetMove(flag);
	}

	protected override void hpBarUpdate(float rate){
		gameObject.transform.FindChild ("HUD").FindChild ("GreenBar").localScale = new Vector3 (rate, 1, 1);
	}
}

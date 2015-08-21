using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public static int charChoice = 3;		//무슨 캐릭터를 선택했는지
	
	public BoardManager boardManager;
	public EnemyManager enemyManager;		//현재 Stage의 EnemyManager

	public int turn = Turn.Select;			//turn은 유저의 입력을 받느냐 마느냐를 결정한다.
											//Select이면 입력을받고 Acting이면 입력을 받지않는다.
											//turnCount가 0이 되면 Select로 바뀐다.

	private bool buffTurn = true;			//buffTurn은 매턴단 한번씩 버프를 적용하는 시점을 결정하는 플래그 이다.
	private bool enemyBuffTurn = true;
	private bool isHangOver = false;		//숙취 상태 여부.

	public int currStage = 0;
	public Transform prefabPlayer;
	public GameObject player;
	public Player playerScript;
	public CameraControl cam;
	//private int level = 0;

	
	// Use this for initialization
	void Awake ()
	{
		boardManager = GetComponent<BoardManager> ();
		InitGame ();
		boardManager.loadLevel (0);
	}

	void InitGame ()
	{
		boardManager.SetupScene ();
		enemyManager = boardManager._Stage [currStage].enemyManager;
		player = (((Transform)Instantiate (prefabPlayer, transform.position, Quaternion.identity))).gameObject;
		player.transform.position = boardManager._Stage [currStage].PlayerSpawnPoint;
		switch (charChoice) {
		case 0:		//대작가
			player.GetComponent<Entity> ().init (IdInfo.DaeJak[0],IdInfo.DaeJak[1]);
			player.transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image_Character/" + Config.entityResources[0]);
			break;
		case 1:		//광전사
			player.GetComponent<Entity> ().init (IdInfo.GwangJun[0],IdInfo.GwangJun[1]);
			player.transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image_Character/" + Config.entityResources[1]);
			break;
		case 2:		//코브라
			player.GetComponent<Entity> ().init (IdInfo.Cobra[0],IdInfo.Cobra[1]);
			player.transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image_Character/" + Config.entityResources[2]);
			break;
		case 3:		//술병 싸움꾼
			player.GetComponent<Entity> ().init (IdInfo.SullSsa[0],IdInfo.SullSsa[1]);
			player.transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image_Character/" + Config.entityResources[3]);
			break;
		case 4:		//투척병
			player.GetComponent<Entity> ().init (IdInfo.TuChuck[0],IdInfo.TuChuck[1]);
			player.transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image_Character/" + Config.entityResources[4]);
			break;
		case 5:		//공사판 아재
			player.GetComponent<Entity> ().init (IdInfo.GongSa[0],IdInfo.GongSa[1]);
			player.transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image_Character/" + Config.entityResources[5]);
			break;
		case 6:		//소믈리에르
			player.GetComponent<Entity> ().init (IdInfo.Somul[0],IdInfo.Somul[1]);
			player.transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image_Character/" + Config.entityResources[6]);
			break;
		}
		cam.SetPlayer (player);
		//GameObject.Find ("Player").transform.position = new Vector3(10,10,0);
		enemyManager.CreateEnemy (currStage,Config.numOfEnemy[currStage]);
		enemyManager.IncAllTurnCount();
		playerScript = player.GetComponent<Player>();
		randomize ();
	}

	private void randomize(){
		RandomManager.randomizeBuff ();
		RandomManager.randomizeDegree ();
		RandomManager.randomizeDrinkSprite ();
	}

	public void stageChange(int stage){
		currStage = stage;
		enemyManager = boardManager._Stage [currStage].enemyManager;
	}

	public void HoldTurn(){		//한 턴 쉰다.
		turn = Turn.Acting;
		playerScript.incTurnCount ();
	}

	public string GetSkillName(int slotNum){
		return playerScript.skillManager.GetSkillName (slotNum);
	}

	public string GetSkillExplain(int slotNum){
		return playerScript.skillManager.GetSkillExplain(slotNum);
	}

	public string GetSkillEffect(int slotNum){
		return playerScript.skillManager.GetSkillEffect(slotNum);
	}

	public void makeItemGO (Item sc_item, Vector3 itemPos){
		TileInfo ti = boardManager._Stage [currStage].get_tileInfo ()[((int)itemPos.y), ((int)itemPos.x)];
		GameObject go_item = Instantiate (Resources.Load ("ItemPrefab/" + Config.spriteName_Item[sc_item.GetItemCode()], typeof(GameObject))) as GameObject;
		go_item.transform.position = itemPos;
		go_item.GetComponent<SpriteRenderer> ().sortingOrder = ti.i.Count+ti.d.Count;			//소팅 레이어 등록
		//Debug.Log ("sortingLayer : " + (ti.i.Count + ti.d.Count));
		sc_item.SetGameObject (go_item);
		ti.i.Add (sc_item);						//Tile Info에 등록
	}

/*	public void makeDrink (Item sc_drink, Vector3 drinkPos){
		//todo 미리 뿌려져있는 술과 겹쳐지게 해야함.
		TileInfo ti = boardManager._Stage [currStage].get_tileInfo ()[((int)drinkPos.y), ((int)drinkPos.x)];
		GameObject go_drink = Instantiate (Resources.Load ("Image_Drink/" + Config.spriteName_Item[sc_drink.GetItemCode()], typeof(GameObject))) as GameObject;
		go_drink.transform.position = drinkPos;
		ti.i.Add (sc_drink);					//Tile Info에 등록
	}*/

	public void makeItemGO (int itemType, Vector3 itemPos, string resourceName){

		TileInfo ti = boardManager._Stage [currStage].get_tileInfo ()[((int)itemPos.y), ((int)itemPos.x)];

		if (itemType < 0) {																			//골드일 경우
			GameObject go_gold = Instantiate (Resources.Load (resourceName, typeof(GameObject))) as GameObject;
			go_gold.GetComponent<ObjectID> ().secondCode = -itemType;
			go_gold.transform.position = itemPos;
			go_gold.GetComponent<SpriteRenderer> ().sortingOrder = ti.i.Count+ti.d.Count;			//소팅 레이어 등록
			Gold sc_gold = new Gold ();
			sc_gold.SetGameObject (go_gold);
			sc_gold.init (-1, -itemType, 0);
//			Debug.Log("make Item in " + itemPos.x + "," + itemPos.y);
			ti.i.Add (sc_gold);		//Tile Info에 드랍 골드 등록
		} else {
			switch(itemType){
			case 0:			//drink
				Debug.Log("Can't make drink here");
				break;
			case 1:			//weapon
				GameObject go_weapon = Instantiate (Resources.Load (resourceName, typeof(GameObject))) as GameObject;
				go_weapon.transform.position = itemPos;
				go_weapon.GetComponent<SpriteRenderer> ().sortingOrder = ti.i.Count+ti.d.Count;		//소팅 레이어 등록
				Weapon sc_weapon = new Weapon();
				sc_weapon.SetGameObject (go_weapon);
				ObjectID weaponId = go_weapon.GetComponent<ObjectID>();
				sc_weapon.init (weaponId.firstCode, weaponId.secondCode, weaponId.thirdCode);
				ti.i.Add (sc_weapon);		//Tile Info에 등록
				break;
			case 2:			//accessory
				GameObject go_acce = Instantiate (Resources.Load (resourceName, typeof(GameObject))) as GameObject;
				go_acce.transform.position = itemPos;
				go_acce.GetComponent<SpriteRenderer> ().sortingOrder = ti.i.Count+ti.d.Count;		//소팅 레이어 등록
				Accessory sc_acce = new Accessory();
				sc_acce.SetGameObject (go_acce);
				ObjectID acceId = go_acce.GetComponent<ObjectID>();
				sc_acce.init (acceId.firstCode, acceId.secondCode, acceId.thirdCode);
				ti.i.Add (sc_acce);		//Tile Info에 등록
				break;
			case 3:			//snack
				GameObject go_snack = Instantiate (Resources.Load (resourceName, typeof(GameObject))) as GameObject;
				go_snack.transform.position = itemPos;
				go_snack.GetComponent<SpriteRenderer> ().sortingOrder = ti.i.Count+ti.d.Count;		//소팅 레이어 등록
				Snack sc_snack = new Snack();
				sc_snack.SetGameObject (go_snack);
				ObjectID snackId = go_snack.GetComponent<ObjectID>();
				sc_snack.init (snackId.firstCode, snackId.secondCode, snackId.thirdCode);
				ti.i.Add (sc_snack);		//Tile Info에 등록
				break;
			case 4:			//alcoholBag
				GameObject go_abg = Instantiate (Resources.Load (resourceName, typeof(GameObject))) as GameObject;
				go_abg.transform.position = itemPos;
				go_abg.GetComponent<SpriteRenderer> ().sortingOrder = ti.i.Count+ti.d.Count;		//소팅 레이어 등록
				AlcoholBag sc_abg = new AlcoholBag();
				sc_abg.SetGameObject (go_abg);
				ObjectID abgId = go_abg.GetComponent<ObjectID>();
				sc_abg.init (abgId.firstCode, abgId.secondCode, abgId.thirdCode);
				ti.i.Add (sc_abg);			//Tile Info에 등록
				break;
			case 5:			//alcoholBottle
				GameObject go_abt = Instantiate (Resources.Load (resourceName, typeof(GameObject))) as GameObject;
				go_abt.transform.position = itemPos;
				go_abt.GetComponent<SpriteRenderer> ().sortingOrder = ti.i.Count+ti.d.Count;		//소팅 레이어 등록
				AlcoholBottle sc_abt = new AlcoholBottle();
				sc_abt.SetGameObject (go_abt);
				ObjectID abtId = go_abt.GetComponent<ObjectID>();
				sc_abt.init (abtId.firstCode, abtId.secondCode, abtId.thirdCode);
				ti.i.Add (sc_abt);			//Tile Info에 등록
				break;
			}
		}
	}

	public void takeItem (){
		Vector3 pos = playerScript.transform.position;
		TileInfo ti = (boardManager._Stage [currStage].get_tileInfo ())[((int)pos.y), ((int)pos.x)];
		List<Item> itemList = ti.i;
		for (int i = itemList.Count - 1; i >= 0 ; i--) {
			if(itemList[i].GetItemCode()<0){					//골드인경우
				playerScript.AddGold((Gold)(itemList[i]));		//플레이어의 템창 및 UI에 적용
				Destroy(itemList[i].GetGameObject());			//GameObject 디스트로이
				itemList.RemoveAt(i);							//TileInfo 에서 등록 해제
			}
			else{												//아이템인경우
				if(playerScript.AddItem(itemList[i])){			//플레이어의 템창 및 UI에 적용
					Destroy(itemList[i].GetGameObject());		//GameObject 디스트로이
					itemList.RemoveAt(i);						//TileInfo 에서 등록 해제
				}
			}
		}
	}

	public void take9Drink(){								//투척병 전용 (3x3)의 영역을 습득
		Vector3 pos = playerScript.transform.position;
		TileInfo[,] ti = new TileInfo[3, 3];
		for (int i = 0; i < 3; i++) {
			for(int j = 0 ; j < 3 ; j++){
				List<Drink> drinkList = (boardManager._Stage [currStage].get_tileInfo ())[((int)pos.y + i - 1), ((int)pos.x + j - 1)].d;
				for(int k = drinkList.Count-1 ; k >= 0 ; k--){
					if(playerScript.AddDrink((Drink)(drinkList[k]))){		//플레이어의 템창 및 UI에 적용
						Destroy(drinkList[k].GetGameObject());				//GameObject 디스트로이
						drinkList.RemoveAt(k);								//TileInfo 에서 등록 해제
					}
				}
			}
		}

	}

	public void takeDrink(){								//일반 캐릭터용
		Vector3 pos = playerScript.transform.position;
		TileInfo ti = (boardManager._Stage [currStage].get_tileInfo ())[((int)pos.y), ((int)pos.x)];
		List<Drink> drinkList = (boardManager._Stage [currStage].get_tileInfo ())[((int)pos.y), ((int)pos.x)].d;
		for(int i = drinkList.Count-1 ; i >= 0 ; i--){
			if(playerScript.getEntityCode()==6){						//소믈리에는 전부 술이 감정 된다.
				drinkList[i].hasEver = true;
			}
			playerScript.AddDrink((Drink)(drinkList[i]));				//플레이어의 템창 및 UI에 적용
			Destroy(drinkList[i].GetGameObject());						//GameObject 디스트로이
			drinkList.RemoveAt(i);										//TileInfo 에서 등록 해제
		}
	}

	//turn이 Select일 경우 공격 명령을 Accept한다. turn을 즉시 Acting으로 바꾸어준다.
	public void PlayerAttack(int attackFlag){
		if (turn == Turn.Select) {
			switch(attackFlag){
			case Direction.UP:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.DOWN:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.LEFT:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.RIGHT:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.LEFTDOWN:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.LEFTUP:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.RIGHTUP:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.RIGHTDOWN:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			}
		}
	}

	//turn이 Select일 경우 이동 명령을 Accept한다. turn을 즉시 Acting으로 바꾸어준다.
	public void PlayerMove(int moveFlag){
		if (turn == Turn.Select && playerScript.getTurnCount()==0) {

			switch(playerScript.skillStandBy){//온히트 쓰고 이상한곳 찍으면 스킬 취소및 코스트 지불.
			case -1:
				break;
			case 16:
				UI_Control.AddLog("허공에 연타를 날렸다!");
				turn = Turn.Acting;
				playerScript.skillManager.CancelSkill(16);
				playerScript.skillManager.payCost(16);
				playerScript.incTurnCount();
				return;
			case 17:
				UI_Control.AddLog("허공에 빈병을 내리쳤다!");
				turn = Turn.Acting;
				playerScript.skillManager.CancelSkill(17);
				playerScript.skillManager.payCost(17);
				playerScript.incTurnCount();
				return;
			}


			switch(moveFlag){
			case Direction.UP:
				//Debug.Log (localPlayer.up);
				if (playerScript.up == KindTag.empty || playerScript.up == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.DOWN:
				//Debug.Log (localPlayer.down);
				if (playerScript.down == KindTag.empty || playerScript.down == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.LEFT:
				//Debug.Log (localPlayer.left);
				if (playerScript.left == KindTag.empty || playerScript.left == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.RIGHT:
				//Debug.Log (localPlayer.right);
				if (playerScript.right == KindTag.empty || playerScript.right == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.LEFTDOWN:
				//Debug.Log (localPlayer.leftDown);
				if (playerScript.leftDown == KindTag.empty || playerScript.leftDown == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.LEFTUP:
				//Debug.Log (localPlayer.leftUp);
				if (playerScript.leftUp == KindTag.empty || playerScript.leftUp == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.RIGHTUP:
				//Debug.Log (localPlayer.rightUp);
				if (playerScript.rightUp == KindTag.empty || playerScript.rightUp == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.RIGHTDOWN:
				//Debug.Log (localPlayer.rightDown);
				if (playerScript.rightDown == KindTag.empty || playerScript.rightDown == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			}
		}
	}

	public void UseSkill(int btnNum){
		if (turn == Turn.Select && playerScript.getTurnCount () == 0) {
			player.GetComponent<SkillManager>().ActSkill(btnNum);
		}
	}

	void Update ()
	{
		//Debug.Log ("playerScript.isStay() " + playerScript.isStay());
		//Debug.Log ("playerScript.getTurnCount() " + playerScript.getTurnCount ());
		//Debug.Log ("enemyManager.IsAllStay() " + enemyManager.IsAllStay());
		//Debug.Log ("enemyManager.isAllNonZeroTurn()" + enemyManager.isAllNonZeroTurn ());


		//모든 Entity의 행동이 끝났는데 모두의 tunrCount가 양수일 경우,
		//모든 Entity의 turnCount를 1씩 감소시킨다.
		if (playerScript.isStay() && playerScript.getTurnCount() > 0 && enemyManager.IsAllStay() && enemyManager.isAllNonZeroTurn()) {
			playerScript.decTurnCount();
			enemyManager.decTurnCount();
		}

		if (buffTurn) {
			playerScript.decCoolTime();							//플레이어의 스킬들의 쿨타임을 1턴씩 감소시킴

			if(!isHangOver && playerScript.getCurrMp() == 0){									//숙취 처리
				if(playerScript.getEntityCode()!=4 && playerScript.getEntityCode()!=6){			//투척병과, 소믈리에는 상관없음.
					playerScript.buffManager.HangOver();
					isHangOver = true;
				}
			}else{
				if(isHangOver && playerScript.getCurrMp() > 0){
					if(playerScript.getEntityCode()!=4 && playerScript.getEntityCode()!=6){		//투척병과, 소믈리에는 상관없음.
						playerScript.buffManager.RemoveHangOver();
					}
				}
			}

			if(playerScript.regenHp == 0.2f){						//기본 체력 재생일 경우, 5턴당 1씩 적용.
				playerScript.regenHpBuffer += 0.2f;
				if(playerScript.regenHpBuffer >= 1){
					playerScript.incCurrHp(1);
					playerScript.regenHpBuffer = 0;
				}
			}else{													//그외 (취기 유지)의 경우
				playerScript.incCurrHp(playerScript.regenHp);		//턴당 HP변동 적용
			}
			playerScript.incCurrMp(playerScript.regenMp);			//턴당 MP변동 적용
			player.GetComponent<Buffmanager>().giveBuff();
			player.GetComponent<Buffmanager>().takeBuff();
			playerScript.gaugeUpdate();
			buffTurn = false;
		}

		//유저의 입력을 받지않는동안 Enemy를 계속 움직인다.
		if (turn == Turn.Acting) {
			if (playerScript.isStay() && playerScript.getTurnCount() == 0 && enemyManager.IsAllStay() && enemyBuffTurn) {
				enemyManager.EnemyGiveBuff();
				//Debug.Log("에네미 버프 콜");
				enemyBuffTurn = false;
			}
			enemyManager.EnemyAct ();
			//모든 Entity의 행동이 끝나고 플레이어의 turnCount가 0일경우,
			//유저의 입력을 받는 상태가 된다.
			if (playerScript.isStay() && playerScript.getTurnCount() == 0 && enemyManager.IsAllStay()) {
				if(playerScript.isStun){
					playerScript.incTurnCount();
				}else if(playerScript.isOoC){
					playerScript.incTurnCount();
					//todo 랜덤한 방향으로 한칸 이동. 그 곳에 공격대상이 있을 시 공격.
				}
				enemyManager.EnemyTakeBuff();
				turn = Turn.Select;
				buffTurn = true;
				enemyBuffTurn = true;
				if (isHangOver) {
					UI_Control.AddLog ("숙취로 능력이 저하된 상태다.");
				}
			}
		}
		Quit();
	}

	private void Quit(){
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}
}

public class Turn
{
	public const int Select = 0;
	public const int Acting = 1;
}
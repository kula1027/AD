using UnityEngine;
using System.Collections;

public class Player : Entity {
	
	public int charCode;														//플레이어 코드
	public int[] coolTime;														//스킬 쿨타임
	private int playerLevel;													//플레이어 레벨
	private float currExp;														//현재 누적 경험치
	private float fullExp;														//총 필요 경험치
	private Drink[] drink = new Drink[Config.inventoryRow * Config.inventoryCol];	//“drink”인벤토리 목록
	public Drink GetDrink(int index){
		return drink [index];
	}
	public void RemoveDrink(int index){
		drink [index] = null;
	}
	private Item[] equip = new Item[Config.inventoryRow * Config.inventoryCol];	//“equip”인벤토리 목록
	public Item GetItem(int index){
		return equip [index];
	}
	public void RemoveItem(int index){
		equip [index] = null;
	}
	
	public UI_Control ui_control;
	
	public override void initChild(int charCode_){
		ui_control = GameObject.Find ("UI Manager").GetComponent<UI_Control> ();
		ui_control.SetPlayer (GetComponent<Player> ());
		
		charCode = charCode_;
		coolTime = new int[5]{0,0,0,0,0};
		playerLevel = 1;
		currExp = 0;
		fullExp = Config.requiredExp [playerLevel - 1];
		for(int i = 0 ; i < Config.inventoryRow * Config.inventoryCol;i++){
			drink[i] = null;
			
		}
		for(int i = 0 ; i < Config.inventoryRow * Config.inventoryCol;i++){
			equip[i] = null;
		}
		equip [4] = new Gold ();
		equip [4].init (-1, 0, 0);
	}
	int temp = 0;
	void Update(){
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			Weapon tempItem = new Weapon();
			tempItem.init(2,2,2);
			AddItem(tempItem);
		}


		if (Input.GetKeyDown (KeyCode.Keypad2)) {
			Drink tempItem = new Drink();
			tempItem.init(temp++);
			AddDrink(tempItem);
		}
	}
	
	public void DestroyGameObject(){
		Destroy (gameObject);
	}
	
	public int getRareItemRate100(){
		return (int)(rareItemDropRate * 100);
	}
	
	public int getLabeledRate100(){
		return (int)(labeledAlcoholDropRate * 100);
	}

	public void DropItem(int index){
		//Debug.Log (index);
		GameObject.Find ("GameManager").GetComponent<GameManager> ().makeItemGO(equip[index] ,transform.position);
		GameObject.Find ("GameManager").GetComponent<GameManager> ().HoldTurn ();
		UI_Control.AddLog(equip[index].GetName() + "을(를) 버렸다.");
	}
	
	public void levelUp(int count){
		ui_control.LevelUp ();
		UI_Control.AddLog("레벨 업!!");
		float offSet = 2f + count * 0.5f;
		GameObject canvas = Instantiate(Resources.Load("ValueDisplay", typeof(GameObject))) as GameObject;
		canvas.GetComponent<RectTransform>().localPosition = gameObject.transform.position;
		GameObject damageDisplay2 = Instantiate(Resources.Load("levelUpDisplay", typeof(GameObject))) as GameObject;
		damageDisplay2.transform.SetParent(canvas.transform);
		damageDisplay2.GetComponent<RectTransform> ().localPosition = new Vector3(0,offSet,0);
		damageDisplay2.GetComponent<UI_ValueDisplay> ().activeDisplay ();
		incFullHp (2f);
		if((entityCode == 4) && (playerLevel%2==0)){		//투척병은 2레벨업당 Dex가 1 오른다.
			UI_Control.AddLog("일취월장 발동. 민첩 1상승!!");
			incDex(1f);
		}
		playerLevel++;
	}
	
	public void decCoolTime(){
		for (int i = 0; i < 5; i++) {
			coolTime[i]--;
			if(coolTime[i] < 0){
				coolTime[i] = 0;
			}
		}
	}
	
	public void incCurrExp(float exp){
		currExp += exp;
		int counter = 0;
		while (currExp > fullExp) {
			levelUp (counter);
			currExp = currExp - fullExp;
			fullExp = Config.requiredExp[playerLevel - 1];
			counter++;
		}
		gaugeUpdate ();
	}
	
	protected override void hpBarUpdate(float rate){
		UI_Gauge.SetPlayerHP(rate);
	}
	
	public void gaugeUpdate (){
		UI_Gauge.SetPlayerHP (currHp / fullHp);
		UI_Gauge.SetPlayerMP (currMp / fullMp);
		UI_Gauge.SetPlayerEXP(currExp / fullExp);
	}

	public bool AddItem(Item item_){
		int index = 0;
		for (int loop = 5; loop < Config.inventoryRow * Config.inventoryCol; loop++) {
			if(equip[loop] == null){
				index = loop;
				equip[loop] = item_;
				UI_Control.AddLog(item_.GetName() + "을(를) 획득하였다.");
				break;
			}
			if(loop == Config.inventoryRow * Config.inventoryCol - 1){
				UI_Control.AddLog("빈 자리가 없다!");
				return false;
			}
		}

		ui_control.AddItem (item_, index);
		return true;
	}

	public bool AddDrink(Drink drink_){
		int index = 0;
		
		if (entityCode == 4) {
			UI_Control.AddLog("금단증상 발동. 취기가 상승하였다!");
			incCurrMp (10f);
		}
		
		index = CheckSameDrink (drink_);
		if (index == -1) {
			index = CheckEmptySlot (drink_);
			if (index != -1) {
				ui_control.AddDrink (drink_, index);
			}
		} else {
			ui_control.AddDrink (drink_, index);
		}
		return true;
	}
	
	private int CheckSameDrink(Drink drink_){
		for (int loop = 5; loop < Config.inventoryRow * Config.inventoryCol; loop++) {
			if(drink[loop] == null)continue;
			if(drink[loop].GetDrinkCode() == drink_.GetDrinkCode()){
				drink[loop].SetCount(drink[loop].GetCount() + 1);
				UI_Control.AddLog(drink_.GetName() + "을(를) 획득하였다.");
				return loop;
			}
		}
		return -1;
	}
	
	private int CheckEmptySlot(Drink drink_){
		for (int loop = 5; loop < Config.inventoryRow * Config.inventoryCol; loop++) {
			if(drink[loop] == null){
				drink[loop] = drink_;
				UI_Control.AddLog(drink_.GetName() + "을(를) 획득하였다.");
				return loop;
			}
		}
		
		UI_Control.AddLog("빈 자리가 없다!");
		return -1;
	}

	public void SwapItem(int idx, int idx2){
		Item temp = equip[idx];
		equip [idx] = equip [idx2];
		equip [idx2] = temp;

	}

	public void SwapDrink(int idx, int idx2){
		Drink temp = drink[idx];
		drink [idx] = drink [idx2];
		drink [idx2] = temp;
	}

	public void AddGold(Gold gold){
		UI_Control.AddLog(gold.getValue() + " 골드를 획득하였다.");
		((Gold)equip [4]).incAmount (gold.getValue());
		ui_control.SetGold (((Gold)equip[4]).getValue());
	}


}

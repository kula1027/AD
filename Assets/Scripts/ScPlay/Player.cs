using UnityEngine;
using System.Collections;

public class Player : Entity {
	
	private int charCode;														//플레이어 코드
	private int exp;															//누적경험치
	private int playerLevel;													//플레이어 레벨
	private float trapFindRate;													//함정발견률
	private float cocktailSuccessRate;											//세이커(칵테일 제조기) 제작 성공률
	private float labeledAlcoholDropRate;										//라벨이 있는 술 드랍 확률
	private float rareItemDropRate;												//rareItem드랍확률
	private float luckZeroDeathRate;											//운이 0이 되어 다음턴에 죽을 확률
	private int[,] drink = new int[Config.inventoryRow, Config.inventoryCol];	//“drink”인벤토리 목록
	private int[,] equip = new int[Config.inventoryRow, Config.inventoryCol];	//“equip”인벤토리 목록

	//private Gauge hpBar;

	void Start () {
		//hpBar = GameObject.Find("Gauge_HP").GetComponent<Gauge>();
		//hpBar.InitValue(fullHp, currHp);
	}

	public override void initChild(int charCode_){
		charCode = charCode_;
		exp = 0;
		playerLevel = 1;
		trapFindRate = intelligence * Config.trapFindRatePerIntelligence;
		cocktailSuccessRate = 0.5f + intelligence * Config.cocktailSuccessRatePerIntelligence;
		labeledAlcoholDropRate = 0.05f + luck * Config.labeledAlcoholDropRatePerLuck;
		rareItemDropRate = 0.1f + luck * Config.rareItemDropRatePerLuck;
		luckZeroDeathRate = Config.luckZeroDeathRate;
		for(int i = 0 ; i < Config.inventoryRow;i++){
			for(int j = 0 ; j< Config.inventoryCol; j++){
				drink[i,j] = 0;
			}
		}
		for(int i = 0 ; i < Config.inventoryRow;i++){
			for(int j = 0 ; j< Config.inventoryCol; j++){
				equip[i,j] = 0;
			}
		}
	}

	void Update () {
	}
	
	protected override void hpBarUpdate(){
		//hpBar.SetValue(currHp);
	}


}

using UnityEngine;
using System.Collections;

public class Player : Entity {

	private int charCode;														//플레이어 코드
	private int exp;															//누적경험치
	private int playerLevel;													//플레이어 레벨
	private int intelligence;													//지식
	private int cocktailSuccessRate;											//세이커(칵테일 제조기) 제작 성공률
	private int cocktailSuccessRatePerIntelligence;								//세이커(칵테일 제조기)제작성공률 나누기 지식
	private int luck;															//운
	private int labeledAlcoholDropRate;											//라벨이 있는 술 드랍 확률
	private int labeledAlcoholDropRatePerLuck;									//라벨이있는술드랍확률 나누기 운
	private int criticalRate;													//크리티컬 확률
	private int criticalRatePerLuck;											//크리티컬확률 나누기 운
	private int absoluteAvoidRate;												//완전 회피율
	private int absoluteAvoidRatePerLuck;										//완전회피율 나누기 운
	private int luckZeroDeathRate;												//운이 0이 되어 다음턴에 죽을 확률
	private int[,] drink = new int[Config.inventoryRow, Config.inventoryCol];	//“drink”인벤토리 목록
	private int[,] equip = new int[Config.inventoryRow, Config.inventoryCol];	//“equip”인벤토리 목록

	private Gauge hpBar;

	void Start () {
		hpBar = GameObject.Find("Gauge_HP").GetComponent<Gauge>();
		hpBar.InitValue(fullHp, currHp);
	}

	public override void initChild(int charCode_){
		charCode = charCode_;
		exp = 0;
		playerLevel = 1;
		intelligence = Config.intelligence[charCode];
		cocktailSuccessRatePerIntelligence = Config.cocktailSuccessRatePerIntelligence[charCode] ;
		cocktailSuccessRate = intelligence * cocktailSuccessRatePerIntelligence;
		luck = Config.luck[charCode];
		labeledAlcoholDropRatePerLuck = Config.labeledAlcoholDropRatePerLuck[charCode];
		labeledAlcoholDropRate = luck * labeledAlcoholDropRatePerLuck;
		criticalRatePerLuck = Config.criticalRatePerLuck[charCode];
		criticalRate = luck * criticalRatePerLuck;
		luckZeroDeathRate = Config.luckZeroDeathRate[charCode];
		luckZeroDeathRate = luck * luckZeroDeathRate;
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
		hpBar.SetValue(currHp);
	}


}

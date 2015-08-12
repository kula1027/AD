using UnityEngine;
using System.Collections;

public class Config {
	public static readonly int inventoryRow = 4;							//인벤토리 열 수
	public static readonly int inventoryCol = 5;							//인벤토리 행 수
	public static readonly int buffLimit = 5 ;							//버프 최대 갯수
	public static readonly int dropItemLimit = 0 ;						//몬스터 드롭템 최대수
	
	//entity info
	public static readonly string[] name = new string[38]					//케릭터 이름
	{"대작가","광전사","코브라","술병싸움꾼","투척병","공사판아재","소믈리에르",		//player
		"술라임","술라임 5년산","술라임 10년산","술라임 20년산","술라임 30년산",			//enemy
		"개진상","어리버리한 모험가","킬러비-어","술뱀","압생티",
		"순디네","키아주몬","요호홋","술꾼 지옥","만취다고라",
		"코볼트 도굴꾼","코볼트 술병수집가","코볼트 좀도둑","식인효모","효모 골렘",
		"기생효모","어리버리한 유령","코볼트 원령","효모 유령","모험가 유령",
		"양조머신 R4D4","주정뱅이 과학자","술의 사도","술의 대리자","술의 시녀",
		"자동경비 술독 D-300"};
	public static readonly int[] fullHp = new int[38]						//HP 최대치
	{20,30,20,25,20,25,20,
		5,0,30,0,0,
		7,8,8,11,0,
		0,0,0,0,0,
		28,30,30,35,50,
		0,0,0,0,0,
		0,0,0,0,0,
		0};
	public static readonly int[] str = new int[38]							//힘
	{5,10,5,8,5,8,3,
		5,0,30,0,0,
		7,7,6,10,0,
		0,0,0,0,0,
		14,15,10,30,40,
		0,0,0,0,0,
		0,0,0,0,0,
		0};
	public static readonly int[] dex = new int[38]							//민첩
	{5,4,10,6,3,6,5,
		2,0,12,0,0,
		3,5,15,10,0,
		0,0,0,0,0,
		30,32,40,20,10,
		0,0,0,0,0,
		0,0,0,0,0,
		0};
	public static readonly float accuracyRatePerDex = 0.005f;					//명중률 나누기 민첩 (명중률 dex계수)
	public static readonly float avoidRatePerDex = 0.005f;						//회피율 나누기 민첩 (회피율 dex계수)
	public static readonly int[] intelligence = new int[38]					//지식
	{7,2,5,5,5,5,10,
		1,0,1,0,0,
		1,1,5,5,0,
		0,0,0,0,0,
		1,1,1,1,1,
		0,0,0,0,0,
		0,0,0,0,0,
		0};
	public static readonly int[] luck = new int[38]							//운
	{7,4,4,4,7,4,6,
		1,0,1,0,0,
		1,2,3,5,0,
		0,0,0,0,0,
		1,1,1,1,1,
		0,0,0,0,0,
		0,0,0,0,0,
		0};
	public static readonly int fullMp = 100;								//MP(취기) 최대치
	public static readonly int[] drunkenType = new int[38]					//주사유형(0=없음, 1=통제불능, 2=행동불능, 3=사망, 5=미정)
	{1,1,2,1,2,2,3,
		0,5,0,5,5,
		1,2,2,2,5,
		5,5,5,5,5,
		1,1,2,0,2,
		5,5,5,5,5,
		5,5,5,5,5,
		5};
	
	//player info
	public static readonly float trapFindRatePerIntelligence = 0.005f;			//함정발견률 나누기 지식
	public static readonly float cocktailSuccessRatePerIntelligence = 0.01f;	//세이커(칵테일 제조기)제작성공률 나누기 지식
	public static readonly float labeledAlcoholDropRatePerLuck = 0.005f;		//라벨이있는술드랍확률 나누기 운
	public static readonly float rareItemDropRatePerLuck = 0.005f;				//rareItem드랍확률 나누기 운
	public static readonly float criticalRatePerLuck = 0.005f;					//크리티컬확률 나누기 운
	public static readonly float absoluteAvoidRatePerLuck = 0.005f;			//완전회피율 나누기 운
	public static readonly float luckZeroDeathRate = 0.5f;						//운이 0이 되어 다음턴에 죽을 확률
	
	//enemy info
	public static readonly int[] enemyLevel = new int[31]					//적 레벨
	{1,5,10,20,30,
		2,2,3,3,0,
		0,0,0,0,0,
		11,13,15,17,19,
		0,0,0,0,0,
		0,0,0,0,0,
		0};
	public static readonly int[] dropExp = new int[31] {1,2,3,4,5,6,7,8,9,0,
														1,2,3,4,5,6,7,8,9,0,
														1,2,3,4,5,6,7,8,9,0,
														1};			//몬스터 드롭 경험치
	
	//buff info
	public static readonly int[] buffType = new int[3] {0,0,0};				//0=영구버프, 1=시한버프
	public static readonly int[] buffTime = new int[3] {10,0,0};			//버프 지속시간
	
	//item info
	public static readonly string[] itemName = new string[7]									//아이템 이름
	{"라그랑지안","취한 검","주정뱅이 검","대마법사의 지팡이","장팔사모 모형","술뱀채찍","마술쇼용 나이프 세트",	//weapon
	};																							//
	public static readonly int[] canBuy = new int[3] {0,0,0};				//
	public static readonly int[] itemPrice = new int[3] {0,0,0};			//상점 아이템 가격
	public static readonly int[] canSell = new int[3] {0,0,0};				//
	public static readonly int[] sellPrice = new int[3] {0,0,0};			//
	public static readonly int[] canEquip = new int[3] {0,0,0};				//
	
	//drink info
	public static readonly string[] drinkName = new string[3]{"c","b","a"};	//술 이름
	public static readonly int[] canBuyDrink = new int[3] {0,0,0};			//
	public static readonly int[] drinkPrice = new int[3] {0,0,0};			//
	public static readonly int[] canSellDrink = new int[3] {0,0,0};			//
	public static readonly int[] drinkSellPrice = new int[3] {0,0,0};		//
	public static readonly int[] degreeMin = new int[3] {0,0,0};			//도수범위 최소값
	public static readonly int[] degreeMax = new int[3] {0,0,0};			//도수범위 최대값
	public static readonly int[,] effectIndex = new int[4, 3]; 				//effectIndex[drinkCode, drinkLevel] => buffCode
	
}

public class IdInfo{
	public const int SullSsa = 3;
	public const int CoboltDogul = 22;
}

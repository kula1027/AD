using UnityEngine;
using System.Collections;

public class Config {
	public static readonly int inventoryRow = 4;							//인벤토리 열 수
	public static readonly int inventoryCol = 5;							//인벤토리 행 수
	public static readonly int buffLimit = 5 ;							//버프 최대 갯수
	public static readonly int dropItemLimit = 0 ;						//몬스터 드롭템 최대수

	//entity info
	public static readonly string[] name = new string[29]					//케릭터 이름
		{"대작가","광전사","코브라","술병싸움꾼","투척병","공사판아재","소믈리에르",		//player
		//0
		"술라임","술라임 10년산","술라임 20년산","어리버리한 모험가","킬러비-어",			//enemy
		//7
		"술뱀","압생티","순디네","술꾼 지옥","만취다고라",
		//12
		"코볼트 도굴꾼","코볼트 술병수집가","코볼트 좀도둑","식인효모","효모 골렘",
		//17
		"코볼트 원령","효모 유령","모험가 유령","양조머신 R4D4","주정뱅이 과학자",
		//22
		"술의 사도","술의 대리자"};
		//27
	public static readonly string[] entityResources = new string[29]						//엔타이티 프리팹 이름
		{null,null,null,"character_bottlebattler_01","character_thrower_01",null,null,
		//0
		"sulime_7_0","sulime_8_1","sulime_9_2","monster_naiveadventurer_1",null,
		//7
		null,null,null,null,null,
		//12
		"cobolt_17_10",null,null,null,null,
		//17
		null,null,null,null,null,
		//22
		null,null};
		//27
	public static readonly int[] fullHp = new int[29]						//HP 최대치
		{20,30,20,25,20,25,20,
		5,15,25,8,8,
		11,25,15,30,25,
		35,30,30,35,50,
		40,40,50,44,30,
		80,50};
	public static readonly int[] str = new int[29]							//힘
		{5,10,5,8,5,8,3,
		5,15,25,7,6,
		10,15,17,18,15,
		25,20,15,30,40,
		30,30,35,24,10,
		60,30};
	public static readonly int[] dex = new int[29]							//민첩
		{5,4,10,6,3,6,5,
		2,12,22,5,15,
		10,5,10,15,15,
		25,20,40,20,10,
		31,1,61,24,10,
		70,15};
	public static readonly float accuracyRatePerDex = 0.005f;				//명중률 나누기 민첩
	public static readonly float avoidRatePerDex = 0.005f;					//회피율 나누기 민첩
	public static readonly float[] intelligence = new float[29]				//지식
		{7,2,5,5,5,5,10,
		1,11,21,1,5,
		5,5,5,1,1,
		10,10,10,20,15,
		11,11,11,14,50,
		20,11};
	public static readonly float[] luck = new float[29]						//운
		{7,4,4,4,7,4,6,
		1,11,21,2,3,
		5,1,1,1,1,
		10,10,40,20,15,
		11,11,11,4,1,
		10,11};
	public static readonly float criticalRatePerLuck = 0.005f;				//크리티컬확률 나누기 운
	public static readonly float absoluteAvoidRatePerLuck = 0.005f;			//완전회피율 나누기 운
	public static readonly float fullMp = 100f;								//MP(취기) 최대치
	public static readonly int[] drunkenType = new int[29]					//주사유형(0=없음, 1=통제불능, 2=행동불능, 3=사망, 4=착지, 5=미정)
		{1,1,2,1,2,2,3,
		2,2,2,2,2,
		2,1,2,1,1,
		1,1,2,4,2,
		0,0,0,0,2,
		2,1};
	public static readonly int[] range = new int[29]						//사거리
		{1,1,1,1,1,1,1,
		1,1,1,1,1,
		1,1,1,1,1,
		1,1,1,1,2,
		1,1,1,4,1,
		3,1};
	public static readonly int[] sight = new int[29]						//시야
		{4,4,4,4,4,4,4,
		4,5,6,4,4,
		4,4,4,4,4,
		5,5,5,3,3,
		3,3,5,4,5,
		100,10};
	
	
	//player info
	public static readonly float trapFindRatePerIntelligence = 0.005f;			//함정발견률 나누기 지식
	public static readonly float cocktailSuccessRatePerIntelligence = 0.01f;	//세이커(칵테일 제조기)제작성공률 나누기 지식
	public static readonly float labeledAlcoholDropRatePerLuck = 0.005f;		//라벨이있는술드랍확률 나누기 운
	public static readonly float rareItemDropRatePerLuck = 0.005f;				//rareItem드랍확률 나누기 운
	public static readonly float luckZeroDeathRate = 0.5f;						//운이 0이 되어 다음턴에 죽을 확률
	
	//enemy info
	public static readonly int[] enemyLevel = new int[22]					//적 레벨
		{1,10,20,2,3,
		3,8,9,10,11,
		13,14,15,17,19,
		22,23,25,26,28,
		28,30};
	public static readonly bool[] isAlive = new bool[22]						//생물유무(true=생물, false=무생물)
		{true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,false,false,
		false,false,false,false,true,
		true,true};
	public static readonly bool[] attackFirst = new bool[22]					//선공여부(true=선공, false=비선공)
		{false,true,true,false,true,
		true,false,false,true,true,
		true,true,true,true,true,
		true,true,true,true,false,
		true,true};
	public static readonly int[] moveType = new int[22]						//이동방식유형(0=지상, 1=비행, 2=비행/지상)
		{0,0,0,0,1,
		0,0,0,0,0,
		0,0,0,2,0,
		1,1,1,0,1,
		1,0};
	public static readonly int[] appearFloorMin = new int[22]				//출현 층 범위 최소값
		{1,9,17,1,1,
		1,5,5,5,5,
		9,9,9,9,9,
		13,13,13,0,17,
		0,17};
	public static readonly int[] appearFloorMax = new int[22]				//출현 층 범위 최대값
		{20,20,20,4,4,
		4,8,8,8,8,
		12,12,12,12,12,
		16,16,16,0,20,
		0,20};
	public static readonly int[] appearFrequency = new int[22]				//출현빈도(0=하, 1=중, 2=상, 3=없음)
		{2,0,0,1,1,
		0,2,0,2,1,
		1,1,2,1,0,
		0,3,0,3,1,
		3,2};
	public static readonly int[] dropExp = new int[22]						//몬스터 드롭 경험치
		{1,10,20,2,3,
		3,10,12,13,15,
		20,22,23,25,30,
		28,29,30,44,60,
		120,80};
	public static readonly int[] dropBottleNum = new int[22]				//드랍하는 술병 최대 개수
		{1,2,3,1,1,
		1,2,2,1,1,
		1,3,2,0,0,
		1,1,1,5,2,
		2,5};
	public static readonly int[] breakBottleNum =  new int[22]				//기본 공격 시 깨뜨리는 술병 수
		{1,1,1,1,0,
		1,1,1,2,1,
		2,1,1,1,2,
		1,1,1,1,1,
		1,1};
	public static readonly int[] dropGold = new int[22]						//드랍 골드량
		{1,10,20,5,0,
		0,2,2,0,0,
		10,3,10,0,0,
		0,0,0,5,5,
		5,5};
	
	//buff info
	public static readonly string[] buffName = new string[]					//버프 이름
	{"Debug"};
	public static readonly int[] buffTime = new int[]						//버프 지속시간
		{0,5,5,5,5,
		1,5,5,2,5,
		5,1,5,5,1,
		1,5,2,2,5,
		3,1,5,5,5,
		5,5,5,5,1,
		1,-1,1,1,1,
		10,5,3,1,5,
		5,2,2,2,2,
		2,3,3,3,3,
		3,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,5,5,5};
	
	//item info

	//골드의 아이템 코드는 -1로 한다!
	public static readonly string[] itemName = new string[57]				//아이템 이름
		{"한손검","허니버터 한손검","마술쇼용 나이프 세트","강철 빠따","쌍절곤",			//weapon
		"숟가락","A.D.","주정뱅이","장팔사모 모형","술뱀채찍",
		"욕심쟁이 나무꾼의 쇠도끼","역행부 바그","스크램블 메이스","황금 벽돌","핏빛 방망이",
		"각설이의 엿가위","좌수의 흑룡검","우수의 백룡검","스매시 브라더스","돌주먹",

		"치느님","삼겹살","캐비아","안심 스테이크","홍탁삼합",							//snack
		"소시지","매운 닭발","올리브","광어 뫼니에르","해물파전",
		"약속의 증표","괴력의 장갑","마나의 비밀","고슴도치 신발","행운의 부적",			//accessory
		"붕대","소믈리에의 안경","사발대야","코볼트의 포대기","마스터피스",
		"성장형","돋보기 안경","초록 보석","푸른 빛 눈물","앉은뱅이 의자",
		"야행매복의 룬다","작은 네모","TRTT","메테오스","최후의 방패",
		"야구글러브","모에라에의 모래시계","술 먹는 하마","술친구","저주용 인형",
		"호리병",																//alcohol bottle
		"가죽 가방"};															//bag

	public static readonly string[] spriteName_Item = new string[57]{
		"weapon_sword", "weapon_honeysword", "weapon_throwingknife", "weapon_ironbat", "weapon_nunchuck", 
		"weapon_spoon", "weapon_ad", "weapon_drunkard", "weapon_samo", "weapon_snake",
		"weapon_axe", "weapon_bag", "weapon_mace", "weapon_goldenbrick", "weapon_bloodbat", 
		"weapon_scissors", "weapon_blackdragon", "weapon_whitedragon", "weapon_smash", "weapon_fist",

		null, null, null, null, null, 
		null, null, null, null, null, 
		null, null, null, null, null,
		null, null, null, null, null, 
		null, null, null, null, null,
		null, null, null, null, null, 
		null, null, null, null, null,
		null, null
	};

	public static readonly bool[] canBuy = new bool[57]						//true=구매가능, false=구매불가
		{true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,
		true};
	public static readonly int[] itemPrice = new int[57]					//구매시 아이템 가격
		{0,0,0,0,0,
		2,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,
		0};
	public static readonly bool[] canSell = new bool[57]					//true=판매가능, false=판매불가
		{true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,
		true};
	public static readonly int[] sellPrice = new int[57]					//판매시 아이템 가격
		{0,0,0,0,0,
		1,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,
		0};
	public static readonly bool[] canEquip = new bool[57]					//true=장비가능, false=장비불가
		{true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,true,true,true,true,
		true,
		true};
	public static readonly int[] itemClass = new int [57]					//등급(0=일반, 1=레어)
		{0,0,0,0,0,
		0,1,1,1,1,
		1,1,1,1,1,
		1,1,1,1,1,
		1,1,1,1,1,
		1,1,1,1,1,
		0,0,0,0,0,
		0,0,0,0,1,
		1,1,1,1,1,
		1,1,1,1,1,
		1,1,1,1,1,
		0,
		0};
	
	//equipment info
	
	//"weapon info
	public static readonly int[] attackPower = new int[20]					//공격력
		{3,3,2,4,5,
		0,8,8,5,3,
		5,5,6,4,5,
		3,10,10,3,8};
	public static readonly int[] weaponRange = new int[20]					//사거리
		{1,1,4,1,1,
		1,2,1,2,2,
		1,1,1,1,1,
		1,1,3,1,1};
	public static readonly bool[] isPierce = new bool[20]					//관통 여부
	   {true, true, false,true, true,
		true, true, true, true,false,
		true ,true, true, true, true,
		true ,true, true, true, true};
	
	//snack info
	//accessory info
	//alcohol bottle info
	//alcohol bag info
	
	//drink info
	public static readonly string[] drinkName = new string[66]				//술 이름
		{"맥주1 1단계","맥주1 2단계","맥주1 3단계",
		"맥주2 1단계","맥주2 2단계","맥주2 3단계",
		"맥주3 1단계","맥주3 2단계","맥주3 3단계",
		"맥주4 1단계","맥주4 2단계","맥주4 3단계",

		"소주1 1단계","소주1 2단계","소주1 3단계",
		"소주2 1단계","소주2 2단계","소주2 3단계",
		"소주3 1단계","소주3 2단계","소주3 3단계",
		"소주4 1단계","소주4 2단계","소주4 3단계",

		"양주1 1단계","양주1 2단계","양주1 3단계",
		"양주2 1단계","양주2 2단계","양주2 3단계",
		"양주3 1단계","양주3 2단계","양주3 3단계",
		"양주4 1단계","양주4 2단계","양주4 3단계",

		"와인1 1단계","와인1 2단계","와인1 3단계",
		"와인2 1단계","와인2 2단계","와인2 3단계",
		"와인3 1단계","와인3 2단계","와인3 3단계",
		"와인4 1단계","와인4 2단계","와인4 3단계",

		"막걸리1 1단계","막걸리1 2단계","막걸리1 3단계",
		"막걸리2 1단계","막걸리2 2단계","막걸리2 3단계",
		"막걸리3 1단계","막걸리3 2단계","막걸리3 3단계",
		"막걸리4 1단계","막걸리4 2단계","막걸리4 3단계",

		"궁극의 술 1단계","궁극의 술 2단계","궁극의 술 3단계",

		"빈병","빈병","빈병"};

	public static readonly string[] spriteName_Drink = new string[66]
	{
		"alcohol_beer_1", "alcohol_beer_1", "alcohol_beer_1", 
		"alcohol_beer_2", "alcohol_beer_2", "alcohol_beer_2",
		"alcohol_beer_3", "alcohol_beer_3", "alcohol_beer_3", 
		"alcohol_beer_4", "alcohol_beer_4", "alcohol_beer_4", 

		"alcohol_soju_1", "alcohol_soju_1", "alcohol_soju_1", 
		"alcohol_soju_2", "alcohol_soju_2", "alcohol_soju_2",
		"alcohol_soju_3", "alcohol_soju_3", "alcohol_soju_3",
		"alcohol_soju_4", "alcohol_soju_4", "alcohol_soju_4",

		"alcohol_brandy_1", "alcohol_brandy_1", "alcohol_brandy_1", 
		"alcohol_brandy_3", "alcohol_brandy_3", "alcohol_brandy_3",
		"alcohol_brandy_3", "alcohol_brandy_3", "alcohol_brandy_3", 
		"alcohol_brandy_4", "alcohol_brandy_4", "alcohol_brandy_4", 

		"alcohol_wine_1", "alcohol_wine_1", "alcohol_wine_1",
		"alcohol_wine_2", "alcohol_wine_2", "alcohol_wine_2",  
		"alcohol_wine_3", "alcohol_wine_3", "alcohol_wine_3",
		"alcohol_wine_4", "alcohol_wine_4", "alcohol_wine_4",

		"alcohol_makgeolli_1", "alcohol_makgeolli_1", "alcohol_makgeolli_1",
		"alcohol_makgeolli_2", "alcohol_makgeolli_2", "alcohol_makgeolli_2",  
		"alcohol_makgeolli_3", "alcohol_makgeolli_3", "alcohol_makgeolli_3", 
		"alcohol_makgeolli_4", "alcohol_makgeolli_4", "alcohol_makgeolli_4", 

		null, null, null, 

		null, null, null
	};
	public static readonly bool[] canBuyDrink = new bool[66]				//true=can, false=can't
		{true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true};
	public static readonly int[] drinkPrice = new int[66]					//구매시 술 가격
		{0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0};
	public static readonly bool[] canSellDrink = new bool[66]				//true=can, false=can't
		{true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true,
		true,true,true};
	public static readonly int[] drinkSellPrice = new int[66]				//판매시 술 가격
		{0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0,
		0,0,0};
	public static readonly int[] degreeMin = new int[66]					//도수범위 최소값
		{5,5,5,
		5,5,5,
		5,5,5,
		5,5,5,
		19,19,19,
		19,19,19,
		19,19,19,
		19,19,19,
		40,40,40,
		40,40,40,
		40,40,40,
		40,40,40,
		13,13,13,
		13,13,13,
		13,13,13,
		13,13,13,
		5,5,5,
		5,5,5,
		5,5,5,
		5,5,5,
		21,21,21,
		0,0,0};
	public static readonly int[] degreeMax = new int[66]					//도수범위 최대값
		{6,6,6,
		6,6,6,
		6,6,6,
		6,6,6,
		21,21,21,
		21,21,21,
		21,21,21,
		21,21,21,
		98,98,98,
		98,98,98,
		98,98,98,
		98,98,98,
		15,15,15,
		15,15,15,
		15,15,15,
		15,15,15,
		8,8,8,
		8,8,8,
		8,8,8,
		8,8,8,
		21,21,21,
		0,0,0};
	public static readonly int[,] effectIndex = new int[24, 3]; 				//effectIndex[drinkCode, drinkLevel] => buffCode

	//skill info
	public static readonly string[] skillName = new string [35]
	   {"정신 차리기", "자작하면 여친없음", "고집 피우기", "필름 끊기", "대작",
		"원기 회복", "광분", "피 폭발", "취기 유지", "분노",
		"사격", "불 뿜기", "부식성 사격", "속사", "체내 보관",
		"깨진 병", "연타", "빈병 내리치기", "병목 치기", "빈병 수집가",
		"탐색", "일취월장", "알코올의 부름", "금단 증상", "단호한 의지",
		"함정 해체", "함정 설치-화염 함정", "함정 설치-토 함정", "함정 설치-전기 함정", "함정 설치-대야 함정",
		"탐색", "라벨 바꾸기", "감별사", "유혹", "술의 가호"};

	public static readonly bool[] isPassive = new bool[35]
	   {false,false,false,false, true,
		false,false,false,true , true,
		false,false,false,false, true,
		false,false,false,false, true,
		false, true, true, true, true,
		false,false,false,false,false,
		false, true, true, true, true};

	public static readonly int[] coolTime = new int[35]
	   {0,5,3,0,0,
		5,5,0,0,0,
		0,0,0,0,0,
		-1,0,0,5,0,
		0,0,0,0,0,
		0,0,0,0,0,
		0,0,0,0,0,};

	public static readonly string[] skillExplain = new string [35]
	   {"정신 차리기", 
		"자작하면 여친없음", 
		"고집 피우기", 
		"필름 끊기", 
		"대작",

		"원기 회복", 
		"광분", 
		"피 폭발", 
		"취기 유지", 
		"분노",

		"사격", 
		"불 뿜기", 
		"부식성 사격", 
		"속사", 
		"체내 보관",

		"깨진 병에 찔리면 꼬메지도 못한다지.", 
		"키 큰 놈은 낼 수 없는 속도지!", 
		"이걸 이렇게도 깰 수 있지!", 
		"손날 격파! 니들도 이렇게 해줄까?", 
		"술을 마시고 마음에 드는 병을 모은다.",

		"술을 멀리하니 주변이 보이기 시작했다.", 
		"실력이 쌓이니 술병을 더 잘 던지겠군!", 
		"대게 다가오지 말란 말이다아아아앗!!!",
		"몸에 새겨진 기억", 
		"사악한 놈들! 저리 꺼져라! 꺼져!!",

		"함정 해체", 
		"함정 설치-화염 함정", 
		"함정 설치-토 함정", 
		"함정 설치-전기 함정", 
		"함정 설치-대야 함정",

		"소믈리에르는 주의력도 남다릅니다.", 
		"라벨을 바꿔치워서 비싸게 팔아보자.", 
		"소믈리에르의 기본중의 기본.", 
		"이거 줄테니 저리로 가.", 
		"술과의 연대감이 강해지면 당신도 가능해집니다."};
		
	public static readonly string[] skillEffect = new string [35]
		{"액티브 스킬\n현재 HP 10% 소모\n쿨타임 없음\n\n취기가 0이 된다.", 
		"액티브 스킬\n취기 20 소모\n쿨타임 5턴\n\n자신에게 \"분노\"를 건다.\n\n*분노 : 뭐였더라 (나중에 고침)", 
		"액티브 스킬\n취기 80 소모\n쿨타임 3턴\n\n자신에게 \"고집\"을 건다.\n\n*고집 : 3턴 간 무적.", 
		"액티브 스킬\n모든 취기 소모\n쿨타임 없음\n\n해당 층의 랜덤한 방으로 이동한다.", 
		"패시브 스킬\n\n최대 취기 +100",

		"액티브 스킬\n모든 취기 소모\n쿨타임 5턴\n\n소모한 취기수치 이하 랜덤으로 체력을 회복한다.",
		"광분",
		"피 폭발", 
		"취기 유지", 
		"분노",

		"사격", 
		"불 뿜기", 
		"부식성 사격", 
		"속사", 
		"체내 보관",

		"액티브 스킬\n빈병 1개 소모\n쿨타임 없음\n\n자신에게 \"깨진 병\"을 건다.\n\n*깨진 병 : 다음 공격 시 3 의 추가 데미지.", 
		"액티브 스킬\n빈병 2개 소모\n쿨타임 없음\n\n기본 공격의 90% 피해로 2번 공격한다.", 
		"액티브 스킬\n빈병 1개 소모\n쿨타임 없음\n\n근접 1칸, 기본 공격의 120% 데미지로 공격한다.\n적은 3턴 간 행동불능이 된다.", 
		"액티브 스킬\n빈병 1개 소모\n쿨타임 5턴\n\n주변 5X5에 있는 적에게 5턴 간 \"공포\"를 건다.\n\n10% 확률로 자신의 체력 5% 감소.", 
		"패시브 스킬\n\n술을 마실 때 마다 \"빈 병\"을 생성한다.",

		"액티브 스킬\n소모값 없음\n쿨타임 없음\n\n주변 3X3에 숨겨진 문이나 함정이 있으면 발견된다.", 
		"패시브 스킬\n\n2레벨 당 민첩이 1 오른다.", 
		"패시브 스킬\n\n술 드랍률이 100%가 된다.\n주변 3X3의 술을 자동으로 습득한다.", 
		"패시브 스킬\n\n술 습득 시 1병 당 취기 +10", 
		"패시브 스킬\n\n술 투척 가능, 술 1회 투척 시 취기 -10",

		"함정 해체", 
		"함정 설치-화염 함정", 
		"함정 설치-토 함정",
		"함정 설치-전기 함정", 
		"함정 설치-대야 함정",

		"액티브 스킬\n소모값 없음\n쿨타임 없음\n\n주변 3X3에 숨겨진 문이나 함정이 있으면 발견된다.", 
		"패시브 스킬\n\n모든 술의 판매 가격이 5배 증가한다.", 
		"패시브 스킬\n\n모든 술은 얻는 즉시 감정된다.", 
		"패시브 스킬\n\n술을 8방향중 한 방향으로 2칸 거리에 던질 수 있다.\n모든 몬스터는 던진 술을 마시러간다.", 
		"패시브 스킬\n\n죽음에 이르는 피해를 받으면, 보유한 모든 술이 깨지고 체력 1로 살아남는다."};

	//exp info
	public static readonly float[] requiredExp = new float[50]
	   {12,21,30,39,49,
		58,68,79,89,100,
		111,122,134,146,158,
		171,184,197,211,225,
		240,255,271,287,303,
		321,338,357,375,395,
		415,436,458,481,504,
		529,554,581,608,637,
		667,698,731,765,801,
		839,878,920,963,1009};

	//stage info
	public static readonly int[] numOfEnemy = new int[20]
		{20,20,20,20,20,
		20,20,20,20,20,
		20,20,20,20,20,
		20,20,20,20,20};
}

public class IdInfo{
	public static readonly int[] DaeJak = new int[2]{0,0};
	public static readonly int[] GwangJun = new int[2]{1,1};
	public static readonly int[] Cobra = new int[2]{2,2};
	public static readonly int[] SullSsa = new int[2]{3,3};
	public static readonly int[] TuChuck = new int[2]{4,4};
	public static readonly int[] GongSa = new int[2]{5,5};
	public static readonly int[] Somul = new int[2]{6,6};
}

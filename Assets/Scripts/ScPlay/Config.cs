using UnityEngine;
using System.Collections;

public class Config {
	public static readonly int inventoryRow = 4;											//인벤토리 열 수
	public static readonly int inventoryCol = 5;											//인벤토리 행 수
	public static readonly int buffLimit = 5 ;												//버프 최대 갯수
	public static readonly int dropItemLimit = 0 ;											//몬스터 드롭템 최대수

	//entity variable
	public static readonly string[] name = new string[3]{"Debug","test2","test3"};						//케릭터 이름
	public static readonly int[] fullHp = new int[3]{1000,0,0};								//HP 최대치
	public static readonly int[] str = new int[3]{10,0,0};									//힘
	public static readonly int[] dex = new int[3]{10,0,0};									//민첩
	public static readonly int[] accuracyRatePerDex = new int[3]{10,0,0};					//명중률 나누기 민첩
	public static readonly int[] avoidRatePerDex = new int[3]{10,0,0};						//회피율 나누기 민첩
	public static readonly int[] fullMp = new int[3]{10,0,0};								//MP 최대치
	public static readonly int[] drunkenType = new int[3]{10,0,0};							//주사유형

	//character variable
	public static readonly int[] intelligence = new int[3]{10,0,0};							//지식
	public static readonly int[] cocktailSuccessRatePerIntelligence = new int[3]{10,0,0};	//세이커(칵테일 제조기)제작성공률 나누기 지식
	public static readonly int[] luck = new int[3]{10,0,0};									//운
	public static readonly int[] labeledAlcoholDropRatePerLuck = new int[3]{10,0,0};			//라벨이있는술드랍확률 나누기 운
	public static readonly int[] criticalRatePerLuck = new int[3]{10,0,0};					//크리티컬확률 나누기 운
	public static readonly int[] absoluteAvoidRatePerLuck = new int[3]{10,0,0};				//완전회피율 나누기 운
	public static readonly int[] luckZeroDeathRate = new int[3]{10,0,0};						//운이 0이 되어 다음턴에 죽을 확률

	//enemy variable
	public static readonly int[] enemyLevel = new int[3] {10,0,0};							//적 레벨
	public static readonly int[] dropExp = new int[3] {10,0,0};								//몬스터 드롭 경험치

	//buff variable
	public static readonly int[] buffTime = new int[3] {10,0,0};								//버프 지속시간

	//item variable
	public static readonly int[] itemPrice = new int[3] {10,0,0};							//상점 아이템 가격
}

public class IdInfo{
	public const int DEBUG = 0;
}

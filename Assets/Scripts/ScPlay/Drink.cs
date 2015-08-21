using UnityEngine;
using System.Collections;

public class Drink
{
	public GameObject objInMap;		//맵에 깔리는 실제 오브젝트

	public static int numOfDrink = 66;
	public static int[] randomDegree = new int[numOfDrink];

	protected string drinkName;													//drink 이름
	public string GetName(){
		return drinkName;
	}
	protected int drinkCode;
	public int GetDrinkCode(){
		return drinkCode;
	}
	protected bool canBuyDrink;
	protected int drinkPrice;
	protected bool canSellDrink;
	protected int drinkSellPrice;
	protected int degree;														//술의 도수
	protected int drinkLevel;													//술의 단계
	protected Sprite sprite;
	public Sprite GetSprite(){
		return sprite;
	}
	protected int count;
	public int GetCount(){
		return count;
	}
	public void SetCount(int value){
		count = value;
	}
	public bool isLabeled;														//라벨 여부
	public bool hasEver;														//너, 이거, 마셔보았니?
	

	public Drink(){
	
	}

	public void init(int drinkCode_)
	{
		drinkCode = drinkCode_;
		drinkName = Config.drinkName[drinkCode];
		canBuyDrink = Config.canBuyDrink[drinkCode];
		drinkPrice = Config.drinkPrice[drinkCode];
		canSellDrink = Config.canSellDrink[drinkCode];
		drinkSellPrice = Config.drinkSellPrice[drinkCode];
		degree = Random.Range (Config.degreeMin[drinkCode], Config.degreeMax[drinkCode]);
		drinkLevel = 1;
		count = 1;
		isLabeled = false;
		hasEver = false;
		sprite = Resources.Load<Sprite> ("Image_Drink/" + Config.spriteName_Drink[drinkCode]);
	}

	public void SetGameObject(GameObject obj){
		objInMap = obj;
	}
	
	public GameObject GetGameObject(){
		return objInMap;
	}
}

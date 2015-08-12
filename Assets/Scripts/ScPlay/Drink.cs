using UnityEngine;
using System.Collections;

public class Drink
{
	protected string drinkName;													//drink 이름
	protected int drinkCode;
	protected bool canBuyDrink;
	protected int drinkPrice;
	protected bool canSellDrink;
	protected int drinkSellPrice;
	protected int degree;														//술의 도수
	protected int drinkLevel;													//술의 단계

	public Drink(int drinkCode_)
	{
		drinkCode = drinkCode_;
		drinkName = Config.drinkName[drinkCode];
		//canBuyDrink = Config.canBuyDrink[drinkCode];
		drinkPrice = Config.drinkPrice[drinkCode];
		//canSellDrink = Config.canSellDrink[drinkCode];
		drinkSellPrice = Config.drinkSellPrice[drinkCode];
		degree = Random.Range (Config.degreeMin[drinkCode], Config.degreeMax[drinkCode]);
		drinkLevel = 1;
	}
}

using UnityEngine;
using System.Collections;

public class Item
{
	protected string itemName;		//item 이름
	protected int itemCode;
	protected bool canBuy;
	protected int itemPrice;
	protected bool canSell;
	protected int sellPrice;
	protected bool canEquip;	//기획서 28슬라이드((인벤토리의)첫 줄의 1~4칸은 장비 칸으로 사용된다)에서 장비템과 아닌것으로 구별하기 위함

	public void initChild(int itemCode_)
	{
		itemCode = itemCode_;
		itemName = Config.itemName[itemCode];
//		canBuy = Config.canBuy[itemCode];
		itemPrice = Config.itemPrice[itemCode];
//		canSell = Config.canSell[itemCode];
		sellPrice = Config.sellPrice[itemCode];
//		canEquip = Config.canEquip[itemCode];
	}
}

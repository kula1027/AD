using UnityEngine;
using System.Collections;

public class Item
{
	public GameObject objInMap;		//맵에 깔리는 실제 오브젝트

	protected string itemName;		//item 이름
	protected int itemCode;
	protected bool canBuy;
	protected int itemPrice;
	protected bool canSell;
	protected int sellPrice;
	protected bool canEquip;	//기획서 28슬라이드((인벤토리의)첫 줄의 1~4칸은 장비 칸으로 사용된다)에서 장비템과 아닌것으로 구별하기 위함
	protected int itemClass;	//등급
	protected Sprite sprite;

	public Item (){

	}

	public void SetGameObject(GameObject obj){
		objInMap = obj;
	}

	public GameObject GetGameObject(){
		return objInMap;
	}

	public void init(int itemCode_, int childCode_, int grandChildCode_)
	{
		itemCode = itemCode_;

		if (itemCode_ < 0) {		//아이템 코드가 음수이면 골드
			itemName = "gold";
		} else {
			itemName = Config.itemName [itemCode];
			canBuy = Config.canBuy [itemCode];
			itemPrice = Config.itemPrice [itemCode];
			canSell = Config.canSell [itemCode];
			sellPrice = Config.sellPrice [itemCode];
			canEquip = Config.canEquip [itemCode];
			itemClass = Config.itemClass [itemCode];
			sprite = Resources.Load <Sprite> ("Image_Item/" + Config.spriteName_Item [itemCode_]);
		}

		initChild (childCode_, grandChildCode_);
	}

	public virtual void initChild(int childCode_, int grandChildCode_){

	}

	public int GetItemCode(){
		return itemCode;
	}
	public Sprite GetSprite(){
		return sprite;
	}
	public string GetName(){
		return itemName;
	}
}

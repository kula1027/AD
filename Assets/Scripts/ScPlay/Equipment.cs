using UnityEngine;
using System.Collections;

public class Equipment : Item 
{
	protected int equipmentCode;
	protected bool isEquip;

	public void initEquipment (int equipmentCode, int itemCode)
	{
		isEquip = false;
		initChild (itemCode);
	}
	public void initChild (int itemCode){
	}
}

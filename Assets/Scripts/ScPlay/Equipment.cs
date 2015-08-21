using UnityEngine;
using System.Collections;

public class Equipment : Item 
{
	protected int equipmentCode;
	protected bool isEquip;

	public override void initChild (int equipmentCode_, int grandChildCode)
	{
		equipmentCode = equipmentCode_;
		isEquip = false;
		initGrandChild (grandChildCode);
	}

	public virtual void initGrandChild (int grandChildCode){
	}
}

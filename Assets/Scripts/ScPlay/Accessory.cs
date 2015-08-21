using UnityEngine;
using System.Collections;

public class Accessory : Equipment 
{
	private int accessoryCode;

	public void initGrandChild (int accessoryCode_)
	{
		accessoryCode = accessoryCode_;
	}
}

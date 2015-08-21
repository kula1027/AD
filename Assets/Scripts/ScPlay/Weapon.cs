using UnityEngine;
using System.Collections;

public class Weapon : Equipment
{
	public int weaponCode;
	public int attackPower;
	public int weaponRange;
	public bool isPierce;

	public override void initGrandChild (int weaponCode_)
	{
		weaponCode = weaponCode_;
		attackPower = Config.attackPower [weaponCode];
		weaponRange = Config.weaponRange [weaponCode];
		isPierce = Config.isPierce [weaponCode];
	}
}

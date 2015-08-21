using UnityEngine;
using System.Collections;

public class Skill{

	public int skillCode;
	public string skillName;
	public int coolTime;
	public string explain;
	public string effect;
	public bool isPassive;

	public Skill(int skillCode_){
		skillCode = skillCode_;
		skillName = Config.skillName [skillCode];
		coolTime = Config.coolTime [skillCode];
		explain = Config.skillExplain [skillCode];
		effect = Config.skillEffect [skillCode];
		isPassive = Config.isPassive [skillCode];
	}


}


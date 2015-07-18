using UnityEngine;
using System.Collections;

public class Buff{
	public int buffCode;
	public int remainTime;

	public Buff(){
		buffCode=0;
		remainTime=0;
	}

	public Buff(int buffCode_){
		buffCode=buffCode_;
		remainTime=Config.buffTime[buffCode];
	}
}
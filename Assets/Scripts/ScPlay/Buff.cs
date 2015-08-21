using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buff{
	public string buffName;
	public int buffCode;
	public int remainTime;
	public bool needExcute;		//버프를 줘야 하는가, (줬다 뺐는버프에서 이미 줬다면 false)
	public List<float> valueMemory;

	public Buff(){
		buffCode=0;
		remainTime=0;
		needExcute = true;
		valueMemory = new List<float>();
	}

	public Buff(int buffCode_){
		buffCode=buffCode_;
		remainTime=Config.buffTime[buffCode];
		needExcute = true;
		valueMemory = new List<float>();
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buffmanager {

	public void addBuff (Entity obj, int buffCode_){//버프 추가
		if(obj.buffList.Count>=Config.buffLimit){
			return;
		}
		obj.buffList.Add(new Buff(buffCode_));
	}

	public void giveBuff(Entity obj){//효과 적용

		for (int i=0; i<obj.buffList.Count; i++){
			switch (obj.buffList[i].buffCode) {
			case 1 : 
				obj.RegenHp(10);
				break;
			case 2 :
				break;
		
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buffmanager : MonoBehaviour {

	public static int numOfBuff = 300;					//모든 버프의 최대 갯수.
	public static bool[] randomBuff = new bool[numOfBuff];
	private bool isHangOver;
	private float[] hangOverBuffer = new float[]{0,0,0,0};
	public Entity master;
	

	void Start(){
		master = gameObject.GetComponent<Entity> ();
		isHangOver = false;
	}

	public void addPassive( int passiveCode){
		master.passiveList.Add (new Buff (passiveCode));
	}

	public void addBuff ( int buffCode_){//버프 추가
		if(master.buffList.Count>=Config.buffLimit){
			takeBuff(master.buffList[0].buffCode);				//todo!!!!!!!! 버프 강제 해제는 아직 구현되지 않았음.
			master.buffList.RemoveAt(0);
			master.buffList.Add(new Buff(buffCode_));
			return;
		}
		master.buffList.Add(new Buff(buffCode_));
	}

	public void ImmediatePassive(int passiveCode){				//사용이 필요없을수도 있음.
		master.passiveList.Add (new Buff (passiveCode));
		switch (master.passiveList[master.passiveList.Count-1].buffCode) {
		case 29:	//통제불능 1턴
			master.isOoC = true;
			break;
		case 30:	//행동불능 1턴
			master.isStun = true;
			break;
		case 41:	//통제불능 2턴
			master.isOoC = true;
			break;
		case 42:	//행동불능 2턴
			master.isStun = true;
			break;
		case 46:	//통제불능 3턴
			master.isOoC = true;
			break;
		case 47:	//행동불능 3턴
			master.isStun = true;
			//Debug.Log ("enemy isStun" + master.isStun);
			break;
		}
		master.passiveList[master.passiveList.Count-1].needExcute = false;
	}

	public void HangOver(){
		UI_Control.AddLog ("술이 깨니 숙취가 몰려온다.");

		hangOverBuffer[0]=(master.getStr()*0.2f);
		hangOverBuffer[1]=(master.getDex()*0.2f);
		hangOverBuffer[2]=(master.getIntelligence()*0.2f);
		hangOverBuffer[3]=(master.getLuck()*0.2f);
		master.incStr(-hangOverBuffer[0]);
		master.incDex(-hangOverBuffer[1]);
		master.incIntelligence(-hangOverBuffer[2]);
		master.incLuck (-hangOverBuffer[3]);

		isHangOver = true;
	}

	public void RemoveHangOver(){
		UI_Control.AddLog ("취기가 오르니 숙취가 사라졌다.");

		master.incStr(hangOverBuffer[0]);
		master.incDex(hangOverBuffer[1]);
		master.incIntelligence(hangOverBuffer[2]);
		master.incLuck (hangOverBuffer[3]);

		for (int i = 0; i < 4; i++) {
			hangOverBuffer[i] = 0;
		}

		isHangOver = false;
	}

	private void givePassive(){
		for (int i = 0; i < master.passiveList.Count; i++) {
			if(master.passiveList[i].needExcute){
				switch(master.passiveList[i].buffCode){
				case 29:	//통제불능 1턴
					master.isOoC = true;
					break;
				case 30:	//행동불능 1턴
					master.isStun = true;
					break;
				case 35:	//주량
					break;
				case 41:	//통제불능 2턴
					master.isOoC = true;
					break;
				case 42:	//행동불능 2턴
					master.isStun = true;
					break;
				case 46:	//통제불능 3턴
					master.isOoC = true;
					break;
				case 47:	//행동불능 3턴
					master.isStun = true;
					//Debug.Log("enemy isStun" + master.isStun);
					break;
				case 51:	//취기유지
					if(master.isSustain){
						master.isSustain =false;
						master.regenHp = 1;
						master.regenMp = -2;
					}else{
						master.isSustain =true;
						master.regenHp = -1;
						master.regenMp = 0;
					}
					break;
				}
				master.passiveList[i].needExcute = false;
			}
			master.passiveList[i].remainTime--;
		}

		int counter = 0;

		// take passive 패시브 해제
		for (int i = master.passiveList.Count-1; i >= 0 ; i--) {
			if(master.passiveList[i].remainTime < 0){
				switch(master.passiveList[i].buffCode){
				case 29:	//통제불능 1턴
					counter = 0;
					for(int j = 0 ; j < master.passiveList.Count ; j++ ){
						if(master.passiveList[j].buffCode == 29 || master.passiveList[j].buffCode == 41 || master.passiveList[j].buffCode == 46){
							counter++;	//현재 걸려있는 행동불능의 갯수
						}
					}
					if(counter == 1){
						master.isOoC = false;
					}
					break;
				case 30:	//행동불능 1턴
					counter = 0;
					for(int j = 0 ; j < master.passiveList.Count ; j++ ){
						if(master.passiveList[j].buffCode == 30 || master.passiveList[j].buffCode == 42 || master.passiveList[j].buffCode == 47){
							counter++;	//현재 걸려있는 행동불능의 갯수
						}
					}
					if(counter == 1){
						master.isStun = false;
					}
					break;
				case 35:	//주량
					master.incAlcoholStack(-1);
					master.passiveList[i].remainTime=10;
					break;
				case 41:	//통제불능 2턴
					counter = 0;
					for(int j = 0 ; j < master.passiveList.Count ; j++ ){
						if(master.passiveList[j].buffCode == 29 || master.passiveList[j].buffCode == 41 || master.passiveList[j].buffCode == 46){
							counter++;	//현재 걸려있는 행동불능의 갯수
						}
					}
					if(counter == 1){
						master.isOoC = false;
					}
					break;
				case 42:	//행동불능 2턴
					counter = 0;
					for(int j = 0 ; j < master.passiveList.Count ; j++ ){
						if(master.passiveList[j].buffCode == 30 || master.passiveList[j].buffCode == 42 || master.passiveList[j].buffCode == 47){
							counter++;	//현재 걸려있는 행동불능의 갯수
						}
					}
					if(counter == 1){
						master.isStun = false;
					}
					break;
				case 46:	//통제불능 3턴
					counter = 0;
					for(int j = 0 ; j < master.passiveList.Count ; j++ ){
						if(master.passiveList[j].buffCode == 29 || master.passiveList[j].buffCode == 41 || master.passiveList[j].buffCode == 46){
							counter++;	//현재 걸려있는 행동불능의 갯수
						}
					}
					if(counter == 1){
						master.isOoC = false;
					}
					break;
				case 47:	//행동불능 3턴
					counter = 0;
					for(int j = 0 ; j < master.passiveList.Count ; j++ ){
						if(master.passiveList[j].buffCode == 30 || master.passiveList[j].buffCode == 42 || master.passiveList[j].buffCode == 47){
							counter++;	//현재 걸려있는 행동불능의 갯수
						}
					}
					if(counter == 1){
						master.isStun = false;
					}
					break;
				case 51:	//취기유지
					master.passiveList[i].remainTime++;
					break;
				}
				if(master.passiveList[i].buffCode!=51){
					master.passiveList.RemoveAt(i);
				}
			}
		}
	}
	
	public void giveBuff(){//효과 적용

		givePassive ();
		
		for (int i=0; i<master.buffList.Count; i++){

			if(master.buffList[i].needExcute){

				switch (master.buffList[i].buffCode) {
				/*************************일시 버프*************************/

							//술이름 단계-종류 : 버프내용

				case 1:		//맥주 1-4 : 5턴간 공격 시 자신의 HP 5 감소
					master.incBloodSuck(-5f);
					break;
				case 2:		//맥주 2-4 : 5턴간 공격 시 자신의 HP 10 회복 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incBloodSuck(10f);
					}else{
						master.incBloodSuck(-10f);
					}
					break;
				case 3:		//맥주 3-4 : 5턴간 공격 시 자신의 HP 15 회복 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incBloodSuck(15f);
					}else{
						master.incBloodSuck(-15f);
					}
					break;
				case 4:		//소주 1-2 : 5턴간 힘 10 감소
					master.incStr(-10f);
					break;
/*todo*/		case 5:		//소주 1-3 : 다음 공격 시 대상 1턴동안 행동불능
					break;
				case 6:		//소주 1-4 : 5턴간 공격 시 추가 데미지를 1 가한다
					master.incBonusDamage(1f);
					break;
				case 7:		//소주 2-2 : 5턴간 힘 12 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incStr(12f);
					}else{
						master.incStr(-12f);
					}
					break;
/*todo*/		case 8:		//소주 2-3 : 다음 공격 시 대상 2턴동안 행동불능 or 자신 2턴동안 행동불능
					if(randomBuff[master.buffList[i].buffCode]){
						master.buffList[i].remainTime = 1;
					}else{
						master.buffList[i].remainTime = 2;
					}
					break;
				case 9:		//소주 2-4 : 5턴간 공격 시 추가 데미지를 2 가한다 or 데미지감소 2
					if(randomBuff[master.buffList[i].buffCode]){
						master.incBonusDamage(2f);
					}else{
						master.incBonusDamage(-2f);
					}
					break;
				case 10:	//소주 3-2 : 5턴간 힘 15 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incStr(15f);
					}else{
						master.incStr(-15f);
					}
					break;
/*todo*/		case 11:	//소주 3-3 : 다음 공격 시 대상 3턴동안 행동불능 or 자신 3턴동안 행동불능
					if(randomBuff[master.buffList[i].buffCode]){
						master.buffList[i].remainTime = 1;
					}else{
						master.buffList[i].remainTime = 3;
					}
					break;
				case 12:	//소주 3-4 : 5턴간 공격 시 추가 데미지를 3 가한다 or 데미지감소 3
					if(randomBuff[master.buffList[i].buffCode]){
						master.incBonusDamage(3f);
					}else{
						master.incBonusDamage(-3f);
					}
					break;
				case 13:	//양주 1-2 : 5턴간 민첩 10 감소
					break;
/*todo*/		case 14:	//양주 1-3 : 다음 1번의 공격 시 대상의 회피율을 무시
					master.buffList[i].remainTime = 1;
					break;
/*todo*/		case 15:	//양주 1-4 : 다음 공격 시 대상 1턴동안 감전
					master.buffList[i].remainTime = 1;
					break;
				case 16:	//양주 2-2 : 5턴간 민첩 12 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
/*todo*/		case 17:	//양주 2-3 : 다음 2번의 공격 시 대상의 회피율을 무시 or 내가 2번 무시당함
					if(randomBuff[master.buffList[i].buffCode]){
						master.buffList[i].remainTime = 2;
					}else{
						master.buffList[i].remainTime = 2;
					}
					break;
/*todo*/		case 18:	//양주 2-4 : 다음 공격 시 대상 2턴동안 감전 or 자신 2턴동안 감전
					if(randomBuff[master.buffList[i].buffCode]){
						master.buffList[i].remainTime = 2;
					}else{
						master.buffList[i].remainTime = 2;
					}
					break;
				case 19:	//양주 3-2 : 5턴간 민첩 15 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
/*todo*/		case 20:	//양주 3-3 : 다음 3번의 공격 시 대상의 회피율을 무시 or 내가 3번 무시당함
					if(randomBuff[master.buffList[i].buffCode]){
						master.buffList[i].remainTime = 3;
					}else{
						master.buffList[i].remainTime = 3;
					}
					break;
/*todo*/		case 21:	//양주 3-4 : 다음 공격 시 대상 3턴동안 감전 or 자신 3턴동안 감전
					if(randomBuff[master.buffList[i].buffCode]){
						master.buffList[i].remainTime = 3;
					}else{
						master.buffList[i].remainTime = 3;
					}
					break;
				case 22:	//와인 1-2 : 5턴간 지능 10 감소
					master.incIntelligence(-10f);
					break;
/*todo*/		case 23:	//와인 1-4, 2-3, 3-5 : 5턴간 현재 층의 맵이 전부 보인다	
					break;
				case 24:	//와인 2-2 : 5턴간 지능 12 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incIntelligence(12f);
					}else{
						master.incIntelligence(-12f);
					}
					break;
				case 25:	//와인 3-2 : 5턴간 지능 15 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incIntelligence(15f);
					}else{
						master.incIntelligence(-15f);
					}
					break;
				case 26:	//막걸리 1-1 : 5턴간 운 10 증가
					master.incLuck(10f);
					break;
				case 27:	//막걸리 2-1 : 5턴간 운 15 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incLuck(15f);
					}else{
						master.incLuck(-15f);
					}
					break;
				case 28:	//막걸리 3-1 : 5턴간 운 20 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incLuck(20f);
					}else{
						master.incLuck(-20f);
					}
					break;
				case 32:	//화상 1턴
					break;
				case 33:	//중독 1턴
					break;
				case 34:	//감전 1턴
					break;
				case 36:	//분노
					break;
				case 37:	//고집
					break;
				case 38:	//깨진 병
					master.attackScript.onHitFlag.Add (8);
					break;
				case 39:	//공포 : 5턴간 힘 20% 감소 (병목 치기)
					master.buffList[i].valueMemory.Add(master.getStr()*0.2f);
					master.incStr(-master.buffList[i].valueMemory[0]);
					break;
				case 40:	//광분
					break;
				case 43:	//화상 2턴
					break;
				case 44:	//중독 2턴
					break;
				case 45:	//감전 2턴
					break;
				case 48:	//화상 3턴
					break;
				case 49:	//중독 3턴
					break;
				case 50:	//감전 3턴
					break;



				/*************************영구 버프*************************/
				case 101:	//맥주 1-1 : 영구적으로  최대 HP 10 증가
					master.incFullHp(10f);
					break;
				case 102:	//맥주 1-2 : HP 10 회복
					master.incCurrHp(10f);
					break;
				case 103:	//맥주 2-1 : 영구적으로 최대 HP 15 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incFullHp(15f);
					}else{
						master.incFullHp(-15f);
					}
					break;
				case 104:	//맥주 2-2 : HP 15 회복 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incCurrHp(15f);
					}else{
						master.incCurrHp(-15f);
					}
					break;
				case 105:	//맥주 3-1 : 영구적으로 최대 HP 20 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incFullHp(20f);
					}else{
						master.incFullHp(-20f);
					}
					break;
				case 106:	//맥주 3-2 : HP 20 회복 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incCurrHp(20f);
					}else{
						master.incCurrHp(-20f);
					}
					break;
				case 107:	//소주 1-1 : 영구적으로 힘 1 증가
					master.incStr(1f);
					break;
				case 108:	//소주 2-1 : 영구적으로 힘 2 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incStr(2f);
					}else{
						master.incStr(-2f);
					}
					break;
				case 109:	//소주 3-1 : 영구적으로 힘 3 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incStr(3f);
					}else{
						master.incStr(-3f);
					}
					break;
				case 110:	//양주 1-1 : 영구적으로 민첩 1 증가
					master.incDex(1f);
					break;
				case 111:	//양주 2-1 : 영구적으로 민첩 2 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incDex(2f);
					}else{
						master.incDex(-2f);
					}
					break;
				case 112:	//양주 2-1 : 영구적으로 민첩 3 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incDex(3f);
					}else{
						master.incDex(-3f);
					}
					break;
				case 113:	//와인 1-1 : 영구적으로 지능 1 증가
					master.incIntelligence(1f);
					break;
/*todo*/		case 114:	//와인 1-3, 2-5, 3-4 : 술 1병을 감정한다
					break;
/*todo*/		case 115:	//와인 1-5, 2-4, 3-3 : 동일한 층 내에서 랜덤한 방으로 이동한다
					break;
/*todo*/		case 116:	//와인 1-6 : 자신 주변에 이 층 몬스터 1마리 소환
					break;
				case 117:	//와인 2-1 : 영구적으로 지능 2 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incIntelligence(2f);
					}else{
						master.incIntelligence(-2f);
					}
					break;
/*todo*/		case 118:	//와인 2-6 : 자신 주변에 이 층 몬스터 2마리 소환
					break;
				case 119:	//와인 3-1 : 영구적으로 지능 3 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incIntelligence(3f);
					}else{
						master.incIntelligence(-3f);
					}
					break;
/*todo*/		case 120:	//와인 3-6 : 자신 주변에 이 층 몬스터 3마리 소환
					break;
/*todo*/		case 121:	//막걸리 1-2 : 보유한 술병 중 1개가 랜덤으로 깨짐
					break;
/*todo*/		case 122:	//막걸리 1-3 : 10의 골드를 얻는다.
					break;
/*todo*/		case 123:	//막걸리 2-2 : 보유한 술병 중 50%가 랜덤으로 깨짐 or 라벨 술 1 획득
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
/*todo*/		case 124:	//막걸리 2-3 : 보유한 소지금 50% 골드를 얻음 or 잃음
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
/*todo*/		case 125:	//막걸리 3-2 : 보유한 술병 중 100%가 랜덤으로 깨짐 or 라벨 술 3 획득
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
/*todo*/		case 126:	//막걸리 3-3 : 보유한 소지금 100% 골드를 얻음 or 잃음
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
						
				/*************************반복 실행*************************/
				case 201:	//맥주 1-3 : 5턴간 매턴 최대 HP의 3%만큼 HP가 증가
					master.incCurrHp(master.getFullHp()*0.03f);
					break;
				case 202:	//맥주 2-3 : 5턴간 매턴 최대 HP의 4%만큼 HP가 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incCurrHp(master.getFullHp()*0.04f);
					}else{
						master.incCurrHp(master.getFullHp()*(-0.04f));
					}
					break;
				case 203:	//맥주 3-3 : 5턴간 매턴 최대 HP의 5%만큼 HP가 증가 or 감소
					if(randomBuff[master.buffList[i].buffCode]){
						master.incCurrHp(master.getFullHp()*0.05f);
					}else{
						master.incCurrHp(master.getFullHp()*(-0.05f));
					}
					break;
				}

				//일시 버프일 경우 더이상 버프를 받을 필요가 없음을 체크한다.
				if(master.buffList[i].buffCode<=100){
					master.buffList[i].needExcute=false;
				}
			}

			switch(master.buffList[i].buffCode){//매턴 감소하는 버프는 감소시켜주고 공격해야 감소하는것들은 냅둔다.
			case 5:		//소주 1-3 : 다음 공격 시 대상 1턴동안 행동불능
				break;
			case 8:		//소주 2-3 : 다음 공격 시 대상 2턴동안 행동불능 or 자신 2턴동안 행동불능
				if(randomBuff[master.buffList[i].buffCode]){
				}else{
					master.buffList[i].remainTime--;
				}
				break;
			case 11:	//소주 3-3 : 다음 공격 시 대상 3턴동안 행동불능 or 자신 3턴동안 행동불능
				if(randomBuff[master.buffList[i].buffCode]){
				}else{
					master.buffList[i].remainTime--;
				}
				break;
			case 14:	//양주 1-3 : 다음 1번의 공격 시 대상의 회피율을 무시
				break;
			case 15:	//양주 1-4 : 다음 공격 시 대상 1턴동안 감전
				break;
			case 17:	//양주 2-3 : 다음 2번의 공격 시 대상의 회피율을 무시 or 내가 2번 무시당함
				if(randomBuff[master.buffList[i].buffCode]){
				}else{
				}
				break;
			case 18:	//양주 2-4 : 다음 공격 시 대상 2턴동안 감전 or 자신 2턴동안 감전
				if(randomBuff[master.buffList[i].buffCode]){
				}else{
					master.buffList[i].remainTime--;
				}
				break;
			case 20:	//양주 3-3 : 다음 3번의 공격 시 대상의 회피율을 무시 or 내가 3번 무시당함
				if(randomBuff[master.buffList[i].buffCode]){
				}else{
				}
				break;
			case 21:	//양주 3-4 : 다음 공격 시 대상 3턴동안 감전 or 자신 3턴동안 감전
				if(randomBuff[master.buffList[i].buffCode]){
				}else{
					master.buffList[i].remainTime--;
				}
				break;
			case 38:	//자신에게 '깨진 병' 을 건다.
				break;
			default:
				master.buffList[i].remainTime--;
				break;
			}
		}
	}

	public void takeBuff(){//효과 해제
		bool[] isEnd = new bool[master.buffList.Count];
		for (int i=0; i<master.buffList.Count; i++){
			if(master.buffList[i].remainTime <= 0) {
				isEnd[i] = true;
				switch (master.buffList[i].buffCode) {
					/*************************일시 버프*************************/
				case 1:		//맥주 1-4 : 5턴간 공격 시 자신의 HP 5 감소 해제
					master.incBloodSuck(5f);
					break;
				case 2:		//맥주 2-4 : 5턴간 공격 시 자신의 HP 10 회복 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incBloodSuck(-10f);
					}else{
						master.incBloodSuck(10f);
					}
					break;
				case 3:		//맥주 3-4 : 5턴간 공격 시 자신의 HP 15 회복 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incBloodSuck(-15f);
					}else{
						master.incBloodSuck(15f);
					}
					break;
				case 4:		//소주 1-2 : 5턴간 힘 10 감소 해제
					master.incStr(10f);
					break;
/*todo*/		case 5:		//소주 1-3 : 다음 공격 시 대상 1턴동안 행동불능 해제
					break;
				case 6:		//소주 1-4 : 5턴간 공격 시 추가 데미지를 1 가한다 해제
					master.incBonusDamage(-1f);
					break;
				case 7:		//소주 2-2 : 5턴간 힘 12 증가 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incStr(-12f);
					}else{
						master.incStr(12f);
					}
					break;
/*todo*/		case 8:		//소주 2-3 : 다음 공격 시 대상 2턴동안 행동불능 or 자신 2턴동안 행동불능 해제
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
				case 9:		//소주 2-4 : 5턴간 공격 시 추가 데미지를 2 가한다 or 데미지감소 2 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incBonusDamage(-2f);
					}else{
						master.incBonusDamage(2f);
					}
					break;
				case 10:	//소주 3-2 : 5턴간 힘 15 증가 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incStr(-15f);
					}else{
						master.incStr(15f);
					}
					break;
/*todo*/		case 11:	//소주 3-3 : 다음 공격 시 대상 3턴동안 행동불능 or 자신 3턴동안 행동불능 해제
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
				case 12:	//소주 3-4 : 5턴간 공격 시 추가 데미지를 3 가한다 or 데미지감소 3 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incBonusDamage(-3f);
					}else{
						master.incBonusDamage(3f);
					}
					break;
				case 13:	//양주 1-2 : 5턴간 민첩 10 감소 해제
					master.incDex(10f);
					break;
/*todo*/		case 14:	//양주 1-3 : 다음 1번의 공격 시 대상의 회피율을 무시 해제
					break;
/*todo*/		case 15:	//양주 1-4 : 다음 공격 시 대상 1턴동안 감전 해제
					break;
				case 16:	//양주 2-2 : 5턴간 민첩 12 증가 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incDex(-12f);
					}else{
						master.incDex(12f);
					}
					break;
/*todo*/		case 17:	//양주 2-3 : 다음 2번의 공격 시 대상의 회피율을 무시 or 내가 2번 무시당함 해제
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
/*todo*/		case 18:	//양주 2-4 : 다음 공격 시 대상 2턴동안 감전 or 자신 2턴동안 감전 해제
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
				case 19:	//양주 3-2 : 5턴간 민첩 15 증가 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incDex(-15f);
					}else{
						master.incDex(15f);
					}
					break;
/*todo*/		case 20:	//양주 3-3 : 다음 3번의 공격 시 대상의 회피율을 무시 or 내가 3번 무시당함 해제
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
/*todo*/		case 21:	//양주 3-4 : 다음 공격 시 대상 3턴동안 감전 or 자신 3턴동안 감전 해제
					if(randomBuff[master.buffList[i].buffCode]){
						
					}else{
						
					}
					break;
				case 22:	//와인 1-2 : 5턴간 지능 10 감소 해제
					master.incIntelligence(-10f);
					break;
/*todo*/		case 23:	//와인 1-4, 2-3, 3-5 : 5턴간 현재 층의 맵이 전부 보인다	해제
					break;
				case 24:	//와인 2-2 : 5턴간 지능 12 증가 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incIntelligence(-12f);
					}else{
						master.incIntelligence(12f);
					}
					break;
				case 25:	//와인 3-2 : 5턴간 지능 15 증가 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incIntelligence(-15f);
					}else{
						master.incIntelligence(15f);
					}
					break;
				case 26:	//막걸리 1-1 : 5턴간 운 10 증가 해제
					master.incLuck(-10f);
					break;
				case 27:	//막걸리 2-1 : 5턴간 운 15 증가 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incLuck(-15f);
					}else{
						master.incLuck(15f);
					}
					break;
				case 28:	//막걸리 3-1 : 5턴간 운 20 증가 or 감소 해제
					if(randomBuff[master.buffList[i].buffCode]){
						master.incLuck(-20f);
					}else{
						master.incLuck(20f);
					}
					break;
				case 39:	//공포 : 5턴간 힘 20% 감소 (병목 치기) 해제
					master.incStr(master.buffList[i].valueMemory[0]);
					break;
				default:	//일시 버프가 아닐경우 아무것도 해제하지 않는다.
					break;	
				}
			}
		}

		//해제 시키고 Buff객체를 BuffList에서 제거한다.
		for(int i = master.buffList.Count-1 ; i>=0 ; i--){
			if(isEnd[i]){
				master.buffList.RemoveAt(i);
			}
		}
	}

	private void takeBuff(int buffCode){
		Debug.Log("인자가 1개인 takeBuff 함수는 아직 구현되지 않았음");
		//버프 강제 해제.
		//todo!!!!!!!!!!!!!!!!
	}
}
//버프코드	(1-)	: 일시 버프
//			(101-)	: 영구 버프
//			(201-)	: 반복 실행

/* 맥주 1 - 1. 영구적으로  최대 HP 10 증가				code101
 * 			2. HP 10 회복							code102
 * 			3. 5턴간 매턴 최대 HP의 3%만큼 HP가 증가	code201
 * 			4. 5턴간 공격 시 자신의 HP 5 감소			code1			
 * 
 * 		2 - 1.영구적으로 최대 HP 15 증가				code103
 * 			1.영구적으로 최대 HP 15 감소				code103
 * 			2.HP 15 회복								code104
 * 			2.HP 15 감소								code104
 * 			3.5턴간 매턴 최대 HP의 4%만큼 HP가 증가	code202
 * 			3.5턴간 매턴 최대 HP의 4%만큼 HP가 감소	code202
 * 			4.5턴간 공격 시 자신의 HP 10 회복			code2
 * 			4.5턴간 공격 시 자신의 HP 10 감소			code2
 * 
 * 		3 - 1.영구적으로 최대 HP 20 증가				code105
 * 			1.영구적으로 최대 HP 20 감소				code105
 * 			2.HP 20 회복								code106
 * 			2.HP 20 감소								code106
* 			3.5턴간 매턴 최대 HP의 5%만큼 HP가 증가	code203
 * 			3.5턴간 매턴 최대 HP의 5%만큼 HP가 감소	code203
 * 			4.5턴간 공격 시 자신의 HP 15 회복			code3
 * 			4.5턴간 공격 시 자신의 HP 15 감소			code3
 * 
 * 
 * 
 * 소주 1 - 1.영구적으로 힘 1 증가						code107
 * 			2.5턴간 힘 10 감소						code4
 * 			3.다음 공격 시 대상 1턴동안 행동불능		code5		<special> 	: 남은 기간 무한짜리 버프를 걸었다가, 공격시 기간을 0 으로 만든다. (X)
 * 			4.5턴간 공격 시 추가 데미지를 1 가한다		code6
 * 		
 * 		2 - 1.영구적으로 힘 2 증가					code108
 * 			1.영구적으로 힘 2 감소					code108
 * 			2.5턴간 힘 12 증가						code7
 * 			2.5턴간 힘 12 감소						code7
 * 			3.다음 공격 시 대상 2턴동안 행동불능		code8		<special>
 * 			3.자기 자신 2턴동안 행동불능				code8		<special>
 * 			4.5턴간 공격 시 추가 데미지를 2 가한다		code9
 * 			4.5턴간 공격 시 적이 데미지를 2 덜받는다	code9
 * 		
 * 		3 - 1.영구적으로 힘 3 증가					code109
 * 			1.영구적으로 힘 3 감소					code109
 * 			2.5턴간 힘 15 증가						code10
 * 			2.5턴간 힘 15 감소						code10
 * 			3.다음 공격 시 대상 3턴동안 행동불능		code11		<special>
 * 			3.자기 자신 3턴동안 행동불능				code11		<special>
 * 			4.5턴간 공격 시 추가 데미지를 3 가한다		code12
 * 			4.5턴간 공격 시 적이 데미지를 3 덜받는다	code12
 *
 *
 *
 * 양주 1 - 1.영구적으로 민첩 1 증가					code110
 * 			2.5턴간 민첩 10 감소						code13
 * 			3.다음 1번의 공격 시 대상의 회피율을 무시	code14		<special>
 *			4.다음 공격 시 대상 1턴동안 감전			code15		<special>
 *		
 *		2 - 1.영구적으로 민첩 2 증가					code111
 *			1.영구적으로 민첩 2 감소					code111
 *			2.5턴간 민첩 12 증가						code16
 *			2.5턴간 민첩 12 감소						code16
 *			3.다음 2번의 공격 시 대상의 회피율을 무시	code17		<special>
 *			3.다음 2번의 피격 시 대상의 회피율을 무시	code17		<special>
 *			4.다음 공격 시 대상 2턴동안 감전			code18		<special>
 *			4.자기 자신 2턴동안 감전					code18		<special>
 *		
 *		3 - 1.영구적으로 민첩 3 증가					code112
 *			1.영구적으로 민첩 3 감소					code112
 *			2.5턴간 민첩 15 감소						code19
 *			2.5턴간 민첩 15 감소						code19
 *			3.다음 3번의 공격 시 대상의 회피율을 무시	code20		<special>
 *			3.다음 3번의 피격 시 대상의 회피율을 무시	code20		<special>
 *			4.다음 공격 시 대상 3턴동안 감전			code21		<special>
 *			4.자기 자신 3턴동안 감전					code21		<special>
 *
 *
 *
 *와인  1 - 1.영구적으로 지능 1 증가					code113
 *			2.5턴간 지능 10 감소						code22
 *			3.술 1병을 감정한다						code114		<special>
 *			4.5턴간 현재 층의 맵이 전부 보인다			code23		<special>
 *			5.동일한 층 내에서 랜덤한 방으로 이동한다	code115		<special>
 *			6.자신 주변에 이 층 몬스터 1마리 소환		code116		<special>
 *		
 *		2 - 1.영구적으로 지능 2 증가					code117
 *			1.영구적으로 지능 2 감소					code117
 *			2.5턴간 지능 12 증가						code24
 *			2.5턴간 지능 12 감소						code24
 *			3.5턴간 현재 층의 맵이 전부 보인다			code23		<special>
 *			4.동일한 층 내에서 랜덤한 방으로 이동한다	code115		<special>
 *			5.술 1병을 감정한다						code114		<special>
 *			6.자신 주변에 이 층 몬스터 2마리 소환		code118		<special>
 *		
 *		3 -	1.영구적으로 지능 3 증가					code119
 *			1.영구적으로 지능 3 감소					code119
 *			2.5턴간 지능 15 증가						code25
 *			2.5턴간 지능 15 감소						code25
 *			3.동일한 층 내에서 랜덤한 방으로 이동한다.	code115		<special>
 *			4.술 1병을 감정한다.						code114		<special>
 *			5.5턴간 현재 층의 맵이 전부 보인다			code23		<special>
 *			6.자신 주변에 이 층 몬스터 3마리 소환.		code120		<special>
 *
 *
 *
 *막걸리 1 - 1.5턴간 운 10 증가						code26
 *			2.보유한 술병 중 1개가 랜덤으로 깨짐		code121
 *			3.10의 골드를 얻는다.						code122
 *			4.1단계 숙성 효과 랜덤하게 1가지 발동		code -
 *		
 *		2 - 1.5턴간 운 15 증가						code27
 *			1.5턴간 운 15 감소						code27
 *			2.보유한 술병 중 50%가 랜덤으로 깨짐		code123
 *			2.라벨있는 랜덤 술 1병이 가방으로 들어옴	code123
 *			3.보유한 소지금의 50%를 얻는다				code124
 *			3.보유한 소지금의 50%를 잃는다				code124
 *			4.2단계 숙성 효과 랜덤하게 1가지 발동		code -
 *		
 *		3 - 1.5턴간 운 20 증가						code28
 *			1.5턴간 운 20 감소						code28
 *			2.보유한 술병 중 100%가 랜덤으로 깨짐		code125
 *			2.라벨있는 랜덤 술 3병이 가방으로 들어옴	code125
 *			3.보유한 소지금의 100%를 얻는다			code126
 *			3.보유한 소지금의 100%를 잃는다			code126
 *			4.3단계 숙성 효과 랜덤하게 1가지 발동		code -
 *
 *
 *
 *
 *
 *상태 이상
 *
 *통제불능	- 자기 차례에  랜덤한 방향으로 이동한다		code29		code41		code46
 *행동불능	- 자기 차례에 바로 턴이 넘어간다			code30		code42		code47
 *숙취		- 힘, 민첩, 지식, 운이 20% 감소한다		code31
 *
 *화상		- 공격력 10  감소, 매 턴마다 3의 데미지		code32		code43		code48
 *중독		- 회피율 10% 감소, 매 턴마다 3의 데미지		code33		code44		code49
 *감전		- 속도등급 1 감소, 매 턴마다 3의 데미지		code34		code45		code50
 *
 *
 *주량		- 10 턴간 술을 마시지 않으면 스택 1감소		code35		<special>	<todo>	:	10턴짜리 일시 버프를걸어서 해제시에 스택1 감소후 버프 갱신, 술을 마실시 그냥 갱신.
 *취기 유지	- 매 턴 체력이 1감소, 취기가 감소하지않음	code51
 *분노		- 5턴간 공격력 +10, 회피율 -5				code36
 *고집		- 3턴간 무적								code37
 *깨진 병	- 다음 공격까지 공격력 +10					code38
 *공포		- 5턴간 공격력 20% 하락					code39
 *광분		- 5턴간 힘 20증가							code40
 *
 *
 *아이템 효과 (패시브로 적용)
 *
 *쌍절곤 		(4)	- 타격 시 HP 1 감소
 *숟가락 		(5)	- 공격력이 0이 된다, 같은 대상을 44번 타격 시 일격사
 *A.D. 		 	(6)	- 매 턴 취기 2 감소
 **주정뱅이		(7) - 10턴마다 인벤토리의 술이 하나씩 사라진다
 *장팔사모 모형	(8)	- 취기가 50 이상이면 공격력 5 증가
 *술뱀 채찍		(9)	- 타격한 적의 취기 5 증가
 *쇠도끼			(10)- 소지금 50골드마다 공격력 + 1 / 최대 10
 *역행부 바그		(11)- 대상의 회피율을 무시한다
 *스크램블 메이스	(12)- 체력이 50%이하인 적을 떄릴시 공격력 2배, 자신의 체력이 50%이하일 경우, 자신이 입는 피해량 2배
 *황금벽돌		(13)- 공격시 일정량의 돈 획득
 *핏빛 방망이		(14)- 내가 걸린 디버프 하나당 힘이 3 증가한다
 *각설이의 엿가위	(15)- 4번째 공격마다 1턴무적
 *죄수의 흑룡검	(16)- 공격 시 30% 확률로 자신에게 3턴짜리 통제불능을 건다. 선택한 방향의 좌우도 같이 공격한다
 *우수의 백룡검	(17)- 공격 시 30% 확률로 자신에게 3턴짜리 행동불능을 건다
 *스매시 브라더스	(18)- 타격한 적을 몹이나 벽이 존재하는 곳까지 넉백
 *돌주먹			(19)- 힘 20이하 일 시 속도등급 -1, 공격력 +5
 *
 *
 */
 

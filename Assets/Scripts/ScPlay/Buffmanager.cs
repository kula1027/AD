using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buffmanager {
	
	public Entity master;
	public int fullHp;
	public int currHp;
	
	public void addBuff ( int buffCode_){//버프 추가
		if(master.buffList.Count>=Config.buffLimit){
			return;
		}
		master.buffList.Add(new Buff(buffCode_));
	}
	
	public void giveBuff(){//효과 적용
		
		for (int i=0; i<master.buffList.Count; i++){
			
			switch (master.buffList[i].buffCode) {
			case 1 : 
				master.RegenHp(30);		
				break;
			case 2 : 
				master.setFullHp(master.getFullHp()*1.1f);					
				break;
			case 3:	//5턴간 매턴 최대 HP의 3%만큼 HP가 회복
				master.RegenHp(master.getFullHp()*0.03f );	
				break;
			case 4: 
				master.RegenHp(40);
				break;
			case 5:
				master.setFullHp(master.getFullHp()*1.15f);
				break;
			case 6:
				master.RegenHp(master.getFullHp()*0.04f );
				break;
			case 7:
				master.RegenHp(-50f);
				break;
			case 8:
				master.setFullHp(master.getFullHp()*0.8f);
				break;
			case 9:
				master.RegenHp( - master.getFullHp()*0.05f );
				break;
			case 10:
				master.setStr(master.getStr() - 10f); //get-set 추가
				break;
			case 11:
				master.setBloodSuck(20f);    //bloodSuck == 플레이어의 흡혈량
				break;
			case 12:
				//몬스터를 때리면 몬스터피 20회복됨
				break;
			case 13:
				//착용한 장비가 모두 해제됩니다
				break;
			case 14:
				master.setStr(master.getStr() - 12f);
				break;
			case 15:
				master.setBloodSuck(30f);	//get-set 추가
				break;
			case 16:
				//몬스터를 때리면 몬스터피 30회복됨
				break;
			case 17:
				master.setStr(master.getStr() - 15f);
				break;
			case 18:
				master.setBloodSuck(40f);
				break;
			case 19:
				//몬스터를 때리면 몬스터피 40회복됨
				break;
			case 20:
				master.setDex(master.getDex() -10f);	//get-set 추가
				break;
			case 21:
				master.setSpeedLevel(master.getSpeedLevel()+1); //get-set 추가
				break;
			case 22:
				master.setSpeedLevel(master.getSpeedLevel()-1);
				break;
			case 23:
				master.setAccuracyRate(1f); //get-set 추가해야됨 , accuracyRate(명중률) = 1 이면 명중률이 100% 
				master.setCriticalRate(1f); //get-set 추가해야됨 , criticalRate(크리티컬율) = 1
				break;
			case 24: //(모르겠음) == 피격 시 무조건 명중하며 크리티컬이 터진다
				
				break;
			case 25:
				master.setDex(master.getDex() -12f);
				break;
			case 26:
				master.setDex(master.getDex() -15f);
				break;
			case 27:
				master.setIntelligence(master.getIntelligence()-10f); ////get-set 추가 , intelligence변수 추
				break;
			case 28: //현재 층의 맵이 전부 보인다.	
				break;
			case 29://동일한 층 내에서 랜덤한 방으로 이동한다.
				break;
			case 30://술 1병을 감정한다.	
				break;
			case 31://자신 주변에 이 층에서 나오는 몬스터 2마리를 소환한다.
				break;
			case 32:
				master.setIntelligence(master.getIntelligence()-12f);
				break;
			case 33://자신 주변에 이 층에서 나오는 몬스터 3마리를 소환한다.
				break;
			case 34:
				master.setIntelligence(master.getIntelligence()-15);
				break;
			case 35://자신 주변에 이 층에서 나오는 몬스터 4마리를 소환한다.
				break;
			case 36:
				master.setLuck(master.getLuck()+20f);  //get-set 추가 , luck변수 추
				break;
			case 37://5턴간 받는 공격의 10%의 데미지를 반사한다.
				break;
			case 38: //5턴간 라벨이 있는 술 드랍 확률 10% 증가
				break;
			case 39://5턴간 드랍하는 재화량 50% 증가
				break;
			case 40: //1단계 숙성 효과들 중 랜덤하게 1가지가 발동 -- 코드: 1,2,3,10,11,12,13,20,21,22,23,24,27,28,29,30,31,36,37,38,39,40,100,103,106,109
				break;
			case 41:
				master.setLuck(master.getLuck()-20f);
				break;
			case 42://5턴간 받는 공격의 10%의 데미지를 추가로 받는다.
				break;
			case 43://5턴간 라벨이 있는 술 드랍 확률 10% 감소
				break;
			case 44://5턴간 드랍하는 재화량 50% 감소
				break;
			case 45: //2단계 숙성 효과들 중 랜덤하게 1가지가 발동 -- 코드: 4,5,6,13,14,15,16,21,22,23,24,25,28,29,30,32,33,41,42,43,44,45,101,104,107,110
				break;
			case 46:
				master.setLuck(master.getLuck()-30f);
				break;
			case 47: //5턴간 받는 공격의 20%의 데미지를 추가로 받는다.
				break;
			case 48: //5턴간 라벨이 있는 술 드랍 확률 20% 감소
				break;
			case 49: //5턴간 드랍하는 재화량 100% 감소	
				break;
			case 50: //3단계 숙성 효과들 중 랜덤하게 1가지가 발동 -- 코드: 7,8,9,13,17,18,19,21,22,23,24,26,27,28,29,34,35,46,47,48,49,50,102,105,108,111
				break;
			}
		}
	}
}
// 버프코드 (1-50): 일시버프, (100-): 영구 버프

/* 맥주 1 - 1. 영구 hp 20							0	code100		반복실행이 필요한것
 * 			2. HP 30 회복							code1
 * 			3. 5턴간 최대 HP 10% 증가					code2
 * 			4. 5턴간 매턴 최대 HP의 3%만큼 HP가 증가	code3				v
 * 
 * 		2 - 1.영구적으로 최대 HP 25 증가				0	code101
 * 			2.HP 40 회복								code4			
 * 			3.5턴간 최대 HP 15% 증가					code5
 * 			4.5턴간 매턴 최대 HP의 4%만큼 HP가 증가	code6				v
 * 
 * 		3 - 1.영구적으로 최대 HP 30 감소				0	code102
 * 			2.HP 50 감소								code7
 * 			3.5턴간 최대 HP 20% 감소					code8
 * 			4.5턴간 매턴 최대 HP의 5%만큼 HP가 감소	code9				v
 * 
 * 소주 1 - 1.영구적으로 힘 1 증가						0	code103
 * 			2.5턴간 힘 10 감소						code10
 * 			3.5턴간 공격 시 자신의 HP 20 회복			code11			
 * 			4.5턴간 공격 시 대상의 HP 20 회복			code12			
 * 			5.착용한 장비가 모두 해제된다.				code13
 * 		
 * 		2 - 1.영구적으로 힘 2 증가					0	code104
 * 			2.5턴간 힘 12 감소						code14
 * 			3.5턴간 공격 시 자신의 HP 30 회복			code15			
 * 			4.5턴간 공격 시 대상의 HP 30 회복			code16			
 * 			5.착용한 장비가 모두 해제된다.				code13
 * 		
 * 		3 - 1.영구적으로 힘 3 증가					0	code105
 * 			2.5턴간 힘 15 감소						code17
 * 			3.5턴간 공격 시 자신의 HP 40 회복			code18			
 * 			4.5턴간 공격 시 대상의 HP 40 회복			code19			
 * 			5.착용한 장비가 모두 해제된다.				code13
 *
 * 양주 1 - 1.영구적으로 민첩 1 증가					0	code106
 * 			2.5턴간 민첩 10 감소						code20
 * 			3.5턴간 속도등급 1 증가					code21
 *			4. 5턴간 속도등급 1 감소					code22
 *			5.5턴간 공격 시 무조건 명중하며 크리티컬이 터진다. code23
 *			6.5턴간 피격 시 무조건 명중하며 크리티컬이 터진다. code24
 *		
 *		2 - 1.영구적으로 민첩 2 증가					0	code107
 *			2.5턴간 민첩 12 감소						code25
 *			3.6턴간 속도등급 1 증가					code21
 *			4.6턴간 속도등급 1 감소					code22
 *			5.6턴간 공격 시 무조건 명중하며 크리티컬이 터진다. code23
 *			6.6턴간 피격 시 무조건 명중하며 크리티컬이 터진다. code24
 *		
 *		3 - 1.영구적으로 민첩 3 증가					0	code108
 *			2.5턴간 민첩 15 감소						code26
 *			3.7턴간 속도등급 1 증가					code21
 *			4.7턴간 속도등급 1 감소					code22
 *			5.7턴간 공격 시 무조건 명중하며 크리티컬이 터진다. code23
 *			6.7턴간 피격 시 무조건 명중하며 크리티컬이 터진다. code24
 *
 *와인  1 - 1.영구적으로 지능 1 증가					0	code109
 *			2.5턴간 지능 10 감소						code27
 *			3.5턴간 현재 층의 맵이 전부 보인다.			code28
 *			4.동일한 층 내에서 랜덤한 방으로 이동한다.	code29
 *			5.술 1병을 감정한다.						code30
 *			6.자신 주변에 이 층에서 나오는 몬스터 2마리를 소환한다. code31
 *		
 *		2 - 1.영구적으로 지능 2 증가					0	code110
 *			2.5턴간 지능 12 감소						code32
 *			3.6턴간 현재 층의 맵이 전부 보인다.			code28
 *			4.동일한 층 내에서 랜덤한 방으로 이동한다.	code29
 *			5.술 1병을 감정한다.						code30
 *			6.자신 주변에 이 층에서 나오는 몬스터 3마리를 소환한다. code33
 *		
 *		3 -	1.영구적으로 지능 3 증가					0	code111
 *			2.5턴간 지능 15 감소						code34
 *			3.7턴간 현재 층의 맵이 전부 보인다.			code27
 *			4.동일한 층 내에서 랜덤한 방으로 이동한다.	code28
 *			5.술 1병을 감정한다.						code29
 *			6.자신 주변에 이 층에서 나오는 몬스터 4마리를 소환한다.	code35
 *
 *막걸리 1 - 1.5턴간 운 20 증가							code36
 *			2.5턴간 받는 공격의 10%의 데미지를 반사한다.	code37
 *			3.5턴간 라벨이 있는 술 드랍 확률 10% 증가		code38
 *			4.5턴간 드랍하는 재화량 50% 증가				code39
 *			5.1단계 숙성 효과들 중 랜덤하게 1가지가 발동		code40
 *		
 *		2 - 1.5턴간 운 20 감소							code41
 *			2.5턴간 받는 공격의 10%의 데미지를 추가로 받는다.code42	
 *			3.5턴간 라벨이 있는 술 드랍 확률 10% 감소		code43
 *			4.5턴간 드랍하는 재화량 50% 감소				code44
 *			5.2단계 숙성 효과들 중 랜덤하게 1가지가 발동		code45
 *		
 *		3 - 1.5턴간 운 30 감소							code46
 *			2.5턴간 받는 공격의 20%의 데미지를 추가로 받는다.code47
 *			3.5턴간 라벨이 있는 술 드랍 확률 20% 감소		code48
 *			4.5턴간 드랍하는 재화량 100% 감소				code49
 *			5.3단계 숙성 효과들 중 랜덤하게 1가지가 발동		code50
 */		


















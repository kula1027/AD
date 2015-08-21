using UnityEngine;
using System.Collections;

public class SkillManager : MonoBehaviour {

	private GameManager gameManager;
	private Player master;
	private Skill[] skillArr = new Skill[35];

	void Start(){
		for(int i = 0 ; i < 35 ; i++){
			skillArr[i] = new Skill(i);
		}
		master = gameObject.GetComponent<Player> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	public string GetSkillName(int slotNum){
		return skillArr[(master.charCode * 5) + slotNum].skillName;
	}

	public string GetSkillExplain(int slotNum){
		return skillArr[(master.charCode * 5) + slotNum].explain;
	}

	public string GetSkillEffect(int slotNum){
		return skillArr[(master.charCode * 5) + slotNum].effect;
	}

	private bool canPay(int skillCode){
		switch (skillCode) {
		case 1:		//취기 20
			if(master.getCurrMp() >= 20){
				return true;
			}else{
				return false;
			}
		case 2:		//취기 80
			if(master.getCurrMp() >= 80){
				return true;
			}else{
				return false;
			}
		case 10:	//주량 스택1
			if(master.alcoholStack >= 1){
				return true;
			}else{
				return false;
			}
		case 11:	//주량 스택2
			if(master.alcoholStack >= 2){
				return true;
			}else{
				return false;
			}
		case 12:	//주량 스택1
			if(master.alcoholStack >= 1){
				return true;
			}else{
				return false;
			}
		case 13:	//주량 스택3
			if(master.alcoholStack >= 3){
				return true;
			}else{
				return false;
			}
		case 15:	//빈병1
			//todo
			return true;
		case 16:	//빈병2
			//todo
			return true;
		case 17:	//빈병1
			//todo
			return true;
		case 18:	//빈병1
			//todo
			return true;
		default:
			return true;
		}
	}

	public void payCost(int skillCode){
		switch (skillCode) {
		case 0:		//HP 10%
			master.incCurrHp(-(master.getCurrHp()*0.1f));
			break;
		case 1:		//취기 20
			master.incCurrMp(-20f);
			break;
		case 2:		//취기 80
			master.incCurrMp(-80f);
			break;
		case 3:		//모든 취기
			master.incCurrMp(master.getCurrMp());
			break;
		case 5:		//모든 취기
			master.incCurrMp(master.getCurrMp());
			break;
		case 6:		//HP 10%
			master.incCurrHp(-(master.getCurrHp()*0.1f));
			break;
		case 7:		//HP 50%
			master.incCurrHp(-(master.getCurrHp()*0.5f));
			break;
		case 8:		//없음
			break;
		case 10:	//주량 스택1
			master.incAlcoholStack(-1);
			break;
		case 11:	//주량 스택2
			master.incAlcoholStack(-2);
			break;
		case 12:	//주량 스택1
			master.incAlcoholStack(-1);
			break;
		case 13:	//주량 스택3
			master.incAlcoholStack(-3);
			break;
		case 15:	//빈병1
			//todo
			break;
		case 16:	//빈병2
			//todo
			break;
		case 17:	//빈병1
			//todo
			break;
		case 18:	//빈병1
			//todo
			break;
		case 20:	//없음
			break;
		case 25:	//없음
			break;
		case 26:	//없음
			break;
		case 27:	//없음
			break;
		case 28:	//없음
			break;
		case 29:	//없음
			break;
		case 30:	//없음
			break;
		}
	}

	public void ActSkill(int slotNum){
		int skillCode = (master.charCode * 5) + slotNum;
		if (canPay (skillCode)) {
			switch (skillCode) {
			case 0:		//정신 차리기				-취기가 0이 된다.
				break;
			case 1:		//자작하면 여친없음		-자신에게 '분노'를 건다.
				break;
			case 2:		//고집 피우기				-자신에게 '고집'을 건다.
				break;
			case 3:		//필름 끊기				-해당층의 랜덤한 방으로 이동한다.
				break;
			case 5:		//원기 회복				-0%~(소모한 취기량)% 중 랜덤으로 체력을 회복
				break;
			case 6:		//광분					-자신에게 '광분'을 건다.
				break;
			case 7:		//피 폭발				-선택한 방향으로 근거리 4칸, 소모한 체력만큼의 데미지로 공격한다.
				break;
			case 8:		//취기 유지				-자신에게 '취기유지'를 건다. 이미 걸려있으면 '취기유지'를 제거한다.
				break;
			case 10:	//사격					-선택한 방향으로 원거리 4칸, (5+민첩)의 데미지로 공격한다.
				break;
			case 11:	//불 뿜기				-선택한 방향으로 원뿔모양 범위에 (5 +민첩)의 데미지로 공격한다. 맞은 적은 '화상'에 걸린다.
				break;
			case 12:	//부식성 사격				-선택한 방향으로 원거리 4칸, (4+민첩)의 데미지로 공격한다. 맞은 적은 '중독'에 걸린다.
				break;
			case 13:	//속사					-선택한 방향으로 원거리 4칸, (5+민첩)의 데미지로 2번 공격한다.
				break;
			case 15:	//깨진 병				-자신에게 '깨진 병'을 건다.
				if(master.isBrokenBottle||master.skillStandBy == 17|| master.skillStandBy == 16){	//다른 스킬 시전중이거나 이미 깨진병이 걸려있을땐, 무시.
					break;
				}
				UI_Control.AddLog("깨진 병 사용");
				gameManager.turn = Turn.Acting;
				master.isBrokenBottle = true;
				master.buffManager.addBuff(38);	//38 : 깨진 병 버프를 건다.
				master.incTurnCount ();
				break;
			case 16:	//연타					-기본 공격의 90% 피해로 2번 공격한다.
				if(master.skillStandBy == 16){
					UI_Control.AddLog("연타 취소");
					CancelSkill(16);
					break;
				}else if(master.skillStandBy == 17){
					break;
				}
				UI_Control.AddLog("연타 장전");
				master.attackScript.onHitFlag.Add (9);	//9 : 힘을 90%로
				master.attackScript.onHitFlag.Add (10);	//10: 한번 더 공격
				master.attackScript.needPay.Add(16);	//16번 코스트 예약
				master.skillStandBy = 16;
				break;
			case 17:	//빈병 내리치기			-근접 1칸, 기본 공격의 120% 데미지로 공격한다. 적은 3턴 간 행동불능이 된다.
				//todo : 무조건 근접 1칸이 적용 되도록 해야함.
				if(master.skillStandBy == 17){
					UI_Control.AddLog("빈병 내리치기 취소");
					CancelSkill(17);
					break;
				}else if(master.skillStandBy == 16){
					break;
				}
				UI_Control.AddLog("빈병 내리치기 장전");
				master.attackScript.onHitFlag.Add(3);	// 3 : 3턴 행동불능
				master.attackScript.onHitFlag.Add(7);	// 7 : 120% 로 공격
				master.attackScript.needPay.Add(17);	// 17번 코스트 예약
				master.skillStandBy = 17;
				break;
			case 18:	//병목 치기				-주변5x5에 있는 적의 공격력 5턴 간 20%하락
				if(master.coolTime[4] > 0 || master.skillStandBy == 17|| master.skillStandBy == 16){	//다른 스킬 시전 중이거나 쿨타임 이면 무시.
					//Debug.Log("Cool Time : " + master.coolTime[4]);
					break;
				}
				UI_Control.AddLog("병목 치기 사용");
				gameManager.turn = Turn.Acting;
				TileInfo[,] t = gameManager.boardManager._Stage[gameManager.currStage].get_tileInfo();
				Vector3 pos = transform.position;
				int hitCount = 0;
				for(int y = ((int)pos.y)-2 ; y < ((int)pos.y)+3 ; y++){
					for(int x = ((int)pos.x)-2 ; x < ((int)pos.x)+3 ; x++){
						if(t[y,x].e!=null){
							hitCount++;
							t[y,x].e.buffManager.addBuff(39);
							UI_Control.AddLog(t[y,x].e.name + "에게 공포를 줬다!");
						}
					}
				}
				if(hitCount == 0){
					UI_Control.AddLog("주위에 아무도 없었다!");
				}
				if(Random.Range(0,10)<1){
					master.incCurrHp(-(master.getCurrHp()*0.05f));
					UI_Control.AddLog("격파 하고보니 조금 아프다..");
					UI_Control.AddLog("체력이 5% 감소하였다.");
				}
				master.coolTime[4] = skillArr[18].coolTime;
				master.incTurnCount ();
				break;
			case 20:	//탐색					-주변 3x3에 숨겨진 문이나 함정이 있으면 발견된다.
				gameManager.turn = Turn.Acting;
				UI_Control.AddLog("주변을 탐색했다! (미구현)");
				master.incTurnCount();
				break;
			case 25:	//함정 해체				-정면의 함정을 제거한다. 함정이 없을 시 아무 일도 일어나지 않는다.
				break;
			case 26:	//함정 설치-화염 함정		-정면에 화염 함정을 설치한다.
				break;
			case 27:	//함정 설치-토 함정		-정면에 토 함정을 설치한다.
				break;
			case 28:	//함정 설치-전기 함정		-정면에 전기 함정을 설치한다.
				break;
			case 29:	//함정 설지-대야 함정		-정면에 대야 함정을 설치한다.
				break;
			case 30:	//탐색					-주변 3x3에 숨겨진 문이나 함정이 있으면 발견된다.
				break;
			}
		} else {
			//코스트를 지불할 수 없으므로 스킬 사용이 불가능
		}
	}

	public void CancelSkill(int skillCode){
		switch(skillCode){
		case 0:		//정신 차리기				-취기가 0이 된다.
			break;
		case 1:		//자작하면 여친없음		-자신에게 '분노'를 건다.
			break;
		case 2:		//고집 피우기				-자신에게 '고집'을 건다.
			break;
		case 3:		//필름 끊기				-해당층의 랜덤한 방으로 이동한다.
			break;
		case 5:		//원기 회복				-0%~(소모한 취기량)% 중 랜덤으로 체력을 회복
			break;
		case 6:		//취기 유지				-자신에게 '취기유지'를 건다. 이미 걸려있으면 '취기유지'를 제거한다.
			break;
		case 7:		//광분					-자신에게 '광분'을 건다.
			break;
		case 8:		//피 폭발				-선택한 방향으로 근거리 4칸, 소모한 체력만큼의 데미지로 공격한다.
			break;
		case 10:	//사격					-선택한 방향으로 원거리 4칸, (5+민첩)의 데미지로 공격한다.
			break;
		case 11:	//불 뿜기				-선택한 방향으로 원뿔모양 범위에 (5 +민첩)의 데미지로 공격한다. 맞은 적은 '화상'에 걸린다.
			break;
		case 12:	//부식성 사격				-선택한 방향으로 원거리 4칸, (4+민첩)의 데미지로 공격한다. 맞은 적은 '중독'에 걸린다.
			break;
		case 13:	//속사					-선택한 방향으로 원거리 4칸, (5+민첩)의 데미지로 2번 공격한다.
			break;
		case 15:	//깨진 병				-자신에게 '깨진 병'을 건다.
			break;
		case 16:	//연타					-기본 공격의 90% 피해로 2번 공격한다.
			master.attackScript.onHitFlag.Remove (9);	//9 : 힘을 90%로 취소
			master.attackScript.onHitFlag.Remove (10);	//10: 한번 더 공격 취소
			master.attackScript.needPay.Remove(16);	//16번 코스트 취소

			//Debug.Log (master.attackScript.onHitFlag.Count);
			//Debug.Log (master.attackScript.needPay.Count);

			master.skillStandBy = -1;
			break;
		case 17:	//빈병 내리치기			-근접 1칸, 기본 공격의 120% 데미지로 공격한다. 적은 3턴 간 행동불능이 된다.
			master.attackScript.onHitFlag.Remove(3);	// 3 : 3턴 행동불능 취소
			master.attackScript.onHitFlag.Remove(7);	// 7 : 120% 로 공격 취소
			master.attackScript.needPay.Remove(17);		// 17번 코스트 취소

			//Debug.Log (master.attackScript.onHitFlag.Count);
			//Debug.Log (master.attackScript.needPay.Count);

			master.skillStandBy = -1;
			break;
		case 18:	//병목 치기				-주변5x5에 있는 적의 공격력 5턴 간 20%하락
			break;
		case 20:	//탐색					-주변 3x3에 숨겨진 문이나 함정이 있으면 발견된다.
			break;
		case 25:	//함정 해체				-정면의 함정을 제거한다. 함정이 없을 시 아무 일도 일어나지 않는다.
			break;
		case 26:	//함정 설치-화염 함정		-정면에 화염 함정을 설치한다.
			break;
		case 27:	//함정 설치-토 함정		-정면에 토 함정을 설치한다.
			break;
		case 28:	//함정 설치-전기 함정		-정면에 전기 함정을 설치한다.
			break;
		case 29:	//함정 설지-대야 함정		-정면에 대야 함정을 설치한다.
			break;
		case 30:	//탐색					-주변 3x3에 숨겨진 문이나 함정이 있으면 발견된다.
			break;
		}
	}
}

/*
 *  0. 정신 차리기 		-	대작가_1			Active
 *  1. 자작하면 여친없음	-	대작가_2			Active
 *  2. 고집 피우기		-	대작가_3			Active
 *  3. 필름끊기			-	대작가_4			Active
 *  4. 대작				-	대작가_5			Passive
 * 
 *  5. 원기 회복			-	광전사_1			Active
 *  6. 광분				-	광전사_2			Active
 *  7. 피폭발			-	광전사_3			Active
 *  8. 취기 유지			-	광전사_4			Passive
 *  9. 분노				-	광전사_5			Passive
 * 
 * 10. 사격				-	코브라_1			Active
 * 11. 불뿜기			-	코브라_2			Active
 * 12. 부식성 사격		-	코브라_3			Active
 * 13. 속사				-	코브라_4			Active
 * 14. 체내보관			-	코브라_5			Passive
 * 
 * 15. 깨진 병			-	술병 싸움꾼_1		Active
 * 16. 연타				-	술병 싸움꾼_2		Active
 * 17. 빈병 내리치기		-	술병 싸움꾼_3		Active
 * 18. 병목치기			-	술병 싸움꾼_4		Active
 * 19. 빈병 수집가		-	술병 싸움꾼_5		Passive
 * 
 * 20. 탐색				-	투척병_1			Active
 * 21. 일취월장			-	투척병_2			Passive
 * 22. 알코올의 부름		-	투척병_3			Passive
 * 23. 금단증상			-	투척병_4			Passive
 * 24. 단호한 의지		-	투척병_5			Passive
 * 
 * 25. 함정 해체			-	공사판 아재_1		Active
 * 26. 함정 설치-화염		-	공사판 아재_2		Active
 * 27. 함정 설치-토		-	공사판 아재_3		Active
 * 28. 함정 설치-전기		-	공사판 아재_4		Active
 * 29. 함정 설치-대야		-	공사판 아재_5		Active
 * 
 * 30. 탐색				-	소믈리에_1		Active
 * 31. 라벨 바꾸기		-	소믈리에_2		Passive
 * 32. 감별사			-	소믈리에_3		Passive
 * 33. 유혹				-	소믈리에_4		Passive
 * 34. 술의 가호			-	소믈리에_5		Passive
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ATTACK : MonoBehaviour {

	public int attackFlag = 0;
	public List<int> onHitFlag;
	public List<int> needPay;
	public List<GameObject> attackableArr;
	private float weaponRange = 1f;					//무기 사정거리
	private bool isPierce = true;					//관통 여부
	private float weaponPower = 0f;					//무기 데미지
	private float attackPower = 0f;					//무기 데미지 + 힘
	private float finalDamage = 0f;					//최종 데미지 (무기데미지 + 힘 + 보너스데미지(트루뎀))
	private Entity master;

	private float buffer7 = 0f;
	private float buffer9 = 0f;

	void Start(){
		master = gameObject.GetComponent<Entity> ();
		attackableArr = master.attackable;

	}
	
	public bool attackable(int attackFlag){
		weaponRange = master.range;
		if (master.equipWeapon!=null) {					//장착장비가 있는경우 정보를 받아옴.
			isPierce = master.equipWeapon.isPierce;
			weaponRange = master.equipWeapon.weaponRange;
		}
		if (master.skillStandBy == 17) {	//빈병 내리치기 : 근접 1칸
			weaponRange = 1;
		}

		//Debug.Log ("isPierce : " + isPierce);
		//Debug.Log ("weaponRange : " + weaponRange);
	
		RaycastHit2D[] hit = new RaycastHit2D[0];
		switch (attackFlag) {
		case Direction.STAY:
			break;
		case Direction.LEFT:
			hit = Physics2D.RaycastAll (transform.position, Vector2.left, weaponRange);
			//Debug.Log ("l");
			break;
		case Direction.LEFTUP:		
			hit = Physics2D.RaycastAll (transform.position, new Vector2 (-1, 1), weaponRange * 1.414f);
			//Debug.Log ("lu");
			break;
		case Direction.UP:		
			hit = Physics2D.RaycastAll (transform.position, Vector2.up, weaponRange);
			//Debug.Log ("u");
			break;
		case Direction.RIGHTUP:		
			hit = Physics2D.RaycastAll (transform.position, new Vector2 (1, 1), weaponRange * 1.414f);
			//Debug.Log ("ru");
			break;
		case Direction.RIGHT:
			;			
			hit = Physics2D.RaycastAll (transform.position, Vector2.right, weaponRange);
			//Debug.Log ("r");
			break;
		case Direction.RIGHTDOWN:		
			hit = Physics2D.RaycastAll (transform.position, new Vector2 (1, -1), weaponRange * 1.414f);
			//Debug.Log ("rd");
			break;
		case Direction.DOWN:
			hit = Physics2D.RaycastAll (transform.position, Vector2.down, weaponRange);
			//Debug.Log ("d");
			break;
		case Direction.LEFTDOWN:		
			hit = Physics2D.RaycastAll (transform.position, new Vector2 (-1, -1), weaponRange * 1.414f);
			break;
		}

		if(hit.Length==0){
			return false;
		}
		if (isPierce) {			//관통형
			for (int loop = 0; loop < hit.Length; loop++) {
				if(hit[loop].collider.transform.tag == "Wall"){
					//break;
				}
				if (gameObject.GetComponent<Player> ()) {
					if (hit [loop].collider.transform.tag == "Enemy") {
						//Debug.Log (hit[loop].transform.position);
						master.attackable.Add (hit [loop].transform.gameObject);
					}
				} else {
					if (hit [loop].collider.transform.tag == "Player") {
						//Debug.Log (hit[loop].transform.position);
						master.attackable.Add (hit [loop].transform.gameObject);
					}
				}
			}
		} else {				//비관통형
			for (int loop = 0; loop < hit.Length; loop++) {
				if(hit[loop].collider.transform.tag == "Wall"){
					//break;
				}
				if (gameObject.GetComponent<Player> ()) {
					if (hit [loop].collider.transform.tag == "Enemy") {
						//Debug.Log (hit[loop].transform.position);
						master.attackable.Add (hit [loop].transform.gameObject);
						return true;
					}
				} else {
					if (hit [loop].collider.transform.tag == "Player") {
						//Debug.Log (hit[loop].transform.position);
						master.attackable.Add (hit [loop].transform.gameObject);
						return true;
					}
				}
			}
		}

		
		if(master.attackable.Count!=0){
			return true;
		}
		else{
			return false;
		}
	}
	
	public void SetAttack(int attackFlag){
		this.attackFlag = attackFlag;
		master.incTurnCount();
		for (int i = 0; i < attackableArr.Count; i++) {
			//즉발 온히트 처리
			for (int j = 0; j < onHitFlag.Count; j++) {
				switch (onHitFlag [j]) {
				case 1:		//공격 시 대상 1턴동안 행동불능
					attackableArr [i].GetComponent<Buffmanager> ().addPassive (30);
					break;
				case 2:		//공격 시 대상 2턴동안 행동불능
					attackableArr [i].GetComponent<Buffmanager> ().addPassive (42);
					break;
				case 3:		//공격 시 대상 3턴동안 행동불능
					attackableArr [i].GetComponent<Buffmanager> ().addPassive (47);
					break;
				case 4:		//공격 시 대상 1턴동안 감전
					attackableArr [i].GetComponent<Buffmanager> ().addPassive (34);
					break;
				case 5:		//공격 시 대상 2턴동안 감전
					attackableArr [i].GetComponent<Buffmanager> ().addPassive (45);
					break;
				case 6:		//공격 시 대상 3턴동안 감전
					attackableArr [i].GetComponent<Buffmanager> ().addPassive (50);
					break;
				}
			}
		}
		StartCoroutine ("Attack");
	}

	// Update is called oncer frame
	IEnumerator Attack(){
		Vector3 pos = gameObject.transform.position;
		while (true) {
			switch (attackFlag) {
			case Direction.STAY:
				yield break;
			case Direction.LEFT		:
				break;
			case Direction.LEFTUP	:
				break;
			case Direction.UP		:
				break;
			case Direction.RIGHTUP	:
				break;
			case Direction.RIGHT	:
				break;
			case Direction.RIGHTDOWN:
				break;
			case Direction.DOWN		:
				break;
			case Direction.LEFTDOWN	:
				break;
			}
			if(true){//공격 애니메이션이 끝나면
				GameObject canvas = Instantiate(Resources.Load("ValueDisplay", typeof(GameObject))) as GameObject;
				canvas.GetComponent<RectTransform>().localPosition = gameObject.transform.position;
				if(master.equipWeapon != null){
					weaponPower = master.equipWeapon.attackPower;				//무기공격력 받아옴
				}else{
					weaponPower = 0;											//무기 없으면 0
				}
				attackPower = weaponPower + master.getStr();					//무기공격력과 힘 합산

				for(int i = 0; i < attackableArr.Count; i++){

					//공격 애니메이션 후 온히트 처리
					for(int j = 0 ; j < onHitFlag.Count ; j++){
						switch(onHitFlag[j]){
						case 7:		//120% 데미지로 공격 & 빈병 내리치기 스탠바이 해제
							UI_Control.AddLog("빈병 내리치기가 작렬!");
							UI_Control.AddLog("상대방이 행동불능이 되었다!");
							buffer7 = attackPower * 0.2f;
							attackPower += buffer7;
							master.skillStandBy = -1;
							break;
						case 8:		//3의 추가데미지	& '깨진 병' 버프 해제
							UI_Control.AddLog(gameObject.name + "의 깨진 병 공격!");
							attackPower += 3f;
							for(int k = master.buffList.Count - 1 ; k >= 0 ; k--){
								if(master.buffList[i].buffCode == 38){
									master.buffList.RemoveAt(i);
								}
							}
							break;
						case 9:		//90% 데미지로 공격 & 연타 스탠바이 해제
							UI_Control.AddLog("연타가 작렬!");
							buffer9 = attackPower * 0.1f;
							attackPower -= buffer9;
							master.skillStandBy = -1;
							break;
						case 10:	//한번 더 공격 (연타 전용)
							finalDamage = attackPower+master.getBonusDamage();
							GameObject damageDisplay2 = Instantiate(Resources.Load("damageDisplay", typeof(GameObject))) as GameObject;
							damageDisplay2.transform.SetParent(canvas.transform);
							damageDisplay2.GetComponent<RectTransform> ().localPosition = attackableArr[i].transform.position - gameObject.transform.position + new Vector3(0,1.5f,0);
							damageDisplay2.GetComponent<UnityEngine.UI.Text> ().text = finalDamage.ToString();
							damageDisplay2.GetComponent<UI_ValueDisplay> ().activeDisplay ();

							float exp2 = attackableArr[i].GetComponent<Entity>().Damage(finalDamage);									//타겟에게 데미지
							master.incCurrHp(master.getBloodSuck());																	//흡혈 적용
							if(gameObject.GetComponent<Player>()){																		//경험치 적용
								gameObject.GetComponent<Player>().incCurrExp(exp2);
							}
							break;
						}
					}

					for(int j = needPay.Count-1 ; j >= 0 ; j--){	//온히트 스킬들의 코스트를 지불.
						master.skillManager.payCost(needPay[j]);
						needPay.RemoveAt (j);
					}

					if(onHitFlag.Count==0){
						UI_Control.AddLog(gameObject.name + "의 공격!");
					}

					//데미지 수치 표시
					finalDamage = attackPower+master.getBonusDamage();
					GameObject damageDisplay = Instantiate(Resources.Load("damageDisplay", typeof(GameObject))) as GameObject;
					damageDisplay.transform.SetParent(canvas.transform);
					damageDisplay.GetComponent<RectTransform> ().localPosition = attackableArr[i].transform.position - gameObject.transform.position + new Vector3(0,1,0);
					damageDisplay.GetComponent<UnityEngine.UI.Text> ().text = finalDamage.ToString();
					damageDisplay.GetComponent<UI_ValueDisplay> ().activeDisplay ();

					float exp = attackableArr[i].GetComponent<Entity>().Damage(finalDamage);											//타겟에게 데미지
					master.incCurrHp(master.getBloodSuck());																			//흡혈 적용
					if(gameObject.GetComponent<Player>()){																				//경험치 적용
						gameObject.GetComponent<Player>().incCurrExp(exp);
					}

					//온히트 제거
					for(int j = onHitFlag.Count-1 ; j >= 0 ; j--){
						switch(onHitFlag[j]){
						case 7:
							attackPower -= buffer7;
							buffer7 = 0f;
							break;
						case 8:
							attackPower -= 3f;
							break;
						case 9:
							attackPower += buffer9;
							buffer9 = 0f;
							break;
						}
						onHitFlag.RemoveAt(j);
					}
					master.isBrokenBottle = false;
				}
				attackFlag = Direction.STAY;
				gameObject.GetComponent<Entity>().attackable.Clear();
				yield break;
			}
			yield return null;
		}
	}
}


/*
 * 
 * 아무 효과 없음						-	code0
 * 다음 공격 시 대상 1턴동안 행동불능	-	code1
 * 다음 공격 시 대상 2턴동안 행동불능	-	code2
 * 다음 공격 시 대상 3턴동안 행동불능	-	code3
 * 다음 공격 시 대상 1턴동안 감전		-	code4
 * 다음 공격 시 대상 2턴동안 감전		-	code5
 * 다음 공격 시 대상 3턴동안 감전		-	code6
 * 120%의 공격력으로 공격				-	code7	-빈병 내리치기 전용
 * 다음 공격 시 10의 추가 데미지		-	code8	-깨진 병 전용
 * 90%의 공격력으로 공격				-	code9	-연타 전용
 * 한번 더 공격						-	code10	-연타 전용
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */
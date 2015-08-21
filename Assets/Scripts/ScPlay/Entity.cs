using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {
		
	public MOVE moveScript;								//무브 스크립트
	public ATTACK attackScript;							//어택 스크립트
	public SkillManager skillManager;					//스킬매니저
	public Buffmanager buffManager;						//버프매니저

	public Weapon equipWeapon;							//현재 착용한 무기

	protected int entityCode;							//엔타이티 분류코드
	protected string entityName;						//엔타이티 이름
	protected float fullHp;								//HP 최대치
	protected float currHp;								//현재 HP
	protected float bonusDamage;						//추가 데미지 (트루 데미지로 적용)
	protected float str;								//힘
	protected float dex;								//민첩
	protected float accuracyRate;						//명중률
	protected float avoidRate;							//회피율
	protected float criticalRate;						//크리티컬 확률
	protected float absoluteAvoidRate;					//완전 회피율
	protected float trapFindRate;						//함정발견률
	protected float cocktailSuccessRate;				//세이커(칵테일 제조기) 제작 성공률
	protected float labeledAlcoholDropRate;				//라벨이 있는 술 드랍 확률
	protected float rareItemDropRate;					//rareItem드랍확률
	protected float luckZeroDeathRate;					//운이 0이 되어 다음턴에 죽을 확률
	protected int speedLevel;							//속도등급
	protected int countPerTurn;							//턴당 턴카운트 증가 수
	protected float intelligence;						//지식
	protected float luck;								//운
	protected float fullMp;								//취기 최대치
	protected float currMp;								//현재 취기
	protected float bloodSuck;							//타격시 흡혈량
	public bool isKoala;								//만취상태 여부
	public bool isStun;									//행동불능 여부
	public bool isOoC;									//통제불능 여부 (Out of Conrtol)
	protected int drunkenType;							//주사유형
	protected int turnCount;							//턴카운트
	public List<Buff> buffList = new List<Buff>();		//버프 목록
	public List<Buff> passiveList = new List<Buff> ();	//패시브 버프 목록
	public int range;									//사거리
	public int sight;									//시야

	public float regenHp;								//턴당 HP 증가량
	public float regenMp;								//턴당 MP 증가량
	public float regenHpBuffer;							//턴당 증가량이 0~1 사이일 경우 저장했다가 1이 채워지면 증가

	public int skillStandBy;							//-1이면 사용중인 스킬이 없는 상태 그외엔 스킬코드로 사용
	public bool isSustain;								//취기 유지 적용 여부		(광전사 전용 수치)
	public bool isBrokenBottle;							//깨진 병 적용 여부		(술병 싸움꾼 전용 수치)
	public int alcoholStack;							//주량 스택				(코브라 전용 수치)


	public int up, down, left, right, leftUp, rightUp, leftDown, rightDown;
	public List<GameObject> attackable = new List<GameObject>();
	public void init(int entityCode_, int childCode_){
		moveScript = gameObject.GetComponent<MOVE>();
		attackScript = gameObject.GetComponent<ATTACK>();
		skillManager = gameObject.GetComponent<SkillManager> ();
		buffManager = gameObject.GetComponent<Buffmanager>();
		entityCode = entityCode_;
		entityName = Config.name[entityCode];
		gameObject.name = entityName;
		fullHp = Config.fullHp[entityCode];
		currHp = fullHp;
		bonusDamage = 0;
		str = Config.str[entityCode];
		dex = Config.dex[entityCode];
		accuracyRate = dex * Config.accuracyRatePerDex;
		avoidRate = dex * Config.avoidRatePerDex;
		criticalRate = luck * Config.criticalRatePerLuck;
		absoluteAvoidRate = luck * Config.absoluteAvoidRatePerLuck;
		trapFindRate = intelligence * Config.trapFindRatePerIntelligence;
		cocktailSuccessRate = 0.5f + intelligence * Config.cocktailSuccessRatePerIntelligence;
		labeledAlcoholDropRate = 0.05f + luck * Config.labeledAlcoholDropRatePerLuck;
		rareItemDropRate = 0.1f + luck * Config.rareItemDropRatePerLuck;
		luckZeroDeathRate = Config.luckZeroDeathRate;
		if (dex <= 0){
			speedLevel = 1;
			countPerTurn = 8;
		}else if (dex > 0 && dex <= 30){
			speedLevel = 2;
			countPerTurn = 4;
		}else if (dex > 30 && dex <= 60){
			speedLevel = 3;
			countPerTurn = 2;
		}else if (dex >60){
			speedLevel = 4;
			countPerTurn = 1;
		}
		//Debug.Log (name + " : counterPerTurn == " + countPerTurn);
		intelligence = Config.intelligence[entityCode];
		luck = Config.luck[entityCode];
		fullMp = Config.fullMp;
		currMp = 0;
		bloodSuck = 0;
		isKoala = false;
		isStun = false;
		isOoC = false;
		drunkenType = Config.drunkenType[entityCode];
		turnCount = 0;
		range = Config.range[entityCode];
		sight = Config.sight[entityCode];
		regenHp = 0.2f;
		regenMp = -2;
		regenHpBuffer = 0;

		skillStandBy = -1;
		isSustain = false;
		alcoholStack = 0;

		transform.FindChild ("Image").GetComponent<SpriteRenderer> ().sortingOrder = ((int)transform.position.y);

		initChild (childCode_);
	}

	public virtual void initChild(int childeCode){
		
	}

	/********************Getter***********************/

	public float getBonusDamage(){
		return this.bonusDamage;
	}

	public int getEntityCode(){
		return this.entityCode;
	}

	public int getTurnCount(){
		return this.turnCount;
	}

	public float getStr(){
		return this.str;
	}

	public float getDex(){
		return this.dex;
	}

	public int getSpeedLevel(){
		return this.speedLevel;
	}

	public float getIntelligence(){
		return this.intelligence;
	}

	public float getLuck(){
		return this.luck;
	}

	public float getCurrHp(){
		return this.currHp;
	}

	public float getFullHp(){
		return this.fullHp;
	}

	public float getCurrMp(){
		return this.currMp;
	}

	public float getFullMp(){
		return this.fullMp;
	}
	
	public float getBloodSuck(){
		return this.bloodSuck;
	}

	/********************Setter***********************/

	public void setStr(float s){
		this.str = s;
	}

	public void setDex(float d){
		this.dex = d;
		if (dex <= 0){
			speedLevel = 1;
			countPerTurn = 8;
		}else if (dex > 0 && dex <= 30){
			speedLevel = 2;
			countPerTurn = 4;
		}else if (dex > 30 && dex <= 60){
			speedLevel = 3;
			countPerTurn = 2;
		}else if (dex >60){
			speedLevel = 4;
			countPerTurn = 1;
		}
		accuracyRate = dex * Config.accuracyRatePerDex;
		avoidRate = dex * Config.avoidRatePerDex;
	}

/*	public void setAccuracyRate(float ar){				//Danger!! 기본능력치가 아닌 능력치를 직접 수정하는것은 위험하다.
		this.accuracyRate = ar;
	}

	public void setCriticalRate(float cr){				//Danger!! 기본능력치가 아닌 능력치를 직접 수정하는것은 위험하다.
		this.criticalRate = cr;
	}

	public void setSpeedLevel(int sl){					//Danger!! 기본능력치가 아닌 능력치를 직접 수정하는것은 위험하다.
		speedLevel = sl;
		switch(speedLevel){
		case 0:
			countPerTurn = 16;
			break;
		case 1:
			countPerTurn = 8;
			break;
		case 2:
			countPerTurn = 4;
			break;
		case 3:
			countPerTurn = 2;
			break;
		case 4:
			countPerTurn = 1;
			break;
		}
	}*/

	public void setIntelligence(float i){
		this.intelligence = i;
		trapFindRate = intelligence * Config.trapFindRatePerIntelligence;
		cocktailSuccessRate = 0.5f + intelligence * Config.cocktailSuccessRatePerIntelligence;
	}
	
	public void setLuck(float l){
		this.luck = l;
		criticalRate = luck * Config.criticalRatePerLuck;
		absoluteAvoidRate = luck * Config.absoluteAvoidRatePerLuck;
		labeledAlcoholDropRate = 0.05f + luck * Config.labeledAlcoholDropRatePerLuck;
		rareItemDropRate = 0.1f + luck * Config.rareItemDropRatePerLuck;
	}

	//Hp를 직접 Set하는것은 별로 좋지 않을것으로 예상되므로 가급적 쓰지 말것.
	public void setCurrHp(float ch){
		this.currHp = ch;
		float rate = (float)currHp / (float)fullHp;
		if(rate<0){
			rate = 0;
		}
		if(currHp<=0){
			if(gameObject.GetComponent<Enemy>()){
				gameObject.GetComponent<Enemy>().DestroyGameObject();
				//가지고있던 템을 떨궈야함.
			}
			else{
				gameObject.GetComponent<Player>().DestroyGameObject();
			}
		}
		hpBarUpdate(rate);
	}

	public void setFullHp(float fh){
		this.fullHp = fh;
	}

	public void setBloodSuck(float bs){
		this.bloodSuck = bs;
	}


	/******************Incrementer*********************/
	

	public void incTurnCount(){
		if (this.turnCount == 0) {
			this.turnCount += countPerTurn;
		}
	}

	public void incTurnCount(int amount){
		this.turnCount += amount;
	}

	public void incFullHp(float amount){
		if (amount < 0) {
			this.fullHp += amount;
			if (currHp > fullHp) {
				Damage (currHp - fullHp);
			}
		} else {
			this.fullHp += amount;
			this.currHp += amount;
		}
	}

	public void incCurrHp(float amount){
		if(amount < 0){
			Damage (-amount);
		}else{
			this.currHp +=amount;
			if(currHp>fullHp){
				currHp = fullHp;
			}
		}
	}

	public void incFullMp(float amount){
		if (amount < 0) {
			this.fullMp += amount;
			if (currMp > fullMp) {
				currMp = fullMp;
			}
		} else {
			this.fullMp += amount;
		}
	}

	public void incCurrMp(float amount){
		currMp += amount;
		if (currMp < 0) {
			currMp = 0;
		}
		if (currMp > fullMp) {
			currMp = fullMp;
		}
	}

	public void incBonusDamage(float amount){
		this.bonusDamage += amount;
	}

	public void incStr(float amount){
		this.str += amount;
	}

	public void incDex(float amount){
		this.dex += amount;
		if (dex <= 0){
			speedLevel = 1;
			countPerTurn = 8;
		}else if (dex > 0 && dex <= 30){
			speedLevel = 2;
			countPerTurn = 4;
		}else if (dex > 30 && dex <= 60){
			speedLevel = 3;
			countPerTurn = 2;
		}else if (dex >60){
			speedLevel = 4;
			countPerTurn = 1;
		}
		accuracyRate = dex * Config.accuracyRatePerDex;
		avoidRate = dex * Config.avoidRatePerDex;
	}

	public void incIntelligence(float amount){
		this.intelligence += amount;
		trapFindRate = intelligence * Config.trapFindRatePerIntelligence;
		cocktailSuccessRate = 0.5f + intelligence * Config.cocktailSuccessRatePerIntelligence;
	}
	
	public void incLuck(float amount){
		this.luck += amount;
		criticalRate = luck * Config.criticalRatePerLuck;
		absoluteAvoidRate = luck * Config.absoluteAvoidRatePerLuck;
		labeledAlcoholDropRate = 0.05f + luck * Config.labeledAlcoholDropRatePerLuck;
		rareItemDropRate = 0.1f + luck * Config.rareItemDropRatePerLuck;
	}

	public void incBloodSuck(float amount){
		this.bloodSuck += amount;
	}

	public void incAlcoholStack(int amount){
		this.alcoholStack += amount;
		if (alcoholStack < 0) {
			alcoholStack = 0;
			Debug.Log("alcoholStack can't be negative value");
		}
	}
	

	/******************Decrementer*********************/


	public void decTurnCount(){
		if (0 < this.turnCount) {
			this.turnCount--;
		}
	}

	public void SetMove(int direction){
		moveScript.SetMove(direction);
	}

	public void SetAttack(int direction){
		attackScript.SetAttack(direction);
	}

	public bool isStay(){
		if(moveScript.moveFlag==Direction.STAY &&attackScript.attackFlag==Direction.STAY){
			return true;
		}
		return false;
	}

	public void EnterDetection(int flag, int kindTag){
		switch(flag){
		case Direction.UP:
			up = kindTag; 
			break;
		case Direction.DOWN:
			down = kindTag;
			break;
		case Direction.LEFT:
			left = kindTag;
			break;
		case Direction.RIGHT:
			right = kindTag;
			break;
		case Direction.LEFTUP:
			leftUp = kindTag;
			break;
		case Direction.LEFTDOWN:
			leftDown = kindTag;
			break;
		case Direction.RIGHTUP:
			rightUp = kindTag;
			break;
		case Direction.RIGHTDOWN:
			rightDown = kindTag;
			break;
		}
	}

	public void ExitDetection(int flag){
		EnterDetection(flag, KindTag.empty);
	}

	public float Damage(float dam){//사망시에 드랍 경험치 리턴(사망아닐 시 0 리턴)
		currHp-=dam;
		float rate = (float)currHp / (float)fullHp;
		float exp = 0;
		if(rate<0){
			rate = 0;
		}
		if(currHp<=0){
			if(gameObject.GetComponent<Enemy>()){	//에네미 사망 시
				UI_Control.AddLog(gameObject.name + "은(는) 사망 하였다");
				gameObject.GetComponent<Enemy>().DestroyGameObject();
				exp = gameObject.GetComponent<Enemy>().GetDropExp();
				//가지고있던 템을 떨궈야함.
			}
			else{									//플레이어 사망 시
				UI_Control.AddLog(gameObject.name + "은(는) 사망 하였다");
				hpBarUpdate(rate);
				gameObject.GetComponent<Player>().DestroyGameObject();
			}
			return exp;
		}
		hpBarUpdate(rate);
		return exp;
	}

	protected virtual void hpBarUpdate(float rate){
	}
}

public class KindTag{
	public const int empty = 0;
	public const int wall = 1;
	public const int item = 2;
	public const int enemy = 3;
	public const int player = 4;
	public const int entity = 5;
}

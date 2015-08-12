using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

	protected MOVE moveScript;							//무브 스크립트
	protected ATTACK attackScript;						//어택 스크립트
	protected Buffmanager buffManager;					//버프매니저

	protected int entityCode;							//케릭터 분류코드
	protected float fullHp;								//HP 최대치
	protected float currHp;								//현재 HP
	protected float str;								//힘
	protected float dex;								//민첩
	protected float accuracyRate;						//명중률
	protected float avoidRate;							//회피율
	protected float criticalRate;						//크리티컬 확률
	protected float absoluteAvoidRate;					//완전 회피율
	protected int speedLevel;							//속도등급
	protected int countPerTurn;							//턴당 턴카운트 증가 수
	protected float intelligence;						//지식
	protected float luck;								//운
	protected float fullMp;								//취기 최대치
	protected float currentMp;							//현재 취기
	protected float bloodSuck;							//타격시 흡혈량
	protected bool isKoala;								//만취상태 여부
	protected int drunkenType;							//주사유형
	protected int turnCount;							//턴카운트
	public List<Buff> buffList = new List<Buff>();		//버프 목록

	public int up, down, left, right, leftUp, rightUp, leftDown, rightDown;
	public List<GameObject> attackable = new List<GameObject>();
	public void init(int entityCode_, int childCode_){
		moveScript = gameObject.GetComponent<MOVE>();
		attackScript = gameObject.GetComponent<ATTACK>();
		buffManager = new Buffmanager ();
		entityCode=entityCode_;
		name = Config.name[entityCode];
		fullHp=Config.fullHp[entityCode];
		currHp = fullHp;
		str = Config.str[entityCode];
		dex = Config.dex[entityCode];
		accuracyRate = dex * Config.accuracyRatePerDex;
		avoidRate = dex * Config.avoidRatePerDex;
		criticalRate = luck * Config.criticalRatePerLuck;
		absoluteAvoidRate = luck * Config.absoluteAvoidRatePerLuck;
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
		Debug.Log (name + " : counterPerTurn == " + countPerTurn);
		intelligence = Config.intelligence[entityCode];
		luck = Config.luck[entityCode];
		fullMp = Config.fullMp;
		currentMp = 0;
		bloodSuck = 0;
		if (currentMp == fullMp)
			isKoala = true;
		else 
			isKoala = false;
		drunkenType = Config.drunkenType[entityCode];
		turnCount = 0;
		initChild (childCode_);
	}

	public virtual void initChild(int childeCode){
		
	}

	/********************Getter***********************/

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
	}

	public void setAccuracyRate(float ar){
		this.accuracyRate = ar;
	}

	public void setCriticalRate(float cr){
		this.criticalRate = cr;
	}

	public void setSpeedLevel(int sl){
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
	}

	public void setIntelligence(float i){
		this.intelligence = i;
	}
	
	public void setLuck(float l){
		this.luck = l;
	}

	public void setCurrHp(float ch){
		this.currHp = ch;
		float rate = (float)currHp / (float)fullHp;
		if(rate<0){
			rate = 0;
		}
		if (gameObject.GetComponent<Enemy> ()) {
			gameObject.transform.FindChild ("HUD").FindChild ("GreenBar").localScale = new Vector3 (rate, 1, 1);
		} else {
			UI_Gauge.SetPlayerHP(rate);
		}
		if(currHp<=0){
			if(gameObject.GetComponent<Enemy>()){
				gameObject.GetComponent<Enemy>().DestroyGameObject();
				//가지고있던 템을 떨궈야함.
			}
			else{
				DestroyGameObject();
			}
		}
		hpBarUpdate();
	}

	public void setFullHp(float fh){
		this.fullHp = fh;
	}

	public void setBloodSuck(float bs){
		this.bloodSuck = bs;
	}

	/***************************************************/

	public void incTrunCount(){
		if (this.turnCount == 0) {
			this.turnCount += countPerTurn;
		}
	}

	public void incTrunCount(int amount){
		this.turnCount += amount;
	}

	public void decTurnCount(){
		if (0 < this.turnCount) {
			this.turnCount--;
		}
	}

	public void RegenHp(float amount){
		if(amount<0){
			Debug.Log("amount must be positive value");
			Damage (amount);
		}else{
			this.currHp +=amount;
			if(currHp>fullHp){
				currHp = fullHp;
			}
		}
	}

	public void DestroyGameObject(){
		Destroy (gameObject);
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

	public bool Damage(float dam){//사망시에 true 리턴
		currHp-=dam;
		float rate = (float)currHp / (float)fullHp;
		if(rate<0){
			rate = 0;
		}
		if (gameObject.GetComponent<Enemy> ()) {
			gameObject.transform.FindChild ("HUD").FindChild ("GreenBar").localScale = new Vector3 (rate, 1, 1);
		} else {
			UI_Gauge.SetPlayerHP(rate);
		}
		if(currHp<=0){
			if(gameObject.GetComponent<Enemy>()){
				gameObject.GetComponent<Enemy>().DestroyGameObject();
				//가지고있던 템을 떨궈야함.
			}
			else{
				DestroyGameObject();
			}
			return true;
		}
		hpBarUpdate();
		return false;
	}

	protected virtual void hpBarUpdate(){}
}

public class KindTag{
	public const int empty = 0;
	public const int wall = 1;
	public const int item = 2;
	public const int enemy = 3;
	public const int player = 4;
}

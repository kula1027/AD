using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

	protected int entityCode;							//케릭터 분류코드
	protected int fullHp;								//HP 최대치
	protected int currHp;								//현재 HP
	protected int str;									//힘
	protected int dex;									//민첩
	protected int accuracyRate;							//명중률
	protected int accuracyRatePerDex;					//명중률 나누기 민첩
	protected int avoidRate;							//회피율
	protected int avoidRatePerDex;						//회피율 나누기 민첩
	protected int speedLevel;							//속도등급
	protected int fullMp;								//취기 최대치
	protected int currentMp;							//현재 취기
	protected bool isKoala;								//만취상태 여부
	protected int drunkenType;							//주사유형
	protected int turnCount;							//턴카운트
	public List<Buff> buffList = new List<Buff>();		//버프 목록

	public int up, down, left, right, leftUp, rightUp, leftDown, rightDown;
<<<<<<< HEAD

=======
	public List<GameObject> attackable = new List<GameObject>();
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
	public void init(int entityCode_, int childCode_){
		entityCode=entityCode_;
		name = Config.name[entityCode];
		fullHp=Config.fullHp[entityCode];
		currHp = fullHp;
		str = Config.str[entityCode];
		dex = Config.dex[entityCode];
		accuracyRatePerDex = Config.accuracyRatePerDex[entityCode];
		accuracyRatePerDex = dex * accuracyRatePerDex;
		avoidRatePerDex = Config.avoidRatePerDex[entityCode];
		avoidRatePerDex = dex * avoidRatePerDex;
		if (dex == 0)
			speedLevel = 1;
		else if (dex > 0 && dex <= 30)
			speedLevel = 2;
		else if (dex > 30 && dex <= 60)
			speedLevel = 3;
		else if (dex >60)
			speedLevel = 4;
		fullMp = Config.fullMp[entityCode];
		currentMp = fullMp;
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

	public int getTurnCount(){
		return this.turnCount;
	}

	public int getDex(){
		return this.dex;
	}

<<<<<<< HEAD
=======
	public int getStr(){
		return this.str;
	}

>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
	public void incTrunCount(int amount){
		if (this.turnCount == 0) {
			this.turnCount+=amount;
		}
	}

	public void decTurnCount(){
		if (0 < this.turnCount) {
			this.turnCount--;
		}
	}

	public void RegenHp(int amount){
		if(amount<0){
			Debug.Log("amount must be positive value");
		}else{
			this.currHp +=amount;
			if(currHp>fullHp){
				currHp = fullHp;
			}
		}
	}

	public void SetHp(int hp){
		currHp = hp;
		if(currHp <= 0){
			DestroyGameObject();
		}
	}

	public void DestroyGameObject(){
		Destroy(gameObject);
	}

	public int GetHp(){
		return currHp;
	}

	public void SetMove(int direction){
		transform.GetComponent<MOVE>().SetMove(direction);
	}

<<<<<<< HEAD
	public void EnterDetection(int flag, int kindTag){
		switch(flag){
		case MoveFlag.UP:
			up = kindTag;
			break;
		case MoveFlag.DOWN:
			down = kindTag;
			break;
		case MoveFlag.LEFT:
			left = kindTag;
			break;
		case MoveFlag.RIGHT:
			right = kindTag;
			break;
		case MoveFlag.LEFTUP:
			leftUp = kindTag;
			break;
		case MoveFlag.LEFTDOWN:
			leftDown = kindTag;
			break;
		case MoveFlag.RIGHTUP:
			rightUp = kindTag;
			break;
		case MoveFlag.RIGHTDOWN:
=======
	public void SetAttack(int direction){
		transform.GetComponent<ATTACK>().SetAttack(direction);
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
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			rightDown = kindTag;
			break;
		}
	}

	public void ExitDetection(int flag){
		EnterDetection(flag, KindTag.empty);
	}

<<<<<<< HEAD
	public void Damage(int dam){
		currHp-=dam;
		if(currHp<=0){
			Application.Quit();
		}
		hpBarUpdate();
=======
	public bool Damage(int dam){//사망시에 true 리턴
		currHp-=dam;
		if(currHp<=0){
			if(gameObject.GetComponent<Enemy>()){
				gameObject.GetComponent<Enemy>().DestroyGameObject();
				//가지고있던 템을 떨궈야함.
			}
			Destroy(gameObject);
			return true;
		}
		hpBarUpdate();
		return false;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
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

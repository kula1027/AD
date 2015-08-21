using UnityEngine;
using System.Collections;

public class RandomManager : MonoBehaviour {

	public static void randomizeBuff () {						//게임 시작시 정해지는 랜덤 버프
		for(int i = 0 ; i < Buffmanager.numOfBuff ; i++){
			Buffmanager.randomBuff[i] = (Random.Range (0,2) < 1);
		}
	}

	public static void randomizeDegree (){						//게임 시작시 정해지는 랜덤 도수
		for (int i = 0; i < Drink.numOfDrink; i++) {
			if(i % 3 == 0){
				Drink.randomDegree[i] = Random.Range(Config.degreeMin[i],Config.degreeMax[i]+1);
			}else{
				Drink.randomDegree[i] = Drink.randomDegree[i - 1];
			}
		}

/*		for (int i = 0; i < Drink.numOfDrink; i++) {
			Debug.Log (Drink.randomDegree [i]);
		}*/
	}

	public static void randomizeDrinkSprite(){

	}

	public static void randomizeEnemy (int stage, int[] enemyCode){
		int region = stage / 4 + 1;
		int randomNum = 0;
		switch (region) {
		case 1:												//제 1구역
			for(int i = 0 ; i < enemyCode.Length ; i++){
				randomNum = Random.Range (0,1000);
				if(randomNum < 375){
					enemyCode[i] = 0;						//술라임
				}else if(randomNum < 625){
					enemyCode[i] = 3;						//어리버리한 모험가
				}else if(randomNum < 875){
					enemyCode[i] = 4;						//킬러 비-어
				}else{
					enemyCode[i] = 5;						//술뱀
				}
			}
			break;
		case 2:												//제 2구역
			for(int i = 0 ; i < enemyCode.Length ; i++){
				randomNum = Random.Range (0,1000);
				if(randomNum < 250){
					enemyCode[i] = 0;						//술라임
				}else if(randomNum < 500){
					enemyCode[i] = 6;						//압생티
				}else if(randomNum < 584){
					enemyCode[i] = 7;						//순디네
				}else if(randomNum < 834){
					enemyCode[i] = 8;						//술꾼 지옥
				}else{
					enemyCode[i] = 9;						//만취다고라
				}
			}
			break;
		case 3:												//제 3구역
			for(int i = 0 ; i < enemyCode.Length ; i++){
				randomNum = Random.Range (0,1000);
				if(randomNum < 250){
					enemyCode[i] = 0;						//술라임
				}else if(randomNum < 334){
					enemyCode[i] = 1;						//술라임 10년산
				}else if(randomNum < 500){
					enemyCode[i] = 10;						//코볼트 도굴꾼
				}else if(randomNum < 666){
					enemyCode[i] = 11;						//코볼트 술병수집가
				}else if(randomNum < 750){
					enemyCode[i] = 12;						//코볼트 좀도둑
				}else if(randomNum < 916){
					enemyCode[i] = 13;						//식인 효모
				}else{
					enemyCode[i] = 14;						//효모 골렘
				}
			}
			break;
		case 4:												//제 4구역
			for(int i = 0 ; i < enemyCode.Length ; i++){
				randomNum = Random.Range (0,1000);
				if(randomNum < 300){
					enemyCode[i] = 0;						//술라임
				}else if(randomNum < 400){
					enemyCode[i] = 1;						//술라임 10년산
				}else if(randomNum < 600){
					enemyCode[i] = 15;						//코볼트 원령
				}else if(randomNum < 800){
					enemyCode[i] = 16;						//효모 유령
				}else{
					enemyCode[i] = 17;						//모험가 유령
				}
			}
			break;
		case 5:												//제 5구역
			for(int i = 0 ; i < enemyCode.Length ; i++){
				randomNum = Random.Range (0,1000);
				if(randomNum < 300){
					enemyCode[i] = 0;						//술라임
				}else if(randomNum < 400){
					enemyCode[i] = 1;						//술라임 10년산
				}else if(randomNum < 500){
					enemyCode[i] = 2;						//술라임 20년산
				}else if(randomNum < 700){
					enemyCode[i] = 19;						//주정뱅이 과학자
				}else{
					enemyCode[i] = 21;						//술의 대리자
				}
			}
			break;
		}
	}
}

/*
 * 
 * 제 1구역					출현 확률	에네미 코드		천분율 누적값
 * 
 * 술라임					37.5%		 0				375
 * 어리버리한 모험가			25.0%		 3				625
 * 킬러비-어					25.0%		 4				875
 * 술뱀						12.5%		 5				1000
 * 
 * 
 * 
 * 제 2구역
 * 
 * 술라임					25.0%		 0				250
 * 압생티					25.0%		 6				500
 * 순디네					 8.4%		 7				584
 * 술꾼지옥					25.0%		 8 				834
 * 만취다고라					16.6%		 9				1000
 * 
 *
 * 제 3구역
 * 
 * 술라임					25.0%		 0				250
 * 술라임 10년산				 8.4%		 1				334
 * 코볼트 도굴꾼				16.6%		10				500
 * 코볼트 술병수집가			16.6%		11				666
 * 코볼트 좀도둑				 8.4%		12				750
 * 식인 효모					16.6%		13				916
 * 효모 골렘					 8.4%		14				1000
 * 
 * 
 * 제 4구역
 * 
 * 술라임					30.0%		 0				300
 * 술라임 10년산				10.0%		 1				400
 * 코볼트 원령				20.0%		15				600
 * 효모 유령					20.0%		16				800
 * 모험가 유령				20.0%		17				1000
 * 
 * 
 * 제 5구역
 * 
 * 술라임					30.0%		 0				300
 * 술라임 10년산				10.0%		 1				400
 * 술라임 20년산				10.0%		 2				500
 * 주정뱅이 과학자			20.0%		19				700
 * 술의 대리자				30.0%		21				1000
 * 
 * 
 * 술의사도					?
 * 양조머신R4D4				?
 * 
 * 
 * 
 * 
 */

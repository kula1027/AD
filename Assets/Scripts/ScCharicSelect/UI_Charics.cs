using UnityEngine;
using System.Collections;

public class UI_Charics : MonoBehaviour {

	private int number = 0;
	public int CharictorNumers = 3;
	public Vector3 vurPos;
	private bool isMoving = false;
	private float timer = 0;

	public void Right(){
		if(!isMoving){
			SetMove(true);
		}
	}

	public void Left(){
		if(!isMoving){
			SetMove(false);
		}
	}

	void Start(){

	}

	void Update(){
		if(!isMoving){
			if(Input.GetKeyUp("d")){
				Right();
			}
			if(Input.GetKeyUp("a")){
				Left();
			}
		}
		Move();
	}

	private void SetMove(bool right){
		if(right){
			if(number<CharictorNumers-1){
				isMoving = true;
				timer = 0;
				number++;
				vurPos = transform.position + new Vector3(-4, 0, 0);
			}
		}else{
			if(0< number){
				isMoving = true;
				timer = 0;
				number--;
				vurPos = transform.position + new Vector3(4, 0, 0);
			}
		}
	}

	private void Move(){
		if(isMoving){
			transform.position = Vector3.Lerp(transform.position, vurPos, Time.deltaTime * 10);
			transform.position = new Vector3(transform.position.x, 2, 0);
			timer += Time.deltaTime;
			if(0.4f < timer){
				isMoving = false;
				transform.position = new Vector3(((int)(transform.position.x-0.1f)), 2, 0);
			}
		}
	}
}

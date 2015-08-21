using UnityEngine;
using System.Collections;

public class EntityCollider : MonoBehaviour {

	public int dirc;

	private int stack = 0;
	private Transform master;

	void Start(){
		master = transform.parent;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Enemy"){
			stack++;
			if(master.GetComponent<Player>()){
				master.GetComponent<Player>().EnterDetection(dirc, KindTag.player);
			}else{
				master.GetComponent<Enemy>().EnterDetection(dirc, KindTag.entity);
			}
		}else if(coll.tag == "Player"){
			stack++;
			if(master.GetComponent<Player>()){
				master.GetComponent<Player>().EnterDetection(dirc, KindTag.player);
			}else{
				master.GetComponent<Enemy>().EnterDetection(dirc, KindTag.entity);
			}
		}else if(coll.tag == "Wall"){
			stack++;
			if(master.GetComponent<Player>()){
				master.GetComponent<Player>().EnterDetection(dirc, KindTag.wall);
			}else{
				master.GetComponent<Enemy>().EnterDetection(dirc, KindTag.wall);
			}
		}else if(coll.tag == "Item"){
			stack++;
			if(master.GetComponent<Player>()){
				master.GetComponent<Player>().EnterDetection(dirc, KindTag.item);
			}else{
				master.GetComponent<Enemy>().EnterDetection(dirc, KindTag.item);
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if(coll.tag == "Item" || coll.tag == "Wall" || coll.tag == "Enemy" || coll.tag == "Player"){
			stack--;
			if(stack<0){	stack = 0;	}
		}
		if(stack == 0){
			if(master.GetComponent<Player>()){
				master.GetComponent<Player>().ExitDetection(dirc);
			}else{
				master.GetComponent<Enemy>().ExitDetection(dirc);
			}
		}
	}

}
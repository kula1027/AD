<<<<<<< HEAD
﻿using UnityEngine;
=======
using UnityEngine;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
using System.Collections;

public class GameManager : MonoBehaviour
{
	
	public BoardManager boardManager;
	public EnemyManager enemyManager;
	public int turn = Turn.Select;
	public int currStage = 0;
	public Transform prefabPlayer;
	public GameObject player;
	public CameraControl cam;
	private int level = 3;

	
	// Use this for initialization
	void Awake ()
	{
		boardManager = GetComponent<BoardManager> ();
		InitGame ();
		boardManager.loadLevel (0);
	}

	void InitGame ()
	{
		boardManager.SetupScene (level);
		player = (((Transform)Instantiate (prefabPlayer, transform.position, Quaternion.identity))).gameObject;
		player.transform.position = boardManager._Stage [0].PlayerSpawnPoint;
<<<<<<< HEAD
=======
		player.GetComponent<Entity> ().init (IdInfo.DEBUG, IdInfo.DEBUG);
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
		cam.SetPlayer (player);
		//GameObject.Find ("Player").transform.position = new Vector3(10,10,0);
		enemyManager.CreateEnemy ();
	}

<<<<<<< HEAD
=======
	public void PlayerAttack(int attackFlag){
		if (turn == Turn.Select && player.GetComponent<Entity>().getTurnCount()==0) {
			Player localPlayer = player.GetComponent<Player> ();
			switch(attackFlag){
			case Direction.UP:
				if (true){//조건문 TODO
					localPlayer.SetAttack (attackFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.DOWN:
				if (true){//조건문 TODO
					localPlayer.SetAttack (attackFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.LEFT:
				if (true){//조건문 TODO
					localPlayer.SetAttack (attackFlag);
					turn = Turn.Acting;
					enemyManager.EnemyAct ();
				}
				break;
			case Direction.RIGHT:
				if (true){//조건문 TODO
					localPlayer.SetAttack (attackFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.LEFTDOWN:
				if (true){//조건문 TODO
					localPlayer.SetAttack (attackFlag);
					turn = Turn.Acting;
					enemyManager.EnemyAct ();
				}
				break;
			case Direction.LEFTUP:
				if (true){//조건문 TODO
					localPlayer.SetAttack (attackFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.RIGHTUP:
				if (true){//조건문 TODO
					localPlayer.SetAttack (attackFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.RIGHTDOWN:
				if (true){//조건문 TODO
					localPlayer.SetAttack (attackFlag);
					turn = Turn.Acting;
				}
				break;
			}
		}
	}

>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
	public void PlayerMove(int moveFlag){
		if (turn == Turn.Select && player.GetComponent<Entity>().getTurnCount()==0) {
			Player localPlayer = player.GetComponent<Player> ();
			switch(moveFlag){
<<<<<<< HEAD
			case MoveFlag.UP:
				if (localPlayer.up == KindTag.empty || localPlayer.up == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Move;
				}
				break;
			case MoveFlag.DOWN:
				if (localPlayer.down == KindTag.empty || localPlayer.down == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Move;
				}
				break;
			case MoveFlag.LEFT:
				if (localPlayer.left == KindTag.empty || localPlayer.left == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Move;
					enemyManager.EnemyAct ();
				}
				break;
			case MoveFlag.RIGHT:
				if (localPlayer.right == KindTag.empty || localPlayer.right == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Move;
				}
				break;
			case MoveFlag.LEFTDOWN:
				if (localPlayer.leftDown == KindTag.empty || localPlayer.leftDown == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Move;
					enemyManager.EnemyAct ();
				}
				break;
			case MoveFlag.LEFTUP:
				if (localPlayer.leftUp == KindTag.empty || localPlayer.leftUp == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Move;
				}
				break;
			case MoveFlag.RIGHTUP:
				if (localPlayer.rightUp == KindTag.empty || localPlayer.rightUp == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Move;
				}
				break;
			case MoveFlag.RIGHTDOWN:
				if (localPlayer.rightDown == KindTag.empty || localPlayer.rightDown == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Move;
=======
			case Direction.UP:
				//Debug.Log (localPlayer.up);
				if (localPlayer.up == KindTag.empty || localPlayer.up == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.DOWN:
				//Debug.Log (localPlayer.down);
				if (localPlayer.down == KindTag.empty || localPlayer.down == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.LEFT:
				//Debug.Log (localPlayer.left);
				if (localPlayer.left == KindTag.empty || localPlayer.left == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Acting;
					enemyManager.EnemyAct ();
				}
				break;
			case Direction.RIGHT:
				//Debug.Log (localPlayer.right);
				if (localPlayer.right == KindTag.empty || localPlayer.right == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.LEFTDOWN:
				//Debug.Log (localPlayer.leftDown);
				if (localPlayer.leftDown == KindTag.empty || localPlayer.leftDown == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Acting;
					enemyManager.EnemyAct ();
				}
				break;
			case Direction.LEFTUP:
				//Debug.Log (localPlayer.leftUp);
				if (localPlayer.leftUp == KindTag.empty || localPlayer.leftUp == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.RIGHTUP:
				//Debug.Log (localPlayer.rightUp);
				if (localPlayer.rightUp == KindTag.empty || localPlayer.rightUp == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Acting;
				}
				break;
			case Direction.RIGHTDOWN:
				//Debug.Log (localPlayer.rightDown);
				if (localPlayer.rightDown == KindTag.empty || localPlayer.rightDown == KindTag.item){
					localPlayer.SetMove (moveFlag);
					turn = Turn.Acting;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				}
				break;
			}
		}
	}

	void Update ()
	{
		if (player.GetComponent<Entity> ().getTurnCount() != 0 && enemyManager.IsAllStop()) {
			player.GetComponent<Entity> ().decTurnCount();
			enemyManager.decTurnCount();
		}
		if (turn == Turn.Select) {
			Player localPlayer = player.GetComponent<Player> ();
			if (localPlayer.leftUp == KindTag.empty || localPlayer.leftUp == KindTag.item)
			if (Input.GetKeyUp ("q")) {
<<<<<<< HEAD
				localPlayer.SetMove (MoveFlag.LEFTUP);
				turn = Turn.Move;
=======
				localPlayer.SetMove (Direction.LEFTUP);
				turn = Turn.Acting;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			}
			
			if (localPlayer.up == KindTag.empty || localPlayer.up == KindTag.item)
			if (Input.GetKeyUp ("w")) {
<<<<<<< HEAD
				localPlayer.SetMove (MoveFlag.UP);
				turn = Turn.Move;
=======
				localPlayer.SetMove (Direction.UP);
				turn = Turn.Acting;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			}
			
			if (localPlayer.rightUp == KindTag.empty || localPlayer.rightUp == KindTag.item)
			if (Input.GetKeyUp ("e")) {
<<<<<<< HEAD
				localPlayer.SetMove (MoveFlag.RIGHTUP);
				turn = Turn.Move;
=======
				localPlayer.SetMove (Direction.RIGHTUP);
				turn = Turn.Acting;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			}
			
			if (localPlayer.right == KindTag.empty || localPlayer.right == KindTag.item)
			if (Input.GetKeyUp ("d")) {
<<<<<<< HEAD
				localPlayer.SetMove (MoveFlag.RIGHT);
				turn = Turn.Move;
=======
				localPlayer.SetMove (Direction.RIGHT);
				turn = Turn.Acting;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			}
			
			if (localPlayer.rightDown == KindTag.empty || localPlayer.rightDown == KindTag.item)
			if (Input.GetKeyUp ("c")) {
<<<<<<< HEAD
				localPlayer.SetMove (MoveFlag.RIGHTDOWN);
				turn = Turn.Move;
=======
				localPlayer.SetMove (Direction.RIGHTDOWN);
				turn = Turn.Acting;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			}
			
			if (localPlayer.down == KindTag.empty || localPlayer.down == KindTag.item)
			if (Input.GetKeyUp ("x")) {
<<<<<<< HEAD
				localPlayer.SetMove (MoveFlag.DOWN);
				turn = Turn.Move;
=======
				localPlayer.SetMove (Direction.DOWN);
				turn = Turn.Acting;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			}
			
			if (localPlayer.leftDown == KindTag.empty || localPlayer.leftDown == KindTag.item)
			if (Input.GetKeyUp ("z")) {
<<<<<<< HEAD
				localPlayer.SetMove (MoveFlag.LEFTDOWN);
				turn = Turn.Move;
=======
				localPlayer.SetMove (Direction.LEFTDOWN);
				turn = Turn.Acting;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
			}
			
			if (localPlayer.left == KindTag.empty || localPlayer.left == KindTag.item)
			if (Input.GetKeyUp ("a")) {
<<<<<<< HEAD
				localPlayer.SetMove (MoveFlag.LEFT);
				turn = Turn.Move;
			}
			
			if (Input.GetKeyUp ("s")) {
				localPlayer.SetMove (MoveFlag.STAY);
				turn = Turn.Move;
			}
		} else if (turn == Turn.Move) {
			enemyManager.EnemyAct ();
			if (player.GetComponent<MOVE>().moveFlag==MoveFlag.STAY && enemyManager.IsAllStop()) {
=======
				localPlayer.SetMove (Direction.LEFT);
				turn = Turn.Acting;
			}
			
			if (Input.GetKeyUp ("s")) {
				localPlayer.SetMove (Direction.STAY);
				turn = Turn.Acting;
			}
		} else if (turn == Turn.Acting) {
			enemyManager.EnemyAct ();
			if (player.GetComponent<MOVE>().moveFlag==Direction.STAY && enemyManager.IsAllStop()) {
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
				turn = Turn.Select;
			}
		}
		Quit();
	}

	private void Quit(){
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}
}

public class Turn
{
	public const int Select = 0;
<<<<<<< HEAD
	public const int Move = 1;
=======
	public const int Acting = 1;
>>>>>>> 6391116034947803d6550d28e9f180d5aed80587
}
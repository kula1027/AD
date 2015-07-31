using UnityEngine;
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
		cam.SetPlayer (player);
		//GameObject.Find ("Player").transform.position = new Vector3(10,10,0);
		enemyManager.CreateEnemy ();
	}

	public void PlayerMove(int moveFlag){
		if (turn == Turn.Select && player.GetComponent<Entity>().getTurnCount()==0) {
			Player localPlayer = player.GetComponent<Player> ();
			switch(moveFlag){
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
				localPlayer.SetMove (MoveFlag.LEFTUP);
				turn = Turn.Move;
			}
			
			if (localPlayer.up == KindTag.empty || localPlayer.up == KindTag.item)
			if (Input.GetKeyUp ("w")) {
				localPlayer.SetMove (MoveFlag.UP);
				turn = Turn.Move;
			}
			
			if (localPlayer.rightUp == KindTag.empty || localPlayer.rightUp == KindTag.item)
			if (Input.GetKeyUp ("e")) {
				localPlayer.SetMove (MoveFlag.RIGHTUP);
				turn = Turn.Move;
			}
			
			if (localPlayer.right == KindTag.empty || localPlayer.right == KindTag.item)
			if (Input.GetKeyUp ("d")) {
				localPlayer.SetMove (MoveFlag.RIGHT);
				turn = Turn.Move;
			}
			
			if (localPlayer.rightDown == KindTag.empty || localPlayer.rightDown == KindTag.item)
			if (Input.GetKeyUp ("c")) {
				localPlayer.SetMove (MoveFlag.RIGHTDOWN);
				turn = Turn.Move;
			}
			
			if (localPlayer.down == KindTag.empty || localPlayer.down == KindTag.item)
			if (Input.GetKeyUp ("x")) {
				localPlayer.SetMove (MoveFlag.DOWN);
				turn = Turn.Move;
			}
			
			if (localPlayer.leftDown == KindTag.empty || localPlayer.leftDown == KindTag.item)
			if (Input.GetKeyUp ("z")) {
				localPlayer.SetMove (MoveFlag.LEFTDOWN);
				turn = Turn.Move;
			}
			
			if (localPlayer.left == KindTag.empty || localPlayer.left == KindTag.item)
			if (Input.GetKeyUp ("a")) {
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
	public const int Move = 1;
}
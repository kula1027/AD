using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	
	public BoardManager boardManager;
	public EnemyManager enemyManager;

	public int turn = Turn.Select;			//turn은 유저의 입력을 받느냐 마느냐를 결정한다.
											//Select이면 입력을받고 Acting이면 입력을 받지않는다.
											//turnCount가 0이 되면 Select로 바뀐다.
	public int currStage = 0;
	public Transform prefabPlayer;
	public GameObject player;
	public Player playerScript;
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
		player.GetComponent<Entity> ().init (IdInfo.SullSsa,IdInfo.SullSsa);
		cam.SetPlayer (player);
		//GameObject.Find ("Player").transform.position = new Vector3(10,10,0);
		enemyManager.CreateEnemy ();
		playerScript = player.GetComponent<Player>();
	}


	//turn이 Select일 경우 공격 명령을 Accept한다. turn을 즉시 Acting으로 바꾸어준다.
	public void PlayerAttack(int attackFlag){
		if (turn == Turn.Select) {
			switch(attackFlag){
			case Direction.UP:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.DOWN:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.LEFT:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.RIGHT:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.LEFTDOWN:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.LEFTUP:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.RIGHTUP:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			case Direction.RIGHTDOWN:
				if (true){//조건문 TODO
					turn = Turn.Acting;
					playerScript.SetAttack (attackFlag);
				}
				break;
			}
		}
	}
	//turn이 Select일 경우 이동 명령을 Accept한다. turn을 즉시 Acting으로 바꾸어준다.
	public void PlayerMove(int moveFlag){
		if (turn == Turn.Select && playerScript.getTurnCount()==0) {
			switch(moveFlag){
			case Direction.UP:
				//Debug.Log (localPlayer.up);
				if (playerScript.up == KindTag.empty || playerScript.up == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.DOWN:
				//Debug.Log (localPlayer.down);
				if (playerScript.down == KindTag.empty || playerScript.down == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.LEFT:
				//Debug.Log (localPlayer.left);
				if (playerScript.left == KindTag.empty || playerScript.left == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.RIGHT:
				//Debug.Log (localPlayer.right);
				if (playerScript.right == KindTag.empty || playerScript.right == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.LEFTDOWN:
				//Debug.Log (localPlayer.leftDown);
				if (playerScript.leftDown == KindTag.empty || playerScript.leftDown == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.LEFTUP:
				//Debug.Log (localPlayer.leftUp);
				if (playerScript.leftUp == KindTag.empty || playerScript.leftUp == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.RIGHTUP:
				//Debug.Log (localPlayer.rightUp);
				if (playerScript.rightUp == KindTag.empty || playerScript.rightUp == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			case Direction.RIGHTDOWN:
				//Debug.Log (localPlayer.rightDown);
				if (playerScript.rightDown == KindTag.empty || playerScript.rightDown == KindTag.item){
					turn = Turn.Acting;
					playerScript.SetMove (moveFlag);
				}
				break;
			}
		}
	}

	void Update ()
	{
		//모든 Entity의 행동이 끝났는데 모두의 tunrCount가 양수일 경우,
		//모든 Entity의 turnCount를 1씩 감소시킨다.
		if (playerScript.isStay() && playerScript.getTurnCount() > 0 && enemyManager.IsAllStay() && enemyManager.isAllNonZeroTurn()) {
			playerScript.decTurnCount();
			enemyManager.decTurnCount();
		}
		//유저의 입력을 받지않는동안 Enemy를 계속 움직인다.
		if (turn == Turn.Acting) {
			enemyManager.EnemyAct ();
			//모든 Entity의 행동이 끝나고 플레이어의 turnCount가 0일경우,
			//유저의 입력을 받는 상태가 된다.
			if (playerScript.isStay() && playerScript.getTurnCount() == 0 && enemyManager.IsAllStay()) {
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
	public const int Acting = 1;
}
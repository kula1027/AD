using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	
	public BoardManager boardScript;
	public EnemyManager enemy;
	public int turn = Turn.Player;
	public Transform prefabPlayer;
	public GameObject player;
	public CameraControl cam;
	private int level = 3;
	private float turnTimer = 0;

	// Use this for initialization
	void Awake ()
	{
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
		boardScript.loadLevel (0);
	}

	void InitGame ()
	{
		boardScript.SetupScene (level);
		player = (((Transform)Instantiate (prefabPlayer, transform.position, Quaternion.identity))).gameObject;
		player.transform.position = boardScript._Stage [0].PlayerSpawnPoint;
		cam.SetPlayer (player);
		//GameObject.Find ("Player").transform.position = new Vector3(10,10,0);
		enemy.CreateEnemy ();
	}

	void Update ()
	{
		if (turn == Turn.Player) {
			Player localPlayer = player.GetComponent<Player> ();
			if (localPlayer.leftUp == KindTag.empty || localPlayer.leftUp == KindTag.item)
			if (Input.GetKeyUp ("q")) {
				localPlayer.SetMove (MoveFlag.LEFTUP);
				turn = Turn.PlayerBeing;
			}
			
			if (localPlayer.up == KindTag.empty || localPlayer.up == KindTag.item)
			if (Input.GetKeyUp ("w")) {
				localPlayer.SetMove (MoveFlag.UP);
				turn = Turn.PlayerBeing;
			}
			
			if (localPlayer.rightUp == KindTag.empty || localPlayer.rightUp == KindTag.item)
			if (Input.GetKeyUp ("e")) {
				localPlayer.SetMove (MoveFlag.RIGHTUP);
				turn = Turn.PlayerBeing;
			}
			
			if (localPlayer.right == KindTag.empty || localPlayer.right == KindTag.item)
			if (Input.GetKeyUp ("d")) {
				localPlayer.SetMove (MoveFlag.RIGHT);
				turn = Turn.PlayerBeing;
			}
			
			if (localPlayer.rightDown == KindTag.empty || localPlayer.rightDown == KindTag.item)
			if (Input.GetKeyUp ("c")) {
				localPlayer.SetMove (MoveFlag.RIGHTDOWN);
				turn = Turn.PlayerBeing;
			}
			
			if (localPlayer.down == KindTag.empty || localPlayer.down == KindTag.item)
			if (Input.GetKeyUp ("x")) {
				localPlayer.SetMove (MoveFlag.DOWN);
				turn = Turn.PlayerBeing;
			}
			
			if (localPlayer.leftDown == KindTag.empty || localPlayer.leftDown == KindTag.item)
			if (Input.GetKeyUp ("z")) {
				localPlayer.SetMove (MoveFlag.LEFTDOWN);
				turn = Turn.PlayerBeing;
			}
			
			if (localPlayer.left == KindTag.empty || localPlayer.left == KindTag.item)
			if (Input.GetKeyUp ("a")) {
				localPlayer.SetMove (MoveFlag.LEFT);
				turn = Turn.PlayerBeing;
			}
			
			if (Input.GetKeyUp ("s")) {
				localPlayer.SetMove (MoveFlag.STAY);
				turn = Turn.PlayerBeing;
			}
			
		} else if (turn == Turn.PlayerBeing) {
			turnTimer += Time.deltaTime;
			if (1 < turnTimer) {
				turn ++;
				turnTimer = 0;
			}
		} else if (turn == Turn.Ai) {
			turnTimer += Time.deltaTime;
			enemy.EnemyAct ();
			turn++;
		} else {
			turnTimer += Time.deltaTime;
			if (1 < turnTimer) {
				turn = Turn.Player;
				turnTimer = 0;
			}
		}
	}
}

public class Turn
{
	public const int Player = 0;
	public const int PlayerBeing = 1;
	public const int Ai = 2;
	public const int AiBeing = 3;
}
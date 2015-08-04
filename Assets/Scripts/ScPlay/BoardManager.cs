using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	public Stage[] _Stage;
	public int roomMin = 4;
	public int roomMax = 7;
	public int columns = 52;
	public int rows = 52;
	public GameObject exit;
	public TileInfo[,] tile;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;
	private GameObject[,] tileObj;
	private GameObject[,] wallObj;
	private GameObject boardHolder;
	private GameObject wallGroup;
	private List<Vector3> gridPositions = new List<Vector3> ();
	
	public void clearStage (){
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				if (tile [y, x].tileType == 0) {
					Destroy (tileObj [y, x]);
				} else {
					Destroy (wallObj [y, x]);
				}
			}
		}
	}

	public void SetEnemyTiles(int enemyNums){
		enemyTiles = new GameObject[enemyNums];
		for(int i = 0 ; i < enemyNums ; i++){
			enemyTiles[i] = new GameObject();
			enemyTiles[i].transform.position = _Stage[(GameObject.Find("GameManager").GetComponent<GameManager>().currStage)].GetRespawnPoint();
		}
	}
	
	void BoardSetup (){
		_Stage = new Stage[2];
		for (int i = 0; i < _Stage.Length; i++) {
			_Stage [i] = new Stage (rows, columns, roomMin, roomMax, floorTiles.Length, wallTiles.Length, outerWallTiles.Length, foodTiles.Length, enemyTiles.Length);
		}
		wallGroup = GameObject.Find ("Wall");
		boardHolder = GameObject.Find ("Board");
	}
	
	public void loadLevel (int stage){
		this.tile = _Stage [stage].get_tileInfo ();
		this.gridPositions = _Stage [stage].getGridPositions ();
		
		GameObject toInstantiate;
		
		tileObj = new GameObject[rows, columns];
		wallObj = new GameObject[rows, columns];
		
		
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				if (tile [y, x].hasWall == false) {
					tile [y, x].tileType = 0;
				}
				//toInstantiate = floorTiles[tile[y, x].floorType];
				//setTile(toInstantiate, x, y);
				if (tile [y, x].tileType == 0) {
					toInstantiate = floorTiles [tile [y, x].floorType];
					tileObj [y, x] = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					tileObj [y, x].transform.SetParent (boardHolder.transform);
				} else if (tile [y, x].tileType == 1) {
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
					wallObj [y, x] = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					wallObj [y, x].transform.SetParent (wallGroup.transform);
				} else if (tile [y, x].tileType == 2) {
					toInstantiate = wallTiles [Random.Range (0, wallTiles.Length)];
					wallObj [y, x] = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					wallObj [y, x].transform.SetParent (wallGroup.transform);
				} else {
					;//
				}
			}
		}
		Instantiate (exit, _Stage [stage].Exit, Quaternion.identity);
	}
	public void SetupScene (int level){
		BoardSetup ();
	}
	
	public TileInfo[,] get_tileInfo (int stage){
		return _Stage [stage].get_tileInfo ();
	}
	

	
	
}
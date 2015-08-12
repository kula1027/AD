using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	
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

    public List<Stage> _Stage = new List<Stage>();


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

        _Stage.Add(new Stage(rows, columns, roomMin, roomMax));
        _Stage.Add(new Stage(rows, columns, roomMin, roomMax));
        _Stage.Add(new Stage(rows, columns, roomMin, roomMax));
        _Stage.Add(new Stage(rows, columns, roomMin, roomMax));

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
                if (tile[y, x].tileType == TileInfo.FLOOR)
                {
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    tileObj[y, x] = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    tileObj[y, x].transform.SetParent(boardHolder.transform);
                }
                else if (tile[y, x].tileType == TileInfo.OUTERWALL)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    wallObj[y, x] = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    wallObj[y, x].transform.SetParent(wallGroup.transform);
                }
                else if (tile[y, x].tileType == TileInfo.WALL)
                {
                    toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
                    wallObj[y, x] = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    wallObj[y, x].transform.SetParent(wallGroup.transform);
                }
                else if (tile[y, x].tileType == TileInfo.ROOM)
                {
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    tileObj[y, x] = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    tileObj[y, x].transform.SetParent(boardHolder.transform);
                }
                else if (tile[y, x].tileType == TileInfo.ROAD)
                {
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    tileObj[y, x] = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    tileObj[y, x].transform.SetParent(boardHolder.transform);
                }
                else
                {
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
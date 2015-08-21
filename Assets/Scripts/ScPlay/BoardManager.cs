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
	public GameObject[] outerWallTiles;
	private GameObject[,] tileObj;
	private GameObject[,] wallObj;

    private bool IsThisFirstLoad = true;

    private GameObject go_STAGE;

	private GameObject boardHolder;
	private GameObject wallGroup;
    private GameObject entity;
    private GameObject item;
    private GameObject trap;
    private GameObject something;
        //private GameObject





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
	

	void BoardSetup (){

        _Stage.Add(new Stage(rows, columns, roomMin, roomMax));
        _Stage.Add(new Stage(rows, columns, roomMin, roomMax));
        _Stage.Add(new Stage(rows, columns, roomMin, roomMax));
        _Stage.Add(new Stage(rows, columns, roomMin, roomMax));

        //wallGroup = GameObject.Find ("Wall");
		//boardHolder = GameObject.Find ("Board");
	}
	
	public void loadLevel (int stage){

        if (IsThisFirstLoad)
        {
            IsThisFirstLoad = false;
            //Debug.Log("change Map");
        }
        else
        {
            Destroy(go_STAGE);
        }

        go_STAGE = new GameObject("STAGE");
        //go_STAGE.AddComponent()
        wallGroup = new GameObject("Wall");
        boardHolder = new GameObject("Board");
        entity = new GameObject("entity");
        item = new GameObject("item");
        trap = new GameObject("trap");
        something = new GameObject("something");



        boardHolder.transform.SetParent(go_STAGE.transform);
        wallGroup.transform.SetParent(go_STAGE.transform);
        entity.transform.SetParent(go_STAGE.transform);
        item.transform.SetParent(go_STAGE.transform);
        trap.transform.SetParent(go_STAGE.transform);
        something.transform.SetParent(go_STAGE.transform);

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
					wallObj[y, x].transform.FindChild("Roof").GetComponent<SpriteRenderer>().sortingOrder = 52 - y;
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

	public void SetupScene (){
		BoardSetup ();
	}
	
	public TileInfo[,] get_tileInfo (int stage){
		return _Stage [stage].get_tileInfo ();
	}
	

	
	
}
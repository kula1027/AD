using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage{
	private Vector3[] room;
	private TileInfo[,] tile;
	public Vector3 PlayerSpawnPoint;
	public Vector3 Exit;
	int rows;
	int columns;
	int roommin;
	int roommax;
	int numOfFloor;
	int numOfWall;
	int numOfOwall;
	int numOfFood;
	int numOfEnemy;
	private List<Vector3> gridPositions = new List<Vector3> ();
	
	public TileInfo[,] get_tileInfo ()
	{
		return tile;
	}
	
	public Stage ()
	{
		setStage (52, 52, 4, 7, 0, 0, 0, 0, 0);
	}
	
	public Stage (int x, int y, int _roommin, int _roommax, int floor, int wall, int owall, int food, int enemy)
	{
		setStage (x, y, _roommin, _roommax, floor, wall, owall, food, enemy);
	}
	
	void InitialiseList (){
		gridPositions.Clear ();
		for (int x = 1; x < columns - 2; x++) {
			for (int y = 1; y < rows - 2; y++) {
				if (tile [y, x].hasWall != true) {
					gridPositions.Add (new Vector3 (x, y, 0));
				}
			}
		}
	}
	
	private void setStage (int _x, int _y, int _roommin, int _roommax, int floor, int wall, int owall, int food, int enemy){
		numOfEnemy = enemy;
		numOfFloor = floor;
		numOfFood = food;
		numOfWall = wall;
		numOfOwall = owall;
		roommin = _roommin;
		roommax = _roommax;
		this.rows = _x;
		this.columns = _y;
		tile = new TileInfo[rows, columns];

		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				tile [y, x] = new TileInfo ();
			}
		}
		SetGeometry ();
		SetPlayerSpawnAndExit ();
		InitialiseList ();
		
		
	}
	
	public List<Vector3> getGridPositions ()
	{
		return gridPositions;
	}
	
	void setUnit (GameObject[] obj, int x, int y)
	{
		GameObject tileChoice = obj [Random.Range (0, obj.Length)];
		//Instantiate (tileChoice, new Vector3 (x, y, 0), Quaternion.identity);
	}
	
	void setUnitAtRandom (GameObject[] obj, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);
		for (int i = 0; i < objectCount; i++) {
			Vector3 tempvec3 = RandomPosition ();
			
			setUnit (obj, (int)tempvec3.x, (int)tempvec3.y);
			
		}
	}
	
	void setItem (GameObject[] obj, int x, int y)
	{
		GameObject tileChoice = obj [Random.Range (0, obj.Length)];
		//Instantiate (tileChoice, new Vector3 (x, y, 0), Quaternion.identity);
	}
	
	void setItemAtRandom (GameObject[] obj, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);
		for (int i = 0; i < objectCount; i++) {
			Vector3 tempvec3 = RandomPosition ();
			
			setItem (obj, (int)tempvec3.x, (int)tempvec3.y);
			
		}
	}
	
	Vector3 RandomPosition ()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}
	
	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);
		for (int i = 0; i < objectCount; i++) {
			Vector3 randomPosition = RandomPosition ();
			GameObject tileChoice = tileArray [Random.Range (0, tileArray.Length)];
			//Instantiate (tileChoice, RandomPosition (), Quaternion.identity);
		}
	}
	
	void SetPlayerSpawnAndExit ()
	{
		int min = 400;
		int dst;
		int remem = 0;
		;
		for (int i = 0; i < room.Length; i++) {
			dst = (int)Vector3.Distance (new Vector3 (0, 0, 0), room [i]);
			if (dst < min) {
				min = dst;
				remem = i;
			}
		}
		
		tile [(int)room [remem].y, (int)room [remem].x].isPlayerStartPoint = true;
		PlayerSpawnPoint = room [remem];
		
		min = 400;
		for (int i = 0; i < room.Length; i++) {
			dst = (int)Vector3.Distance (new Vector3 (columns, rows, 0), room [i]);
			if (dst < min) {
				min = dst;
				remem = i;
			}
		}
		
		Exit = room [remem];
		//Instantiate (exit, room[remem], Quaternion.identity);
		
	}
	
	public Vector3 GetRespawnPoint ()
	{
		
		return RandomPosition ();
	}
	
	void digRoad (int x1, int y1, int x2, int y2)
	{
		
		int width = x2 - x1;
		int height = y2 - y1;
		
		width = (int)Mathf.Abs (width);
		height = (int)Mathf.Abs (height);
		
		
		if (x2 <= x1 && y2 <= y1) {//1
			if (x2 < x1) {
				int tmp = x2;
				x2 = x1;
				x1 = tmp;
			}
			if (y2 < y1) {
				int tmp = y2;
				y2 = y1;
				y1 = tmp;
			}
			
			int middleway = 0;
			if (width > height) { //width first go
				middleway = (x1 + x2) / 2;
				for (int x = x1; x <= middleway; x++) {
					if (tile [y1, x].hasWall == true) {
						tile [y1, x].hasWall = false;
					}
				}
				for (int y = y1; y < y2; y++) {
					if (tile [y, middleway].hasWall == true) {
						tile [y, middleway].hasWall = false;
					}
				}
				
				for (int x = middleway; x < x2; x++) {
					if (tile [y2, x].hasWall == true) {
						tile [y2, x].hasWall = false;
					}
				}
			} else {
				middleway = (y1 + y2) / 2;
				for (int y = y1; y <= middleway; y++) {
					if (tile [y, x1].hasWall == true) {
						tile [y, x1].hasWall = false;
					}
				}
				for (int x = x1; x < x2; x++) {
					
					if (tile [middleway, x].hasWall == true) {
						tile [middleway, x].hasWall = false;
					}
				}
				
				for (int y = middleway; y < y2; y++) {
					if (tile [y, x2].hasWall == true) {
						tile [y, x2].hasWall = false;
					}
				}
			}
			
			
			
			
		} else if (x2 >= x1 && y2 <= y1) {//2
			int middleway = 0;
			if (width > height) { //width first go
				middleway = (x1 + x2) / 2;
				for (int x = x1; x <= middleway; x++) {
					if (tile [y1, x].hasWall == true) {
						tile [y1, x].hasWall = false;
						
					}
				}
				for (int y = y2; y < y1; y++) {
					if (tile [y, middleway].hasWall == true) {
						tile [y, middleway].hasWall = false;
						
					}
				}
				
				for (int x = middleway; x < x2; x++) {
					if (tile [y2, x].hasWall == true) {
						tile [y2, x].hasWall = false;
						
					}
				}
			} else {
				middleway = (y1 + y2) / 2;
				for (int y = y2; y <= middleway; y++) {
					if (tile [y, x2].hasWall == true) {
						tile [y, x2].hasWall = false;
						
					}
					
				}
				
				
				for (int x = x1; x < x2; x++) {
					
					if (tile [middleway, x].hasWall == true) {
						tile [middleway, x].hasWall = false;
						
					}
					
				}
				
				for (int y = middleway; y < y1; y++) {
					if (tile [y, x1].hasWall == true) {
						tile [y, x1].hasWall = false;
						
					}
					
				}
			}
			
		} else if (x2 <= x1 && y2 >= y1) {//3
			if (x2 < x1) {
				int tmp = x2;
				x2 = x1;
				x1 = tmp;
			}
			if (y2 < y1) {
				int tmp = y2;
				y2 = y1;
				y1 = tmp;
			}
			int middleway = 0;
			if (width > height) { //width first go
				middleway = (x1 + x2) / 2;
				for (int x = x1; x <= middleway; x++) {
					if (tile [y2, x].hasWall == true) {
						tile [y2, x].hasWall = false;
						
					}
				}
				for (int y = y1; y < y2; y++) {
					if (tile [y, middleway].hasWall == true) {
						tile [y, middleway].hasWall = false;
						
					}
				}
				
				for (int x = middleway; x < x2; x++) {
					if (tile [y1, x].hasWall == true) {
						tile [y1, x].hasWall = false;
						
					}
				}
			} else {
				middleway = (y1 + y2) / 2;
				for (int y = y1; y <= middleway; y++) {
					if (tile [y, x2].hasWall == true) {
						tile [y, x2].hasWall = false;
						
					}
					
				}
				
				
				for (int x = x1; x < x2; x++) {
					
					if (tile [middleway, x].hasWall == true) {
						tile [middleway, x].hasWall = false;
						
					}
					
				}
				
				for (int y = middleway; y < y2; y++) {
					if (tile [y, x1].hasWall == true) {
						tile [y, x1].hasWall = false;
						
					}
					
				}
			}
			
			
		} else {  //4
			int middleway = 0;
			if (width > height) { //width first go
				middleway = (x1 + x2) / 2;
				for (int x = x1; x <= middleway; x++) {
					if (tile [y1, x].hasWall == true) {
						tile [y1, x].hasWall = false;
						
					}
				}
				for (int y = y1; y < y2; y++) {
					if (tile [y, middleway].hasWall == true) {
						tile [y, middleway].hasWall = false;
						
					}
				}
				
				for (int x = middleway; x < x2; x++) {
					if (tile [y2, x].hasWall == true) {
						tile [y2, x].hasWall = false;
						
					}
				}
			} else {
				middleway = (y1 + y2) / 2;
				for (int y = y1; y <= middleway; y++) {
					if (tile [y, x1].hasWall == true) {
						tile [y, x1].hasWall = false;
						
					}
					
				}
				
				
				for (int x = x1; x < x2; x++) {
					
					if (tile [middleway, x].hasWall == true) {
						tile [middleway, x].hasWall = false;
						
					}
					
				}
				
				for (int y = middleway; y < y2; y++) {
					if (tile [y, x2].hasWall == true) {
						tile [y, x2].hasWall = false;
						
					}
					
				}
			}
		}
	}
	
	public void SetGeometry ()
	{
		
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				tile [y, x].floorType = Random.Range (0, numOfFloor);
				tile [y, x].hasWall = true;
				if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1) {
					tile [y, x].tileType = 1;
				} else {
					tile [y, x].tileType = 2;
					tile [y, x].wallType = numOfWall;
				}
			}
		}
		
		createRoom ();
		createRoad ();
	}
	
	private void createRoad ()
	{
		int[,] nodeToNode;
		int[,] haveConnection;
		int[] _1way = new int[room.Length];
		nodeToNode = new int[room.Length, room.Length];
		haveConnection = new int[room.Length, room.Length];
		
		
		for (int x = 0; x < room.Length; x++) {
			for (int y = 0; y < room.Length; y++) {
				haveConnection [y, x] = 0;
				nodeToNode [y, x] = (int)Vector3.Distance (room [x], room [y]);
			}
			_1way [x] = 0;
		}
		
		
		int loc = 0;
		int min = 30;
		_1way [0] = 1;
		
		
		int numOfRoad = 0;
		for (int k = 1; k < room.Length; k++) {
			int g = 0;
			min = 3000;
			loc = 0;
			
			for (int i = 0; i < room.Length; i++) {
				
				if (_1way [i] == 0) {
					continue;
				}
				for (int j = 0; j < room.Length; j++) {
					if (i == j) {
						continue;
					}
					if (_1way [j] == 1) {
						continue;
					}
					if (nodeToNode [i, j] < min) {
						g = i;
						loc = j;
						min = nodeToNode [i, j];
					}
					
				}
				
			}
			if (min != 300) {
				_1way [loc] = 1;
				haveConnection [g, loc] = 1;
				haveConnection [loc, g] = 1;
				digRoad ((int)room [g].x, (int)room [g].y, (int)room [loc].x, (int)room [loc].y);
				numOfRoad++;
			}
		}
		
		min = 300;
		int loc_1 = 0;
		int gg = 0;
		int g_1 = 0;
		for (int kk = 0; kk < room.Length; kk++) {
			
			for (int j = 0; j < room.Length; j++) {
				if (kk == j) {
					continue;
				}
				if (_1way [j] == kk) {
					continue;
				}
				if (nodeToNode [kk, j] < min) {
					g_1 = gg;
					gg = kk;
					loc_1 = loc;
					loc = j;
					min = nodeToNode [kk, j];
				}
			}
			//if (min != 300){
			_1way [loc] = 1;
			haveConnection [g_1, loc_1] = 1;
			haveConnection [loc_1, g_1] = 1;
			digRoad ((int)room [g_1].x, (int)room [g_1].y, (int)room [loc_1].x, (int)room [loc_1].y);
			//numOfRoad++;
			//}
			
		}
	}
	
	private void digging (int x1, int y1, int x2, int y2)
	{
		if (x2 < x1) {
			int tmp = x2;
			x2 = x1;
			x1 = tmp;
		}
		if (y2 < y1) {
			int tmp = y2;
			y2 = y1;
			y1 = y2;
		}
		for (int tilex = x1; tilex <= x2; tilex++) {
			for (int tiley = y1; tiley <= y2; tiley++) {
				tile [tiley, tilex].tileType = 0;
				tile [tiley, tilex].hasWall = false;
			}
		}
		
	}
	
	private void createRoom (){
		int roomDistFactor = 13;
		//Debug.Log(Time.)
		int roomNum = (int)(1.5 * (Mathf.Sqrt ((rows + columns) / 2)));
		//roomNum -= 2;
		int count = 0;
		int maxLoop = 10000;
		room = new Vector3[roomNum];
		
		List<Vector3> roomloc = new List<Vector3> ();
		
		
		for (int x = 0; x < columns / roomDistFactor; x++) {
			for (int y = 0; y < rows / roomDistFactor; y++) {
				roomloc.Add (new Vector3 (x * roomDistFactor, y * roomDistFactor, 0));
			}
		}
		
		
		
		for (int i = 0; i < roomNum; i++) {
			room [i] = new Vector3 (0, 0, 0);
		}
		roomNum = 0;
		
		
		while (roomNum < room.Length && count < maxLoop) {
			//int x = (int)(Random.Range(30, (columns - 9)*10) / 15);
			//int y = (int)(Random.Range(30, (rows - 9) * 10) / 10);
			
			int RandomIndex = Random.Range (0, roomloc.Count);
			
			int x = (int)roomloc [RandomIndex].x + Random.Range (2, 5);
			int y = (int)roomloc [RandomIndex].y + Random.Range (2, 5);
			
			roomloc.RemoveAt (RandomIndex);
			
			bool stopcheck = false;
			
			int width = Random.Range (roommin, roommax);
			int height = Random.Range (roommin, roommax);
			/*
                for (int a = x; a <= x + width; a = a + 2)
                {
                    for (int b = y; b <= y + height; b = b + 2)
                    {
                        if ((a >= 0 && a < columns) && (b >= 0 && b < rows)) {
                            if (tile[b, a].hasWall == false)
                            {
                                count++;
                                stopcheck = true;
                                continue;
                            }
                        }
                    }
                }*/
			
			
			
			if (stopcheck == true) {
				continue;
			}
			
			digging (x, y, x + width, y + height);
			room [roomNum] = new Vector3 ((x + x + width) / 2, (y + y + height) / 2, 0);
			roomNum++;
		}
		
	}
}

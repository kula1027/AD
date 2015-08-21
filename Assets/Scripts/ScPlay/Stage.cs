using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage
{
	public BoardManager boardManager;				//해당 Stage를 관리하는 매니저
	public EnemyManager enemyManager;				//해당 Stage의 Enemy들을 관리하는 매니저
//	public ItemManager itemManager;					//해당 Stage의 Item들을 관리하는 매니저
//	public DrinkManager drinkManager;				//해당 Stage의 Drink들을 관리하는 매니저


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
	
	public List<Vector3> enemyPos = new List<Vector3>();		//Enemy 가 스폰될 위치들
	public List<Vector3> itemPos = new List<Vector3>();			//Item	이 스폰될 위치들
	public List<Vector3> drinkPos = new List<Vector3>();		//Drink 가 스폰될 위치들

	private List<Vector3> gridPositions = new List<Vector3>();

	private void init(){
		boardManager = GameObject.Find ("GameManager").GetComponent<BoardManager> ();
		enemyManager = new EnemyManager ();
//		itemManager = new ItemManager ();
//		drinkManager = new DrinkManager ();

	}

	public void debugLog(){					//Enemy의 위치가 잘 등록되었는지 확인하기 위한 디버그전용 함수
		for(int i = 0 ; i < 52 ; i++){
			for(int j = 0 ; j < 52 ; j++){
				if(tile[i,j].e!=null){
					Debug.Log ("Here is Enemy : (" + j + "," + i + ",0)");
				}
			}
		}
	}

	public void SetEnemyPos(int enemyNums){
		for(int i = 0 ; i < enemyNums ; i++){
			enemyPos.Add (GetRespawnPoint());
		}
	}

	public TileInfo[,] get_tileInfo()
	{
		return tile;
	}
	
	public Stage()
	{
		init ();
		setStage(52, 52, 4, 7, 0, 0, 0, 0, 0);
	}
	public Stage(int x, int y, int _roommin, int _roommax)
	{
		init ();
		setStage(x, y, _roommin, _roommax);
	}
	public Stage(int x, int y, int _roommin, int _roommax, int floor, int wall, int owall, int food, int enemy)
	{
		init ();
		setStage(x, y, _roommin, _roommax, floor, wall, owall, food, enemy);
	}

	void InitialiseList()
	{
		gridPositions.Clear();
		for (int x = 1; x < columns - 2; x++)
		{
			for (int y = 1; y < rows - 2; y++)
			{
				if (tile[y, x].hasWall != true)
				{
					gridPositions.Add(new Vector3(x, y, 0));
				}
			}
		}
	}

	private void setStage(int _x, int _y, int _roommin, int _roommax)
	{
		
		roommin = _roommin;
		roommax = _roommax;
		this.rows = _x;
		this.columns = _y;
		tile = new TileInfo[rows, columns];
		
		for (int x = 0; x < columns; x++)
		{
			for (int y = 0; y < rows; y++)
			{
				tile[y, x] = new TileInfo(x, y);
			}
		}
		SetGeometry();
		SetPlayerSpawnAndExit();
		InitialiseList();
		
	}
	
    public List<Vector3> getEnemyPositionToLoad()
    {
        return enemyPos;
    }



	private void setStage(int _x, int _y, int _roommin, int _roommax, int floor, int wall, int owall, int food, int enemy)
	{
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
		
		for (int x = 0; x < columns; x++)
		{
			for (int y = 0; y < rows; y++)
			{
				tile[y, x] = new TileInfo(x, y);
			}
		}
		SetGeometry();
		SetPlayerSpawnAndExit();
		InitialiseList();
		
		
	}
	
	public List<Vector3> getGridPositions()
	{
		return gridPositions;
	}
	
	void setUnit(GameObject[] obj, int x, int y)
	{
		GameObject tileChoice = obj[Random.Range(0, obj.Length)];
		//Instantiate (tileChoice, new Vector3 (x, y, 0), Quaternion.identity);
	}
	
	void setUnitAtRandom(GameObject[] obj, int minimum, int maximum)
	{
		int objectCount = Random.Range(minimum, maximum + 1);
		for (int i = 0; i < objectCount; i++)
		{
			Vector3 tempvec3 = RandomPosition();
			
			setUnit(obj, (int)tempvec3.x, (int)tempvec3.y);
			
		}
	}
	
	void setItem(GameObject[] obj, int x, int y)
	{
		GameObject tileChoice = obj[Random.Range(0, obj.Length)];
		//Instantiate (tileChoice, new Vector3 (x, y, 0), Quaternion.identity);
	}
	
	void setItemAtRandom(GameObject[] obj, int minimum, int maximum)
	{
		int objectCount = Random.Range(minimum, maximum + 1);
		for (int i = 0; i < objectCount; i++)
		{
			Vector3 tempvec3 = RandomPosition();
			
			setItem(obj, (int)tempvec3.x, (int)tempvec3.y);
			
		}
	}
	
	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);
		return randomPosition;
	}
	
	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range(minimum, maximum + 1);
		for (int i = 0; i < objectCount; i++)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
			//Instantiate (tileChoice, RandomPosition (), Quaternion.identity);
		}
	}
	
	void SetPlayerSpawnAndExit()
	{
		int min = 400;
		int dst;
		int remem = 0;
		for (int i = 0; i < room.Length; i++)
		{
			dst = (int)Vector3.Distance(new Vector3(0, 0, 0), room[i]);
			if (dst < min)
			{
				min = dst;
				remem = i;
			}
		}
		
		tile[(int)room[remem].y, (int)room[remem].x].isPlayerStartPoint = true;
		PlayerSpawnPoint = room[remem];
		
		min = 400;
		for (int i = 0; i < room.Length; i++)
		{
			dst = (int)Vector3.Distance(new Vector3(columns, rows, 0), room[i]);
			if (dst < min)
			{
				min = dst;
				remem = i;
			}
		}
		
		Exit = room[remem];
		//Instantiate (exit, room[remem], Quaternion.identity);
		
	}
	
	public Vector3 GetRespawnPoint()
	{
		
		return RandomPosition();
	}
	
	void digRoad(int x1, int y1, int x2, int y2)
	{
		//x2> x1일때 만을 다루는 걸로
		//---->
		int width;
		int height;
		
		width = Mathf.Abs(x2 - x1);
		height = Mathf.Abs(y2 - y1);
		
		int middleway = 0;
		
		if (width > height)//1세로 2가로
		{
			middleway = (x2 + x1) / 2;
			DrowLineRoad(x1, y1, middleway, y1);
			DrowLineRoad(middleway, y1, middleway, y2);
			DrowLineRoad(middleway, y2, x2, y2);
			
		}
		else//2세로 1가로
		{//height > width
			middleway = (y2 + y1) / 2;
			DrowLineRoad(x1, y1, x1, middleway);
			DrowLineRoad(x1, middleway, x2, middleway);
			DrowLineRoad(x2, middleway, x2, y2);
		}
		
	}
	
	public void drawRndRoad(int x, int y)
	{
		// dirctMeans
		// 1 is Up
		// 2 is Right
		// 3 is Down
		// 4 is Left
		
		
		
		bool start = false;
		int length = Random.Range(30, 70);
		
		
		int backDirct = Random.Range(1,4) ;
		
		for(int i = 0; i < length; i++)
		{
			if(x<4 || x>46 || y < 4 || y > 46)
			{
				return;
			}
			
			if (start == false)
			{
				if (tile[y, x].hasWall == true)
				{
					start = true;
				}
			}
			
			if (start == true)
			{//그리기 시작했을 때;
				if (test(x, y))
				{
					diggingRoad(x, y);
				}
				else
				{
					//Debug.Log("!!");
					return;
				}
			}
			
			
			int dirct = 0;
			do
			{
				dirct = Random.Range(1, 5);
				if (Random.Range(0, 100) < 50)
				{
					dirct = backDirct;
				}
			} while ((dirct + 2) % 4 == backDirct);
			//Debug.Log(dirct);
			backDirct = dirct;
			
			switch (dirct)
			{
			case 1:
				y++;
				break;
			case 2:
				x++;
				break;
			case 3:
				y--;
				break;
			case 4:
				x--;
				break;
			default:
				Debug.Log("방향 설정 에러");
				break;
			}
		}
	}
	
	public bool test(int x, int y)
	{
		int counter = 0;
		
		x++;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		y++;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		x--;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		if (counter == 3)
		{
			return false;
		}
		y--;
		counter = 0;
		
		
		
		
		x++;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		y--;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		x--;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		if (counter == 3)
		{
			return false;
		}
		y++;
		counter = 0;
		
		
		
		x--;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		y++;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		x++;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		if (counter == 3)
		{
			return false;
		}
		y--;
		counter = 0;
		
		
		
		x--;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		y--;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		x++;
		if (tile[y, x].hasWall == false)
		{
			counter++;
		}
		if (counter == 3)
		{
			return false;
		}
		y++;
		counter = 0;
		
		
		
		return true;
	}
	
	public int DrowLineRoad(int x1, int y1, int x2, int y2)
	{
		//리턴 0은 이상 무
		//
		
		//4가지 경우
		
		if (x2 == x1)//세로일때
		{
			int x = x2;
			if (y2 > y1)//위쪽으로 진행
			{
				
				for (int y = y1; y <= y2; y++)
				{
					diggingRoad(x, y);
				}
			}
			else//아래쪽으로 진행
			{
				for (int y = y2; y <= y1; y++)
				{
					diggingRoad(x, y);
				}
			}
		}
		else//가로일때
		{
			int y = y1;
			if (x2 > x1)//오른쪽
			{
				int counter = 0;
				for (int x = x1; x <= x2; x++)
				{
					diggingRoad(x, y);
				}
			}
			else//왼쪽
			{
				int counter = 0;
				for (int x = x2; x <= x1; x++)
				{
					diggingRoad(x, y);
				}
			}
			
			
		}
		
		
		
		return 0;
	}
	
	public void SetGeometry()
	{
		
		for (int x = 0; x < columns; x++)
		{
			for (int y = 0; y < rows; y++)
			{
				//tile [y, x].floorType = Random.Range (0, numOfFloor);
				tile[y, x].hasWall = true;
				if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1)
				{
					tile[y, x].tileType = TileInfo.OUTERWALL;
				}
				else
				{
					tile[y, x].tileType = TileInfo.WALL;
					//tile [y, x].wallType = numOfWall;
				}
			}
		}	
		createRoom();
		createRoad();
	}
	
	private void createRoad()
	{
		int[,] nodeToNode;
		int[,] haveConnection;
		int[] _1way = new int[room.Length];
		nodeToNode = new int[room.Length, room.Length];
		haveConnection = new int[room.Length, room.Length];
			
		for (int x = 0; x < room.Length; x++)
		{
			for (int y = 0; y < room.Length; y++)
			{
				haveConnection[y, x] = 0;
				nodeToNode[y, x] = (int)Vector3.Distance(room[x], room[y]);
			}
			_1way[x] = 0;
		}
		
		
		int loc = 0;
		int min = 30;
		_1way[0] = 1;
		
		
		int numOfRoad = 0;
		for (int k = 1; k < room.Length; k++)
		{
			int g = 0;
			min = 3000;
			loc = 0;
			
			for (int i = 0; i < room.Length; i++)
			{
				
				if (_1way[i] == 0)
				{
					continue;
				}
				for (int j = 0; j < room.Length; j++)
				{
					if (i == j)
					{
						continue;
					}
					if (_1way[j] == 1)
					{
						continue;
					}
					if (nodeToNode[i, j] < min)
					{
						g = i;
						loc = j;
						min = nodeToNode[i, j];
					}
					
				}
				
			}
			if (min != 300)
			{
				_1way[loc] = 1;
				haveConnection[g, loc] = 1;
				haveConnection[loc, g] = 1;
				digRoad((int)room[g].x, (int)room[g].y, (int)room[loc].x, (int)room[loc].y);
				numOfRoad++;
			}
		}
		
		
		/* for(int i = 0; i < Random.Range(room.Length / 2, room.Length); i++)
        {
            drawRndRoad((int)room[i].x, (int)room[i].y);
        }*/
		
		for (int i = 0; i < room.Length; i++)
		{
			drawRndRoad((int)room[i].x, (int)room[i].y);
			drawRndRoad((int)room[i].x, (int)room[i].y);
			drawRndRoad((int)room[i].x, (int)room[i].y);
		}
		
		
		
		//필수적이지 않은 길 추가하기(방 to 방)
		
		min = 300;
		int loc_1 = 0;
		int gg = 0;
		int g_1 = 0;
		/*
        digRoad((int)room[1].x, (int)room[1].y, (int)room[2].x, (int)room[2].y);
        digRoad((int)room[3].x, (int)room[3].y, (int)room[4].x, (int)room[4].y);
        digRoad((int)room[0].x, (int)room[0].y, (int)room[6].x, (int)room[6].y);
        */
		
		
		/* for (int j = 0; j < room.Length / 2; j++)
        {
            int temp_X = 0;
            int temp_Y = 0;
            bool BOO = true;
            while (BOO)
            {
                temp_X = Random.Range(5, columns - 5);
                temp_Y = Random.Range(5, rows - 5);
                BOO = false;
                //if(tile[temp_Y, temp_X].tileType == TileInfo.WALL){
                //	BOO = false;
                //}
            }
            int minDistance_Between_Space_To_Room = 1000;
            int tempI = 0; ;
            for (int i = 0; i < room.Length / 3; i++)
            {
                int tempDist;
                tempDist = (int)Vector3.Distance(room[i], new Vector3(temp_X, temp_Y, 0f));
                if (tempDist < minDistance_Between_Space_To_Room)
                {
                    minDistance_Between_Space_To_Room = tempDist;
                    tempI = i;
                }

            }

            //digRoad(temp_X, temp_Y, (int)room[tempI].x, (int)room[tempI].y);
        }*/
		
		
		//막힌 길 생성하기
		
	}
	
	private void diggingRoom(int x1, int y1, int x2, int y2)
	{
		if (x2 < x1)
		{
			int tmp = x2;
			x2 = x1;
			x1 = tmp;
		}
		if (y2 < y1)
		{
			int tmp = y2;
			y2 = y1;
			y1 = y2;
		}
		for (int tilex = x1; tilex <= x2; tilex++)
		{
			for (int tiley = y1; tiley <= y2; tiley++)
			{
				tile[tiley, tilex].tileType = TileInfo.ROOM;
				tile[tiley, tilex].roomType = TileInfo.NORMALROOM;
				tile[tiley, tilex].hasWall = false;
			}
		}
		
	}
	
	private void createRoom()
	{
		int roomDistFactor = 13;
		//Debug.Log(Time.)
		int roomNum = (int)(1.5 * (Mathf.Sqrt((rows + columns) / 2)));
		//roomNum -= 2;
		int count = 0;
		int maxLoop = 10000;
		room = new Vector3[roomNum];
		
		List<Vector3> roomloc = new List<Vector3>();
		
		
		for (int x = 0; x < columns / roomDistFactor; x++)
		{
			for (int y = 0; y < rows / roomDistFactor; y++)
			{
				roomloc.Add(new Vector3(x * roomDistFactor, y * roomDistFactor, 0));
			}
		}
		
		
		
		for (int i = 0; i < roomNum; i++)
		{
			room[i] = new Vector3(0, 0, 0);
		}
		roomNum = 0;
		
		
		while (roomNum < room.Length && count < maxLoop)
		{
			//int x = (int)(Random.Range(30, (columns - 9)*10) / 15);
			//int y = (int)(Random.Range(30, (rows - 9) * 10) / 10);
			
			int RandomIndex = Random.Range(0, roomloc.Count);
			
			int x = (int)roomloc[RandomIndex].x + Random.Range(2, 4);
			int y = (int)roomloc[RandomIndex].y + Random.Range(2, 4);
			
			roomloc.RemoveAt(RandomIndex);
			
			bool stopcheck = false;
			
			int width = Random.Range(roommin, roommax);
			int height = Random.Range(roommin, roommax);
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
			
			
			
			if (stopcheck == true)
			{
				continue;
			}
			
			diggingRoom(x, y, x + width, y + height);
			room[roomNum] = new Vector3((x + x + width) / 2, (y + y + height) / 2, 0);
			roomNum++;
		}
		
	}
	
	private void diggingRoad(int x, int y)
	{
		//if (tile[y, x].hasWall == true/* && !isThisDoubleRoad(x,y)*/)
		{
			tile[y, x].setTileType(TileInfo.ROAD);
			tile[y, x].hasWall = false;
		}
	}
	
	private bool isThisDoubleRoad(int x, int y)
	{
		//0 North
		//1 West
		//2 South
		//3 East
		
		if (tile[y, x - 1].tileType == TileInfo.ROAD || tile[y, x + 1].tileType == TileInfo.ROAD)
		{
			if (tile[y - 1, x].tileType == TileInfo.ROOM)
			{
				if (tile[y - 1, x - 1].tileType == TileInfo.ROOM && tile[y - 1, x + 1].tileType == TileInfo.ROOM)
				{
					return true;
				}
			}
			else if (tile[y + 1, x].tileType == TileInfo.ROOM)
			{
				if (tile[y + 1, x - 1].tileType == TileInfo.ROOM && tile[y + 1, x + 1].tileType == TileInfo.ROOM)
				{
					return true;
				}
			}
			else
			{
				
			}
		}
		
		
		
		
		if (tile[y, x - 1].tileType == TileInfo.ROOM || tile[y, x + 1].tileType == TileInfo.ROOM)
		{
			if (tile[y - 1, x].tileType == TileInfo.ROAD)
			{
				if (tile[y - 1, x - 1].tileType == TileInfo.ROAD && tile[y - 1, x + 1].tileType == TileInfo.ROAD)
				{
					return true;
				}
			}
			else if (tile[y + 1, x].tileType == TileInfo.ROAD)
			{
				if (tile[y + 1, x - 1].tileType == TileInfo.ROAD && tile[y + 1, x + 1].tileType == TileInfo.ROAD)
				{
					return true;
				}
			}
			else
			{
				
			}
		}
		if (tile[y, x - 1].tileType == TileInfo.ROAD || tile[y, x + 1].tileType == TileInfo.ROAD)
		{
			if (tile[y - 1, x].tileType == TileInfo.ROOM || tile[y + 1, x].tileType == TileInfo.ROOM)
			{
				
				return true;
			}
		}
		if (tile[y, x - 1].tileType == TileInfo.ROOM || tile[y, x + 1].tileType == TileInfo.ROOM)
		{
			if (tile[y - 1, x].tileType == TileInfo.ROAD || tile[y + 1, x].tileType == TileInfo.ROAD)
			{
				return true;
			}
		}
		
		return false;
	}
	
	private class Room
	{
		/*
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * */
		const int NORMAL = 0;
		const int SHOP = 1;
		const int TEMPLE = 2;
		const int BOSSROOM = 3;
		
		
		
		public int RoomType;
		private int upLeft;
		private int upRight;
		private int downLeft;
		private int downRight;
		
		Room()
		{
			RoomType = NORMAL;
			upLeft = 0;
			upRight = 0;
			downLeft = 0;
			downRight = 0;
			
		}
		
		
	}
}

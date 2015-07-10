using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	//complete에 있는 프리팹 연결하면 됨


	//GameOBject("Gamemanerger"로 연결하고 
	//public void SetupScene( int level) 실행하면 맵 생성





	[Serializable]
	public class Count{
		public int minimum;
		public int maximum;

		public Count(int min, int max){
			minimum = min;
			maximum = max;
		}
	}



	public class TileINFO{
		public const int FLOOR = 0;
		public const int OUTERWALL = 1;
		public const int WALL = 2;
		public const int UPSTAIRS = 3;
		public const int DOWNSTAIRS = 4;
		public const int TRAP = 5;
		
		
		public bool hasItem = false;
		public bool hasUnit = false;
		public float locX = 0;
		public float locY = 0;
		public int tileType = 0;
		public bool hasWall = false;
		public bool PlayerSpawn = false;
		public TileINFO(){

		}
		private void setINFO(bool item, bool unit){
			hasItem = item;
			hasUnit = unit;

		}
	}


	public int roomMin = 4;
	public int roomMan = 7;

	public int columns = 8;
	public int rows = 8;
	public Count wallCount = new Count (5,9);
	public Count foodCount = new Count (1,5);
	public GameObject exit;

	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;

	private TileINFO[,] tile;
	private GameObject[,] Gobj;
	private GameObject[,] wall;
	private Vector3[] room;
	public Vector3 PlayerSpawnPoint;
	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3>();

	void InitialiseList(){

		gridPositions.Clear ();

		for(int x = 1; x<columns - 2; x++){
			for(int y = 1; y<rows -2 ; y++){
				if(tile[y,x].hasWall!=true){
					gridPositions.Add (new Vector3(x,y,0));
				}
			}
		}
	}

	void createRoad(){
		int[,] nodeToNode;
		int[,] haveConnection;
		int[] _1way = new int[room.Length];
		nodeToNode = new int[room.Length,room.Length];
		haveConnection = new int[room.Length,room.Length];


		for (int x =0; x<room.Length; x++) {
			for(int y = 0; y<room.Length ; y++){
				haveConnection[y,x] = 0;
				nodeToNode[y,x] = (int)Vector3.Distance(room[x],room[y]);
			}
			_1way[x] = 0;
		}

		//firststep

		int loc = 0;
		int min = 30;
		_1way [0] = 1;


		int numOfRoad = 0;
		//while (numOfRoad<8) {
		for(int k = 1; k<room.Length;k++){
			int g = 0;
			min = 300;
			loc = 0;

			for(int i=0;i<room.Length;i++){

				if(_1way[i]==0){
					continue;
				}
				for(int j = 0;j<room.Length;j++){
					if(i==j){
						continue;
					}
					if(_1way[j]==1){
						continue;
					}
					if(nodeToNode[i,j]<min){
						g= i;
						loc = j;
						min = nodeToNode[i,j];
					}

				}
	
			}
			if(min!=300){
				_1way[loc] = 1;
				haveConnection [g, loc] = 1;
				haveConnection [loc, g] = 1;
				digRoad((int)room[g].x,(int)room[g].y,(int)room[loc].x,(int)room[loc].y);
				numOfRoad++;
			}
		}




	}

	public Vector3 GetRespawnPoint(){


		return RandomPosition();
	}





	void digRoad(int x1, int y1, int x2, int y2){
		/*if (x2 < x1) {
			int tmp = x2;
			x2=x1;
			x1= tmp;
		}
		if (y2 < y1) {
			int tmp = y2;
			y2 = y1;
			y1 = tmp;
		}*/

		int width = x2 - x1;
		int height = y2 - y1;

		width = (int)Mathf.Abs (width);
		height = (int)Mathf.Abs (height);


		if (x2 <= x1 && y2 <= y1) {//1
			if (x2 < x1) {
				int tmp = x2;
				x2=x1;
				x1= tmp;
			}
			if (y2 < y1) {
				int tmp = y2;
				y2 = y1;
				y1 = tmp;
			}

			int middleway = 0;
			if (width > height) { //width first go
				middleway = (x1+x2)/2;
				for(int x = x1; x<=middleway;x++){
					if(tile[y1,x].hasWall ==true){
						Destroy(wall[y1,x]);
						tile[y1,x].hasWall = false;
						
					}
				}
				for(int y = y1; y<y2 ;y++){
					if(tile[y,middleway].hasWall == true){
						Destroy(wall[y,middleway]);
						tile[y,middleway].hasWall = false;
						
					}
				}
				
				for(int x = middleway; x<x2;x++){
					if(tile[y2,x].hasWall ==true){
						Destroy(wall[y2,x]);
						tile[y2,x].hasWall = false;
						
					}
				}
			} else {
				middleway = (y1+y2)/2;
				for(int y = y1; y<=middleway;y++){
					if(tile[y,x1].hasWall ==true){
						Destroy(wall[y,x1]);
						tile[y,x1].hasWall = false;
					}
				}
				
				
				for(int x = x1;x<x2;x++){
					
					if(tile[middleway,x].hasWall == true){
						Destroy(wall[middleway,x]);
						tile[middleway,x].hasWall = false;
					}
				}
				
				for(int y = middleway; y<y2;y++){
					if(tile[y,x2].hasWall ==true){
						Destroy(wall[y,x2]);
						tile[y,x2].hasWall = false;
					}
				}
			}




		} else if (x2 >= x1 && y2 <= y1) {//2
			int middleway = 0;
			if (width > height) { //width first go
				middleway = (x1+x2)/2;
				for(int x = x1; x<=middleway;x++){
					if(tile[y1,x].hasWall ==true){
						Destroy(wall[y1,x]);
						tile[y1,x].hasWall = false;
						
					}
				}
				for(int y = y2; y<y1 ;y++){
					if(tile[y,middleway].hasWall == true){
						Destroy(wall[y,middleway]);
						tile[y,middleway].hasWall = false;
						
					}
				}
				
				for(int x = middleway; x<x2;x++){
					if(tile[y2,x].hasWall ==true){
						Destroy(wall[y2,x]);
						tile[y2,x].hasWall = false;
						
					}
				}
			} else {
				middleway = (y1+y2)/2;
				for(int y = y2; y<=middleway;y++){
					if(tile[y,x2].hasWall ==true){
						Destroy(wall[y,x2]);
						tile[y,x2].hasWall = false;
						
					}
					
				}
				
				
				for(int x = x1;x<x2;x++){
					
					if(tile[middleway,x].hasWall == true){
						Destroy(wall[middleway,x]);
						tile[middleway,x].hasWall = false;
						
					}
					
				}
				
				for(int y = middleway; y<y1;y++){
					if(tile[y,x1].hasWall ==true){
						Destroy(wall[y,x1]);
						tile[y,x1].hasWall = false;
						
					}
					
				}
			}




		}else if (x2<= x1 && y2 >=y1){//3
			if (x2 < x1) {
				int tmp = x2;
				x2=x1;
				x1= tmp;
			}
			if (y2 < y1) {
				int tmp = y2;
				y2 = y1;
				y1 = tmp;
			}
			int middleway = 0;
			if (width > height) { //width first go
				middleway = (x1+x2)/2;
				for(int x = x1; x<=middleway;x++){
					if(tile[y2,x].hasWall ==true){
						Destroy(wall[y2,x]);
						tile[y2,x].hasWall = false;
						
					}
				}
				for(int y = y1; y<y2 ;y++){
					if(tile[y,middleway].hasWall == true){
						Destroy(wall[y,middleway]);
						tile[y,middleway].hasWall = false;
						
					}
				}
				
				for(int x = middleway; x<x2;x++){
					if(tile[y1,x].hasWall ==true){
						Destroy(wall[y1,x]);
						tile[y1,x].hasWall = false;
						
					}
				}
			} else {
				middleway = (y1+y2)/2;
				for(int y = y1; y<=middleway;y++){
					if(tile[y,x2].hasWall ==true){
						Destroy(wall[y,x2]);
						tile[y,x2].hasWall = false;
						
					}
					
				}
				
				
				for(int x = x1;x<x2;x++){
					
					if(tile[middleway,x].hasWall == true){
						Destroy(wall[middleway,x]);
						tile[middleway,x].hasWall = false;
						
					}
					
				}
				
				for(int y = middleway; y<y2;y++){
					if(tile[y,x1].hasWall ==true){
						Destroy(wall[y,x1]);
						tile[y,x1].hasWall = false;
						
					}
					
				}
			}


		}else{  //4
			int middleway = 0;
			if (width > height) { //width first go
				middleway = (x1+x2)/2;
				for(int x = x1; x<=middleway;x++){
					if(tile[y1,x].hasWall ==true){
						Destroy(wall[y1,x]);
						tile[y1,x].hasWall = false;
						
					}
				}
				for(int y = y1; y<y2 ;y++){
					if(tile[y,middleway].hasWall == true){
						Destroy(wall[y,middleway]);
						tile[y,middleway].hasWall = false;
						
					}
				}
				
				for(int x = middleway; x<x2;x++){
					if(tile[y2,x].hasWall ==true){
						Destroy(wall[y2,x]);
						tile[y2,x].hasWall = false;
						
					}
				}
			} else {
				middleway = (y1+y2)/2;
				for(int y = y1; y<=middleway;y++){
					if(tile[y,x1].hasWall ==true){
						Destroy(wall[y,x1]);
						tile[y,x1].hasWall = false;
			
					}
		
				}
				
				
				for(int x = x1;x<x2;x++){
					
					if(tile[middleway,x].hasWall == true){
						Destroy(wall[middleway,x]);
						tile[middleway,x].hasWall = false;
				
					}
				
				}
				
				for(int y = middleway; y<y2;y++){
					if(tile[y,x2].hasWall ==true){
						Destroy(wall[y,x2]);
						tile[y,x2].hasWall = false;
					
					}
				
				}
			}



		}


	}

	void digging(int x1, int y1, int x2, int y2){
		if (x2 < x1) {
			int tmp = x2;
			x2=x1;
			x1= tmp;
		}
		if (y2 < y1) {
			int tmp = y2;
			y2 = y1;
			y1 = y2;
		}
		for (int tilex=x1; tilex <= x2; tilex++) {
			for(int tiley = y1; tiley<= y2;tiley++){
				digTile(tilex,tiley);
			}
		}

	}

	public void createRoom(){
		int roomNum = (int)(1.5*(Mathf.Sqrt ((rows + columns)/2)));
		//roomNum = 3;
		int count = 0;
		int maxLoop = 10000;
		room = new Vector3[roomNum];
		for (int i = 0; i<roomNum; i++) {
			room[i] = new Vector3(0,0,0);
		}
		roomNum = 0;
		while (roomNum <room.Length && count <maxLoop) {
			int x = Random.Range (2,columns-7);
			int y = Random.Range (2,rows-7);
			bool stopcheck =false;

			int width = Random.Range (4,7);
			int height = Random.Range (4,7);

			for(int a=x-1; a<=x+width+1;a=a+2){
				for(int b = y-1; b<=y+height+1;b=b+2){
					if(tile[b,a].hasWall == false){
						count++;
						stopcheck = true;
						continue;
					}
				}
			}
			if(stopcheck==true){
				continue;
			}

			digging (x,y,x+width,y+height);
			room[roomNum] = new Vector3((x+x+width)/2,(y+y+height)/2,0);
			roomNum++;
		}

	}

	void BoardSetup(){

		tile = new TileINFO[rows, columns];
		Gobj = new GameObject[rows, columns];
		wall = new GameObject[rows, columns];

		for (int x = 0; x < columns; x++) {
			for(int y = 0; y < rows; y++){
				tile[y,x] = new TileINFO();
				//Gobj[y,x] = new GameObject("["+y+", " + x +"]");
			}
		}


		boardHolder = GameObject.Find ("Board").transform;
		Transform wallGroup = GameObject.Find ("Wall").transform;

		for(int x = 0; x < columns ; x++){
			for(int y = 0; y < rows ;y++){
				GameObject toInstantiate;
				GameObject instance;
				if( x==0 ||x==columns-1 || y ==0 ||y ==rows-1){
					toInstantiate = outerWallTiles[Random.Range (0,outerWallTiles.Length)];
					setTile (toInstantiate,x,y,1);
					tile[y,x].tileType = 1;
					tile[y,x].hasWall = true;
				}else{
					toInstantiate = floorTiles[Random.Range (0, floorTiles.Length)];
					setTile (toInstantiate,x,y,0);
					tile[y,x].tileType = 0;

					toInstantiate = wallTiles[Random.Range (0, wallTiles.Length)];
					tile[y,x].hasWall = true;
					wall[y,x] = Instantiate(toInstantiate, new Vector3(x,y,0f),Quaternion.identity) as GameObject;
					wall[y,x].transform.SetParent(wallGroup);

				}

				//wall[y,x].transform.SetParent (boardHolder);
				Gobj[y,x].transform.SetParent (boardHolder);
			}
		}

	}

	void digTile(int x, int y){
		Destroy (wall [y, x]);
		tile [y, x].hasWall = false;
	}


	void setTile(GameObject obj, int x, int y, int type){
		Gobj[y,x] = Instantiate(obj, new Vector3(x,y,0f),Quaternion.identity) as GameObject;
		Gobj [y, x].GetComponent<TileInfo> ().tileType = type;
		Gobj [y, x].GetComponent<TileInfo> ().locX = x;
		Gobj [y, x].GetComponent<TileInfo> ().locY = y;
	}

	void setUnit(GameObject[] obj, int x, int y){
		GameObject tileChoice = obj [Random.Range (0, obj.Length)];
		Instantiate (tileChoice, new Vector3(x,y,0), Quaternion.identity);
		Gobj [y, x].GetComponent<TileInfo> ().hasUnit = true;
	}

	void setUnitAtRandom(GameObject[] obj, int minimum, int maximum){
		int objectCount = Random.Range (minimum, maximum + 1);
		for (int i = 0; i< objectCount; i++) {
			Vector3 tempvec3 = RandomPosition();

			setUnit(obj,(int)tempvec3.x,(int)tempvec3.y);
			
		}
	}

	void setItem(GameObject[] obj, int x, int y){
		GameObject tileChoice = obj [Random.Range (0, obj.Length)];
		Instantiate (tileChoice, new Vector3(x,y,0), Quaternion.identity);
		Gobj [y, x].GetComponent<TileInfo> ().hasItem = true;
	}

	void setItemAtRandom(GameObject[] obj, int minimum, int maximum){
		int objectCount = Random.Range (minimum, maximum + 1);
		for (int i = 0; i< objectCount; i++) {
			Vector3 tempvec3 = RandomPosition();
			
			setItem(obj,(int)tempvec3.x,(int)tempvec3.y);
			
		}
	}

	Vector3 RandomPosition(){
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}

	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum){
		int objectCount = Random.Range (minimum, maximum + 1);
		for (int i = 0; i< objectCount; i++) {
			Vector3 randomPosition = RandomPosition ();
			GameObject tileChoice = tileArray [Random.Range (0, tileArray.Length)];
			Instantiate (tileChoice, RandomPosition (), Quaternion.identity);
		}
	}

	void SetSpawnAndEndPoint (){
		int min = 400;
		int dst;
		int remem = 0;;
		for (int i = 0; i<room.Length; i++) {
			dst = (int)Vector3.Distance(new Vector3(0,0,0),room[i]);
			if(dst<min){
				min = dst;
				remem = i;
			}
		}

		tile [(int)room [remem].y, (int)room [remem].x].PlayerSpawn = true;
		Gobj [(int)room [remem].y, (int)room [remem].x].GetComponent<TileInfo> ().PlayerStart = true;
		PlayerSpawnPoint = room[remem];

		min = 400;
		for (int i = 0; i<room.Length; i++) {
			dst = (int)Vector3.Distance(new Vector3(columns,rows,0),room[i]);
			if(dst<min){
				min = dst;
				remem = i;
			}
		}

		//tile[(int)room [remem].y, (int)room [remem].x];
		Instantiate (exit, room[remem], Quaternion.identity);

	}
	

	public void SetupScene( int level){
		BoardSetup ();
		createRoom ();
		createRoad ();
		SetSpawnAndEndPoint ();
		InitialiseList ();
		LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		setItemAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);

	}
}

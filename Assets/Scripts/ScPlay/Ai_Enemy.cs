using UnityEngine;
using System.Collections;
using System;

public class Ai_Enemy : MonoBehaviour{
	
	int flag = 0;
	private Vector3 pos;
	private Enemy myMaster;
	private TileInfo[,] tile;
	
	public void setPos(Vector3 pos){
		this.pos = pos;
	}
	
	public void setMaster(GameObject m){
		myMaster = m.GetComponent<Enemy>();
	}

	public void InitAI(Vector3 pos, GameObject master) {
		setPos (pos);
		setMaster (master);
		into_Loc ();
		flag = do_Dijkstra (in_EnemyIsWall (), in_EnemyIsUnit ());
		//Debug.Log ("end_flag : " + flag);
	}
	
	public int get_Flag(){
		return this.flag;
	}
	
	private void into_Loc(){
		for (int x = 0; x < 52; x++) {
			for (int y = 0; y < 52; y++) {
				tile[y,x].locX = x;
				tile[y,x].locY = y;
			}
		}
	}
	
	
	private float GetEPosX(int enemyNum){	return myMaster.enemyManager.boardManager.enemyTiles[enemyNum].transform.position.x;}
	private float GetEPosY(int enemyNum){	return myMaster.enemyManager.boardManager.enemyTiles[enemyNum].transform.position.y;}
	private int GetEnemyCount(){	return myMaster.enemyManager.boardManager.enemyTiles.Length;}
	private Vector3 GetPlayerPos(){	return myMaster.enemyManager.gameManager.player.transform.position;}
	
	public void setTileInfo(TileInfo[,] tile){
		this.tile = tile;
	}
	
	private TileInfo[] in_EnemyIsWall(){
		int can_move_count = 0;
		Vector3 player_pos = myMaster.enemyManager.gameManager.player.transform.position;
		for (int x = 0; x < 52; x++) {
			for (int y = 0; y < 52; y++) {
				bool search_enemy = false;
				for (int j = 0; j < GetEnemyCount(); j++) {
					if((GetEPosX (j) == x && GetEPosY (j) == y) && (pos.x != x && pos.y != y)){
						search_enemy = true;
						break;
					}
				}
				if (this.tile [y, x].hasWall == false && search_enemy == false) {
					can_move_count++;
				}
			}
		}
		
		TileInfo[] node_Arr_Wall = new TileInfo[can_move_count];
		int i = 0;
		while(i < can_move_count){
			for (int x = 0; x < 52; x++) {
				for (int y = 0; y < 52; y++) {
					bool search_enemy = false;
					for (int j = 0; j < GetEnemyCount(); j++) {
						if((GetEPosX (j) == x && GetEPosY (j) == y) && (pos.x != x && pos.y != y)){
							search_enemy = true;
							break;
						}
					}
					if (this.tile [y, x].hasWall == false && search_enemy == false) {
						node_Arr_Wall [i] = this.tile [y, x];
						i++;
					}
				}
			}
		}
		return node_Arr_Wall;
	}
	
	private TileInfo[] in_EnemyIsUnit(){
		int can_move_count = 0;
		for(int x = 0 ; x < 52 ; x++){
			for(int y = 0 ; y < 52 ; y++){
				if(this.tile[y,x].hasWall == false){
					can_move_count++;
				}
			}
		}
		TileInfo[] node_Arr_Unit = new TileInfo[can_move_count];
		int i = 0;
		while(i < can_move_count){
			for(int x = 0 ; x < 52 ; x++){
				for(int y = 0 ; y < 52 ; y++){
					if(this.tile[y,x].hasWall == false){
						node_Arr_Unit[i] = this.tile[y,x];
						i++;
					}
				}
			}
		}
		return node_Arr_Unit;
	}
	
	private int do_Dijkstra(TileInfo[] node_Arr_Wall, TileInfo[] node_Arr_Unit){
		int flag = 0;
		int node_num_W = node_Arr_Wall.Length, node_num_U = node_Arr_Unit.Length;
		int i, m, j;
		double dist_sum =0;
		int path_count= 0, search_count = 0;
		double min = 9999;
		int start_W = 0, target_W = 0, start_U = 0, target_U = 0;
		
		double[] dist_W = new double[node_num_W];
		int[] prev_W = new int[node_num_W];
		int[] selected_W = new int[node_num_W];
		int[] trace_path_W = new int[node_num_W];
		
		double[] dist_U = new double[node_num_U];
		int[] prev_U = new int[node_num_U];
		int[] selected_U = new int[node_num_U];
		int[] trace_path_U = new int[node_num_U];
		Vector3 player_pos = GetPlayerPos();
		
		
		// enemy 자기자신과 player가 위치한 tail의 index를 구함
		for(i = 0; i<node_Arr_Wall.Length ; i++){
			if((pos.x == node_Arr_Wall[i].locX) && (pos.y == node_Arr_Wall[i].locY)){
				start_W = i;
			}else if((player_pos.x == node_Arr_Wall[i].locX) && (player_pos.y == node_Arr_Wall[i].locY)){
				target_W = i;
			}		
		}
		for(i = 0; i<node_Arr_Unit.Length ; i++){
			if((pos.x == node_Arr_Unit[i].locX) && pos.y == node_Arr_Unit[i].locY){
				start_U = i;
			}else if((player_pos.x == node_Arr_Unit[i].locX) && (player_pos.y == node_Arr_Unit[i].locY)){
				target_U = i;
			}		
		}
		
		// edge Data 입력
		double[,] node_Edge_Wall = new double[node_Arr_Wall.Length,node_Arr_Wall.Length];
		for(int x=0; x < node_Arr_Wall.Length ; x++){
			for(int y=0 ; y < node_Arr_Wall.Length ; y++){
				node_Edge_Wall[y,x] = replace_EdgeDist(node_Arr_Wall[y],node_Arr_Wall[x]);
			}
		}
		
		double[,] node_Edge_Unit = new double[node_Arr_Unit.Length,node_Arr_Unit.Length];
		for(int x = 0; x < node_Arr_Unit.Length ; x++){
			for(int y = 0 ; y < node_Arr_Unit.Length ; y++){
				node_Edge_Unit[y,x] = replace_EdgeDist(node_Arr_Unit[y],node_Arr_Unit[x]);
			}
		}
		
		// 변수 및 배열 초기화
		for (i = 0; i < node_num_W; i++){
			dist_W[i] = 9999;
			prev_W[i] = -1;
			selected_W[i] = 0;
			trace_path_W[i] = -1;
		}
		dist_W[start_W] = 0;
		trace_path_W[0] = start_W;
		selected_W[start_W] = 1;
		
		for (i = 0; i < node_num_U; i++){
			dist_U[i] = 9999;
			prev_U[i] = -1;
			selected_U[i] = 0;
			trace_path_U[i] = -1;
		}
		dist_U[start_U] = 0;
		trace_path_U[0] = start_U;
		selected_U[start_U] = 1;
		
		// Enemy is Wall Dijkstra's Algorithm 수행
		while (selected_W[target_W] == 0) {
			min = 9999;
			m = 0;
			for (i = 0; i < node_num_W; i++) {
				dist_sum = (double)dist_W[start_W] + node_Edge_Wall [i, start_W];
				if (dist_sum < dist_W [i] && selected_W [i] == 0) {
					dist_W [i] = dist_sum;
					prev_W [i] = start_W;
				}
				if (min > dist_W [i] && selected_W [i] == 0) {			
					min = dist_W [i];
					m = i;				
				}
			} 	
			search_count++;
			start_W = m;
			selected_W [start_W] = 1;
			
			if (search_count > 9999) {
				search_count = 0;
				
				// Enemy is Unit Dijkstra's Algorithm 수행
				while (selected_U[target_U] == 0) {
					min = 9999;
					m = 0;
					for (i = 0; i < node_num_U; i++) {
						dist_sum = (double)dist_U[start_U] + node_Edge_Unit [i, start_U];
						
						if (dist_sum < dist_U [i] && selected_U [i] == 0) {
							dist_U [i] = dist_sum;
							prev_U [i] = start_U;
						}
						if (min > dist_U [i] && selected_U [i] == 0) {			
							min = dist_U [i];
							m = i;				
						}
					} 	
					search_count++;
					start_U = m;
					selected_U [start_U] = 1;
					if (search_count > 9999) {
						flag = 0;
						return flag;
					}
				}
				start_U = target_U;
				j = 0;
				// 경로 입력
				while (start_U != -1){
					trace_path_U[j++] = start_U;
					start_U = prev_U[start_U];
					path_count++;
				}
				
				if (path_count > 1) {
					flag = replace_Flag(pos,node_Arr_Unit[trace_path_U[path_count-2]]);
				} else {
					flag = 0;	
				}
				return flag;
			}
		}
		start_W = target_W;
		j = 0;
		// 경로 입력
		while (start_W != -1){
			trace_path_W[j++] = start_W;
			start_W = prev_W[start_W];
			path_count++;
		}
		if (path_count > 1) {
			flag = replace_Flag (pos, node_Arr_Wall [trace_path_W [path_count - 2]]);	
		} else {
			flag = 0;	
		}
		return flag;	
	}
	
	private double replace_EdgeDist(TileInfo base_tile, TileInfo target_tile){
		float dist = 0;
		double edge_dist = 0;
		float p1_x = base_tile.locX;
		float p1_y = base_tile.locY;
		float p2_x = target_tile.locX;
		float p2_y = target_tile.locY;
		
		dist = Mathf.Sqrt((p1_x-p2_x)*(p1_x-p2_x)+(p1_y-p2_y)*(p1_y-p2_y));
		if (dist == 0) {
			edge_dist = 0;
		} else if (dist == 1) {
			edge_dist = 1;
		} else if (dist > 1.4 && dist < 2) {
			edge_dist = 1.4;
		} else {
			edge_dist = 9999;		
		}
		return edge_dist;
	}
	
	private int replace_Flag(Vector3 base_tile, TileInfo target_tile){
		int flag = Direction.STAY;
		float p1_x = base_tile.x;
		float p1_y = base_tile.y;
		float p2_x = target_tile.locX;
		float p2_y = target_tile.locY;
		
		if ((p1_x - p2_x) == 1 && (p1_y - p2_y) == 0) {
			flag = Direction.LEFT;
		}else if((p1_x - p2_x) == 1 && (p1_y - p2_y) == -1){
			flag = Direction.LEFTUP;
		}else if((p1_x - p2_x) == 0 && (p1_y - p2_y) == -1){
			flag = Direction.UP;
		}else if((p1_x - p2_x) == -1 && (p1_y - p2_y) == -1){
			flag = Direction.RIGHTUP;
		}else if((p1_x - p2_x) == -1 && (p1_y - p2_y) == 0){
			flag = Direction.RIGHT;
		}else if((p1_x - p2_x) == -1 && (p1_y - p2_y) == 1){
			flag = Direction.RIGHTDOWN;
		}else if((p1_x - p2_x) == 0 && (p1_y - p2_y) == 1){
			flag = Direction.DOWN;
		}else if((p1_x - p2_x) == 1 && (p1_y - p2_y) == 1){
			flag = Direction.LEFTDOWN;
		}else{
			flag = Direction.STAY;
		}	
		return flag;
	}
}
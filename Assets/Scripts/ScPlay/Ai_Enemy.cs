using UnityEngine;
using System.Collections;
using System;

public class Ai_Enemy : MonoBehaviour{
	
	int flag = 0;
	private Vector3 pos;
	private Enemy myMaster;
	private TileInfo[,] tile;
	private int sight;
	
	public void setPos(Vector3 pos){
		this.pos = pos;
	}
	
	public void setMaster(GameObject m){
		myMaster = m.GetComponent<Enemy>();
	}
	
	public void setTileInfo(TileInfo[,] tile){
		this.tile = tile;
	}
	
	public void InitAI(Vector3 pos, GameObject master, int sight) {
		setPos (pos);
		setMaster (master);
		this.sight = sight;
		Vector3 player_pos = GetPlayerPos();
		//Debug.Log (player_pos);
		flag = do_Dijkstra (in_EnemyIsWall ());

		//Player와 인접해있는경우 공격
		if ((player_pos - pos).magnitude < 1.5f) {
			flag = flag*(-1);	//플래그가 음수일경우, 절대값 방향으로 공격
		}
		
	}
	
	public int get_Flag(){
		return this.flag;
	}
	
	public Vector3 GetPlayerPos(){	return myMaster.enemyManager.gameManager.player.transform.position;}
	
	private TileInfo[] in_EnemyIsWall(){
		int can_move_count = 0;
		Vector3 player_pos = myMaster.enemyManager.gameManager.player.transform.position;
		
		int xInt = Mathf.RoundToInt (pos.x);
		int yInt = Mathf.RoundToInt (pos.y);
		
		for (int x = xInt - sight ; x < xInt + sight ; x++) {
			for (int y = yInt - sight ; y < yInt + sight ; y++) {
				if(x<0 || y<0){
					continue;
				}
				if (this.tile[y,x].hasWall == false && (this.tile[y,x].e == null || myMaster.transform.position == new Vector3(x,y,0))) {
					can_move_count++;
				}
			}
		}
		
		TileInfo[] node_Arr_Wall = new TileInfo[can_move_count];
		int i = 0;
		while(i < can_move_count){
			for (int x = xInt - sight ; x < xInt + sight ; x++) {
				for (int y = yInt - sight ; y < yInt + sight ; y++) {
					//Debug.Log("tile_pos : " + this.tile [y, x].position + "// has_unit : " + (this.tile[y,x].e==null));
					if(x<0 || y<0){
						continue;
					}
					if (this.tile[y,x].hasWall == false && (this.tile[y,x].e == null || myMaster.transform.position == new Vector3(x,y,0))) {
						
						node_Arr_Wall [i] = this.tile [y, x];
						i++;
					}
				}
			}
		}
		return node_Arr_Wall;
	}
	
	private int do_Dijkstra(TileInfo[] node_Arr_Wall){
		int flag = 0;
		int node_num_W = node_Arr_Wall.Length;
		int i, m, j;
		double dist_sum =0;
		int path_count= 0, search_count = 0;
		double min = 9999;
		int start_W = 0, target_W = 0;
		
		double[] dist_W = new double[node_num_W];
		int[] prev_W = new int[node_num_W];
		int[] selected_W = new int[node_num_W];
		int[] trace_path_W = new int[node_num_W];
		
		Vector3 player_pos = GetPlayerPos();
		
		// enemy 자기자신과 player가 위치한 TILE의 index를 구함
		for(i = 0; i<node_Arr_Wall.Length ; i++){
			if(pos == node_Arr_Wall[i].position){
				start_W = i;
			}else if((player_pos - node_Arr_Wall[i].position).magnitude < 0.2f){
				target_W = i;
			}		
		}
		
		// edge Data 입력
		double[,] node_Edge_Wall = new double[node_num_W,node_num_W];
		for(int x=0; x < node_num_W ; x++){
			for(int y=0 ; y < node_num_W ; y++){
				node_Edge_Wall[y,x] = replace_EdgeDist(node_Arr_Wall[y],node_Arr_Wall[x]);
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
		
		// Enemy is Wall Dijkstra's Algorithm 수행
		while (selected_W[target_W] == 0) {
			min = 9999;
			m = 0;
			for (i = 0; i < node_num_W; i++) {
				dist_sum = (double)dist_W [start_W] + node_Edge_Wall [i, start_W];
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
				return flag = 0;
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
		Vector3 p1 = base_tile.position;
		Vector3 p2 = target_tile.position;
		
		dist = Vector3.Distance (p1, p2);
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
		Vector3 p1 = base_tile;
		Vector3 p2 = target_tile.position;
		
		if ((p1.x - p2.x) == 1 && (p1.y - p2.y) == 0) {
			flag = Direction.LEFT;
		}else if((p1.x - p2.x) == 1 && (p1.y - p2.y) == -1){
			flag = Direction.LEFTUP;
		}else if((p1.x - p2.x) == 0 && (p1.y - p2.y) == -1){
			flag = Direction.UP;
		}else if((p1.x - p2.x) == -1 && (p1.y - p2.y) == -1){
			flag = Direction.RIGHTUP;
		}else if((p1.x - p2.x) == -1 && (p1.y - p2.y) == 0){
			flag = Direction.RIGHT;
		}else if((p1.x - p2.x) == -1 && (p1.y - p2.y) == 1){
			flag = Direction.RIGHTDOWN;
		}else if((p1.x - p2.x) == 0 && (p1.y - p2.y) == 1){
			flag = Direction.DOWN;
		}else if((p1.x - p2.x) == 1 && (p1.y - p2.y) == 1){
			flag = Direction.LEFTDOWN;
		}else{
			flag = Direction.STAY;
		}	
		return flag;
	}
}
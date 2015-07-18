using UnityEngine;
using System.Collections;

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
		flag = do_Dijkstra (in_EnemyIsWall (), in_EnemyIsUnit ());
	}
	
	public int get_Flag(){
		return this.flag;
	}

	public void setTileInfo(TileInfo[,] tile){
		this.tile = tile;
	}

	private TileInfo[] in_EnemyIsWall(){
		int can_move_count = 0;
		for(int x = 1 ; x < 51 ; x++){
			for(int y = 1 ; y < 51 ; y++){
				if(this.tile[x,y].hasWall == false && this.tile[x,y].hasUnit == false){
				can_move_count++;
				}
			}
		}
		TileInfo[] node_Arr_Wall = new TileInfo[can_move_count];
		for(int i = 0 ; i < can_move_count ; i++){
			for(int x = 0 ; x < 52 ; x++){
				for(int y = 0 ; y < 52 ; y++){
					if(this.tile[x,y].hasWall == false && this.tile[x,y].hasUnit == false){
						node_Arr_Wall[i] = this.tile[x,y];
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
				if(this.tile[x,y].hasWall == false && this.tile[x,y].hasUnit == true){
					can_move_count++;
				}
			}
		}
		TileInfo[] node_Arr_Unit = new TileInfo[can_move_count];
		for(int i = 0 ; i < can_move_count ; i++){
			for(int x = 0 ; x < 52 ; x++){
				for(int y = 0 ; y < 52 ; y++){
					if(this.tile[x,y].hasWall == false && this.tile[x,y].hasUnit == true){
						node_Arr_Unit[i] = this.tile[x,y];
					}
				}
			}
		}
		return node_Arr_Unit;
	}
	
	private int do_Dijkstra(TileInfo[] node_Arr_Wall, TileInfo[] node_Arr_Unit){
		int flag = 0;
		int node_num = node_Arr_Wall.Length;
		int i, m, j;
		double dist_sum =0;
		int path_count= 0, search_count = 0;
		double min = 9999;
		int start = 0, target = 0;
		double[] dist = new double[node_num];
		int[] prev = new int[node_num];
		int[] selected = new int[node_num];
		int[] trace_path = new int[node_num];
		Vector3 player_pos = myMaster.master.main.player.transform.position;
		
		// enemy 자기자신과 player가 위치한 tail의 index를 구함
		for(i = 0; i<node_Arr_Wall.Length ; i++){
			if((pos.x == node_Arr_Wall[i].locX) && pos.y == node_Arr_Wall[i].locY){
				start = i;
			}else if((player_pos.x == node_Arr_Wall[i].locX) && (player_pos.y == node_Arr_Wall[i].locY)){
				target = i;
			}		
		}
		
		// edge Data 입력
		double[,] node_Edge_Wall = new double[node_Arr_Wall.Length,node_Arr_Wall.Length];
		for(int x=0; x < node_Arr_Wall.Length ; x++){
			for(int y=0 ; y < node_Arr_Wall.Length ; y++){
				node_Edge_Wall[x,y] = replace_EdgeDist(node_Arr_Wall[x],node_Arr_Wall[y]);
			}
		}
		double[,] node_Edge_Unit = new double[node_Arr_Unit.Length,node_Arr_Unit.Length];
		for(int x = 0; x < node_Arr_Unit.Length ; x++){
			for(int y = 0 ; y < node_Arr_Unit.Length ; y++){
				node_Edge_Unit[x,y] = replace_EdgeDist(node_Arr_Unit[x],node_Arr_Unit[y]);
			}
		}
		
		// 변수 및 배열 초기화
		for (i = 0; i < node_num; i++){
			dist[i] = 9999;
			prev[i] = -1;
			selected[i] = 0;
			trace_path[i] = -1;
		}
		dist[start] = 0;
		trace_path[0] = start;
		selected[start] = 1;
		
		// Enemy is Wall Dijkstra's Algorithm 수행
		while (selected[target] == 0) {
			min = 9999;
			m = 0;
			for (i = 0; i < node_num; i++) {
				dist_sum = (double)dist[start] + node_Edge_Wall [start, i];
				
				if (dist_sum < dist [i] && selected [i] == 0) {
					dist [i] = dist_sum;
					prev [i] = start;
				}
				if (min > dist [i] && selected [i] == 0) {			
					min = dist [i];
					m = i;				
				}
				search_count++;
			} 	
			start = m;
			selected [start] = 1;
			
			if (search_count > 9999) {
				// 변수 및 배열 초기화
				for (i = 0; i < node_num; i++) {
					dist [i] = 9999;
					prev [i] = -1;
					selected [i] = 0;
					trace_path [i] = -1;
				}
				search_count =0;
				dist [start] = 0;
				trace_path [0] = start;
				selected [start] = 1;
				
				// Enemy is Unit Dijkstra's Algorithm 수행
				while (selected[target] == 0) {
					min = 9999;
					m = 0;
					for (i = 0; i < node_num; i++) {
						dist_sum = (double)dist[start] + node_Edge_Wall [start, i];
						
						if (dist_sum < dist [i] && selected [i] == 0) {
							dist [i] = dist_sum;
							prev [i] = start;
						}
						if (min > dist [i] && selected [i] == 0) {			
							min = dist [i];
							m = i;				
						}
						search_count++;
					} 	
					start = m;
					selected [start] = 1;
					if (search_count > 9999) {
						return flag = 0;
					}
				}
				start = target;
				j = 0;
				// 경로 입력
				while (start != -1){
					trace_path[j++] = start;
					start = prev[start];
					path_count++;
				}				
				return flag = replace_Flag(pos,node_Arr_Unit[trace_path[path_count-1]]);
			}		
		}
		start = target;
		j = 0;
		// 경로 입력
		while (start != -1){
			trace_path[j++] = start;
			start = prev[start];
			path_count++;
		}
		return flag = replace_Flag(pos,node_Arr_Wall[trace_path[path_count-1]]);	
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
		int flag = MoveFlag.STAY;
		float p1_x = base_tile.x;
		float p1_y = base_tile.y;
		float p2_x = target_tile.locX;
		float p2_y = target_tile.locY;
		
		if ((p1_x - p2_x) == 1 && (p1_y - p2_y) == 0) {
			flag = MoveFlag.LEFT;
		}else if((p1_x - p2_x) == 1 && (p1_y - p2_y) == -1){
			flag = MoveFlag.LEFTUP;
		}else if((p1_x - p2_x) == 0 && (p1_y - p2_y) == -1){
			flag = MoveFlag.UP;
		}else if((p1_x - p2_x) == -1 && (p1_y - p2_y) == -1){
			flag = MoveFlag.RIGHTUP;
		}else if((p1_x - p2_x) == -1 && (p1_y - p2_y) == 0){
			flag = MoveFlag.RIGHT;
		}else if((p1_x - p2_x) == -1 && (p1_y - p2_y) == 1){
			flag = MoveFlag.RIGHTDOWN;
		}else if((p1_x - p2_x) == 0 && (p1_y - p2_y) == 1){
			flag = MoveFlag.DOWN;
		}else if((p1_x - p2_x) == 1 && (p1_y - p2_y) == 1){
			flag = MoveFlag.LEFTDOWN;
		}else{
			flag = MoveFlag.STAY;
		}	
		return flag;
	}
}
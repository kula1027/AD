using UnityEngine;
using System.Collections;

public class TileInfo : MonoBehaviour {
	/*
	 * +++++++++++++++
	 * 0 FLOOR
	 * 1 OUTERWALL
	 * 2 WALL
	 * 3 UPSTAIRS
	 * 4 DOWNSTAIRS
	 * 5 TRAP
	 * 6 ....
	 * +++++++++++++++
	 */
	public int FLOOR = 0;
	public int OUTERWALL = 1;
	public const int WALL = 2;
	public const int UPSTAIRS = 3;
	public const int DOWNSTAIRS = 4;
	public const int TRAP = 5;

	public bool PlayerStart = false;
	public bool hasItem = false;
	public bool hasUnit = false;
	public float locX = 0;
	public float locY = 0;
	public int tileType = 0;

	public void getINFO(){



	}
}

using UnityEngine;
using System.Collections;

public class TileInfo {
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



        public const int FLOOR = 0;
        public const int OUTERWALL = 1;
        public const int WALL = 2;
        public const int UPSTAIRS = 3;
        public const int DOWNSTAIRS = 4;
        public const int TRAP = 5;

        public Vector3 playerStartPoint = new Vector3();
        public bool hasItem = false;
        public bool hasUnit = false;
        public float locX = 0;
        public float locY = 0;
        public int tileType = 0;
        public bool hasWall = false;
        public bool isPlayerStartPoint = false;
        public int wallType = 0;
        public int floorType = 0;

        private void setINFO(bool item, bool unit)
        {
            hasItem = item;
            hasUnit = unit;

        }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileInfo
{


    //tileType
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

    //room type 
    /*
        100 is not room
        101 normal room?
        102 ..
        103..
        104..
        105..


    */


    public const int FLOOR = 0;
    public const int OUTERWALL = 1;
    public const int WALL = 2;
    public const int UPSTAIRS = 3;
    public const int DOWNSTAIRS = 4;
    public const int TRAP = 5;
    public const int ROAD = 6;
    public const int ROOM = 7;

    public const int NOTROOM = 100;
    public const int NORMALROOM = 101;
    public const int TEMPLEROOM = 102;
    public const int SHOPROOM = 103;
    public const int BOSSROOM = 104;
    public const int AROOM = 105;
    public const int BROOM = 106;
    public const int CROOM = 107;

    public Enemy e = null;
    public List<Item> i = new List<Item>();
	public List<Drink> d = new List<Drink> ();





    public int roomType = 0;


    public bool hasItem = false;
    public bool hasUnit = false;  //지울거같다.
    public int entityID = 0; // 0은 없다임.
                             //public float locX = 0;  
                             //public float locY = 0;  // 벡터 포지션으로 바꿈

    public Vector3 position;
    public int tileType = 0;
    public bool hasWall = false;
    public bool isPlayerStartPoint = false;

    private int objectID =0; // 0 은 없다?


    //public int wallType = 0;  //그냥 랜덤으로 맵 생성
    //public int floorType = 0;

    public void setTileType(int x)
    {
        tileType = x;
    }


  

    public void removeEntity()
    {
        e = null;
    }

    public void getEntity(Enemy ee)
    {
        this.e = ee;
    }

    public void addItem(Item iii)
    {
        i.Add(iii);

    }
    public Item getItem(int num)
    {
        Item temp = i[num];
        i.RemoveAt(num);
        return temp;
    }

    public void showItem() //그 타일에 어떤 아이템이 뭐뭐있는지
    {
        //sendmessage( 아이템이 뭐뭐 있는지 말해준다.)


    }

    public void setRoomType(int x)
    {
        if (x < 100 && x > 110)
        {
            roomType = x;
        }
        else
        {
            Debug.LogError("roomType 범위 초과 : " + x);
            return;
        }
    }

    public TileInfo(int tilePosX, int tilePosY)
    {
        position = new Vector3(tilePosX, tilePosY, 0);
    }

    private void setINFO(bool item, bool unit)
    {
        hasItem = item;
        hasUnit = unit;

    }

}

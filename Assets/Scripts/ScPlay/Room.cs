using UnityEngine;
using System.Collections;

public class Room{

    //object[][] roomTile;
    int[,] roomTiles;

    public const int UP = 0;
    public const int RIGHT = 90;
    public const int DOWN = 180;
    public const int LEFT = 270;

    public const int BOX_ROOM = 100001;
    public const int TEMPLE_ROOM = 100002;
    public const int TRAP_ROOM = 100003;
    public const int SHOP_ROOM = 100004;
    public const int BOSS_ROOM1 = 900001;

    private int roomType;
    private int direction;
    private int sizeW;
    private int sizeH;


    public Room()
    {

    }

    public Room(int num)
    {
        setRoom(num);
    }



    private void setRoom(int num)
    {
        setDirection(UP);
        roomType = num;

        switch (num)
        {
            case BOX_ROOM:

                

                break;

            case TEMPLE_ROOM:

                break;

            case TRAP_ROOM:

                break;

            case SHOP_ROOM:
                sizeH = 3;
                sizeW = 3;
                roomTiles = new int[sizeH, sizeW];
                for(int y = 0; y< sizeH; y++)
                {
                    for(int x = 0;x< sizeW; x++)
                    {



                    }
                }


                break;

            case BOSS_ROOM1:

                break;

            default:
                Debug.Log("!!");

                break;
        }



    }

    public void setDirection(int dirct)
    {
        direction = dirct;
    }
}

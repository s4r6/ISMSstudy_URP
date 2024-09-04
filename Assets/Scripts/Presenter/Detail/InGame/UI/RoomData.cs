using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class RoomData
{
    private static Dictionary<int, int> RoomDic = new Dictionary<int, int>();

    public static void SetRoomInfo(int RoomID, int PlayerNum)
    {
        RoomDic[RoomID] = PlayerNum;
    }

    public static Dictionary<int, int> GetRoomList()
    {
        return RoomDic;
    }
}

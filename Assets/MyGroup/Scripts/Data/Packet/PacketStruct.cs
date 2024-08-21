using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ISMS.Data.Packet
{
    public enum PacketId
    {
        StageDataPacket,
        DefaultPacket
    }

    public struct RoomDataPacket
    {
        public Dictionary<int, int> RoomList;
    }
    public struct StageDataPakcet //起動時に読み込んだステージを送信するパケット
    {
        public int PacketID;
        public int StageID; //読み込むステージのID
    }

    public struct DefaultPacket //ゲーム中に送信されるパケット
    {
        public int PacketID;
        public string SurveiedObjName; //調査されたオブジェクトのなまえ
        public int JudgeCount; //判定した回数
        public int correctCount; //正解した数
    }
}



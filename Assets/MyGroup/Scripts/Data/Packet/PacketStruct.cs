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
    public struct StageDataPakcet //�N�����ɓǂݍ��񂾃X�e�[�W�𑗐M����p�P�b�g
    {
        public int PacketID;
        public int StageID; //�ǂݍ��ރX�e�[�W��ID
    }

    public struct DefaultPacket //�Q�[�����ɑ��M�����p�P�b�g
    {
        public int PacketID;
        public string SurveiedObjName; //�������ꂽ�I�u�W�F�N�g�̂Ȃ܂�
        public int JudgeCount; //���肵����
        public int correctCount; //����������
    }
}



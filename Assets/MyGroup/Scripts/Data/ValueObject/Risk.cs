using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Data
{
    public class Risk
    {
        static readonly int DENGER = 1;
        static readonly int SAFE = 0;

        public readonly int _risk;

        public Risk(int risk)
        {
            if (risk != SAFE && risk != DENGER)
                Debug.LogError("���X�N���z�肳��Ă��Ȃ��l�ł�");

            _risk = risk;
        }
    }
}


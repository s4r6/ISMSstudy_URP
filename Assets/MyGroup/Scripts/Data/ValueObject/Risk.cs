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
                Debug.LogError("リスクが想定されていない値です");

            _risk = risk;
        }
    }
}


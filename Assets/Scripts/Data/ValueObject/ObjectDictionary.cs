using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Data
{
    public class ObjectDictionary
    {
        Dictionary<string, Object> _objDic = new Dictionary<string, Object>();

        public void AddObject(string system, Object obj)
        {
            if(!IsExist(system))
                _objDic.Add(system, obj);
        }

        public Object GetObject(string system)
        {
            return _objDic[system];
        }

        bool IsExist(string system)
        {
            if(_objDic.ContainsKey(system))
            {
                Debug.LogError("重複したシステム名が使用されています");
                return true;
            }
            else
                return false;
        }

        public int GetDengerObjNum()
        {
            int objNum = 0;
            foreach(var key in _objDic.Keys)
            {
                if (_objDic[key].ObjRisk == Risk.DENGER)
                    objNum++;
            }
            return objNum;
        }
    }
}


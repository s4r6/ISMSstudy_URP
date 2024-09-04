using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Data
{
    public class Object
    {
        readonly Name _name;
        public string SystemName => _name._systemName;
        public string ObjName => _name._objName;
        
        readonly Describe _describe;
        public string ObjDescribe => _describe._describe; 
        
        readonly Explanation _explanation;
        public string ObjExplanation => _explanation._explanation;

        readonly Risk _risk;
        public int ObjRisk => _risk._risk;

        public Object(string system, string obj, string describe, string explanation, int risk)
        {
            _name = new Name(system, obj);
            _describe = new Describe(describe);
            _explanation = new Explanation(explanation);
            _risk = new Risk(risk);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Data;
using UniRx;
using ISMS.Presenter.Detail.Player;
using Cysharp.Threading.Tasks;
using ISMS.Presenter.Detail.UI;

namespace ISMS.Presenter.Detail.Stage
{
    public abstract class BaseSurveyObject : MonoBehaviour
    {
        public Data.Object _obj;

        public string _name { get; private set; }
        public string _describe { get; private set; }
        public string _explanation { get; private set; }
        public int _risk { get; private set; }

        void Start()
        {
            _name = _obj.ObjName;
            _describe = _obj.ObjDescribe;
            _explanation = _obj.ObjExplanation;
            _risk = _obj.ObjRisk;
        }
    }
}

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
    public enum CheckFlag
    {
        Denger,
        Safe,
        NotSurvey
    }


    public abstract class BaseSurveyObject : MonoBehaviour
    {
        CheckFlag _riskFlag = CheckFlag.NotSurvey;
        ReactiveProperty<bool> _correctFlag = new BoolReactiveProperty();
        public IReadOnlyReactiveProperty<bool> CorrectFlag => _correctFlag;
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

        public void Survey()
        {
            if (_risk == Risk.DENGER)   //危険なオブジェクトなら正解
            {
                _riskFlag = CheckFlag.Denger;
                _correctFlag.Value = true;
            }
            else if (_risk == Risk.SAFE)    //安全なオブジェクトなら不正解
            {
                _riskFlag = CheckFlag.Safe;
                _correctFlag.Value = false;
            }
                
        }
    }
}

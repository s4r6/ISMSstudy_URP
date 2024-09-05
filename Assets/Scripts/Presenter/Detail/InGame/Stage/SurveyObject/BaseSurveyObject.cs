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
        public CheckFlag _riskFlag = CheckFlag.NotSurvey;
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
            if(_riskFlag == CheckFlag.NotSurvey)
            {
                Debug.Log("Survey");
                if (_risk == Risk.DENGER)   //�댯�ȃI�u�W�F�N�g�Ȃ琳��
                {
                    _riskFlag = CheckFlag.Denger;
                    _correctFlag.Value = true;
                }
                else if (_risk == Risk.SAFE)    //���S�ȃI�u�W�F�N�g�Ȃ�s����
                {
                    Debug.Log("IsSafe");
                    _riskFlag = CheckFlag.Safe;
                    _correctFlag.SetValueAndForceNotify(false); //�����l��false�̂��߁A�����l�ɕύX���ꂽ�ꍇ�ł��C�x���g�𔭍s����悤�ɂ���
                }
            }
        }
    }
}

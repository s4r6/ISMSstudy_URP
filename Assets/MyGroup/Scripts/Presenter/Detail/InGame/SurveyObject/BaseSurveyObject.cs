using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Data;
using UniRx;
using ISMS.Presenter.Detail.Player;
using ISMS.Presenter.Detail.UI;

namespace ISMS.Presenter.Detail.Stage
{
    public abstract class BaseSurveyObject : MonoBehaviour
    {
        public ReactiveProperty<Data.Object> _obj = new ReactiveProperty<Data.Object>(null);

        public string _name { get; private set; }
        public string _describe { get; private set; }
        public string _explanation { get; private set; }
        public int _risk { get; private set; }

        void Start()
        {
            _obj
                .Subscribe(x =>
                {
                    _name = _obj.Value.ObjName;
                    _describe = _obj.Value.ObjName;
                    _explanation = _obj.Value.ObjName;
                    _risk = _obj.Value.ObjRisk;
                }).AddTo(this);

            
        }
    }
}

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

    /// <summary>
    /// 調査オブジェクトの元となるクラス
    /// </summary>
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

        async void  Start()
        {
            await UniTask.WaitUntil(() => _obj != null);    //オブジェクトの情報がセットされるまで待機
            _name = _obj.ObjName;
            _describe = _obj.ObjDescribe;
            _explanation = _obj.ObjExplanation;
            _risk = _obj.ObjRisk;
        }

        public void Survey()
        {
            if(_riskFlag == CheckFlag.NotSurvey)
            {
                if (_risk == Risk.DENGER)   //危険なオブジェクトなら正解
                {
                    _riskFlag = CheckFlag.Denger;
                    _correctFlag.Value = true;
                }
                else if (_risk == Risk.SAFE)    //安全なオブジェクトなら不正解
                {
                    _riskFlag = CheckFlag.Safe;
                    _correctFlag.SetValueAndForceNotify(false); //初期値がfalseのため、同じ値に変更された場合でもイベントを発行するようにする
                }
            }
        }
    }
}

using UnityEngine;
using DG.Tweening;
using ISMS.Presenter.Detail.Player;
using ISMS.Presenter.Detail.Stage;
using TMPro;
using UniRx;
using System;

namespace ISMS.Presenter.Detail.UI
{
    /// <summary>
    /// オブジェクトの詳細情報を表示するUIの管理を行うクラス
    /// </summary>
    public class DetailDataWindow : BaseUIWindow
    {
        PlayerInspect _inspect;

        [SerializeField]
        TextMeshProUGUI _objNameText;
        [SerializeField]
        TextMeshProUGUI _objDescribeText;
        [SerializeField]
        TextMeshProUGUI _riskFlagText;

        protected override PlayerState myState { get; set; } = PlayerState.DetailInfo;

        protected override void Initialize()
        {
            _inspect = _player.GetComponent<PlayerInspect>();

            _input.DiscoverButtonPush   //リスク発見ボタンが押されたら
                .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                .Subscribe(_ =>
                {
                    if (_inspect.PreHitObj._riskFlag == CheckFlag.NotSurvey)    //調査済みでないオブジェクトなら
                    {
                        //リスク発見状態に移動
                        _inspect.PreHitObj.Survey();    
                        _state.ChangeCurrentPlayerState(PlayerState.Discover);
                        this.gameObject.SetActive(false);
                    }
                    else
                        SystemMessage.SetMessage(SystemCode.Surveyed);  //調査済みオブジェクトの場合システムメッセージを表示する
                }).AddTo(this);

            this.gameObject.SetActive(false);
        }

        protected override void DisplayWindow() //詳細情報を設定して段々拡大して表示
        {
            SetDetailData();

            this.gameObject.transform.localScale = Vector3.zero;
            this.gameObject.SetActive(true);

            this.gameObject.transform.DOScale(new Vector3(1, 1, 1), DisplayTime)
                .SetEase(Ease.OutCubic);
        }
        void SetDetailData()
        {
            BaseSurveyObject ObjData = _inspect.PreHitObj;
            _objNameText.text = ObjData._name;
            _objDescribeText.text = ObjData._describe;
            switch(ObjData._riskFlag)   //調査したオブジェクトのリスクの有無を表示
            {
                case CheckFlag.NotSurvey:
                    _riskFlagText.text = "?";
                    break;

                case CheckFlag.Denger:
                    _riskFlagText.text = "有";
                    break;

                case CheckFlag.Safe:
                    _riskFlagText.text = "無";
                    break;
            }
        }
    }
}

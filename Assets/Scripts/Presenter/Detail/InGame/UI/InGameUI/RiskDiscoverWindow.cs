using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ISMS.Presenter.Detail.Player;
using ISMS.Presenter.Detail.Stage;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UniRx;
using ISMS.Data;
using UnityEngine.UI;

namespace ISMS.Presenter.Detail.UI
{
    /// <summary>
    /// リスク発見状態になったときに表示されるUIを管理するクラス
    /// </summary>
    public class RiskDiscoverWindow : BaseUIWindow
    {

        [SerializeField]
        PlayerInspect _inspect;

        [SerializeField]
        TextMeshProUGUI _objNameText;

        [SerializeField]
        GameObject[] _judgeMark;
        [SerializeField]
        TextMeshProUGUI _judgeText;
        [SerializeField]
        TextMeshProUGUI _explanationText;

        [SerializeField]
        GameObject _cutIn;
        [SerializeField]
        GameObject _backGround;


        protected override PlayerState myState { get; set; } = PlayerState.Discover;    //自分が表示される状態の設定

        const int CORRECT = 0;
        const int INCORRECT = 1;

        protected override void Initialize()
        {
            foreach (var mark in _judgeMark)
            {
                mark.SetActive(false);
            }

            _cutIn.GetComponent<RiskCutIn>().OnEndAnimation //カットインアニメーションが終わったら
                .Subscribe(_ =>
                {
                    _backGround.transform.localScale = Vector3.zero;    //解説画面をだんだん拡大しながら表示
                    _backGround.SetActive(true);

                    _backGround.transform.DOScale(new Vector3(1, 1, 1), DisplayTime)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() => InputEnable = true);
                }).AddTo(this);

            _backGround.SetActive(false);
            _cutIn.SetActive(false);
            this.gameObject.SetActive(false);
        }

        void SetRiskData()  //表示される前にデータを取得して設定
        {
            BaseSurveyObject ObjData = _inspect.PreHitObj;
            _objNameText.text = ObjData._name;
            _explanationText.text = ObjData._explanation;

            if (ObjData._risk == Risk.DENGER)
            {
                _judgeText.text = "リスク発見 : 成功！";
                _judgeMark[CORRECT].SetActive(true);
            }
            else
            {
                _judgeText.text = "リスク発見 : 失敗…";
                _judgeMark[INCORRECT].SetActive(true);
            }
        }

        protected override async void ExitWindow()  
        {
            if (!_backGround.activeSelf) return;
            await _backGround.transform.DOScale(new Vector3(0, 0, 0), DisplayTime)
                .SetEase(Ease.OutCubic);

            _backGround.SetActive(false);
            this.gameObject.SetActive(false);

            foreach (var mark in _judgeMark)
            {
                mark.SetActive(false);
            }

            _state.ChangeCurrentPlayerState(PlayerState.Explore);
                
        }

        //表示される前にカットインアニメーションを起動
        protected override void DisplayWindow()
        {
            SetRiskData();

            this.gameObject.SetActive(true);
            
            _cutIn.GetComponent<RiskCutIn>().Animation();
        }
    }
}

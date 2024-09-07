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


        protected override PlayerState myState { get; set; } = PlayerState.Discover;

        const int CORRECT = 0;
        const int INCORRECT = 1;

        protected override void Initialize()
        {
            foreach (var mark in _judgeMark)
            {
                mark.SetActive(false);
            }

            _cutIn.GetComponent<RiskCutIn>().OnEndAnimation
                .Subscribe(_ =>
                {
                    _backGround.transform.localScale = Vector3.zero;
                    _backGround.SetActive(true);

                    _backGround.transform.DOScale(new Vector3(1, 1, 1), DisplayTime)
                    .SetEase(Ease.OutCubic);
                }).AddTo(this);

            _backGround.SetActive(false);
            _cutIn.SetActive(false);
            this.gameObject.SetActive(false);
        }

        void SetRiskData()
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

        protected override void DisplayWindow()
        {
            SetRiskData();

            this.gameObject.SetActive(true);
            
            _cutIn.GetComponent<RiskCutIn>().Animation();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ISMS.Presenter.Detail.Player;
using ISMS.Presenter.Detail.Stage;
using DG.Tweening;
using UniRx;
using ISMS.Data;
using UnityEngine.UI;

namespace ISMS.Presenter.Detail.UI
{
    public class RiskDiscoverWindow : BaseUIWindow
    {

        [SerializeField]
        GameObject _player;
        PlayerCore _state;
        IInputProvider _input;
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
        GameObject _dengerMark;
        [SerializeField]
        Material _cutInMaterial;
        [SerializeField]
        float _slideSpeed;

        protected override PlayerState myState { get; set; } = PlayerState.Discover;

        const int CORRECT = 0;
        const int INCORRECT = 1;

        protected override void Initialize()
        {
            foreach (var mark in _judgeMark)
            {
                mark.SetActive(false);
            }

            _cutInMaterial = _cutIn.GetComponent<Image>().material;

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

        void Update()
        {
            if(_cutInMaterial != null)
            {
                //var x = Mathf.Repeat(Time.time * _slideSpeed, )
            }    
        }

        protected override void DisplayWindow()
        {
            SetRiskData();

            var sequence = DOTween.Sequence();
            //sequence.Append()

            this.gameObject.SetActive(true);
        }
    }
}

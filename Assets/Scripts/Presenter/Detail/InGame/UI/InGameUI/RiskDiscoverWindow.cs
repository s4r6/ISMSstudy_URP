using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ISMS.Presenter.Detail.Player;
using ISMS.Presenter.Detail.Stage;
using UniRx;
using ISMS.Data;

namespace ISMS.Presenter.Detail.UI
{
    public class RiskDiscoverWindow : MonoBehaviour
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
        GameObject CutIn;

        const int CORRECT = 0;
        const int INCORRECT = 1;

        // Start is called before the first frame update
        void Start()
        {
            _state = _player.GetComponent<PlayerCore>();
            _inspect = _player.GetComponent<PlayerInspect>();
            _input = _player.GetComponent<IInputProvider>();

            foreach (var mark in _judgeMark)
            {
                mark.SetActive(false);
            }

            _state.CurrentPlayerState
                .Subscribe(x =>
                {
                    if(x == PlayerState.Discover)
                    {
                        SetRiskData();
                        DisplayWindow();
                    }
                    else
                    {
                        this.gameObject.SetActive(false);
                    }
                    
                }).AddTo(this);

            _input.BackButtonPush
               .Where(x => x == true && _state.CurrentPlayerState.Value == PlayerState.Discover)
               .Subscribe(_ =>
               {
                   ExitDiscoverWindow();
               }).AddTo(this);


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

        void DisplayWindow()
        {
            this.gameObject.SetActive(true);
        }

        void ExitDiscoverWindow()
        {
            foreach (var mark in _judgeMark)
            {
                mark.SetActive(false);
            }
            _state.ChangeCurrentPlayerState(PlayerState.Explore);
        }
    }
}

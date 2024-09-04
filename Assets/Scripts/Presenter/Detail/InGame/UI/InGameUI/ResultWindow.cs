using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ISMS.Presenter.Detail.Player;
using TMPro;
namespace ISMS.Presenter.Detail.UI
{
    public class ResultWindow : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI InCorrectCountText;
        [SerializeField]
        TextMeshProUGUI ClearTime;
        [SerializeField]
        PlayerCore _state;

        void Start()
        {
            _state.CurrentPlayerState
                .Subscribe(x =>
                {
                    if(x == PlayerState.Result)
                    {
                        SetResultData();
                        DisplayWindow();
                    }
                    
                }).AddTo(this);

            this.gameObject.SetActive(false);
        }

        void SetResultData()
        {
            InCorrectCountText.text = _state.InCorrectCount.ToString();
        }

        void DisplayWindow()
        {
            this.gameObject.SetActive(true);
        }

    }
}

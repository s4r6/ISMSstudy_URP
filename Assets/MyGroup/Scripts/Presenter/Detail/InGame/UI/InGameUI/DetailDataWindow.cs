using UnityEngine;
using DG.Tweening;
using ISMS.Presenter.Detail.Player;
using ISMS.Presenter.Detail.Stage;
using TMPro;
using UniRx;
using System;

namespace ISMS.Presenter.Detail.UI
{
    public class DetailDataWindow : MonoBehaviour
    {
        [SerializeField]
        GameObject _player;
        PlayerCore _playerState;
        IInputProvider _input;
        PlayerInspect _playerInspect;

        [SerializeField]
        GameObject _window;
        [SerializeField]
        float DisplayTime = 0.2f;

        [SerializeField]
        TextMeshProUGUI _objNameText;
        [SerializeField]
        TextMeshProUGUI _objDescribeText; 

        void Start()
        {
            _playerState = _player.GetComponent<PlayerCore>();
            _playerInspect = _player.GetComponent<PlayerInspect>();
            _input = _player.GetComponent<IInputProvider>();

            _playerState.CurrentPlayerState
                .Where(x => x == PlayerState.DetailInfo)
                .Subscribe(_ =>
                {
                    SetDetailData();
                    DisplayWindow();
                }).AddTo(this);

            _input.BackButtonPush
                .Where(x => x == true)
                .Subscribe(_ =>
                {
                    ExitDetailDataWindow();
                }).AddTo(this);

            this.gameObject.SetActive(false);
        }

        void SetDetailData()
        {
            BaseSurveyObject ObjData = _playerInspect.PreHitObj;
            _objNameText.text = ObjData._name;
            _objDescribeText.text = ObjData._describe;
        }

        void DisplayWindow()
        {
            this.gameObject.transform.localScale = Vector3.zero;
            this.gameObject.SetActive(true);

            _window.transform.DOScale(new Vector3(1, 1, 1), DisplayTime)
                .SetEase(Ease.OutCubic);
        }

        void ExitDetailDataWindow()
        {
            gameObject.SetActive(false);
            _playerState.ChangeCurrentPlayerState(PlayerState.Explore);
        }
    }
}

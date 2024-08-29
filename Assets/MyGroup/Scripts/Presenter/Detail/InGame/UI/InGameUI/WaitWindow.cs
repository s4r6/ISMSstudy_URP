using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using DG.DOTweenEditor;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using ISMS.Presenter.Detail.Player;

namespace ISMS.Presenter.Detail.UI
{
    public class WaitWindow : MonoBehaviour
    {
        [SerializeField]
        GameObject _player;
        PlayerCore _playerState;
        IInputProvider _input;


        [SerializeField]
        Text BlinkText;
        [SerializeField]
        Text CutInText;

        [SerializeField]
        float BlinkInterval;    //�_�Ŏ���
        [SerializeField]
        float CutInOutTime; 
        [SerializeField]
        int CutInWaitTime;

        
        void Start()
        {
            _playerState = _player.GetComponent<PlayerCore>();
            _input = _player.GetComponent<PlayerInputData>();

            _playerState.CurrentPlayerState
                .Where(x => x == PlayerState.Wait)
                .Subscribe(x =>
                {
                    this.gameObject.SetActive(true);
                }).AddTo(this);

            _input.AnyButtonPush
                .Where(x => x == true)
                .Subscribe(_ =>
                {
                    OnExitWaitState();
                }).AddTo(this);
        }

        void OnEnable()
        {
            BlinkText.DOFade(0.0f, BlinkInterval)   //�_��
                .SetLoops(-1, LoopType.Yoyo);
        }

        //�J�b�g�C���̍Đ�
        async UniTask PlayCutIn()
        {
            var CutInTextObj = CutInText.GetComponent<RectTransform>();
            var StartPos = CutInTextObj.localPosition;

            await CutInTextObj.DOAnchorPos(new Vector3(0, 0, 0), CutInOutTime)
                    .SetEase(Ease.OutCubic);

            await UniTask.Delay(CutInWaitTime);

            await CutInTextObj.DOAnchorPos(new Vector3(-StartPos.x, 0, 0), CutInOutTime)
                    .SetEase(Ease.InCubic);

            CutInTextObj.gameObject.SetActive(false);
            CutInTextObj.localPosition = StartPos;
        }

        //�����̃L�[�������ꂽ���̏���
        async void OnExitWaitState()
        {
            BlinkText.gameObject.SetActive(false);
            await PlayCutIn();
            _playerState.ChangeCurrentPlayerState(PlayerState.Explore); //Explore�X�e�[�g�֕ύX
            this.gameObject.SetActive(false);   //��\��
        }

    }
}

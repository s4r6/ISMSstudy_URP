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

namespace ISMS.Presenter.Detail.UI
{
    public class WaitWindow : MonoBehaviour
    {
        [SerializeField]
        Text BlinkText;
        [SerializeField]
        Text CutInText;

        [SerializeField]
        private float BlinkInterval;    //�_�Ŏ���
        [SerializeField]
        private float CutInOutTime; 
        [SerializeField]
        private int CutInWaitTime;


        private void Start()
        {
            /*_player.CurrentPlayerState
                .Where(state => state == PlayerState.Wait)
                .Subscribe(state =>
                {
                    this.gameObject.SetActive(true);
                })
                .AddTo(this);

            _input.AnyButtonPressed
                .Where(x => x == true)
                .Subscribe(_ =>
                {
                    OnExitWaitState();
                }).AddTo(this);*/
        }

        private void OnEnable()
        {
            BlinkText.DOFade(0.0f, BlinkInterval)   //�_��
                .SetLoops(-1, LoopType.Yoyo);
        }

        //�J�b�g�C���̍Đ�
        private async UniTask PlayCutIn()
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
        private async void OnExitWaitState()
        {
            BlinkText.gameObject.SetActive(false);
            await PlayCutIn();
            //ChangePlayerState(PlayerState.Explore); //Explore�X�e�[�g�֕ύX
            this.gameObject.SetActive(false);   //��\��
        }

    }
}

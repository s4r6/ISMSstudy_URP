using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using ISMS.Presenter.Detail.Player;

namespace ISMS.Presenter.Detail.UI
{
    public class WaitWindow : BaseUIWindow
    {
        protected override PlayerState myState { get; set; } = PlayerState.Wait;


        [SerializeField]
        Text BlinkText;
        [SerializeField]
        Text CutInText;

        [SerializeField]
        float BlinkInterval;    //点滅時間
        [SerializeField]
        float CutInOutTime; 
        [SerializeField]
        int CutInWaitTime;

        protected override void Initialize()
        {
            _input.AnyButtonPush
                .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                .Subscribe(_ =>
                {
                    ExitWindow();
                }).AddTo(this);
        }

        void OnEnable()
        {
            BlinkText.DOFade(0.0f, BlinkInterval)   //点滅
                .SetLoops(-1, LoopType.Yoyo);
        }

        //カットインの再生
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

        protected override void DisplayWindow()
        {
            this.gameObject.SetActive(true);
        }

        protected override async void ExitWindow()
        {
            BlinkText.gameObject.SetActive(false);
            await PlayCutIn();
            _state.ChangeCurrentPlayerState(PlayerState.Explore);
            this.gameObject.SetActive(false);
        }
    }
}

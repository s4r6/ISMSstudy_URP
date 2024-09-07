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
        float CutInWaitTime;

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
        void PlayCutIn()
        {
            var CutInTextObj = CutInText.GetComponent<RectTransform>();
            var StartPos = CutInTextObj.localPosition;

            var sequence = DOTween.Sequence();

            sequence.Append(CutInTextObj.DOAnchorPos(new Vector3(0, 0, 0), CutInOutTime)
                    .SetEase(Ease.OutCubic))
                    .AppendInterval(CutInWaitTime)
                    .Append(CutInTextObj.DOAnchorPos(new Vector3(-StartPos.x, 0, 0), CutInOutTime)
                    .SetEase(Ease.InCubic))
                    .OnComplete(() =>
                    {
                        CutInTextObj.gameObject.SetActive(false);
                        CutInTextObj.localPosition = StartPos;
                        _state.ChangeCurrentPlayerState(PlayerState.Explore);
                        this.gameObject.SetActive(false);
                    });

            
        }

        protected override void DisplayWindow()
        {
            this.gameObject.SetActive(true);
        }

        protected override void ExitWindow()
        {
            BlinkText.gameObject.SetActive(false);
            PlayCutIn();
        }
    }
}

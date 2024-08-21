using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Presenter;
using UniRx;
using System;
using UnityEngine.UI;

namespace ISMS.Presenter.Detail
{
    public class StageSelectView : MonoBehaviour,IStageSelectView
    {
        [SerializeField]
        Button Stage1;
        [SerializeField]
        Button Stage2;

        Subject<string> clickedButtonName = new Subject<string>();
        public IObservable<string> ClickedButtonName => clickedButtonName;


        void Start()
        {
            this.gameObject.SetActive(false);
            Stage1.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    clickedButtonName.OnNext("Stage1");
                }).AddTo(this);
        }

        public void ViewObjectActive()
        {
            this.gameObject.SetActive(true);
        }
    }
}

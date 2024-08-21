using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Domain.Presenter;
using UniRx;
using System;

namespace ISMS.Presenter
{
    public class StagePresenter : IStagePresenter 
    {
        IStageSelectView stageSelect;
        public IObservable<string> OnSelectStage => stageSelect.ClickedButtonName;

        public StagePresenter(IStageSelectView view)
        {
            this.stageSelect = view;
        }

        public void ActivateStageList()
        {
            stageSelect.ViewObjectActive();
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace ISMS.Domain.Presenter
{
    public interface IStagePresenter : CleanArchtecture.IPresenter
    {
        public IObservable<string> OnSelectStage { get; }
        public void ActivateStageList();
    }
}


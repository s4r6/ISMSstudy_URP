using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleanArchtecture;
using UniRx;
using System;

namespace ISMS.Domain.Presenter
{
    public interface IMatchingPresenter : IPresenter 
    {
        public IObservable<Unit> StartButtonClicked { get; }
        public IObservable<Unit> JoinButtonClicked { get; }
        public IObservable<Unit> SoloModeButtonClicked { get; }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using ISMS.Domain.Presenter;

namespace ISMS.Presenter
{
    public class RoomPresenter : IMatchingPresenter
    {
        IRoomView roomView;

        public RoomPresenter(IRoomView _roomView)
        {
            this.roomView = _roomView;
        }

        public IObservable<Unit> StartButtonClicked => roomView.StartButton.OnClickAsObservable();
        public IObservable<Unit> JoinButtonClicked => roomView.JoinButton.OnClickAsObservable();
        public IObservable<Unit> SoloModeButtonClicked => roomView.SoloModeButton.OnClickAsObservable();
    }
}


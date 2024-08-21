using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UniRx;

namespace ISMS.Presenter
{
    public interface IStageSelectView
    { 
        public IObservable<string> ClickedButtonName { get; }
        public void ViewObjectActive();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CleanArchtecture;

namespace ISMS.Presenter
{
    public interface IRoomView : IView
    {
        Button StartButton { get; }
        Button JoinButton { get; }
        Button SoloModeButton { get; }
    }
}


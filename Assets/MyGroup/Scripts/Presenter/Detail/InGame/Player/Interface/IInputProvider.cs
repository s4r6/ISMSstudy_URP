using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ISMS.Presenter.Detail.Player
{
    public interface IInputProvider
    {
        IReadOnlyReactiveProperty<Vector2> MoveDirection { get; }
        IReadOnlyReactiveProperty<Vector2> LookDirection { get; }
        IReadOnlyReactiveProperty<bool> AnyButtonPush { get; }
        IReadOnlyReactiveProperty<bool> InspectButtonPush { get; }
        IReadOnlyReactiveProperty<bool> BackButtonPush { get; }
    }
}

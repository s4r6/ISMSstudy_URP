using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ISMS.Presenter.Detail.Player
{
    /// <summary>
    /// 取得した入力を外部に公開
    /// </summary>
    public interface IInputProvider
    {
        IReadOnlyReactiveProperty<Vector2> MoveDirection { get; }
        IReadOnlyReactiveProperty<Vector2> LookDirection { get; }
        IReadOnlyReactiveProperty<bool> AnyButtonPush { get; }
        IReadOnlyReactiveProperty<bool> InspectButtonPush { get; }
        IReadOnlyReactiveProperty<bool> BackButtonPush { get; }
        IReadOnlyReactiveProperty<bool> DiscoverButtonPush { get; }
        IReadOnlyReactiveProperty<bool> GimicActionButtonPush { get; }
        IReadOnlyReactiveProperty<bool> DocumentButtonPush { get; }
        IReadOnlyReactiveProperty<bool> RightPageButtonPush { get; }
        IReadOnlyReactiveProperty<bool> LeftPageButtonPush { get; }
    }
}

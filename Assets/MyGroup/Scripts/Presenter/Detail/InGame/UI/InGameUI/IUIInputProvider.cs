using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ISMS.Presenter.Detail.UI
{
    public interface IUIInputProvider
    {
        IReadOnlyReactiveProperty<bool> SubmitPressed { get; }
        IReadOnlyReactiveProperty<bool> CancelPressed { get; }
        IReadOnlyReactiveProperty<bool> PointPressed { get; }
        IReadOnlyReactiveProperty<bool> ClickPressed { get; }
        IReadOnlyReactiveProperty<bool> RightClickPressed { get; }
        IReadOnlyReactiveProperty<bool> MiddleClickPressed { get; }
        IReadOnlyReactiveProperty<bool> AnyButtonPressed { get; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;

namespace ISMS.Presenter.Detail.UI
{
    public class UIInput : MonoBehaviour, IUIInputProvider
    {
        ReactiveProperty<bool> Submit = new ReactiveProperty<bool>();
        ReactiveProperty<bool> Cancel = new ReactiveProperty<bool>();
        ReactiveProperty<bool> Point = new ReactiveProperty<bool>();
        ReactiveProperty<bool> Click = new ReactiveProperty<bool>();
        ReactiveProperty<bool> RightClick = new ReactiveProperty<bool>();
        ReactiveProperty<bool> MiddleClick = new ReactiveProperty<bool>();
        ReactiveProperty<bool> AnyButton = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> SubmitPressed => Submit;
        public IReadOnlyReactiveProperty<bool> CancelPressed => Cancel;
        public IReadOnlyReactiveProperty<bool> PointPressed => Point;
        public IReadOnlyReactiveProperty<bool> ClickPressed => Click;
        public IReadOnlyReactiveProperty<bool> RightClickPressed => RightClick;
        public IReadOnlyReactiveProperty<bool> MiddleClickPressed => MiddleClick;
        public IReadOnlyReactiveProperty<bool> AnyButtonPressed => AnyButton;

        public void OnSubmited(InputAction.CallbackContext context)
        {

        }

        public void OnCanceled(InputAction.CallbackContext context)
        {

        }

        public void OnClicked(InputAction.CallbackContext context)
        {

        }

        public void OnRightClicked(InputAction.CallbackContext context)
        {

        }

        public void OnMiddleClicked(InputAction.CallbackContext context)
        {

        }
        public void OnAnyButtonPressed(InputAction.CallbackContext context)
        {
            if (context.started)
                AnyButton.Value = true;
            if (context.canceled)
                AnyButton.Value = false;

        }
    }
}

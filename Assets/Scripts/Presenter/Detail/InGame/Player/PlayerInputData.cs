using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

namespace ISMS.Presenter.Detail.Player
{
    public class PlayerInputData : MonoBehaviour,IInputProvider
    {
        ReactiveProperty<Vector2> _move = new ReactiveProperty<Vector2>();
        ReactiveProperty<Vector2> _look = new ReactiveProperty<Vector2>();
        ReactiveProperty<bool> _any = new BoolReactiveProperty();
        ReactiveProperty<bool> _inspect = new BoolReactiveProperty();
        ReactiveProperty<bool> _back = new BoolReactiveProperty();
        ReactiveProperty<bool> _discover = new BoolReactiveProperty();
        ReactiveProperty<bool> _action = new BoolReactiveProperty();
        ReactiveProperty<bool> _document = new BoolReactiveProperty();
        ReactiveProperty<bool> _rightPage = new BoolReactiveProperty();
        ReactiveProperty<bool> _leftPage = new BoolReactiveProperty();

        public IReadOnlyReactiveProperty<Vector2> MoveDirection => _move;
        public IReadOnlyReactiveProperty<Vector2> LookDirection => _look;
        public IReadOnlyReactiveProperty<bool> AnyButtonPush => _any;
        public IReadOnlyReactiveProperty<bool> InspectButtonPush => _inspect;
        public IReadOnlyReactiveProperty<bool> BackButtonPush => _back;
        public IReadOnlyReactiveProperty<bool> DiscoverButtonPush => _discover;
        public IReadOnlyReactiveProperty<bool> GimicActionButtonPush => _action;
        public IReadOnlyReactiveProperty<bool> DocumentButtonPush => _document;
        public IReadOnlyReactiveProperty<bool> RightPageButtonPush => _rightPage;
        public IReadOnlyReactiveProperty<bool> LeftPageButtonPush => _leftPage;

        public void OnMove(InputAction.CallbackContext context)
        {
            _move.Value = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _look.Value = context.ReadValue<Vector2>();
        }

        public void OnAny(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_any.Value == true)
            {
                _any.Value = false;
                return;
            }
            _any.Value = true;
        }

        public void OnInspect(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_inspect.Value == true) 
            {
                _inspect.Value = false;
                return;
            }
            _inspect.Value = true;
        }

        public void OnBack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_back.Value == true)
            {
                _back.Value = false;
                return;
            }
            _back.Value = true;
        }

        public void OnDiscoveRisk(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_discover.Value == true)
            {
                _discover.Value = false;
                return;
            }
            _discover.Value = true;
        }

        public void OnAction(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_action.Value == true)
            {
                _action.Value = false;
                return;
            }
            _action.Value = true;
        }

        public void OnDocument(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_document.Value == true)
            {
                _document.Value = false;
                return;
            }
            _document.Value = true;
        }

        public void OnRightPage(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _rightPage.SetValueAndForceNotify(true);
            }
            if(context.canceled)
            {
                _rightPage.Value = false;
            }
        }

        public void OnLeftPage(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _leftPage.SetValueAndForceNotify(true);
            }
            if (context.canceled)
            {
                _leftPage.Value = false;
            }
        }
    }
}

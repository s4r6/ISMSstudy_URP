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
        public IReadOnlyReactiveProperty<Vector2> MoveDirection => _move;
        public IReadOnlyReactiveProperty<Vector2> LookDirection => _look;
        public IReadOnlyReactiveProperty<bool> AnyButtonPush => _any;
        public IReadOnlyReactiveProperty<bool> InspectButtonPush => _inspect;

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<Vector2>());
            _move.Value = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<Vector2>());
            _look.Value = context.ReadValue<Vector2>();
        }

        public void OnAny(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_any.Value == true) _any.Value = false;

            _any.Value = true;
        }

        public void OnInspect(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_inspect.Value == true) _inspect.Value = false;

            _inspect.Value = true;
        }
    }
}

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
        public IReadOnlyReactiveProperty<Vector2> MoveDirection => _move;
        public IReadOnlyReactiveProperty<Vector2> LookDirection => _look;


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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;

namespace ISMS.Presenter.Detail.Player
{
    public class PlayerCore : MonoBehaviour
    {
        ReactiveProperty<PlayerState> currentPlayerState = new ReactiveProperty<PlayerState>();
        public IReadOnlyReactiveProperty<PlayerState> CurrentPlayerState => currentPlayerState;

        private void Start()
        {
            currentPlayerState.Value = PlayerState.Wait;
        }

        public void ChangeCurrentPlayerState(PlayerState nextState)
        {
            Debug.Log("ChangeTo:" + nextState);
            currentPlayerState.Value = nextState;
        }




    }
}


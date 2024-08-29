using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;

namespace ISMS.Presenter.Detail.Player
{
    public class PlayerCore : MonoBehaviour, IPlayerView
    {
        ReactiveProperty<PlayerState> currentPlayerState = new ReactiveProperty<PlayerState>(PlayerState.Loading);
        public IReadOnlyReactiveProperty<PlayerState> CurrentPlayerState => currentPlayerState;

        public void StateInitialize()
        {
            ChangeCurrentPlayerState(PlayerState.Wait);
        }

        public void ChangeCurrentPlayerState(PlayerState nextState)
        {
            Debug.Log("ChangeTo:" + nextState);
            currentPlayerState.Value = nextState;
        }




    }
}


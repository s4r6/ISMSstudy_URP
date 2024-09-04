using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;

namespace ISMS.Presenter.Detail.Player
{
    public class PlayerCore : MonoBehaviour
    {
        public int InCorrectCount { get; private set; }
        ReactiveProperty<PlayerState> currentPlayerState = new ReactiveProperty<PlayerState>(PlayerState.Loading);
        public IReadOnlyReactiveProperty<PlayerState> CurrentPlayerState => currentPlayerState;

        public void ChangeCurrentPlayerState(PlayerState nextState)
        {
            currentPlayerState.Value = nextState;
        }

        public void AddInCorrectCount()
        {
            InCorrectCount++;
            Debug.Log(InCorrectCount);
        }


    }
}


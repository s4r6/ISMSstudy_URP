using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;

namespace ISMS.Presenter.Detail.Player
{
    /// <summary>
    /// �Q�[���̃}�l�[�W���[�I����
    /// �v���C���[�̌��݂̏�Ԃ����J�A�ύX����
    /// �~�X�����񐔂�ۊ�
    /// </summary>
    public class PlayerCore : MonoBehaviour
    {
        public int InCorrectCount { get; private set; }
        ReactiveProperty<PlayerState> currentPlayerState = new ReactiveProperty<PlayerState>(PlayerState.Loading);
        public IReadOnlyReactiveProperty<PlayerState> CurrentPlayerState => currentPlayerState;
        IInputProvider _input;

        void Start()
        {
            _input = GetComponent<IInputProvider>();

            _input.DocumentButtonPush
                .Where(_ => CurrentPlayerState.Value == PlayerState.Explore)
                .Subscribe(_ =>
                {
                    ChangeCurrentPlayerState(PlayerState.Document);
                }).AddTo(this);
        }
        public void ChangeCurrentPlayerState(PlayerState nextState)
        {
            currentPlayerState.Value = nextState;
        }

        public void AddInCorrectCount()
        {
            InCorrectCount++;
        }


    }
}


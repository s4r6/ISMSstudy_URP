using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ISMS.Presenter.Detail.Player;

namespace ISMS.Presenter.Detail.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        [SerializeField]
        protected PlayerCore _player;
        protected IUIInputProvider _input;

        private void Awake()
        {
            _input = this.gameObject.GetComponentInParent<IUIInputProvider>();

        }
        protected void ChangePlayerState(PlayerState nextState)
        {
            _player.ChangeCurrentPlayerState(nextState);
        }
    }
}

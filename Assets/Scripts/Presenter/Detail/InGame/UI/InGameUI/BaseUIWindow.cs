using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ISMS.Presenter.Detail.Player;
using UniRx;
using Cysharp.Threading.Tasks;

namespace ISMS.Presenter.Detail.UI
{
    public abstract class BaseUIWindow : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _player;
        protected PlayerCore _state;
        protected IInputProvider _input;

        [SerializeField]
        protected float DisplayTime = 0.2f;    //表示までの時間

        protected abstract PlayerState myState { get; set; }

        void Start()
        {
            _state = _player.GetComponent<PlayerCore>();
            _input = _player.GetComponent<IInputProvider>();

            _state.CurrentPlayerState
                .Where(x => x == myState)
                .Subscribe(x =>
                {
                   DisplayWindow();
                }).AddTo(this);

            _input.BackButtonPush
                 .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                 .Subscribe(_ =>
                 {
                     ExitWindow();
                 }).AddTo(this);

            Initialize();
        }
        protected virtual void DisplayWindow()    //段々拡大して大きくする
        {
            this.gameObject.transform.localScale = Vector3.zero;
            this.gameObject.SetActive(true);

            this.transform.DOScale(new Vector3(1, 1, 1), DisplayTime)
                .SetEase(Ease.OutCubic);
        }

        protected virtual void ExitWindow() //段々小さくする
        {
            this.transform.DOScale(new Vector3(0, 0, 0), DisplayTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    this.gameObject.SetActive(false);
                    _state.ChangeCurrentPlayerState(PlayerState.Explore);
                });
        }

        protected abstract void Initialize();   //継承先で使用するStart関数の代わり
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ISMS.Presenter.Detail.Player;
using UniRx;
using Cysharp.Threading.Tasks;

namespace ISMS.Presenter.Detail.UI
{
    /// <summary>
    /// UIの元となるクラス
    /// 各状態に表示されるUIはこのクラスを継承する
    /// </summary>
    public abstract class BaseUIWindow : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _player;
        protected PlayerCore _state;
        protected IInputProvider _input;

        [SerializeField]
        protected float DisplayTime = 0.2f;    //表示までの時間
        protected bool InputEnable = false; //入力を受け付けるか

        protected abstract PlayerState myState { get; set; }    //自分が表示される状態を設定

        void Start()
        {
            _state = _player.GetComponent<PlayerCore>();
            _input = _player.GetComponent<IInputProvider>();

            _state.CurrentPlayerState   //状態がmyStateになったらWindow表示
                .Where(x => x == myState)
                .Subscribe(x =>
                {
                    DisplayWindow();
                }).AddTo(this);

            _input.BackButtonPush   //myState状態の時に戻る入力がされたらWindowを閉じる
                 .Where(x => x == true && _state.CurrentPlayerState.Value == myState && InputEnable)
                 .Subscribe(_ =>
                 {
                     ExitWindow();
                 }).AddTo(this);

            Initialize();
        }
        protected virtual void DisplayWindow()    //段々拡大して表示する
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

        void OnDisable()
        {
            InputEnable = false;    
        }

        protected abstract void Initialize();   //継承先で使用するStart関数の代わり
    }
}

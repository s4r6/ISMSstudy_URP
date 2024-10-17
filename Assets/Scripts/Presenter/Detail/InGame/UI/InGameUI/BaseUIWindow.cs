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
    /// UI�̌��ƂȂ�N���X
    /// �e��Ԃɕ\�������UI�͂��̃N���X���p������
    /// </summary>
    public abstract class BaseUIWindow : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _player;
        protected PlayerCore _state;
        protected IInputProvider _input;

        [SerializeField]
        protected float DisplayTime = 0.2f;    //�\���܂ł̎���
        protected bool InputEnable = false; //���͂��󂯕t���邩

        protected abstract PlayerState myState { get; set; }    //�������\��������Ԃ�ݒ�

        void Start()
        {
            _state = _player.GetComponent<PlayerCore>();
            _input = _player.GetComponent<IInputProvider>();

            _state.CurrentPlayerState   //��Ԃ�myState�ɂȂ�����Window�\��
                .Where(x => x == myState)
                .Subscribe(x =>
                {
                    DisplayWindow();
                }).AddTo(this);

            _input.BackButtonPush   //myState��Ԃ̎��ɖ߂���͂����ꂽ��Window�����
                 .Where(x => x == true && _state.CurrentPlayerState.Value == myState && InputEnable)
                 .Subscribe(_ =>
                 {
                     ExitWindow();
                 }).AddTo(this);

            Initialize();
        }
        protected virtual void DisplayWindow()    //�i�X�g�債�ĕ\������
        {
            this.gameObject.transform.localScale = Vector3.zero;
            this.gameObject.SetActive(true);

            this.transform.DOScale(new Vector3(1, 1, 1), DisplayTime)
                .SetEase(Ease.OutCubic);
        }

        protected virtual void ExitWindow() //�i�X����������
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

        protected abstract void Initialize();   //�p����Ŏg�p����Start�֐��̑���
    }
}

using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ISMS.Presenter.Detail.Player;
using UniRx;
using DG.Tweening;

namespace ISMS.Presenter.Detail.UI
{
    /// <summary>
    /// ���������\�����Ǘ�����UI
    /// </summary>
    public class DocsWindow : BaseUIWindow
    {
        protected override PlayerState myState { get; set; } = PlayerState.Document;    //�����̕\��������Ԃ�ݒ�

        [SerializeField]
        GameObject[] PageArray;
        int CurrentDisplayPageIndex = 0;    //���ݕ\������Ă���y�[�W

        protected override void Initialize()
        {
            _input.RightPageButtonPush  //�E�{�^���������ꂽ�Ƃ��ɉE�y�[�W�ֈړ�
                .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                .Subscribe(_ =>
                {
                    RightPageChange();
                }).AddTo(this);

            _input.LeftPageButtonPush   //���{�^���������ꂽ�Ƃ��ɍ��y�[�W�ֈړ�
                .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                .Subscribe(_ =>
                {
                    LeftPageChange();
                }).AddTo(this);

            for (int i = 0; i < PageArray.Length; i++)
            {
                if (i != 0)
                    PageArray[i].SetActive(false);  //1�y�[�W�ڈȊO���\����
            }

            this.gameObject.SetActive(false);
        }

        void RightPageChange()
        {
            //�y�[�W����
                PageArray[CurrentDisplayPageIndex].SetActive(false);
                if (CurrentDisplayPageIndex == PageArray.Length - 1)  //�Ō�̃y�[�W�Ȃ�
                {
                    CurrentDisplayPageIndex = 0;
                }
                else
                {
                    CurrentDisplayPageIndex++;
                }
                PageArray[CurrentDisplayPageIndex].SetActive(true);
        }

        void LeftPageChange()
        {
            //�y�[�W����
            PageArray[CurrentDisplayPageIndex].SetActive(false);
            if (CurrentDisplayPageIndex == 0)  //�ŏ��̃y�[�W�Ȃ�
            {
                CurrentDisplayPageIndex = PageArray.Length - 1;
            }
            else
            {
                CurrentDisplayPageIndex--;
            }
            PageArray[CurrentDisplayPageIndex].SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ISMS.Presenter.Detail.Player;
using UniRx;
using DG.Tweening;

namespace ISMS.Presenter.Detail.UI
{
    public class DocsWindow : BaseUIWindow
    {
        protected override PlayerState myState { get; set; } = PlayerState.Document;

        [SerializeField]
        GameObject[] PageArray;
        int CurrentDisplayPageIndex = 0;    //現在表示されているページ

        protected override void Initialize()
        {
            _input.RightPageButtonPush
                .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                .Subscribe(_ =>
                {
                    RightPageChange();
                }).AddTo(this);

            _input.LeftPageButtonPush
                .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                .Subscribe(_ =>
                {
                    LeftPageChange();
                }).AddTo(this);

            for (int i = 0; i < PageArray.Length; i++)
            {
                if (i != 0)
                    PageArray[i].SetActive(false);  //1ページ目以外を非表示に
            }

            this.gameObject.SetActive(false);
        }

        void RightPageChange()
        {
            //ページ送り
                PageArray[CurrentDisplayPageIndex].SetActive(false);
                if (CurrentDisplayPageIndex == PageArray.Length - 1)  //最後のページなら
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
            //ページ送り
            PageArray[CurrentDisplayPageIndex].SetActive(false);
            if (CurrentDisplayPageIndex == 0)  //最初のページなら
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

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
    /// 説明資料表示を管理するUI
    /// </summary>
    public class DocsWindow : BaseUIWindow
    {
        protected override PlayerState myState { get; set; } = PlayerState.Document;    //自分の表示される状態を設定

        [SerializeField]
        GameObject[] PageArray;
        int CurrentDisplayPageIndex = 0;    //現在表示されているページ

        protected override void Initialize()
        {
            _input.RightPageButtonPush  //右ボタンが押されたときに右ページへ移動
                .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                .Subscribe(_ =>
                {
                    RightPageChange();
                }).AddTo(this);

            _input.LeftPageButtonPush   //左ボタンが押されたときに左ページへ移動
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

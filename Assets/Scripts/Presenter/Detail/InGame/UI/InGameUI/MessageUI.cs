using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using TMPro;
using Cysharp.Threading.Tasks;

namespace ISMS.Presenter.Detail
{
    /// <summary>
    /// メッセージを表示するUIを管理するクラス
    /// </summary>
    public class MessageUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _messageText;
        [SerializeField]
        float FadeTime = 0.2f;
        void Start()
        {
            SystemMessage.Message   //メッセージが変更されたら表示する
                .Subscribe(x =>
                {
                    _messageText.text = x;
                    FadeMessage();
                }).AddTo(this);

            _messageText.alpha = 0f;
        }

        async void FadeMessage()    //メッセージをフェードイン・アウトする
        {
            await _messageText.DOFade(1, FadeTime);
            await _messageText.DOFade(0, FadeTime);
        }
    }
}

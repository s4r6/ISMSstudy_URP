using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UniRx;

namespace ISMS.Presenter.Detail
{
    public enum SystemCode
    {
        Locked,
        Surveyed,
        FileIOFailed
    }
    public static class SystemMessage
    {
        static ReactiveProperty<string> _messageString = new ReactiveProperty<string>(null);
        public static IReadOnlyReactiveProperty<string> Message => _messageString;
        static Dictionary<SystemCode, string> _message = new()
        {
            [SystemCode.Locked] = "鍵がかかっています。",
            [SystemCode.Surveyed] = "リスク調査済みオブジェクトです。",
            [SystemCode.FileIOFailed] = "ファイル出力できませんでした。"
        };

        public static void SetMessage(SystemCode messageCode)
        {
            _messageString.SetValueAndForceNotify(_message[messageCode]);
        }
    }
}

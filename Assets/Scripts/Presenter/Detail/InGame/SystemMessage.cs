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
            [SystemCode.Locked] = "�����������Ă��܂��B",
            [SystemCode.Surveyed] = "���X�N�����ς݃I�u�W�F�N�g�ł��B",
            [SystemCode.FileIOFailed] = "�t�@�C���o�͂ł��܂���ł����B"
        };

        public static void SetMessage(SystemCode messageCode)
        {
            _messageString.SetValueAndForceNotify(_message[messageCode]);
        }
    }
}

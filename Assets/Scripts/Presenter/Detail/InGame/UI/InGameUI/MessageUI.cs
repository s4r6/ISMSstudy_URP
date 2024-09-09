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
    /// ���b�Z�[�W��\������UI���Ǘ�����N���X
    /// </summary>
    public class MessageUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _messageText;
        [SerializeField]
        float FadeTime = 0.2f;
        void Start()
        {
            SystemMessage.Message   //���b�Z�[�W���ύX���ꂽ��\������
                .Subscribe(x =>
                {
                    _messageText.text = x;
                    FadeMessage();
                }).AddTo(this);

            _messageText.alpha = 0f;
        }

        async void FadeMessage()    //���b�Z�[�W���t�F�[�h�C���E�A�E�g����
        {
            await _messageText.DOFade(1, FadeTime);
            await _messageText.DOFade(0, FadeTime);
        }
    }
}

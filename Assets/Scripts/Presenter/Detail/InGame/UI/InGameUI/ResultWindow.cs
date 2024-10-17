using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ISMS.Presenter.Detail.Player;
using TMPro;
namespace ISMS.Presenter.Detail.UI
{
    /// <summary>
    /// ���U���gUI���Ǘ�����N���X
    /// </summary>
    public class ResultWindow : BaseUIWindow
    {

        protected override PlayerState myState { get; set; } = PlayerState.Result;
        [SerializeField]
        TextMeshProUGUI InCorrectCountText;
        [SerializeField]
        TextMeshProUGUI ClearTime;

        protected override void Initialize()
        {
            this.gameObject.SetActive(false);
        }
        void SetResultData()    //���ʕ\��
        {
            InCorrectCountText.text = _state.InCorrectCount.ToString();
        }

        protected override void DisplayWindow()
        {
            SetResultData();
            this.gameObject.SetActive(true);
        }

    }
}

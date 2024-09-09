using UnityEngine;
using DG.Tweening;
using ISMS.Presenter.Detail.Player;
using ISMS.Presenter.Detail.Stage;
using TMPro;
using UniRx;
using System;

namespace ISMS.Presenter.Detail.UI
{
    /// <summary>
    /// �I�u�W�F�N�g�̏ڍ׏���\������UI�̊Ǘ����s���N���X
    /// </summary>
    public class DetailDataWindow : BaseUIWindow
    {
        PlayerInspect _inspect;

        [SerializeField]
        TextMeshProUGUI _objNameText;
        [SerializeField]
        TextMeshProUGUI _objDescribeText;
        [SerializeField]
        TextMeshProUGUI _riskFlagText;

        protected override PlayerState myState { get; set; } = PlayerState.DetailInfo;

        protected override void Initialize()
        {
            _inspect = _player.GetComponent<PlayerInspect>();

            _input.DiscoverButtonPush   //���X�N�����{�^���������ꂽ��
                .Where(x => x == true && _state.CurrentPlayerState.Value == myState)
                .Subscribe(_ =>
                {
                    if (_inspect.PreHitObj._riskFlag == CheckFlag.NotSurvey)    //�����ς݂łȂ��I�u�W�F�N�g�Ȃ�
                    {
                        //���X�N������ԂɈړ�
                        _inspect.PreHitObj.Survey();    
                        _state.ChangeCurrentPlayerState(PlayerState.Discover);
                        this.gameObject.SetActive(false);
                    }
                    else
                        SystemMessage.SetMessage(SystemCode.Surveyed);  //�����ς݃I�u�W�F�N�g�̏ꍇ�V�X�e�����b�Z�[�W��\������
                }).AddTo(this);

            this.gameObject.SetActive(false);
        }

        protected override void DisplayWindow() //�ڍ׏���ݒ肵�Ēi�X�g�債�ĕ\��
        {
            SetDetailData();

            this.gameObject.transform.localScale = Vector3.zero;
            this.gameObject.SetActive(true);

            this.gameObject.transform.DOScale(new Vector3(1, 1, 1), DisplayTime)
                .SetEase(Ease.OutCubic);
        }
        void SetDetailData()
        {
            BaseSurveyObject ObjData = _inspect.PreHitObj;
            _objNameText.text = ObjData._name;
            _objDescribeText.text = ObjData._describe;
            switch(ObjData._riskFlag)   //���������I�u�W�F�N�g�̃��X�N�̗L����\��
            {
                case CheckFlag.NotSurvey:
                    _riskFlagText.text = "?";
                    break;

                case CheckFlag.Denger:
                    _riskFlagText.text = "�L";
                    break;

                case CheckFlag.Safe:
                    _riskFlagText.text = "��";
                    break;
            }
        }
    }
}

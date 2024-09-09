using UniRx;
using System;
using UnityEngine;
using ISMS.Presenter.Detail.Stage;

namespace ISMS.Presenter.Detail.Player
{
    /// <summary>
    /// �I�u�W�F�N�g�̏ڍׂ��擾����N���X
    /// </summary>
    public class PlayerInspect : MonoBehaviour
    {
        [SerializeField]
        GameObject _player;
        PlayerCore _state;
        IInputProvider _input;

        RaycastHit hit;     // Ray���΂��ē��������I�u�W�F�N�g�̏����i�[����ϐ�
        float rayRange = 60;
        float rayRadius = 2;
        public BaseSurveyObject PreHitObj { get; private set; } //1�t���[���O��Ray��hit���Ă����I�u�W�F�N�g
        IActionable _actionableObj;

        Camera _mainCamera;

        void Start()
        {
            _mainCamera = Camera.main;

            _state = _player.GetComponent<PlayerCore>();
            _input = _player.GetComponent<IInputProvider>();

            _input.InspectButtonPush    //�����{�^���������ꂽ���ԕύX
                .Where(_ => _state.CurrentPlayerState.Value == PlayerState.Explore)
                .Where(x => x == true && PreHitObj != null)
                .Subscribe(_ =>
                {
                    _state.ChangeCurrentPlayerState(PlayerState.DetailInfo);
                }).AddTo(this);

            _input.GimicActionButtonPush    //�A�N�V�����{�^���������ꂽ��ڂ̑O�̃I�u�W�F�N�g�ɑ΂��Đݒ肳��Ă���A�N�V�������s��
                .Where(_ => _state.CurrentPlayerState.Value == PlayerState.Explore)
                .Where(x =>x == true && _actionableObj != null)
                .Subscribe(_ =>
                {
                    _actionableObj.Action();
                }).AddTo(this);
        }

        void FixedUpdate()
        {
            //�T����Ԃ̎��ɖڂ̑O�̃I�u�W�F�N�g���擾
            if (_state.CurrentPlayerState.Value != PlayerState.Explore) return; 
            RayHitProcess();
        }

        void RayHitProcess()
        {
            var rayOrigin = _mainCamera.transform.position;

            var rayDir = _mainCamera.transform.forward;

            var isHit = Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, rayRange);
            
            if (!isHit) return;
            //Ray�̐�ɒ����\�E�A�N�V�����\�I�u�W�F�N�g���L��Ύ擾
            var SurveyObj = hit.collider.gameObject.GetComponent<BaseSurveyObject>();
            var ActionableObj = hit.collider.gameObject.GetComponent<IActionable>();
            if (SurveyObj != null || ActionableObj != null)
            {
                PreHitObj = SurveyObj;
                _actionableObj = ActionableObj;
            }
            else
            {
                PreHitObj = null;
                _actionableObj = null;
            }
        }




    }
}

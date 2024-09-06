using UniRx;
using System;
using UnityEngine;
using ISMS.Presenter.Detail.Stage;

namespace ISMS.Presenter.Detail.Player
{
    public class PlayerInspect : MonoBehaviour
    {
        [SerializeField]
        GameObject _player;
        CameraRayCast _playerCamera;
        PlayerCore _playerState;
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

            _playerCamera = _player.GetComponent<CameraRayCast>();
            _playerState = _player.GetComponent<PlayerCore>();
            _input = _player.GetComponent<IInputProvider>();

            _input.InspectButtonPush
                .Where(_ => _playerState.CurrentPlayerState.Value == PlayerState.Explore)
                .Where(x => x == true && PreHitObj != null)
                .Subscribe(_ =>
                {
                    Debug.Log(PreHitObj.name);
                    _playerState.ChangeCurrentPlayerState(PlayerState.DetailInfo);
                }).AddTo(this);

            _input.GimicActionButtonPush
                .Where(_ => _playerState.CurrentPlayerState.Value == PlayerState.Explore)
                .Where(x =>x == true && _actionableObj != null)
                .Subscribe(_ =>
                {
                    Debug.Log("Action");
                    _actionableObj.Action();
                }).AddTo(this);
        }

        void FixedUpdate()
        {
            if (_playerState.CurrentPlayerState.Value != PlayerState.Explore) return;
            RayHitProcess();
        }

        void RayHitProcess()
        {
            var rayOrigin = _mainCamera.transform.position;

            var rayDir = _mainCamera.transform.forward;

            var isHit = Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, rayRange);
            //Ray�̐�ɃI�u�W�F�N�g���L���
            if (!isHit) return;

            var SurveyObj = hit.collider.gameObject.GetComponent<BaseSurveyObject>();
            var ActionableObj = hit.collider.gameObject.GetComponent<IActionable>();
            Debug.Log(ActionableObj);
            if (SurveyObj != null || ActionableObj != null)
            {
                PreHitObj = SurveyObj;
                _actionableObj = ActionableObj;
                Debug.Log("Actionable");
            }
            else
            {
                PreHitObj = null;
                _actionableObj = null;
                Debug.Log("NoAction");
            }
        }




    }
}

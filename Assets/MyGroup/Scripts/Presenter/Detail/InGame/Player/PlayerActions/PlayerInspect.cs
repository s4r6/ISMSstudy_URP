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

        RaycastHit hit;     // Rayを飛ばして当たったオブジェクトの情報を格納する変数
        float rayRange = 60;
        float rayRadius = 2;
        public BaseSurveyObject PreHitObj { get; private set; } //1フレーム前にRayにhitしていたオブジェクト

        Camera _mainCamera;

        void Start()
        {
            _mainCamera = Camera.main;

            _playerCamera = _player.GetComponent<CameraRayCast>();
            _playerState = _player.GetComponent<PlayerCore>();
            _input = _player.GetComponent<IInputProvider>();

            _input.InspectButtonPush
                .Where(_ => _playerState.CurrentPlayerState.Value == PlayerState.Explore)
                .Where(x => x == true)
                .Subscribe(_ =>
                {
                    _playerState.ChangeCurrentPlayerState(PlayerState.DetailInfo);
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
            //Rayの先にオブジェクトが有れば
            if (!isHit) return;

            var SurveyObj = hit.collider.gameObject.GetComponent<BaseSurveyObject>();
            if (SurveyObj != null)
            {
                PreHitObj = SurveyObj;
                Debug.Log("HitSurvey");
            }
            else
            {
                Debug.Log("NoHit");
            }
        }




    }
}

using UniRx;
using System;
using UnityEngine;
using ISMS.Presenter.Detail.Stage;

namespace ISMS.Presenter.Detail.Player
{
    /// <summary>
    /// オブジェクトの詳細を取得するクラス
    /// </summary>
    public class PlayerInspect : MonoBehaviour
    {
        [SerializeField]
        GameObject _player;
        PlayerCore _state;
        IInputProvider _input;

        RaycastHit hit;     // Rayを飛ばして当たったオブジェクトの情報を格納する変数
        float rayRange = 60;
        float rayRadius = 2;
        public BaseSurveyObject PreHitObj { get; private set; } //1フレーム前にRayにhitしていたオブジェクト
        IActionable _actionableObj;

        Camera _mainCamera;

        void Start()
        {
            _mainCamera = Camera.main;

            _state = _player.GetComponent<PlayerCore>();
            _input = _player.GetComponent<IInputProvider>();

            _input.InspectButtonPush    //調査ボタンが押されたら状態変更
                .Where(_ => _state.CurrentPlayerState.Value == PlayerState.Explore)
                .Where(x => x == true && PreHitObj != null)
                .Subscribe(_ =>
                {
                    _state.ChangeCurrentPlayerState(PlayerState.DetailInfo);
                }).AddTo(this);

            _input.GimicActionButtonPush    //アクションボタンが押されたら目の前のオブジェクトに対して設定されているアクションを行う
                .Where(_ => _state.CurrentPlayerState.Value == PlayerState.Explore)
                .Where(x =>x == true && _actionableObj != null)
                .Subscribe(_ =>
                {
                    _actionableObj.Action();
                }).AddTo(this);
        }

        void FixedUpdate()
        {
            //探索状態の時に目の前のオブジェクトを取得
            if (_state.CurrentPlayerState.Value != PlayerState.Explore) return; 
            RayHitProcess();
        }

        void RayHitProcess()
        {
            var rayOrigin = _mainCamera.transform.position;

            var rayDir = _mainCamera.transform.forward;

            var isHit = Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, rayRange);
            
            if (!isHit) return;
            //Rayの先に調査可能・アクション可能オブジェクトが有れば取得
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

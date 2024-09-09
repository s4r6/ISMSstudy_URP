using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


namespace ISMS.Presenter.Detail.Player
{
    /// <summary>
    /// プレイヤーの移動、カメラ移動を行う
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField]
        float speed;
        [SerializeField]
        float turnSpeed;

        [SerializeField]
        CharacterController playerController;

        [SerializeField]
        Transform WebCameraSet;

        PlayerCore playerCore;

        Vector3 MoveDirection;

        public float vertiRotate = 0.0f;
        float MinLimit = -50.0f;    //縦回転の↑方向の制限
        float MaxLimit = 70.0f;     //縦回転↓方向の制限

        Vector2 LookDir;

        bool isMovable = true;

        IInputProvider _input;

        void Awake()
        {
            _input = GetComponent<IInputProvider>();
        }
        void Start()
        {
            playerCore = this.gameObject.GetComponent<PlayerCore>();

            _input.MoveDirection
                .Subscribe(dir => MoveProcess(dir))
                .AddTo(this);

            _input.LookDirection
                .Subscribe(dir => LookDir = dir)
                .AddTo(this);

            //探索状態以外では移動不可能に設定
            playerCore.CurrentPlayerState
                .Subscribe(x =>
                {
                    if (x == PlayerState.Explore)
                    {
                        isMovable = true;
                    }
                    else
                    {
                        isMovable = false;
                    }
                }).AddTo(this);
        }

        void FixedUpdate()
        {
            //移動可能状態であれば移動,カメラ回転
            if (!isMovable) return;
            playerController.Move(MoveDirection * speed * Time.deltaTime);
            CameraRotate();
        }

        void CameraRotate()
        {
            float turn = LookDir.x * turnSpeed * Time.deltaTime;
            this.gameObject.transform.Rotate(0, turn, 0);
            vertiRotate += -LookDir.y * turnSpeed * Time.deltaTime;
            vertiRotate = Mathf.Clamp(vertiRotate, MinLimit, MaxLimit);
            WebCameraSet.localEulerAngles = new Vector3(vertiRotate, 0.0f, 0.0f);
        }

        void MoveProcess(Vector2 MoveDir)
        {
            MoveDirection = new Vector3(MoveDir.x, 0.0f, MoveDir.y);
            MoveDirection = WebCameraSet.TransformDirection(MoveDirection);
            MoveDirection.y = 0f;
        }




    }
}

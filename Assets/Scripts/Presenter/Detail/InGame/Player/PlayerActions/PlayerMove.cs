using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


namespace ISMS.Presenter.Detail.Player
{
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
        float MinLimit = -50.0f;    //ècâÒì]ÇÃÅ™ï˚å¸ÇÃêßå¿
        float MaxLimit = 70.0f;     //ècâÒì]Å´ï˚å¸ÇÃêßå¿

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

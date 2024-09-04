using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Presenter.Detail.Stage;
using UniRx;
using System;

namespace ISMS.Presenter.Detail.Player
{
    public class CameraRayCast : MonoBehaviour
    {
        RaycastHit hit;     // Ray���΂��ē��������I�u�W�F�N�g�̏����i�[����ϐ�
        //Vector3 rayOrigin, rayDir;
        float rayRange = 60;
        float rayRadius = 2;

        [SerializeField]
        PlayerCore _player;

        [SerializeField]
        GameObject SurveyIcon;

        public BaseSurveyObject PreHitObj { get; private set; }

        void FixedUpdate()
        {
            if (_player.CurrentPlayerState.Value != PlayerState.Explore) return;
            RayHitProcess();
        }

        void RayHitProcess()
        {
            var rayOrigin = Camera.main.transform.position;

            var rayDir = Camera.main.transform.forward;

            var isHit = Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, rayRange);
            //Ray�̐�ɃI�u�W�F�N�g���L���
            if (!isHit) return;

            var SurveyObj = hit.collider.gameObject.GetComponent<BaseSurveyObject>();
            if (SurveyObj != null)
            {
                PreHitObj = SurveyObj;
                Debug.Log("HitSurvey");
            }
            else
            {

                SurveyIcon.SetActive(false);
                Debug.Log("NoHit");
            }
        }
    }
}

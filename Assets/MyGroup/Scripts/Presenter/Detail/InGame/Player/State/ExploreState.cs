using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerState
{
    public class ExploreState : PlayerStateBase
    {
        public override void OnUpdate(PlayerState owner)
        {
            Debug.Log("�T�����[�h");
            PlayerMove.instance.Move(); //�ړ�����

            //�J�������Z�b�g�̃{�^���������ꂽ��
            if (GameInput.instance._CameraResetAction.triggered == true)
            {
                Debug.Log("�J�������Z�b�g");
                PlayerMove.instance.ResetCamera();  //�J�������Z�b�g
            }

            //�}�j���A���\���{�^���������ꂽ��
            if (GameInput.instance._R1Action.triggered == true)
            {
                Debug.Log("�h�L�������g�\��");
                owner.ChangeState(Documentstate);
            }

            if (GameInput.instance._DecisionAction.triggered == true)
            {
                Debug.Log("����{�^������");
                //���E�ɒ��ׂ���I�u�W�F�N�g�����鎞�A�ڍ׏���\������t�F�[�Y�Ɉڍs�B
                if (CameraRayCast.instance.GetHitObjectName() != "none")
                {
                    Debug.Log("�ڍו\��");
                    owner.ChangeState(DetailInfostate);
                }
            }

            if (GameInput.instance._GimicAction.triggered == true)
            {
                string gimicName = CameraRayCast.instance.GetHitGimicName();

                //���E�ɃM�~�b�N�𔭓��ł���I�u�W�F�N�g�����鎞
                if (gimicName != "none")
                {
                    //�Ώۂ̃M�~�b�N�I�u�W�F��T���āA�����̎��A�N�V���������s����
                    GameObject.Find(gimicName).GetComponent<GimicObjBase>().Action();
                }
            }

            // �A�[�J�C�u��ʂɈڍs
            if (GameInput.instance._ArchivesAction.triggered == true)
            {
                ArchivesManager.instance.InfoOn();
                owner.ChangeState(Archivesstate);
            }
        }
    }
}

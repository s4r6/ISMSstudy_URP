using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRKaiGimicIcon : MonoBehaviour
{
    private Transform _cashIconTransform;

    private Vector3 startScale;     // Unity��Ŏ��O�ɒ��������\���ʒu��ۑ�������

    private Vector3 hitObjPosition;

    private string hitGimicName;

    [SerializeField]
    private Transform UICanvas = null;

    // Start is called before the first frame update
    void Start()
    {

        _cashIconTransform = this.transform;

        startScale = _cashIconTransform.localScale;  //Unity��Œ��������傫����ۑ�����B

        hitGimicName = "none";

    }

    // Update is called once per frame
    void Update()
    {
        bool activeFlag = PlayerMove.instance.GetIconFlag();
        if (activeFlag == true)
        {
            //�v���C���[�̎��E����ɂԂ������M�~�b�N�I�u�W�F�N�g�̖��O�𓾂�
            hitGimicName = CameraRayCast.instance.GetHitGimicName();


            //���E�ɒ����ΏۃI�u�W�F�N�g������("none"�ł͂Ȃ�)�ꍇ�A
            //�A�C�R���̕\���ʒu���v�Z�A���g�̕\�����A�N�e�B�u�ɂ���B
            if (hitGimicName != "none")//
            {
                //�����ΏۃI�u�W�F�N�g�̃J������̈ʒu���v�Z

                /*  �I�u�W�F�N�g�̒��S�ƍ����Ōv�Z����p�^�[��***/
                
                //�I�u�W�F�N�g�̍��W�E�������擾
                hitObjPosition = CameraRayCast.instance.GetHitGimicPosition();
                float objSizeY = CameraRayCast.instance.GetGimicSizeY();

                //���W���v�Z�����
                Vector3 pos = new Vector3(hitObjPosition.x, hitObjPosition.y + objSizeY / 2, hitObjPosition.z);
                _cashIconTransform.position = pos;

                //���ʂ�UI�Ɠ����p�x�ɂ���B
                _cashIconTransform.eulerAngles = UICanvas.eulerAngles;


                //�\�����A�N�e�B�u�ɂ���
                OnWindow();
            }
            //�f���Ă��Ȃ����͕\�����B��
            else
            {
                //myRectTfm.position = startPosition;
                OffWindow();
            }

        }
        else
        {
            OffWindow();
        }

    }


    // �\���̐؂�ւ���������B
    private void OnWindow()
    {
        _cashIconTransform.localScale = startScale;
    }

    private void OffWindow()
    {
        _cashIconTransform.localScale = Vector3.zero;
    }
}

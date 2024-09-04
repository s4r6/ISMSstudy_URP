using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRKaiSurvey : MonoBehaviour
{
    private Transform _cashIconTransform;

    private Vector3 startScale;     // Unity��Ŏ��O�ɒ��������\���ʒu��ۑ�������

    private Vector3 hitObjPosition;

    private string hitObjName = "none";

    
    [SerializeField]
    private Transform UICanvas = null;


    /*** Unity���Ŏ����ŌĂ΂��֐��Q ******************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        _cashIconTransform = this.transform;  //�A�^�b�`���ꂽ�I�u�W�F�N�g��transform���L���b�V��

        startScale = _cashIconTransform.localScale;          //Unity��Œ��������傫����ۑ�����B

        hitObjName = "none";
    }



    // Update is called once per frame
    void Update()
    {
        bool activeFlag = PlayerMove.instance.GetIconFlag();
        if (activeFlag == true)
        {
            /*** �v���C���[�̎��E����ɒ����I�u�W�F�N�g������ꍇ�A�I�u�W�F�N�g�̏�ɃA�C�R����\������ ***/

            //�v���C���[�̎��E����ɂԂ������I�u�W�F�N�g�̖��O�𓾂�
            hitObjName = CameraRayCast.instance.GetHitObjectName();


            //���E�ɒ����ΏۃI�u�W�F�N�g������("none"�ł͂Ȃ�)�ꍇ�A���W�v�Z�ƕ\���B
            if (hitObjName != "none")//
            {
                //�����ΏۃI�u�W�F�N�g�̃J������̈ʒu���v�Z

                //���W���擾�BGameObject.Find�̏������d���炵���̂ŕs�̗p
                //hitObjPosition = GameObject.Find(hitObjName).transform.position;  

                //�I�u�W�F�N�g�̍��W�ƍ������擾
                hitObjPosition = CameraRayCast.instance.GetRayHitObjPosition();
                float objSizeY = CameraRayCast.instance.GetRaycastHitObjSizeY();

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

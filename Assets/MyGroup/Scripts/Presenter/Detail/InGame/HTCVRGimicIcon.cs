using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTCVRGimicIcon : MonoBehaviour
{
    /*private Transform _cashIconTransform;

    private Vector3 startScale;     // Unity��Ŏ��O�ɒ��������\���ʒu��ۑ�������
    
    private Vector3 hitObjPosition;

    private string hitGimicName = "none";

    [SerializeField] private Transform VRcanvas;

    //�Ȃ���null�̒l���g�������������Ɠ��삷��̂�null�ɂ��Ă�B
    private Camera _camera = null;

    // Start is called before the first frame update
    void Start()
    {


        _cashIconTransform = this.transform;

        startScale = _cashIconTransform.localScale;          //Unity��Œ��������傫����ۑ�����B

        hitGimicName = "none";
      

    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̎��E����ɂԂ������M�~�b�N�I�u�W�F�N�g�̖��O�𓾂�
        //hitGimicName = CameraRayCast.instance.GetHitGimicName();


        //���E�ɒ����ΏۃI�u�W�F�N�g������("none"�ł͂Ȃ�)�ꍇ�A
        //�A�C�R���̕\���ʒu���v�Z�A���g�̕\�����A�N�e�B�u�ɂ���B
        if (hitGimicName != "none")//
        {
            //�����ΏۃI�u�W�F�N�g�̃J������̈ʒu���v�Z

            /*  �I�u�W�F�N�g�̒��S�ƍ����Ōv�Z����p�^�[��***/
            //hitObjPosition = GameObject.Find(hitObjName).transform.position;  //���W���擾�BGameObject.Find�̏������d���炵���̂ŕs�̗p�ɂ���

            //�I�u�W�F�N�g�̍��W�E�������擾
            /*hitObjPosition = CameraRayCast.instance.GetHitGimicPosition();     
            float objSizeY = CameraRayCast.instance.GetGimicSizeY();
            */
            ////���W���v�Z�����
            //Vector3 pos = new Vector3(hitObjPosition.x, hitObjPosition.y + objSizeY / 2, hitObjPosition.z);

            //Vector3 y = new Vector3(0, 0.5f, 0);

            
            /*Vector3 rectPos = hitObjPosition + y * objSizeY;
            Vector3 pos = new Vector3(rectPos.x, rectPos.y,VRcanvas.position.z);

            _cashIconTransform.localPosition = pos;

            _cashIconTransform.localScale = startScale* ScaleCalibration();

            //�\�����A�N�e�B�u�ɂ���
            ActiveDisplay();


        }
        //�f���Ă��Ȃ����͕\�����B��
        else
        {
            //myRectTfm.position = startPosition;
            InActiveDisplay();
        }


    }


    // �\���̐؂�ւ���������B
    private void ActiveDisplay()
    {

        //_cashIconTransform.localScale = startScale * ScaleCalibration();
        //_cashIconTransform.localScale = startScale;

    }

    private void InActiveDisplay()
    {
        _cashIconTransform.localScale = Vector3.zero;
    }

    private float ScaleCalibration()
    {

        float Amax = CameraRayCast.instance.rayRange;
        float Amin = 0.0f;
        float Bmax = 0.5f;
        float Bmin = 1.3f;

        float nowA = CameraRayCast.instance.GetRayHitDistance();

        float calibration = Bmin + (Bmax - Bmin) * (nowA - Amin) / (Amax - Amin);


        return calibration;

        ////float calibration =  1.5f * CameraRayCast.instance.GetRayHitDistance()/ CameraRayCast.instance.rayRange;

        //myRectTfm.localScale = startScale * calibration;
    }*/
}

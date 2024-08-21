using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTCVRSurveyIcon : MonoBehaviour
{

    private Transform _cashIconTransform;

    private Vector3 startScale;     // Unity��Ŏ��O�ɒ��������\���ʒu��ۑ�������

    private Vector3 hitObjPosition;

    private string hitObjName = "none";

    //�Ȃ���null�̒l���g�������������Ɠ��삷��̂�null�ɂ��Ă�B
    [SerializeField]
    private Camera _cameraMain = null;

    [SerializeField]
    private Camera _cameraL = null;

    [SerializeField] private Transform VRcanvas;
   

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

            Vector3 yVec = new Vector3(0, 0.5f, 0);
            //Vector3 objPos = hitObjPosition + objSizeY * yVec;


            //Vector2 rectPos = RectTransformUtility.WorldToScreenPoint(_cameraMain, hitObjPosition + y * objSizeY);
            Vector3 rectPos = hitObjPosition + yVec * objSizeY;
            Vector3 pos = new Vector3(rectPos.x, rectPos.y, VRcanvas.position.z);
            _cashIconTransform.localPosition = pos;

            

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


    // �\���̐؂�ւ���������B
    private void OnWindow()
    {
        _cashIconTransform.localScale = startScale* ScaleCalibration();
    }

    private void OffWindow()
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
    }



}

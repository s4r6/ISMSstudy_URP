using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRayCast : MonoBehaviour
{

    public static CameraRayCast instance;

    [SerializeField]
    private Camera _cameraMain = null;

    [SerializeField]
    private Transform _cameraFollower = null;

    [SerializeField]
    private LayerMask layerMask;

   

    private bool modeVR = false;


    private float RayHitDistance;

    private string hitObjName = "none";
    private string hitGimicName = "none";

    private float hitObjSizeY;


    private Vector3 rootTfmPosition;


    private Vector3 hitGimicPosition;
    private float gimicSizeY;

    /*** Unity���Ŏ����ŌĂ΂��֐��Q ******************************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        /*if (instance == null)
        {
            instance = this;
        }*/


    }

    // Update is called once per frame
    void Update()
    {
        //VR���[�h���ǂ������ׂ鏈���B
        if (_cameraMain.enabled == true)
        {
            modeVR = false;
        }
        else
        {
            modeVR = true;
        }

        //�v���C���[�̑����烌�C�L���X�g�̋�������Γ����蔻��̏���������B
        //bool rayCastFlag = PlayerMove.instance.GetRayCastFlag();
        /*if (rayCastFlag == true)
        {
          
            RayHitProcess(_cameraFollower);
        }*/

    }

    RaycastHit hit;     // Ray���΂��ē��������I�u�W�F�N�g�̏����i�[����ϐ�
    Vector3 rayOrigin, rayDir;
    public float rayRange = 60;
    private int rayHitID;
    public float rayRadius = 2;

    // Ray���΂��ăI�u�W�F�N�g�Ƃ̏Փ˂𒲂ׂ鏈��
    void RayHitProcess(Transform CameraTransform)
    {
        // Ray�̌��_���J�����̈ʒu��(local���Ƌ��������������Ȃ�̂Œ���)
        rayOrigin = CameraTransform.position;

        // Ray�𔭎˂���������J�����������Ă�������ɐݒ�
        rayDir = CameraTransform.forward;

        //���C���΂�
       
        // ���`��Ray���΂��A��������̃I�u�W�F�N�g�ɓ����邩����B
        if (Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, rayRange, layerMask) == true)
        {
            //���݂̃��C�����������I�u�W�F�N�g�̃C���X�^���XID���擾�B
            int nowHitObjID = hit.transform.GetInstanceID();

            //�O���[�v�ƈႤ�I�u�W�F�N�g�ɓ������Ă���ꍇ,�^�O����������B
            if (rayHitID != nowHitObjID)
            {
                rayHitID = nowHitObjID;


                string hitTag = hit.collider.gameObject.tag;
                //Debug.Log(hitTag);

                // ���������I�u�W�F�N�g�̃^�O�ŕ��򏈗�
                switch (hitTag)
                {

                    case "RootObj":
                        /* ���ׂ�A�C�R���ɕK�v�ȏ����擾���� ********************************/

                        //���[�g�ɓ��������ꍇ�́A�����̏�񂩂� ���O�E���W�E�������擾
                        hitObjName = hit.transform.name;

                        rootTfmPosition = hit.transform.position;

                        BoxCollider bc = hit.transform.gameObject.GetComponent<BoxCollider>();

                        hitObjSizeY = bc.bounds.size.y; // �����蔻��̑傫�����擾

                        //�M�~�b�N�I�u�W�F�ł͂Ȃ��Ɛݒ�
                        hitGimicName = "none";
                        break;

                    case "SurveyObj":

                        /* ���ׂ�A�C�R���ɕK�v�ȏ����擾���� ********************************/
                        RootInfoGetProcess();

                        //�M�~�b�N�I�u�W�F�ł͂Ȃ��Ɛݒ�
                        hitGimicName = "none";

                        break;

                    case "ChildSurveyObj":

                        RootInfoGetProcess();

                        //�M�~�b�N�I�u�W�F�ł͂Ȃ��Ɛݒ�
                        hitGimicName = "none";
                        break;

                    case "GimicSurveyObj":
                        /* ���ׂ�A�C�R���ɕK�v�ȏ����擾���� **********************************************/

                        RootInfoGetProcess();

                        /* �M�~�b�N�A�C�R���p�̍��W���� *****************************************************/

                        //�M�~�b�N�I�u�W�F�̖��O�E���W�E�������擾����B
                        hitGimicName = hit.transform.name;
                        hitGimicPosition = hit.transform.position;

                        bc = hit.transform.gameObject.GetComponent<BoxCollider>();
                        gimicSizeY = bc.bounds.size.y;
                        break;


                    case "ChildGimicSurveyObj":

                        /* ���ׂ�A�C�R���ɕK�v�ȏ����擾���� **********************************************/
                        RootInfoGetProcess();

                        /*�M�~�b�N�A�C�R���p�̍��W���� * ****************************************************/
                        //�q���I�u�W�F�N�g�̏ꍇ�e(1��̃I�u�W�F�N�g)�̏����i�[����
                        Transform parent = hit.transform.parent;
                        hitGimicName = parent.name;
                        hitGimicPosition = parent.position;

                        bc = hit.transform.gameObject.GetComponent<BoxCollider>();
                        gimicSizeY = bc.bounds.size.y;          //�M�~�b�N�I�u�W�F�N�g�̍������擾�B
                        break;

                    default:
                        //�ǂȂǂɓ��������ꍇ�A�����Ȃ��Ƃ���B
                        UnSurveyProcess();
                        break;
                }

            }

            //�O���[�v�̃��C�Ɠ����I�u�W�F�N�g�ɓ������Ă���ꍇ�A�A�C�R���̕\���ʒu�Ȃǂ̏��������Ȃ�
            else
            {
               
            }

        }
        else
        {// �������ĂȂ���Ή����Ȃ�
            UnSurveyProcess();

            rayHitID = 0;
        }


    }


    //���C�����������I�u�W�F�N�g��Root���璲�ׂ�A�C�R���ɕK�v�ȏ����擾���鏈��
    void RootInfoGetProcess()
    {
        Transform thisRoot = hit.transform.root;

        //���������I�u�W�F�N�g�̖��O�E���W�E�������i�[����
        hitObjName = thisRoot.name;

        rootTfmPosition = thisRoot.position;  // ���������I�u�W�F�N�g�̈�ԏ�̐e�̍��W���i�[����

        BoxCollider bc = thisRoot.gameObject.GetComponent<BoxCollider>();   // Root�̓����蔻��i���������擾�j

        //BoxCollider bc = thisRoot.

        hitObjSizeY = bc.bounds.size.y; // �����蔻��̑傫�����擾

        //hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;

    }


    //���ׂ�I�u�W�F�N�g����Ȃ����̏����Z��
    void UnSurveyProcess()
    {
        hitObjName = "none";
        hitGimicName = "none";   //�M�~�b�N�ł͂Ȃ�����
        hitObjSizeY = 0;

    }


    // �f�o�b�O�p�̃��c
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;//
        Gizmos.DrawSphere(rayOrigin + rayDir * rayRange, rayRadius);
        Gizmos.DrawRay(rayOrigin, rayDir * rayRange);
        //Gizmos.DrawCube(rayOrigin + rayDir * rayRange, boxCastSize);
    }



    /*** �Q�b�^�[(���̊֐�����Ăяo���āA�l���擾������) **************************************************************************/

    // Ray���������Ă���I�u�W�F�N�g�̖��O��Ԃ��B 
    public string GetHitObjectName()
    {
        return hitObjName;
    }

    public bool GetModeVR()
    {
        return modeVR;

    }

    //Ray�����������I�u�W�F�N�g��Y�������̑傫����Ԃ��B
    public float GetRaycastHitObjSizeY()
    {
        return hitObjSizeY;
    }

    public Vector3 GetRayHitObjPosition()
    {
        return rootTfmPosition;
    }

    public float GetRayHitDistance()
    {
        return hit.distance;
    }


    /*** �M�~�b�N�n�̃Q�b�^�[ *******************************/
    // Ray���������Ă���M�~�b�N�t���I�u�W�F�̖��O��Ԃ��B
    public string GetHitGimicName()
    {
        return hitGimicName;
    }

    public Vector3 GetHitGimicPosition()
    {
        return hitGimicPosition;
    }

    public float GetGimicSizeY()
    {
        return gimicSizeY;
    }

}

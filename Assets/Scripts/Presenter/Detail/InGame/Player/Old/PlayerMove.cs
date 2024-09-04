using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UniRx;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    /*** 3D�I�u�W�F�N�g�𐧌䂷��ϐ� *******************************************************************/

    public CharacterController _cachController; //�L�����N�^�[�̓����蔻��L���b�V��
    public Transform _cashPlayerTransform;     //�v���C���[�̃I�u�W�F�N�g�̃L���b�V��

    [SerializeField]
    public Camera CameraMain = null;

    [SerializeField]
    public Transform CameraL = null;

    [SerializeField]
    public Transform CameraR = null;

    [SerializeField]
    public Transform CameraFollower = null;

    [SerializeField]
    public Transform WebCameraSet = null;

    [SerializeField]
    public Transform DebugWindow = null;


    /*** ���͎擾�ϐ� **********************************************************************************/

    /* �ȉ��@InputSystem�p�̕ϐ� ***/
    /*public InputAction _MoveAction, _LookAction, _DecisionAction,
        _BackAction, _RiskAction, _GimicAction, _ArchivesAction,
        _JujiKeyAction, _AnyAction, _CameraResetAction;

    public InputAction _L2Action, _R2Action, _R1Action, _DebugMode;
    public Vector2 moveInput, lookInput, JujiKeyInput;

    public float rightButtonframe;  //�\���L�[�̉E��������Ă���t���[����
    public float leftButtonframe;  //�\���L�[����������Ă���t���[����

    public bool debugModeFlag = false;
    */
    /*** �v���C���[�̃X�e�[�^�X�ϐ� ********************************************************************/

    [SerializeField] public float speed = (float)70;
    [SerializeField] private float turnSpeed = (float)120;


    public float PlayerStartY;  //�v���C���[��Y���W�̏�����ۑ��B�d�͂̑���

    public Vector3 moveDirection = Vector3.zero;//�L�����������Ă������

    //public DateTime startTime, endTime;

    //�t�F�[�Y�̒�`
    public enum Phase
    {
        NONE,
        WAIT,
        EXPLORE,
        DETAILINFO,
        DISCOVER,
        ARCHIVES,
        RESULT,
        DOCUMENT
    }
    public Phase nowPhase, nextPhase;

    public bool rayCastFlag;   //�J�����̓����蔻����s�����ǂ����̃t���O
    public bool IconFlag;      //�A�C�R����\�����邩�̃t���O
    public UDPConnection myUdpConnection;




    /*** Unity���Ŏ����ŌĂ΂��֐��Q ******************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerMove Start");
        if (instance == null)
        {
            instance = this;
        }
        PlayerStartY = this.transform.position.y;
        
        _cachController = GetComponent<CharacterController>();

        _cashPlayerTransform = this.transform;

        //InputInit();

        PhaseChangeProcess(Phase.WAIT);

        myUdpConnection = GameObject.Find("udpClient").GetComponent<UDPConnection>();

        /*myUdpConnection.OnJudgeFlagChanged
            .ObserveOnMainThread()
            .Where(value => value == true)
            .Subscribe(_ => JudgeEnd())
            .AddTo(this);*/

        Debug.Log("PlayerMove End");
    }

    // Update is called once per frame
    void Update()
    {
        //InputUpdate();
        rayCastFlag = false;
        IconFlag = false;

        
        //�e�t�F�[�Y���̏���
        /*switch (nowPhase)
        {
            case Phase.WAIT:
                if (_AnyAction.triggered == true && WaitPhaseScripts.instance.GetInputReception() == true)
                {
                    WaitPhaseScripts.instance.PushAny();
                }

                if (WaitPhaseScripts.instance.GetNextPhase() == true)
                {

                    startTime = DateTime.Now;   //���ݎ�����ۑ�

                    WaitPhaseScripts.instance.StopWaitPhaseWindow();  //waitPhase�̃I�u�W�F�N�g���~����

                    PhaseChangeProcess(Phase.EXPLORE);      //�T���t�F�[�Y�Ɉڍs

                }


                break;

            case Phase.EXPLORE:*/ /*** �T�����Ă����� *****/

                /*rayCastFlag = true;
                IconFlag = true;

                // �v���C���[�̉�]����
                PlayerRotate();

                //�v���C���[�̈ړ�����
                MoveProcess();

                //�J�������Z�b�g�̃{�^���������ꂽ��
                if (_CameraResetAction.triggered == true)
                {
                    WebCameraSet.localEulerAngles = Vector3.zero;

                    CameraMain.transform.localEulerAngles = Vector3.zero;

                    CameraL.localEulerAngles = Vector3.zero;

                    CameraR.localEulerAngles = Vector3.zero;

                    vertiRotate = 0;

                }

                if (_R1Action.triggered == true)
                {
                    DocsWindow.instance.InfoOn();
                    PhaseChangeProcess(Phase.DOCUMENT);


                }

                //�T������
                SearchPhaseProcess();

                break;

            case Phase.DETAILINFO:*/ /*** �����{�����Ă����� *****/

                /*DetailBrowsePhaseProcess();
                break;

            case Phase.DISCOVER://�����t�F�[�Y

                if (RiskDiscover.instance.GetInputReception() == true)
                {

                    DiscoverPhaseProcess();
                }

                break;


            case Phase.ARCHIVES://�A�[�J�C�u�{���t�F�[�Y�B


                ArchivesPhaseProcess();




                break;

            case Phase.RESULT://���U���g�t�F�[�Y

                if (ResultScript.instance.GetInputReception() == true)
                {
                    if (_DecisionAction.triggered == true)
                    {
                        //SceneManager.LoadScene("Title");
                    }
                }
                break;

            case Phase.DOCUMENT:

                if (DocsWindow.instance.GetInputReception() == true)
                {
                    if (_BackAction.triggered == true)
                    {
                        DocsWindow.instance.InfoOff();
                    }*/

                    /* ���X�e�B�b�N���\���L�[�̍��E�Ńy�[�W����B****************/

                    /*if (0 < JujiKeyInput.x || 0 < moveInput.x)
                    {
                        rightButtonframe++;
                        if (rightButtonframe % 60 == 1)//�������ɂ��Ή��ł���悤�Ȋ����ŁB1�b��1�񓮂��B
                        {
                            DocsWindow.instance.PageChange(true);

                            AudioManager.instance.playSE(9);
                        }

                    }
                    else if (JujiKeyInput.x < 0 || moveInput.x < 0)
                    {
                        leftButtonframe++;
                        if (leftButtonframe % 60 == 1)
                        {
                            DocsWindow.instance.PageChange(false);

                            AudioManager.instance.playSE(9);
                        }
                    }
                    else
                    {
                        rightButtonframe = 0; leftButtonframe = 0;
                    }



                }
                //DocsWindow����~���Ă��ăt�F�[�Y�ύX�����o�Ă���ΒT���t�F�[�Y�ɖ߂�B
                if (DocsWindow.instance.GetPhaseChange() == true && DocsWindow.instance.GetActiveFlag() == false)
                {
                    PhaseChangeProcess(Phase.EXPLORE);
                }


                break;

            default:
                break;

        }*/
    }


    /* ����̊֐��Q ************************************************************************************/

    /***** �t�F�[�Y�̏���  ********************************************/

    // �T���t�F�[�Y�̏����܂Ƃ�
    /*public void SearchPhaseProcess()
    {
        // ����{�^���������ꂽ��
        if (GameInput.instance._DecisionAction.triggered == true)
        {
            Debug.Log("����{�^������");
            //���E�ɒ��ׂ���I�u�W�F�N�g�����鎞�A�ڍ׏���\������t�F�[�Y�Ɉڍs�B
            if (CameraRayCast.instance.GetHitObjectName() != "none")
            {
                Debug.Log("�ڍו\��");
                DetailInfoWindow.instance.OnWindow(); //�I�u�W�F�N�g�̏ڍ׏��E�B���h�E���A�N�e�B�u�ɂ���

                PhaseChangeProcess(Phase.DETAILINFO);
                AudioManager.instance.playSE(8);


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

            PhaseChangeProcess(Phase.ARCHIVES);


        }
    }*/

    // �ڍ׏��t�F�[�Y�̏����܂Ƃ�
    public void DetailBrowsePhaseProcess()
    {
        // �L�����Z���{�^���������ꂽ��A�T���t�F�[�Y�ɖ߂�B
        /*if (GameInput.instance._BackAction.triggered == true)
        {
            AudioManager.instance.playSE(0);

            DetailInfoWindow.instance.OffWindow();

            PhaseChangeProcess(Phase.EXPLORE);

        }*/

        // L1�{�^���������ꂽ��A���\������Ă�������A�[�J�C�u�ɒǉ�����B
        /*if (GameInput.instance._ArchivesAction.triggered == true)
        {
            // �ڍ׏��E�B���h�E�ɕ\������Ă�������A�[�J�C�u�ɒǉ�����
            // �ǉ�������true ���łɒǉ�����Ă���ꍇ��false
            bool addFlag = ArchivesManager.instance.AddArchives(DetailInfoWindow.instance.GetNowData());
            if (addFlag == true)
            {
                MessageManager.instance.SetMessage("�A�[�J�C�u�ɒǉ����܂���");
                AudioManager.instance.playSE(9);
            }
            else
            {
                MessageManager.instance.SetMessage("���łɒǉ�����Ă��܂�");
                AudioManager.instance.playSE(10);
            }

        }*/


        //���X�N�����R�}���h
        /*if (GameInput.instance._RiskAction.triggered == true)
        {

            // ���X�N������ʂ̃v���O�����̕��Ɍ��݂̃f�[�^�𑗂�B
            // ���łɃ��X�N��������Ă���I�u�W�F�̏ꍇ��false���Ԃ��Ă���
            bool phaseChangeFlag = RiskDiscover.instance.DiscoverJudge(DetailInfoWindow.instance.GetNowData());

            //�܂����X�N��������Ă��Ȃ��I�u�W�F�N�g�̏ꍇ
            if (phaseChangeFlag == true)
            {
                // �ڍ׏��E�B���h�E���I�t
                DetailInfoWindow.instance.OffWindow();

                // ���X�N������ʂ��I��
                RiskDiscover.instance.InfoOn();

                // ���X�N�����t�F�[�Y�ֈڍs
                PhaseChangeProcess(Phase.DISCOVER);


            }
            //���łɔ����R�}���h��K�p����Ă���ꍇ
            else
            {
                MessageManager.instance.SetMessage("���X�N�����ς݂̃I�u�W�F�N�g�ł��B");
                AudioManager.instance.playSE(10);
            }

        }
        */
    }

    //���X�N�����R�}���h�̏���
    public void DiscoverPhaseProcess()
    {
        /*Manual.instance.WindowOff();
        Manual.instance.BackCommandActive();


        //if (Input.GetButtonDown("CancelButton"))
        if (GameInput.instance._BackAction.triggered == true)
        {
            myUdpConnection.SendByte(prefix: "_Fn", msg: "Pls_Judge");

            AudioManager.instance.playSE(0);
            RiskDiscover.instance.InfoOff();

            //�S�Ẵ��X�N�𔭌��������ǂ�������
            if (RiskDiscover.instance.GetCorrectCount() == StageData.GetRiskNum())
            {
                //�I�����Ԃ��擾���A�N���A�܂ł̃^�C�����v�Z���đ���
                //endTime = DateTime.Now;
                //ResultScript.instance.SetLapsedTime(endTime - startTime);
                ResultScript.instance.InfoOn();
                ArchivesManager.instance.ArchivesFileSave();

                //���U���g�Ɉڍs
                PhaseChangeProcess(Phase.RESULT);


            }
            else
            {
                //�T���Ɉڍs
                PhaseChangeProcess(Phase.EXPLORE);

            }
        }*/
    }

    /*public void JudgeEnd()
    {
        //�S�Ẵ��X�N�𔭌��������ǂ�������
        if (RiskDiscover.instance.GetCorrectCount() == StageData.GetRiskNum())
        {
            //�I�����Ԃ��擾���A�N���A�܂ł̃^�C�����v�Z���đ���
            //endTime = DateTime.Now;
            //ResultScript.instance.SetLapsedTime(endTime - startTime);
            ResultScript.instance.InfoOn();
            ArchivesManager.instance.ArchivesFileSave();

            //���U���g�Ɉڍs
            PhaseChangeProcess(Phase.RESULT);
        }
        myUdpConnection.ResetJudgeFlag();
    }*/

    // �A�[�J�C�u�{���t�F�[�Y�̏����܂Ƃ�
    public void ArchivesPhaseProcess()
    {
        // ���͂��󂯕t���鎞
        if (ArchivesManager.instance.GetInputReception() == true)
        {

            //�L�����Z���{�^���ŒT���t�F�[�Y(phase:0)�ɖ߂�B
            if (GameInput.instance._BackAction.triggered == true)
            {
                AudioManager.instance.playSE(0);
                ArchivesManager.instance.InfoOff();

                PhaseChangeProcess(Phase.EXPLORE);
                //nextPhase = Phase.EXPLORE;
            }

            //���X�N�����R�}���h
            //if (Input.GetButtonDown("RectButton"))
            if (GameInput.instance._RiskAction.triggered == true)
            {
                // ���X�N������ʂ̃v���O�����̕��Ɍ��݂̃f�[�^�𑗂�B
                // ���łɃ��X�N��������Ă���I�u�W�F�̏ꍇ��false���Ԃ��Ă���
                bool phaseChangeFlag = RiskDiscover.instance.DiscoverJudge(ArchivesManager.instance.GetNowData());

                // �܂����X�N��������Ă��Ȃ��I�u�W�F�N�g�̏ꍇ
                if (phaseChangeFlag == true)
                {
                    // �ڍ׏��E�B���h�E���I�t
                    ArchivesManager.instance.InfoOff();

                    // ���X�N������ʂ��I��
                    RiskDiscover.instance.InfoOn();

                    // ���X�N�����t�F�[�Y�ֈڍs
                    PhaseChangeProcess(Phase.DISCOVER);

                }
                //���łɔ����R�}���h��K�p����Ă���ꍇ
                else
                {
                    MessageManager.instance.SetMessage("���X�N�����ς݂̃I�u�W�F�N�g�ł��B");
                    AudioManager.instance.playSE(10);
                }

            }

            /* ���X�e�B�b�N���\���L�[�̍��E�Ńy�[�W����B****************/

            if (0 < GameInput.instance.JujiKeyInput.x || 0 < GameInput.instance.moveInput.x)
            {
                GameInput.instance.rightButtonframe++;
                if (GameInput.instance.rightButtonframe % 60 == 1)//�������ɂ��Ή��ł���悤�Ȋ����ŁB1�b��1�񓮂��B
                {
                    ArchivesManager.instance.IndexNext();
                    if (ArchivesManager.instance.GetArchivesCount() > 1)
                    {
                        AudioManager.instance.playSE(9);
                    }


                }

            }
            else if (GameInput.instance.JujiKeyInput.x < 0 || GameInput.instance.moveInput.x < 0)
            {
                GameInput.instance.leftButtonframe++;
                if (GameInput.instance.leftButtonframe % 60 == 1)
                {
                    ArchivesManager.instance.IndexBefore();
                    if (ArchivesManager.instance.GetArchivesCount() > 1)
                    {
                        AudioManager.instance.playSE(9);
                    }
                }
            }
            else
            {
                GameInput.instance.rightButtonframe = 0; GameInput.instance.leftButtonframe = 0;
            }
        }
    }


    //�t�F�[�Y�ς��鏈���܂Ƃ߁B
    void PhaseChangeProcess(Phase next_phase)
    {
        nowPhase = next_phase;
        switch (next_phase)
        {

            case Phase.WAIT:
                break;


            case Phase.EXPLORE:
                Manual.instance.WindowOff();
                Manual.instance.ExploreActive();
                break;


            case Phase.DETAILINFO: /*** �����{�����Ă����� *****/
                Manual.instance.WindowOff();
                Manual.instance.DetailActive();

                break;

            case Phase.DISCOVER://�����t�F�[�Y
                Manual.instance.WindowOff();

                break;


            case Phase.ARCHIVES://�A�[�J�C�u�{���t�F�[�Y�B
                Manual.instance.WindowOff();
                Manual.instance.ArchiveActive();

                break;

            case Phase.RESULT://���U���g�t�F�[�Y
                Manual.instance.WindowOff();
                break;

            case Phase.DOCUMENT://�h�L�������g�{���t�F�[�Y
                Manual.instance.WindowOff();

                break;

            default:
                break;
        }
    }

    
    /**** ���͏��� ******************************************************/

    //���͌n�̏�����
    /*public void InputInit()
    {
        var pInput = GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        var actionMap = pInput.currentActionMap;
        Debug.Log(actionMap.name);
        //�A�N�V�����}�b�v����A�N�V�������擾
        _MoveAction = actionMap["Move"];
        Debug.Log(_MoveAction);
        _LookAction = actionMap["Look"];

        _DecisionAction = actionMap["Decision"];
        _BackAction = actionMap["Back"];
        _RiskAction = actionMap["Risk"];
        _ArchivesAction = actionMap["Archives"];
        _GimicAction = actionMap["Gimic"];
        _JujiKeyAction = actionMap["JujiKey"];
        _AnyAction = actionMap["Any"];
        _CameraResetAction = actionMap["CameraReset"];

        _L2Action = actionMap["L2Action"];
        _R2Action = actionMap["R2Action"];

        _R1Action = actionMap["R1Action"];

        _DebugMode = actionMap["DebugMode"];

        debugModeFlag = false;
        DebugWindow.gameObject.SetActive(debugModeFlag);

    }
    */
    //���͌n�̍X�V
   /* void InputUpdate()
    {
        //Debug.Log("InputUpdate");
        moveInput = _MoveAction.ReadValue<Vector2>();
        //Debug.Log(moveInput);
        lookInput = _LookAction.ReadValue<Vector2>();

        JujiKeyInput = _JujiKeyAction.ReadValue<Vector2>();

        //if (_DebugMode.triggered)
        //{
        //    debugModeFlag = !debugModeFlag;
        //    DebugWindow.gameObject.SetActive(debugModeFlag);
        //}



    }*/



    public bool L2Pressed()
    {
        return GameInput.instance._L2Action.IsPressed();
    }

    public bool R2Pressed()
    {
        return GameInput.instance._R2Action.IsPressed();
    }

    public bool R1Triggered()
    {
        return GameInput.instance._R1Action.triggered;
    }

    public bool GetDebugMode()
    {
        return GameInput.instance.debugModeFlag;
    }

    /***** �v���C���[�̈ړ��֘A *****************************************/

    public float vertiRotate = 0.0f;
    private float MinLimit = -50.0f;    //�c��]�́������̐���
    private float MaxLimit = 70.0f;     //�c��]�������̐���

    Vector3 newVec = new Vector3();


    public void Move()
    {
        rayCastFlag = true;
        IconFlag = true;

        PlayerRotate();
        MoveProcess();
    }

    public void ResetCamera()
    {
        WebCameraSet.localEulerAngles = Vector3.zero;

        CameraMain.transform.localEulerAngles = Vector3.zero;

        CameraL.localEulerAngles = Vector3.zero;

        CameraR.localEulerAngles = Vector3.zero;

        vertiRotate = 0;
    }

    //�v���C���[�̉�]����B
    private void PlayerRotate()
    {

        //���������̉�]
        float turn = GameInput.instance.lookInput.x * turnSpeed * Time.deltaTime;
        _cashPlayerTransform.Rotate(0, turn, 0);

        //���������̉�] webCameraSet�𒼐ډ�]�������B
        //�C���X�y�N�^�[��̐��l�ƁA�ϐ��̒l���قȂ��Ă���̂��������Ă���B
        //�C���X�y�N�^�[��ł�-180�`180�̐��l�����A�ϐ����0�`360�̊ԂɎ��܂��Ă���悤�Ȋ���������B

        vertiRotate += -GameInput.instance.lookInput.y * turnSpeed * Time.deltaTime;
        vertiRotate = Mathf.Clamp(vertiRotate, MinLimit, MaxLimit);
        WebCameraSet.localEulerAngles = new Vector3(vertiRotate, 0.0f, 0.0f);

    }

    //�v���C���[�̈ړ�����
    public void MoveProcess()
    {
        //�ړ������̎擾
        moveDirection = new Vector3(GameInput.instance.moveInput.x, 0.0f, GameInput.instance.moveInput.y);

        //�����ꂾ�ƃL�������󒆂ɕ����Ă��܂�
        moveDirection = CameraFollower.TransformDirection(moveDirection);


        //�ړ��ʂ��|����
        moveDirection = moveDirection * speed;

        /* Time.deltaTime��1�t���[���Ԃ̎��ԁB
         * ���ԗʂ��|���邱�ƂŎ��Ԃɑ΂���ړ��ʂ��ς��Ȃ��悤�ɏo����炵��*/
        _cachController.Move(moveDirection * Time.deltaTime);

        //�J�����������Ă�������Ɉړ�����ꍇ�ɁA�L�����N�^�[�����ɕ����Ă��܂���肪����
        //���̑Ώ��Ƃ���y���W���Œ肵�Ă���B�ǂ����@������Ώ������������B
        _cashPlayerTransform.position = new Vector3(_cashPlayerTransform.position.x, PlayerStartY, _cashPlayerTransform.position.z);

    }
    public bool GetRayCastFlag()
    {
        return rayCastFlag;
    }

    public bool GetIconFlag()
    {
        return IconFlag;
    }



}

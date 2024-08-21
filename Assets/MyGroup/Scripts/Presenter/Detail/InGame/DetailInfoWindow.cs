using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UniRx;
using TMPro;
using System;
using UnityEngine.Networking;

public class DetailInfoWindow : MonoBehaviour
{

    /***** Unity�I�u�W�F�N�g�𐧌䂷��ϐ��Q ***************************************************************/

    public static DetailInfoWindow instance; // ���̃X�N���v�g����֐����Ăяo����悤�ɂ����B


    // ��ʂɔz�u���ꂽ�e�L�X�g�I�u�W�F�N�g��M���B�C���X�y�N�^�[�ŕR�Â��K�{ 
    [SerializeField] private TextMeshProUGUI ObjNameJP, ObjDetail, TMPJudgedMark;



    /***** ���̑��ϐ� ***********************************************************************************************/

    //�t�@�C�����Ɋg���q��t���邩���ɂ���
    //Unity��ŃA�Z�b�g�Ƃ������ɓo�^���ꂽ�t�@�C���͊g���q��t���Ă̓_����
    //�o�^����ĂȂ��O���̃t�@�C���͕K�v�ɂȂ�炵���B��₱����
    //


    [SerializeField] private string FolderName = "/SmallOffice"; //�X�e�[�W�f�[�^�������Ă�t�H���_�[���B

    [SerializeField] string stageNameFilePath = "/StageNames.txt"; //�g���q�ɒ��ӁB

    private string[] StageDataFiles;   //�X�e�[�W��񂪏����ꂽ�t�@�C���̖��O�B�t�@�C������ǂ݂��ށB

    //List<string[]> SurveyObjList;   // CSV�t�@�C������ǂݍ��񂾒����I�u�W�F�N�g�̃f�[�^�i�[.


    private string hitName = "none";  // ���݃v���C���[�̎��_�������Ă���I�u�W�F�N�g��
    private int hitObjNum = 0;      // hitName�ƈ�v�������X�g��Index�ԍ�

    private bool SeachDataFlag; //�����������J��Ԃ��Ȃ��悤�ɂ���t���O�B
    //private int riskObjNum;     //�ǂݍ��񂾃X�e�[�W�f�[�^�ɂ��郊�X�N�̐�

    private bool activeFlag;           // �E�B���h�E���A�N�e�B�u���ǂ����B

    protected bool InputReceptionFlag;  //�v���C���[�̓��͂��󂯕t���邩�̃t���O

    protected bool phaseChangeFlag;     //�t�F�[�Y��ς��Ă悢���̃t���O

    //private UDPConnection myUdpConnection;
    //private StageManager myStageManager;

    private const int DISCOVERSTATE = 1;
    private const int EXPLORESTATE = 2;



    /* Unity���Ŏ����ŌĂяo��������Ȋ֐��Q **********************************************************/

    // �����������BUpdate�̒��O�ɌĂ΂��
    void Start()
    {
        //�C���X�^���X����
        if (instance == null)
        {
            instance = this;
        }


        // //�X�e�[�W�f�[�^�̖��O���t�@�C������Ăяo��
        //StageDataFiles = GetStageName(Application.streamingAssetsPath + FolderName + stageNameFilePath);

        // //�X�e�[�W�̃i���o�[�������_���Ō���B
        // //�ŏ��̈����i�ŏ��l�j�͗����Ɋ܂܂�邪��̈����i�ő�l�͊܂܂�Ȃ��j
        // int stageNum =  UnityEngine.Random.Range(0, StageDataFiles.Length);
        // //stageNum = 0;

        // //�X�e�[�W�f�[�^�Ɖ���f�[�^��ǂݍ���
        // SurveyObjList = CsvDataLoad(Application.streamingAssetsPath + FolderName + StageDataFiles[stageNum]);

        //myUdpConnection = GameObject.Find("udpClient").GetComponent<UDPConnection>();
        //myStageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        //StartCoroutine(GetWebStageDataProcess());

        //var stageNum = myStageManager.GetStageNum;
        //Debug.Log(stageNum);
        //StartCoroutine(DownloadStageData(Application.streamingAssetsPath + FolderName + myStageManager.StageDataFiles[stageNum]));

        //�E�B���h�E�̏���������\��
        //WindowDrawInit();
        

        //�E�B���h�E������
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*//�E�B���h�E���A�N�e�B�u�Ȏ�
        if (activeFlag == true)
        {
            ActiveProcess();
            bool animeWork = AppearAnimation();

            if (animeWork == true)
            {
                InputReceptionFlag = false;
            }
            else
            {
                InputReceptionFlag = true;
            }

            activeFrame++;

        }
        else
        {
            bool animeWork = DisappearAnimation();

            if (animeWork == true)
            {
                phaseChangeFlag = false;
            }
            else
            {
                phaseChangeFlag = true;

                this.gameObject.SetActive(false);
            }

            disappearFrame++;
        }

        //�E�B���h�E�����X�ɑ傫��(������)���鏈���B
        //ScaleChangeProcess();*/

    }

    //�E�B���h�E��\������
    public IEnumerator DisplayDetailInfoWindow()
    {
        this.gameObject.SetActive(true);    //�E�B���h�E�L����
        ActiveProcess();    //�L��������I�u�W�F�N�g�̃f�[�^���擾
        StartCoroutine(DisplayWindow.AppearAnimation(this.gameObject)); //�E�B���h�E�����񂾂�傫���\������
        yield return null;
    }

    private void ActiveProcess()
    {
        Debug.Log("ActiveProcess");

        //���E�̔���Ƀq�b�g���Ă���I�u�W�F�N�g�̖��O���擾
        hitName = CameraRayCast.instance.GetHitObjectName();
        
        Debug.Log(hitName);
        
        //�q�b�g���Ă���I�u�W�F�N�g�����X�g���猟��
        //hitObjNum = SeachData(hitName);

        Debug.Log("SeachData EXE");

        var SurveyObject = StageData.GetObjNum(hitName);
        //�\���ɔ��f����
        ObjNameJP.text = SurveyObject[1];
        ObjDetail.text = SurveyObject[3];

        Debug.Log("�\���ɔ��f");
        //���X�N������s���Ă��邩�ǂ������X�V
        TMPJudgedMark.text = JudgedMarkProcess(hitName);


        //}
    }


    public IEnumerator HiddenDetailInfoWindow()
    {
        IEnumerator DisAppear = DisplayWindow.HiddenAnimation(this.gameObject); //�E�B���h�E�\������\���ɂȂ�܂őҋ@
        yield return DisAppear;
        this.gameObject.SetActive(false);
    }

    public int ActionWindow()
    {
        Debug.Log("���͑҂�");
        if (GameInput.instance._BackAction.triggered == true)
        {
            Debug.Log("�E�B���h�E�I��");
            AudioManager.instance.playSE(0);
            StartCoroutine(HiddenDetailInfoWindow());
            return EXPLORESTATE;
        }
        // L1�{�^���������ꂽ��A���\������Ă�������A�[�J�C�u�ɒǉ�����B
        if (GameInput.instance._ArchivesAction.triggered == true)
        {
            // �ڍ׏��E�B���h�E�ɕ\������Ă�������A�[�J�C�u�ɒǉ�����
            // �ǉ�������true ���łɒǉ�����Ă���ꍇ��false
            bool addFlag = ArchivesManager.instance.AddArchives(StageData.GetSurveyingObj());
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
        }
        //���X�N�����R�}���h
        if (GameInput.instance._RiskAction.triggered == true)
        {

            // ���X�N������ʂ̃v���O�����̕��Ɍ��݂̃f�[�^�𑗂�B
            // ���łɃ��X�N��������Ă���I�u�W�F�̏ꍇ��false���Ԃ��Ă���
            bool phaseChangeFlag = RiskDiscover.instance.DiscoverJudge(StageData.GetSurveyingObj());

            //�܂����X�N��������Ă��Ȃ��I�u�W�F�N�g�̏ꍇ
            if (phaseChangeFlag == true)
            {
                // �ڍ׏��E�B���h�E���I�t
                StartCoroutine(HiddenDetailInfoWindow());

                // ���X�N������ʂ��I��
                RiskDiscover.instance.InfoOn();

                // ���X�N�����t�F�[�Y�ֈڍs
                return DISCOVERSTATE;
            }
            //���łɔ����R�}���h��K�p����Ă���ꍇ
            else
            {
                MessageManager.instance.SetMessage("���X�N�����ς݂̃I�u�W�F�N�g�ł��B");
                AudioManager.instance.playSE(10);
            }
        }
            return 0;
    }
    
    //���X�N��������Ă��邩�ǂ���
    public string JudgedMarkProcess(string obj_name)
    {
        bool discoveredFlag = RiskDiscover.instance.CheckDiscovered(obj_name);

        if (discoveredFlag == true)
        {
            var Obj = StageData.GetObjNum(obj_name);
            int riskNum = Convert.ToInt32(Obj[2]);

            if (riskNum == 0)
            {
                //TMPJudgedMark.text = "��";
                return "��";
            }
            else
            {
                //TMPJudgedMark.text = "�L";
                return "�L";
            }
        }
        else
        {
            //TMPJudgedMark.text = "?";
            return "�H";
        }
    }

}



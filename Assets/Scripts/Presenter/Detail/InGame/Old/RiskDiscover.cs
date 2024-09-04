using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using System;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine.UI;
using Unity.VisualScripting;

public class RiskDiscover : MonoBehaviour
{
    public static RiskDiscover instance;
    private GameData _gameData;

    private List<string> judgeObjList = new List<string>();


    private string judgeObjName;

    private int RiskFlag;

    private bool anserMarkFlag;

    private bool InputReceptionFlag;


    private bool activeFlag;

    private int activeFrame;
    
    private int disappearFrame;

    //private UDPConnection myUdpConnection;

    /*** Unity���Ŏ����ŌĂ΂��֐��Q ******************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _gameData = GameData.GetInstance(); //�Q�[���̐i�s�󋵊Ǘ��N���X�̎擾
  
        /*WindowDrawInit();
        InfoOff();
        this.gameObject.SetActive(false);
        //myUdpConnection = GameObject.Find("udpClient").GetComponent<UDPConnection>();

        /*myUdpConnection.OnObjNameChanged
                       .Where(value => value != null)   //value��null����Ȃ��Ƃ�
                       .Subscribe(value => OnValueChanged(value))
                       .AddTo(this);*/

        /*myUdpConnection.OnCountNumChanged
                       .Where(value => value != 0)  //0����Ȃ��Ƃ�
                       .Subscribe(value => SetCountNum(value))
                       .AddTo(this);

        myUdpConnection.OnAnswerNumChanged
                       .Where(value => value != 0)
                       .Subscribe(value => JudgeAnswerNum(value))
                       .AddTo(this);*/
    }


    // Update is called once per frame
    void Update()
    {
        /*if (activeFlag == true)
        {
            //�E�B���h�E�̃A�j���[�V�����������Ă���Ԃ͓��͂��֎~����B
            if (AppearAnimation() == true)
            {
                InputReceptionFlag = false;
            }
            else
            {
                InputReceptionFlag = true;
            }
            // �t���[�����X�V
            activeFrame++;
        }
        else
        {
            //�E�B���h�E�̃A�j���[�V�����������Ă���Ԃ͓��͂��֎~����B
            if (DisappearAnimation() == true)
            {
                InputReceptionFlag = false;
            }
            else
            {
                InputReceptionFlag = true;
                this.gameObject.SetActive(false);
            }
            disappearFrame++;


        }*/

        
    }




    /*** ����̊֐��Q ************************************************************************************/

    // �����̃I�u�W�F�N�g�f�[�^���烊�X�N�����ł��������肵�ĉ�ʕ\����ύX����
    public bool DiscoverJudge(string[] surveyObjData)
    {
        //���łɃ��X�N�����R�}���h��K�p����Ă��Ȃ������ׂ�
        bool judgedFlag = CheckDiscovered(surveyObjData[0]);

        //�Ώۂ̃I�u�W�F�N�g�������R�}���h��K�p����Ă���ꍇ
        if (judgedFlag == true)
        {
            return false;
        }
        //�����R�}���h��K�p����Ă��Ȃ��ꍇ�A���X�N������ʂ̐��ۂ𔻒�A�\���B
        else
        {
            //�A�[�J�C�u�ɉ����ۑ����ĂȂ����Ȃǂ͏������Ȃ��B
            if (surveyObjData[0] == "none")
            {
                return false;
            }

            //judgeObjList.Add(surveyObjData[0]);

            _gameData.SetObjList(surveyObjData[0]);
            //myUdpConnection.SendByte(prefix: "_Nm", msg: surveyObjData[0]); //����Ώۂ̃I�u�W�F�N�g�𑗐M

            judgeObjName = surveyObjData[1];
            TMPTargetobjName.text = judgeObjName;

            RiskFlag = Convert.ToInt32(surveyObjData[2]);//0�̏ꍇ���X�N�����B����ȊO�͗L��
            
            TMPCommentaryDetail.text = surveyObjData[4];
            if (RiskFlag == 0)
            {
                Xmark.SetActive(true);
                anserMarkFlag = false;  //����s�����̃t���O
                TMPJudgeText.text = "���X�N�����F���s�c";
             

            }
            else if (RiskFlag > 0)
            {
                Omark.SetActive(true);
                anserMarkFlag = true;   //����s�����̃t���O
                TMPJudgeText.text = "���X�N�����F�����I";
              
                _gameData.CorrectCount = 1; //�����񐔂�+1
            }

            _gameData.JudgeCount = 1;   //����񐔂�+1

            //myUdpConnection.SendByte(prefix: "_Ct", num: 1); //����񐔂𑗐M
            //if (RiskFlag != 0)
            //myUdpConnection.SendByte(prefix: "_An", num: RiskFlag); //����s�����𑗐M
            //UDPConnection.instance.SendGameData(_gameData);
            return true;


        }

    }

    // ���łɔ����R�}���h���K�p���ꂽ�I�u�W�F�N�g�łȂ������ׂ�B
    // �K�p����Ă���ꍇ�Ftrue,����ĂȂ��ꍇ�Ffalse
    public bool CheckDiscovered(string obj_name)
    {
        bool rt = false;
        for (int i = 0; i < _gameData.GetSurveiedObjList().Count; i++)
        {
            if (_gameData.GetSurveiedObjList().Contains(obj_name))
            {
                rt = true;
                break;
            }
        }

        return rt;
    }



    private void CutInObjSetActive(bool flag)
    {
        CutInBack.gameObject.SetActive(flag);
        DangerMark.gameObject.SetActive(flag);
        LoupeMark.gameObject.SetActive(flag);
        MessageText.gameObject.SetActive(flag);

    }

    public bool GetInputReception()
    {
        return InputReceptionFlag;
    }




    /*** �E�B���h�E�̕\���⏈���𐧌䂷�鏈�� *****************************************************/


   [SerializeField]�@public TextMeshProUGUI 
        TMPCommentaryDetail, TMPCommentaryTitle, TMPTargetobjName, TMPJudgeText, MessageText;

    [SerializeField]
    public GameObject Omark, Xmark;

    [SerializeField]
    private RectTransform CutInBack;
    private Vector2 CutInPos;
   

    public RectTransform DangerMark;
    private Vector2 DangerPos;
    private float dangerScale = 1.5f;

    public RectTransform LoupeMark;
    private Vector2 LoupePos;

    [SerializeField] private RectTransform RiskResultWindow;
    private Vector3 startScale;
    private float scalePercent;




    // �X�P�[���ς��郄�c�̏������B
    private void WindowDrawInit()
    {
        CutInPos = CutInBack.localPosition;

        Omark.SetActive(false);
        Xmark.SetActive(false);
        CutInObjSetActive(false);

        startScale = RiskResultWindow.localScale;
       
        scalePercent = 0;       
        RiskResultWindow.localScale = startScale * scalePercent;
        
        activeFlag = false;
    }

    //�����A�j���[�V��������
    private bool AppearAnimation()
    {
        bool workFlag = false;


        int endFrame = 180;


        //�A�j���[�V�������쒆��true�A�I�������false;
        if (activeFrame <= endFrame)
        {
            workFlag = true;
        }
        else
        {
            workFlag = false;
        }


        if (0 <= activeFrame && activeFrame < 10)
        {
            //�댯�}�[�N�ƃJ�b�g�C���w�i�̕\���Ə�����
            if (activeFrame == 0)
            {
                CutInBack.gameObject.SetActive(true);
                CutInPos.x = 3580;

                DangerMark.gameObject.SetActive(true);
                DangerMark.localPosition = new Vector2(0, DangerMark.localPosition.y);
                dangerScale = 1.5f;

                InputReceptionFlag = false;
                AudioManager.instance.playSE(13);

            }
            //�������X�P�[����������
            DangerMark.localScale = Vector3.one * dangerScale;
            dangerScale -= 1.0f / 10.0f;

        }
        else if (10 <= activeFrame && activeFrame < 20)
        {
            //�������X�P�[����傫��
            DangerMark.localScale = Vector3.one * dangerScale;
            dangerScale += 0.5f / 10.0f;

        }
        else if (30 <= activeFrame && activeFrame < 50)
        {
            //���ዾ�̏������ƕ\��
            if (activeFrame == 30)
            {
                LoupeMark.gameObject.SetActive(true);
                LoupePos.x = 650.0f;
                LoupePos.y = LoupeMark.localPosition.y;
            }
            //���ዾ�̃X���C�h�@������
            LoupeMark.localPosition = LoupePos;
            LoupePos.x += -1300.0f / 20.0f;

        }
        else if (50 <= activeFrame && activeFrame < 60)
        {
            if (activeFrame == 50)
            {
                LoupePos.x = -650.0f;

            }
            //���ዾ�̃X���C�h�@���������璆�S
            LoupeMark.localPosition = LoupePos;
            LoupePos.x += 650.0f / 10.0f;

        }

        else if (60 <= activeFrame && activeFrame < 70)
        {
            //���W���Y����̂ňꉞ�Œ肵�Ă���
            if (activeFrame == 60)
            {
                LoupePos.x = 0;
                LoupeMark.localPosition = LoupePos;
            }
        }
        else if (70 <= activeFrame && activeFrame < 90)
        {

            if (activeFrame == 70)
            {
                DangerPos = DangerMark.localPosition;
            }

            //���ዾ�Ɗ댯�}�[�N�����ɃX���C�h
            LoupeMark.localPosition = LoupePos;
            DangerMark.localPosition = DangerPos;

            LoupePos.x += (-540.0f) / 20.0f;
            DangerPos.x += (-540.0f) / 20.0f;

            if (LoupePos.x < -540.0f)
            {
                LoupePos.x = -540.0f;
            }
            if (DangerPos.x < -540.0f)
            {
                DangerPos.x = -540.0f;
            }
        }
        else if (100 <= activeFrame && activeFrame < 170)
        {
            //�u���X�N�����I�v�̃e�L�X�g��\��
            if (activeFrame == 100)
            {
                MessageText.gameObject.SetActive(true);
                AudioManager.instance.playSE(14);
            }

        }
        else if (170 <= activeFrame && activeFrame < 180)
        {
            //�J�b�g�C������āA�����\������
            if (activeFrame == 170)
            {
                CutInObjSetActive(false);


            }
            //����̃E�C���h�E�����X�ɑ傫������
            scalePercent += 1.0f / 10;
            if (scalePercent >= 1 )
            {
                scalePercent = 1;
            }
            RiskResultWindow.localScale = startScale * scalePercent;
        }
        else if (activeFrame == 180)
        {
            if (anserMarkFlag == true)
            {
                AudioManager.instance.playSE(1);
            }
            else
            {
                AudioManager.instance.playSE(2);
            }
            //�v���C���[�̓��͋֎~������
            InputReceptionFlag = true;
        }

        //�J�b�g�C���A�j���[�V�����������Ă���ԁA�w�i���X���C�h�����Ă���
        if (activeFrame < 180)
        {
            CutInPos.x -= (3580 * 2) / (60.0f * 4.0f);
           
            if (CutInPos.x < -3580)
            {
                CutInPos.x = 3580;
               
            }
            CutInBack.localPosition = CutInPos;
        }


        return workFlag;
    }

    private void OnValueChanged(string ObjName)
    {
        Debug.Log("OnValueChanged���Ăяo����܂���");
        if (!judgeObjList.Contains(ObjName))   //�󂯎�����I�u�W�F�N�g�����ӂ��܂�Ă��Ȃ����
        {
            Debug.Log("���̃I�u�W�F�N�g�͂܂���������Ă��܂���:"+ ObjName);
            Debug.Log("�����ς݂̃I�u�W�F�N�g��:" + judgeObjList.Count);
            judgeObjList.Add(ObjName);  //����ς݂̃��X�g�ɂ��̃I�u�W�F�N�g��ǉ�
            for(int i = 0; i < judgeObjList.Count; i++)
            {
                Debug.Log("�v�f"+i+":"+judgeObjList[i]);
            }
            Debug.Log("�X�V��:" + judgeObjList.Count);
        }
        //myUdpConnection.SetObjNameProperty = null;
    }

    /*private void SetCountNum(int SetNum)
    {
        judgeCount += SetNum;   //���葤�Ŕ��肵���񐔂����f
        myUdpConnection.SetCountNumProperty = 0;    //�ϐ������Z�b�g
    }*/

    /*private void JudgeAnswerNum(int AnswerNum)
    {
        correctCount++;
        Debug.Log(correctCount);
        myUdpConnection.SetAnswerNumProperty = 0;
    }*/

    //������A�j���[�V����
    private bool DisappearAnimation()
    {
        bool workFlag = false;

        int endFrame = 6;

        //�A�j���[�V�������쒆��true�A�I�������false;
        if (disappearFrame <= endFrame)
        {
            workFlag = true;
        }
        else
        {
            workFlag = false;
        }
        if (0 <= disappearFrame && disappearFrame < endFrame)
        {
            scalePercent -= 1.0f / (float)endFrame;
            if (scalePercent <= 0)
            {
                scalePercent= 0;   
            }
            RiskResultWindow.localScale = startScale * scalePercent;

        }
        return workFlag;
    }



    // �ڍ׏��̃E�B���h�E��On�EOFF��؂�ւ�����B
    public void InfoOn()
    {
        this.gameObject.SetActive(true);
        activeFlag = true;      //�������A�N�e�B�u��
        activeFrame = 0;
        //RiskResultWindow.localScale = startScale;

    }

    public void InfoOff()
    {
        activeFlag = false;
        disappearFrame = 0;
        Omark.SetActive(false);
        Xmark.SetActive(false);
    }
    

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ResultScript : MonoBehaviour
{

    public static ResultScript instance;


    private bool InputReceptionFlag;

    private TimeSpan LapsedTime;
    private int Minutes, seconds, milliSeconds;
    private int m, s, ms;

    private GameData _gameData;

    private int missCount;


    /*�@�֐��R *******************************************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        WindowDrawInit();
        InfoOff();
        _gameData = GameData.GetInstance();

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (activeFlag == true)
        {
            if (AppearAnimation() == true)
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
            //disappearFrame++;
        }

    }




    private void AllObjInActive()
    {
        TMPStageClear.gameObject.SetActive(false);

        TMPClearTimeText.gameObject.SetActive(false);
        TMPClearTimeValue.gameObject.SetActive(false);

        TMPMissText.gameObject.SetActive(false);
        TMPMissValue.gameObject.SetActive(false);


        TMPRankText.gameObject.SetActive(false);
        TMPRank.gameObject.SetActive(false);
        
    }

    // �����N�v�Z����
    private void RankJudgeProcess()
    {
        int rankPoint = 0;
        int riskNum = StageData.GetRiskNum();


        //�~�X�J�E���g�̉��_
        if (missCount==0)
        {
            rankPoint += 3;
        }
        else if(0< missCount && missCount<=1)
        {
            rankPoint += 2;
        }
        else if (1< missCount && missCount <= riskNum)
        {
            rankPoint += 1;
        }
        else
        {
            rankPoint += 0;
        }

        //���Ԃɂ����_
        if (Minutes < 3)
        {
            rankPoint += 3;
        }
        else if (3<= Minutes && Minutes < 4)
        {
            rankPoint += 2;
        }
        else if (4 <= Minutes && Minutes < 5)
        {
            rankPoint += 1;
        }
        else if (5 <= Minutes)
        {
            rankPoint += 0;
        }

        //�����N�̌���
        if (rankPoint == 6)
        {
            TMPRank.text = "S";
            TMPRank.color = UnityEngine.Color.magenta;
        }
        else if (rankPoint == 5)
        {
            TMPRank.text = "A";
            TMPRank.color = UnityEngine.Color.red;
        }
        else if (rankPoint == 4)
        {
            TMPRank.text = "B";
            TMPRank.color = UnityEngine.Color.yellow;
        }
        else if (rankPoint == 3)
        {
            TMPRank.text = "C";
            TMPRank.color = UnityEngine.Color.green;
        }
        else if (rankPoint == 2)
        {
            TMPRank.text = "D";
            TMPRank.color = UnityEngine.Color.blue;
        }
        else if (rankPoint <= 1)
        {
            TMPRank.text = "E";
            TMPRank.color = UnityEngine.Color.gray;
        }






    }

    //�v���C���[�̓��͂��󂯕t���邩
    public bool GetInputReception()
    {
        return InputReceptionFlag;
    }

    //�Q�[���̊J�n�`�I���܂ł̌o�ߎ��Ԃ��Z�b�g����B(������Ăԗp)
    public void SetLapsedTime(TimeSpan ts)
    {
        LapsedTime = ts;
    }




    /*** �E�B���h�E�̕\����傫����ύX���鏈���𐧌䂷��ϐ��Q ********************************************/

    [SerializeField]
    private TextMeshProUGUI
    TMPStageClear, TMPClearTimeText, TMPClearTimeValue,
    TMPMissText, TMPMissValue, TMPRankText, TMPRank;

    [SerializeField] Image backGround;
    private UnityEngine.Color _backColor;
    private float alpha = 0;


    private RectTransform thisWindowRect;   
    private Vector3 startScale;        
    private float scalePercent;        
   
    private bool activeFlag;�@          
    
    private int activeFrame;
    private int disappearFrame;

    // �X�P�[���ς��郄�c�̏������B
    private void WindowDrawInit()
    {

        _backColor = backGround.color;
        alpha = 0.0f;
        _backColor = new UnityEngine.Color(_backColor.r, _backColor.g, _backColor.b, alpha);

        thisWindowRect = this.GetComponent<RectTransform>();
        
        startScale = thisWindowRect.localScale;
        
        scalePercent = 0;

        thisWindowRect.localScale = startScale * scalePercent;
        
        activeFlag = false;
    }

    //�����A�j���[�V����
    private bool AppearAnimation()
    {
        bool workFlag = false;

        int fadeInTime = 30;
        int slideTime = fadeInTime + 30;

        int endTime = slideTime;


        //�A�j���[�V�������쒆��true�A�I�������false;
        if (activeFrame <= endTime)
        {
            workFlag = true;
        }
        else
        {
            workFlag = false;
        }


        //�w�i�̕\��
        if (0 <= activeFrame && activeFrame < 20)
        {
            if (activeFrame == 0)
            {
                InputReceptionFlag = false;
                alpha = 0.0f;

                AllObjInActive();

            }

            // �w�i�̐F�����X�ɔZ��
            alpha += 1.0f / 2.0f / (20.0f - 1.0f);
            _backColor = new UnityEngine.Color(_backColor.r, _backColor.g, _backColor.b, alpha);
        }

        // �X�e�[�W�N���A�̕���\��
        else if (activeFrame == 60)
        {

            TMPStageClear.gameObject.SetActive(true);
            AudioManager.instance.playSE(11);
        }

        //�N���A�^�C���̕\��
        else if (90 <= activeFrame && activeFrame <= 120)
        {
            if (activeFrame == 90)
            {
                TMPClearTimeText.gameObject.SetActive(true);
                TMPClearTimeValue.gameObject.SetActive(true);

                //���Ԃ̏���
                Minutes = LapsedTime.Minutes;
                seconds = LapsedTime.Seconds;
                milliSeconds = LapsedTime.Milliseconds;
                m = s = ms = 0;

                //AudioManager.instance.playSE(11);
            }
            m += Minutes / 30;
            s += seconds / 30;
            ms += milliSeconds / 30;

            if (activeFrame == 120)
            {
                m = Minutes;
                s = seconds;
                ms = milliSeconds;
                AudioManager.instance.playSE(11);
            }

            TMPClearTimeValue.text = m.ToString() + "��" + s.ToString() + "�b" + ms.ToString();

        }

        //�~�X�񐔂̕\��
        else if (140 <= activeFrame && activeFrame <= 180)
        {
            if (activeFrame == 140)
            {
                TMPMissText.gameObject.SetActive(true);
                missCount = _gameData.JudgeCount - StageData.GetRiskNum();
                TMPMissValue.text = missCount.ToString() + "��";
                //AudioManager.instance.playSE(11);
            }

            if (activeFrame == 180)
            {
                TMPMissValue.gameObject.SetActive(true);
                AudioManager.instance.playSE(11);
            }
        }

        //�����N�̕\��
        else if (210 <= activeFrame && activeFrame <= 240)
        {
            if (activeFrame == 210)
            {
                TMPRankText.gameObject.SetActive(true);
                RankJudgeProcess();
            }
            else if (activeFrame == 240)
            {
                TMPRank.gameObject.SetActive(true);
                AudioManager.instance.playSE(12);

                InputReceptionFlag = true;
            }
        }

     
        return workFlag;
    }


    // �ڍ׏��̃E�B���h�E��On�EOFF��؂�ւ�����B
    public void InfoOn()
    {
        this.gameObject.SetActive(true);
        activeFlag = true;      //�������A�N�e�B�u��
       
        scalePercent = 1.0f;
        thisWindowRect.localScale = startScale * scalePercent;
    }

    public void InfoOff()
    {
        activeFlag = false;

        

    }

}

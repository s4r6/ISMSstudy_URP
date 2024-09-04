using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text;
using System;
using System.Linq;


public class ArchivesManager : MonoBehaviour
{
    public static ArchivesManager instance;


    //�A�[�J�C�u�̃��X�g�ϐ�
    private List<string[]> archivesList = new List<string[]>();
    private int nowListIndex;

    //�E�B���h�E���I�����ǂ���
    bool activeFlag;

    //�E�B���h�E���I���ɂȂ��Ă���̃t���[�������L�^����ϐ��B
    private int activeFrame;

    //�E�B���h�E���I�t�ɂȂ��Ă���̃t���[�������L�^
    private int disappearFrame;

    //�v���C���[�̓��͂��󂯕t���邩�ǂ����̃t���O
    private bool InputReceptionFlag;

    //�t�F�[�Y��؂�ւ��邩�ǂ����̃t���O
    private bool phaseChangeFlag;


    //���X�N���肪�s���Ă��邩�ǂ����̃}�[�N�̃��c�B
    public TextMeshProUGUI TMPJudgedMark;


    /* Unity���Ŏ����ŌĂяo��������Ȋ֐��Q **********************************************************/

    // Start is called before the first frame update
    void Start()
    {
        //�C���X�^���X�̏�����
        if (instance == null)
        {
            instance = this;
        }

        
        // Start()�̌Ă΂��^�C�~���O�̖��ŁA
        // ���I�u�W�F�N�g�̊֐����֐���Start()�ŌĂԂ͎̂~�߂������ǂ������B
        //archivesList.Add(DetailInfoWindow.instance.GetData(1));

        // �A�[�J�C�u���X�g���󂾂Ƃ܂����̂ŁA�������p�̃f�[�^��˂����ށB
        // AddArchives������Ă񂾎��ɂ��̃f�[�^�͍폜�����悤�ɂȂ��Ă���B
        string nowTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string[] initData = { "none", "None", "0", "�f�[�^���ǉ�����Ă��܂���", nowTime };
        archivesList.Add(initData);

        //�C���f�b�N�X�ԍ��̏�����
        nowListIndex = 0;


        WindowDrawInit();

        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (activeFlag == true)
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

            ObjNameJP.text = archivesList[nowListIndex][1];
            ObjDetail.text = archivesList[nowListIndex][3];
            int num = archivesList.Count;
            TMPArchivesNumver.text = (nowListIndex + 1).ToString() + "/" + num.ToString();
            //TMPJudgedMark.text = DetailInfoWindow.instance.JudgedMarkProcess(archivesList[nowListIndex][0]);

            DeltaAnimation();


            activeFrame++;



        }
        else
        {

            if (DisappearAnimation() == true)
            {
                InputReceptionFlag = false;
                phaseChangeFlag = false;
            }
            else
            {
                InputReceptionFlag = true;
                phaseChangeFlag = true;
                this.gameObject.SetActive(false);
            }

            disappearFrame++;
        }

    }




    /* ����֐��Q ****************************************************************************************/


    /**** �A�[�J�C�u�@�\�Ɋ֘A **********************************************************/

    // �A�[�J�C�u�Ƀf�[�^��ǉ�����B
    public bool AddArchives(string[] datas)
    {
        bool rt = false;
        //�����̃f�[�^�������ꍇ�́A�A�[�J�C�u�ɒǉ��B
        if (SeachData(datas[0]) == -1)
        {
            //�������̍ۂɒǉ�����none�̏�񂪂��鎞�A�������폜�B
            if (archivesList.Count == 1 && archivesList[0][0] == "none")
            {
                archivesList.RemoveAt(0);
            }


            List<string> list = new List<string>(datas.ToList());

            string nowTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            list.Add(nowTime);

            string[] dataTimePlus = list.ToArray();
            archivesList.Add(dataTimePlus);
            rt = true;
        }
        else
        {

        }
        return rt;
    }

    // �����̃I�u�W�F�N�g���̃f�[�^��T��
    private int SeachData(string obj_name)
    {
        int rtIndexNum = -1;


        for (int i = 0; i < archivesList.Count; i++)
        {
            //�������O������������A���̎���Index�ԍ����擾�B�������甲���o���B
            if (archivesList[i][0] == obj_name)
            {
                rtIndexNum = i;
                break;
            }
        }
        //���������ΐ��̒l���A�������Ȃ�-1���Ԃ�B
        return rtIndexNum;
    }

    // �\������������ɐi�߂�B���X�g�̍Ō�ɍs���ƍŏ��ɖ߂�B
    public void IndexNext()
    {
        nowListIndex++;
        if (archivesList.Count <= nowListIndex)
        {
            nowListIndex = 0;
        }


    }

    // �\���������O�ɖ߂��B���X�g�̍ŏ��ɍs���ƍŌ�ɖ߂�B
    public void IndexBefore()
    {
        nowListIndex--;
        if (nowListIndex < 0)
        {
            nowListIndex = archivesList.Count - 1;
        }

    }

    //�A�[�J�C�u���ꂽ�f�[�^���t�@�C���ɏo�͂���B
    public void ArchivesFileSave()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter("./ArchiveData.csv", false, Encoding.UTF8))
            {
                for (int i = 0; i < archivesList.Count; i++)
                {
                    string s = archivesList[i][1] + "," + archivesList[i][3] + "," + archivesList[i][4];
                    sw.WriteLine(s);

                }
                //MessageManager.instance.SetMessage("�A�[�J�C�u�t�@�C���o��");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            MessageManager.instance.SetMessage("�t�@�C���o�͂ł��܂���ł���");
        }

    }



    /**** Get�֘A *******************************************************************************/

    //���݃A�[�J�C�u�ɒǉ�����Ă���f�[�^����Ԃ�
    public int GetArchivesCount()
    {
        return archivesList.Count;
    }

    //�A�[�J�C�u�̌��݂̃f�[�^��Ԃ��B
    public string[] GetNowData()
    {
        return archivesList[nowListIndex];
    }

    //�v���C���[����̓��͂��󂯕t���邩�ǂ���
    public bool GetInputReception()
    {
        return InputReceptionFlag;
    }

    //�t�F�[�Y��ς��鋖���o��
    public bool GetPhaseChange()
    {
        if (activeFlag == false && phaseChangeFlag == true)
        {
            return true;
        }
        else return false;
    }




    /**** �`��֘A *******************************************************************************/

    /* �`��p�̕ϐ� **********************************************************/

    //�e�L�X�g��ύX�����肷�邽�߂̕ϐ�
    [SerializeField]
    private TextMeshProUGUI ObjNameJP, ObjDetail, TMPArchivesNumver;


    //�X���C�h����A�j���[�V�����p
    [SerializeField]
    private RectTransform TitleWindow, InfoWindow;

    //�X���C�h�A�j���[�V�����p�̃|�W�V����
    private Vector2 StartPosTitle, StartPosInfo;
    private Vector2 TitlePos, InfoPos;

    //�w�i�̍�����̏����p�ϐ�
    [SerializeField]
    private Image backGround;

    private Color bgColor;
    private Vector3 bgStartScale;

    // �E�B���h�E�S�̂̂̑傫���𐧌䂷����
    private RectTransform thisWindowRect;
    private Vector3 startScale;
    private float scalePercent;


    //�A�[�J�C�u��������Əo�Ă��鍶�E�̖��
    [SerializeField]
    private Transform DeltaLeft, DeltaRight;
    private Vector3 LeftPos, RightPos, movePoint;


    float slideX = 2000;
    float slideY = 1200;

    //�E�B���h�E�`��n�̏�����
    private void WindowDrawInit()
    {

        //�w�i�̂�̍ŏ��̑傫���ƐF��ۑ����Ă����B
        bgStartScale = backGround.transform.localScale;
        bgColor = backGround.color;

        //�E�B���h�E�̃A�j���[�V�����p�̏����B
        //�E�B���h�E�̏����ʒu��ۑ�
        TitlePos = StartPosTitle = TitleWindow.localPosition;
        InfoPos = StartPosInfo = InfoWindow.localPosition;

        //�����ʒu����X���C�h������
        TitlePos.x = StartPosTitle.x + slideX;
        InfoPos.y = StartPosInfo.y - slideY;

        //�ʒu�𔽉f
        TitleWindow.localPosition = TitlePos;
        InfoWindow.localPosition = InfoPos;


        //���E�̖��̏�����
        LeftPos = DeltaLeft.localPosition;
        RightPos = DeltaRight.localPosition;


        //�E�B���h�E�S�̂��̑傫�������Ȃ�
        thisWindowRect = this.GetComponent<RectTransform>();
        startScale = thisWindowRect.localScale;
        scalePercent = 0;
        thisWindowRect.localScale = startScale * scalePercent;

    }


    // ����鎞�̃A�j���[�V���������B�������I����false��Ԃ��B
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


        //�w�i�����X�ɈÂ����Ă�������
        if (0 <= activeFrame && activeFrame <= fadeInTime)
        {
            //�ŏ��̃t���[���̏���
            if (activeFrame == 0)
            {
                bgColor.a = 0;
                backGround.color = bgColor;

            }


            //�����ȍ����w�i�����X�ɔZ�����Ă���
            bgColor.a += 1.0f / fadeInTime;

            if (activeFrame == fadeInTime)
            {
                bgColor.a = 1.0f;
            }

            backGround.color = bgColor;
        }

        //�E�B���h�E���X���C�h���鏈��
        else if (fadeInTime < activeFrame && activeFrame <= slideTime)
        {


            TitlePos.x += -((float)slideX / (float)(slideTime - fadeInTime));
            InfoPos.y += (float)slideY / (float)(slideTime - fadeInTime);

            if (activeFrame == slideTime)
            {
                TitlePos.x = StartPosTitle.x;
                InfoPos.y = StartPosInfo.y;
            }
            TitleWindow.localPosition = TitlePos;
            InfoWindow.localPosition = InfoPos;
        }


        return workFlag;
    }


    // �����鎞�̃A�j���[�V���������B�������I����True��Ԃ��B
    private bool DisappearAnimation()
    {
        bool workFlag = false;

        int slideTime = 20;
        int fadeOutTime = slideTime + 20;

        int endTime = fadeOutTime;


        //�A�j���[�V�������쒆��true�A�I�������false;
        if (disappearFrame <= endTime)
        {
            workFlag = true;
        }
        else
        {
            workFlag = false;
        }
        //�E�B���h�E���X���C�h���Ă����A�j���[�V����
        if (0 <= disappearFrame && disappearFrame <= slideTime)
        {
            if (disappearFrame == 0)
            {

            }
            TitlePos.x += (float)slideX / (float)slideTime;
            InfoPos.y += -(float)slideY / (float)slideTime;

            if (disappearFrame == slideTime)
            {
                TitlePos.x = StartPosTitle.x + slideX;
                InfoPos.y = StartPosInfo.y - slideY;
            }
            TitleWindow.localPosition = TitlePos;
            InfoWindow.localPosition = InfoPos;

        }
        //�w�i�����邭�Ȃ�A�j���[�V����
        else if (slideTime < disappearFrame && disappearFrame <= fadeOutTime)
        {
            bgColor.a -= 1.0f / (float)(fadeOutTime - slideTime);
            if (disappearFrame == fadeOutTime)
            {
                bgColor.a = 0;
            }

            backGround.color = bgColor;
        }
        else
        {

        }

        if (disappearFrame <= endTime)
        {
            disappearFrame++;
        }

        return workFlag;
    }


    // �ڍ׏��̃E�B���h�E��On�EOFF��؂�ւ�����B
    public void InfoOn()
    {
        activeFlag = true;      //�������A�N�e�B�u��
        activeFrame = 0;

        //�E�B���h�E�S�̂̑傫����ʏ�T�C�Y�ɁB
        scalePercent = 1;
        thisWindowRect.localScale = startScale * scalePercent;

        phaseChangeFlag = false;

        this.gameObject.SetActive(true);
    }

    public void InfoOff()
    {
        activeFlag = false;
        disappearFrame = 0;

        scalePercent = 0;

    }


    //���E�ɏo�Ă�����̏���
    private void DeltaAnimation()
    {
        if (GetArchivesCount() < 2)
        {
            DeltaLeft.gameObject.SetActive(false);
            DeltaRight.gameObject.SetActive(false);
        }
        else
        {
            DeltaLeft.gameObject.SetActive(true);
            DeltaRight.gameObject.SetActive(true);
        }


        movePoint.x = activeFrame % 30;

        DeltaLeft.localPosition = LeftPos + movePoint;
        DeltaRight.localPosition = RightPos - movePoint;
    }





}

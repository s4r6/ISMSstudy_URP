using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UniRx;
using TMPro;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    private int ReceivedClientStageNum; //�ʐM���肩�瑗���Ă����X�e�[�W�f�[�^

    public string[] StageDataFiles;   //�X�e�[�W��񂪏����ꂽ�t�@�C���̖��O�B�t�@�C������ǂ݂��ށB

    [SerializeField] private string FolderName = "/SmallOffice"; //�X�e�[�W�f�[�^�������Ă�t�H���_�[���B

    [SerializeField] string stageNameFilePath = "/StageNames.txt"; //�g���q�ɒ��ӁB

    private UDPConnection myUdpConnection;

    private ReactiveProperty<bool> LoadEndFlag = new ReactiveProperty<bool>(false);

    public IObservable<bool> OnLoadEndFlagChanged
    {
        get { return LoadEndFlag; }
    }
    

    

    private bool GetStageNumFlag = false;   //�ʐM���肩��X�e�[�W�f�[�^���󂯎������True�ɂȂ�t���O

    private int StageNum;
    public int GetStageNum
    {
        get { return StageNum; }
    }



    private void Start()
    {
#if UNITY_WEBGL
        #region
        //StartCoroutine(GetWebStageDataProcess_WebGL());
        //myUdpConnection = GameObject.Find("udpClient").GetComponent<UDPConnection>();

        /*myUdpConnection.OnStageNumChanged
            .Where(value => value != 100)
            .Subscribe(value => SetClientStageDate(value))
            .AddTo(this);*/
        StartCoroutine(GetWebStageDataProcess_WebGL());
        #endregion
#endif

#if UNITY_EDITOR
        #region
        StartCoroutine(GetWebStageDataProcess());
        #endregion
#endif


        //StartCoroutine(DownloadStageData(Application.streamingAssetsPath + FolderName + myStageManager.StageDataFiles[stageNum]));
    }

    //�O���t�@�C������X�e�[�W�f�[�^��ǂݍ��ޏ���
    private IEnumerator GetWebStageDataProcess_WebGL()
    {
        //�ʐM����Ɠ���̃X�e�[�W��ǂݍ���
        yield return DownloadStageName(Application.streamingAssetsPath + FolderName + stageNameFilePath);
        if(PlayerData.M_Authority == PlayerAuthority.RoomHost)
        {
            Debug.Log("���̓z�X�g�ł�");
            StageData.StageID = UnityEngine.Random.Range(0, StageDataFiles.Length);
        }
        else
        {
            Debug.Log("���̓N���C�A���g�ł�");
        }
        yield return DownloadStageData(Application.streamingAssetsPath + FolderName + StageDataFiles[StageData.StageID]);
        //UDPConnection.instance.SendStageData();
    }

    private IEnumerator GetWebStageDataProcess()
    {
        yield return DownloadStageName(Application.streamingAssetsPath + FolderName + stageNameFilePath);

        StageData.StageID = UnityEngine.Random.Range(0, StageDataFiles.Length);

#if UNITY_EDITOR

#elif UNITY_WEBGL
        yield return new WaitUntil(() => PlayerData.SetAuthority == true);
#endif

        yield return DownloadStageData(Application.streamingAssetsPath + FolderName + StageDataFiles[StageData.StageID]);
    }

    // �X�e�[�W���̒����I�u�W�F�N�g�Ɋւ���CSV�f�[�^��ǂݍ���
    private IEnumerator DownloadStageName(string thisURL)
    {
        UnityWebRequest DLData = UnityWebRequest.Get(thisURL);
        yield return DLData.SendWebRequest();

        // ���s��
        if (!string.IsNullOrEmpty(DLData.error))
        {
            Debug.Log("�擾���s", gameObject);
        }
        else
        {
            List<string> stageNameList = new List<string>();
            StringReader reader = new StringReader(DLData.downloadHandler.text);
            // ��s���z��Ɋi�[����
            while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
            {
                Debug.Log("�ǂݎ��");
                string line = reader.ReadLine(); // ��s���ǂݍ���
                stageNameList.Add(line);
                //print(line);

            }

            StageDataFiles = stageNameList.ToArray();

        }

    }
    private IEnumerator DownloadStageData(string thisFileURL)
    {
        print("CSVDataLoad Start");

        UnityWebRequest DLCsvData = UnityWebRequest.Get(thisFileURL);
        yield return DLCsvData.SendWebRequest();

        // ���s��
        if (!string.IsNullOrEmpty(DLCsvData.error))
        {
            Debug.Log("�擾���s:" + DLCsvData.error);
        }
        else
        {
            print("CSVDataLoad End");
            List<string[]> thisList = new List<string[]>();

            StringReader reader = new StringReader(DLCsvData.downloadHandler.text);

            // �J���}�ŕ�������s���ǂݍ���,���X�g�ɒǉ����Ă���
            while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
            {
                Debug.Log("�ړ���ǂݎ��");
                string line = reader.ReadLine(); // ��s���ǂݍ���
                thisList.Add(line.Split(',')); // �J���}��؂�Ń��X�g�ǉ�
            }

            StageData.SetSurveyObjList(thisList);
            Debug.Log("�X�e�[�W�ǂݍ��݊���");
            LoadEndFlag.Value = true;
        }

    }

    // private�ϐ��ɃA�N�Z�X����֐� ******************************************************************/
    public void SetClientStageDate(int x)
    {
        ReceivedClientStageNum = x;
        Debug.Log(ReceivedClientStageNum);
        GetStageNumFlag = true;
    }
}
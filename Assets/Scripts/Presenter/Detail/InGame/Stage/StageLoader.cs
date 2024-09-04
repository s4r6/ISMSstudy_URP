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
    private int ReceivedClientStageNum; //通信相手から送られてきたステージデータ

    public string[] StageDataFiles;   //ステージ情報が書かれたファイルの名前。ファイルから読みこむ。

    [SerializeField] private string FolderName = "/SmallOffice"; //ステージデータが入ってるフォルダー名。

    [SerializeField] string stageNameFilePath = "/StageNames.txt"; //拡張子に注意。

    private UDPConnection myUdpConnection;

    private ReactiveProperty<bool> LoadEndFlag = new ReactiveProperty<bool>(false);

    public IObservable<bool> OnLoadEndFlagChanged
    {
        get { return LoadEndFlag; }
    }
    

    

    private bool GetStageNumFlag = false;   //通信相手からステージデータを受け取ったらTrueになるフラグ

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

    //外部ファイルからステージデータを読み込む処理
    private IEnumerator GetWebStageDataProcess_WebGL()
    {
        //通信相手と同一のステージを読み込む
        yield return DownloadStageName(Application.streamingAssetsPath + FolderName + stageNameFilePath);
        if(PlayerData.M_Authority == PlayerAuthority.RoomHost)
        {
            Debug.Log("私はホストです");
            StageData.StageID = UnityEngine.Random.Range(0, StageDataFiles.Length);
        }
        else
        {
            Debug.Log("私はクライアントです");
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

    // ステージ内の調査オブジェクトに関するCSVデータを読み込む
    private IEnumerator DownloadStageName(string thisURL)
    {
        UnityWebRequest DLData = UnityWebRequest.Get(thisURL);
        yield return DLData.SendWebRequest();

        // 失敗時
        if (!string.IsNullOrEmpty(DLData.error))
        {
            Debug.Log("取得失敗", gameObject);
        }
        else
        {
            List<string> stageNameList = new List<string>();
            StringReader reader = new StringReader(DLData.downloadHandler.text);
            // 一行ずつ配列に格納する
            while (reader.Peek() != -1) // reader.Peaekが-1になるまで
            {
                Debug.Log("読み取り");
                string line = reader.ReadLine(); // 一行ずつ読み込み
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

        // 失敗時
        if (!string.IsNullOrEmpty(DLCsvData.error))
        {
            Debug.Log("取得失敗:" + DLCsvData.error);
        }
        else
        {
            print("CSVDataLoad End");
            List<string[]> thisList = new List<string[]>();

            StringReader reader = new StringReader(DLCsvData.downloadHandler.text);

            // カンマで分割しつつ一行ずつ読み込み,リストに追加していく
            while (reader.Peek() != -1) // reader.Peaekが-1になるまで
            {
                Debug.Log("移動後読み取り");
                string line = reader.ReadLine(); // 一行ずつ読み込み
                thisList.Add(line.Split(',')); // カンマ区切りでリスト追加
            }

            StageData.SetSurveyObjList(thisList);
            Debug.Log("ステージ読み込み完了");
            LoadEndFlag.Value = true;
        }

    }

    // private変数にアクセスする関数 ******************************************************************/
    public void SetClientStageDate(int x)
    {
        ReceivedClientStageNum = x;
        Debug.Log(ReceivedClientStageNum);
        GetStageNumFlag = true;
    }
}
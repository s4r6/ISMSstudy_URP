using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;
using Cysharp.Threading.Tasks;
using ISMS.Data;
namespace ISMS.Presenter.Detail.Stage
{
    public class CSVReader : IRepository
    {
        string[] StageDataFiles;    //ステージ情報が書かれたファイルの名前
        readonly string FolderName = "/SmallOffice";    //ステージデータのあるフォルダー名
        readonly string stageNameFilePath = "/StageNames.txt";  //ステージデータの名前の一覧とファイルパスが書かれたファイル

        readonly ObjectDictionary _objDictionary;

        public CSVReader()
        {
            _objDictionary = new ObjectDictionary();
        }

        public async 

        async UniTask GetWebStageData()
        {
            await DownloadStageNameList(Application.streamingAssetsPath + FolderName + stageNameFilePath);  //ステージの名前の一覧を取得
            
        }

        //StageDataFilesにステージデータの名前の一覧をセット
        async UniTask DownloadStageNameList(string thisURL)
        {
            UnityWebRequest DLData = UnityWebRequest.Get(thisURL);
            await DLData.SendWebRequest();

            // 失敗時
            if (!string.IsNullOrEmpty(DLData.error))
            {
                Debug.Log("取得失敗");
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

        //リストから名前指定でステージデータを取得
        async UniTask DownloadStageData(string thisFileURL)
        {
            UnityWebRequest DLCsvData = UnityWebRequest.Get(thisFileURL);
            await DLCsvData.SendWebRequest();

            // 失敗時
            if (!string.IsNullOrEmpty(DLCsvData.error))
            {
                Debug.Log("取得失敗:" + DLCsvData.error);
            }
            else
            {
                List<string[]> thisList = new List<string[]>();

                StringReader reader = new StringReader(DLCsvData.downloadHandler.text);

                // カンマで分割しつつ一行ずつ読み込み,リストに追加していく
                while (reader.Peek() != -1) // reader.Peaekが-1になるまで
                {
                    Debug.Log("移動後読み取り");
                    string line = reader.ReadLine(); // 一行ずつ読み込み
                    thisList.Add(line.Split(',')); // カンマ区切りでリスト追加
                }
                Debug.Log("ステージ読み込み完了");

                SetObjData(thisList);
            }
        }

        void SetObjData(List<string[]> objData)
        {
            foreach(var data in objData)
            {
                var system = data[0];
                var obj = data[1];
                var describe = data[3];
                var explanation = data[4];
                var risk = int.Parse(data[2]);

                ISMS.Data.Object _obj = new ISMS.Data.Object(system, obj, describe, explanation, risk);
                _objDictionary.AddObject(system, _obj);
            }
        }

        public async UniTask<ObjectDictionary> GetObjectData(int StageNum)  //指定したステージのデータを読み込んでデータのリストを返す
        {
            await DownloadStageData(Application.streamingAssetsPath + FolderName + StageDataFiles[StageNum]);  //引数で指定したステージのデータを取得
            return _objDictionary;
        }


    }
}

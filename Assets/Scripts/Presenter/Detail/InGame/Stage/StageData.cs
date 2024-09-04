using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StageData
{
    private static int riskObjNum;     //読み込んだステージデータにあるリスクの数

    static List<string[]> SurveyObjList;   // CSVファイルから読み込んだ調査オブジェクトのデータ格納.

    private static int SurveyingObjIndex;    //調査中のオブジェトのインデックス番号

    private static int stageID; //読み込むステージID
    public static int StageID
    {
        get
        {
            return stageID;
        }
        set
        {
            stageID = value;
        }
    }


    public static void SetSurveyObjList(List<string[]> list)
    {

        SurveyObjList = list;

        //リスクオブジェクトの数をカウント
        riskObjNum = 0;
        for (int i = 0; i < SurveyObjList.Count; i++)
        {
            //Debug.Log("オブジェクトセット");
            //Debug.Log(SurveyObjList[i][0]);
            //1の場合はリスクあるオブジェクトなのでカウント
            if (SurveyObjList[i][2] == "1")
            {
                riskObjNum++;
            }
            //-1の場合オブジェクトを消す事ができる。
            else if (SurveyObjList[i][2] == "-1")
            {
                //Debug.Log("-1を検出しました");
                //GameObject.Find(SurveyObjList[i][0]).SetActive(false);
            }
        }
    }
    

    //オブジェクトの名前を入れるとListから検索して返してくれる関数
    public static string[] GetObjNum(string obj_name)
    {
        int rtIndexNum = 0; // 0はunknown
        Debug.Log("SeachData");
        for (int i = 0; i < SurveyObjList.Count; i++)
        {
            //同じ名前が見つかったら、その時のIndex番号を取得。処理から抜け出す。
            if (SurveyObjList[i][0] == obj_name)
            {
                Debug.Log("rtIndexNum:" + i);
                rtIndexNum = i;
                break;
            }

        }
        SurveyingObjIndex = rtIndexNum;
        return SurveyObjList[rtIndexNum];
    }

    public static string[] GetSurveyingObj()   //現在調査中のオブジェクトを返す
    {
        return SurveyObjList[SurveyingObjIndex];
    }
    // 特定のIndex番号のデータを取得する。
    public static string[] GetData(int index)
    {
        return SurveyObjList[index];
    }
    //ステージにあるリスクの数を取得。
    public static int GetRiskNum()
    {
        return riskObjNum;
    }
}

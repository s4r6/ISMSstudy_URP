using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;

public class GameData
{ 
    private static GameData gameData;
    private int StageID { get; set; }
    private List<string> SurveyedObjList;
    private int judgeCount = 0; //正誤判定を行った回数
    public int JudgeCount   
    {
        get { return judgeCount; }
        set { judgeCount += value; }
    }
    private int correctCount = 0;   //正解した回数
    public int CorrectCount
    {
        get { return correctCount; }
        set { correctCount += value; }
    }

    private Stopwatch gameTime = new Stopwatch();
    private GameData()  //コンストラクタ
    {
        UnityEngine.Debug.Log("コンストラクタ");
        gameTime.Start();   //時間の計測開始
    }

    public static GameData GetInstance()    //シングルトン実装
    {
        if(gameData == null)
        {
            UnityEngine.Debug.Log("GameData生成");
            gameData = new GameData();
            return gameData;
        }
        return gameData;
    }

    public void SetObjList(string ObjName)    //調査済みのオブジェクト追加
    {
        gameData.SurveyedObjList.Add(ObjName);
        foreach(var item in SurveyedObjList)
        {
            UnityEngine.Debug.Log(item);
        }
    }

    public List<string> GetSurveiedObjList()  //調査済みのオブジェクト取得
    {
        return gameData.SurveyedObjList;
    }

    public string GetLastSurveiedObjName()
    {
        return gameData.SurveyedObjList[SurveyedObjList.Count - 1];
    }

    public TimeSpan GetClearTime()  //クリアまでの時間を取得
    {
        gameData.gameTime.Stop();
        return gameData.gameTime.Elapsed; //かかった時間をhh:mm:ss形式で返す
    }

}

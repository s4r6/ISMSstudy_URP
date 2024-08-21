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
    private int judgeCount = 0; //���딻����s������
    public int JudgeCount   
    {
        get { return judgeCount; }
        set { judgeCount += value; }
    }
    private int correctCount = 0;   //����������
    public int CorrectCount
    {
        get { return correctCount; }
        set { correctCount += value; }
    }

    private Stopwatch gameTime = new Stopwatch();
    private GameData()  //�R���X�g���N�^
    {
        UnityEngine.Debug.Log("�R���X�g���N�^");
        gameTime.Start();   //���Ԃ̌v���J�n
    }

    public static GameData GetInstance()    //�V���O���g������
    {
        if(gameData == null)
        {
            UnityEngine.Debug.Log("GameData����");
            gameData = new GameData();
            return gameData;
        }
        return gameData;
    }

    public void SetObjList(string ObjName)    //�����ς݂̃I�u�W�F�N�g�ǉ�
    {
        gameData.SurveyedObjList.Add(ObjName);
        foreach(var item in SurveyedObjList)
        {
            UnityEngine.Debug.Log(item);
        }
    }

    public List<string> GetSurveiedObjList()  //�����ς݂̃I�u�W�F�N�g�擾
    {
        return gameData.SurveyedObjList;
    }

    public string GetLastSurveiedObjName()
    {
        return gameData.SurveyedObjList[SurveyedObjList.Count - 1];
    }

    public TimeSpan GetClearTime()  //�N���A�܂ł̎��Ԃ��擾
    {
        gameData.gameTime.Stop();
        return gameData.gameTime.Elapsed; //�����������Ԃ�hh:mm:ss�`���ŕԂ�
    }

}

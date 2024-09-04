using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StageData
{
    private static int riskObjNum;     //�ǂݍ��񂾃X�e�[�W�f�[�^�ɂ��郊�X�N�̐�

    static List<string[]> SurveyObjList;   // CSV�t�@�C������ǂݍ��񂾒����I�u�W�F�N�g�̃f�[�^�i�[.

    private static int SurveyingObjIndex;    //�������̃I�u�W�F�g�̃C���f�b�N�X�ԍ�

    private static int stageID; //�ǂݍ��ރX�e�[�WID
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

        //���X�N�I�u�W�F�N�g�̐����J�E���g
        riskObjNum = 0;
        for (int i = 0; i < SurveyObjList.Count; i++)
        {
            //Debug.Log("�I�u�W�F�N�g�Z�b�g");
            //Debug.Log(SurveyObjList[i][0]);
            //1�̏ꍇ�̓��X�N����I�u�W�F�N�g�Ȃ̂ŃJ�E���g
            if (SurveyObjList[i][2] == "1")
            {
                riskObjNum++;
            }
            //-1�̏ꍇ�I�u�W�F�N�g�����������ł���B
            else if (SurveyObjList[i][2] == "-1")
            {
                //Debug.Log("-1�����o���܂���");
                //GameObject.Find(SurveyObjList[i][0]).SetActive(false);
            }
        }
    }
    

    //�I�u�W�F�N�g�̖��O�������List���猟�����ĕԂ��Ă����֐�
    public static string[] GetObjNum(string obj_name)
    {
        int rtIndexNum = 0; // 0��unknown
        Debug.Log("SeachData");
        for (int i = 0; i < SurveyObjList.Count; i++)
        {
            //�������O������������A���̎���Index�ԍ����擾�B�������甲���o���B
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

    public static string[] GetSurveyingObj()   //���ݒ������̃I�u�W�F�N�g��Ԃ�
    {
        return SurveyObjList[SurveyingObjIndex];
    }
    // �����Index�ԍ��̃f�[�^���擾����B
    public static string[] GetData(int index)
    {
        return SurveyObjList[index];
    }
    //�X�e�[�W�ɂ��郊�X�N�̐����擾�B
    public static int GetRiskNum()
    {
        return riskObjNum;
    }
}

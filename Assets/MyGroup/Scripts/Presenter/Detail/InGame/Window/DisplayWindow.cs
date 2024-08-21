using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DisplayWindow
{
    private const int DISPLAYTIME = 6;
    private const int HIDDENTIME = 6;

    public static IEnumerator AppearAnimation(GameObject Canvas)
    {
        int NowFlame = 0;
        float ScalePercent = 0;
        Vector3 StartScale = new Vector3(1, 1, 1);
        while (NowFlame <= DISPLAYTIME)  //�w��t���[���܂ŌJ��Ԃ�
        {
            yield return null;
            ScalePercent += 1.0f / DISPLAYTIME;     //���t���[���傫���Ȃ��Ă���

            if (NowFlame != DISPLAYTIME)    //�v�Z�덷�΍�ŏꍇ����
                Canvas.transform.localScale = StartScale * ScalePercent;
            else
                Canvas.transform.localScale = StartScale * 1;

            Debug.Log("NowFlame:" + NowFlame);
            Debug.Log("ScalePercent:" + ScalePercent);
            Debug.Log("�L�����o�X�T�C�Y:"+Canvas.transform.localScale);
            Debug.Log("�I�u�W�F�N�g�̏��" + Canvas.activeSelf);
            //Debug.Log("Docs�T�C�Y:" + DocsWindow.instance.transform.localScale);
            NowFlame++;
        }
    }

    public static IEnumerator HiddenAnimation(GameObject Canvas)
    {
        int NowFlame = 0;
        float ScalePercent = 1;
        Vector3 StartScale = new Vector3(1, 1, 1);
        while (NowFlame <= HIDDENTIME)  //�w��t���[���܂ŌJ��Ԃ�
        {
            yield return null;
            ScalePercent -= 1.0f / DISPLAYTIME;     //���t���[���������Ȃ��Ă���

            if (NowFlame != HIDDENTIME)    //�v�Z�덷�΍�ŏꍇ����
                Canvas.transform.localScale = StartScale * ScalePercent;
            else
                Canvas.transform.localScale = StartScale * 0;

            NowFlame++;
        }
    }
}

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
        while (NowFlame <= DISPLAYTIME)  //指定フレームまで繰り返す
        {
            yield return null;
            ScalePercent += 1.0f / DISPLAYTIME;     //毎フレーム大きくなっていく

            if (NowFlame != DISPLAYTIME)    //計算誤差対策で場合分け
                Canvas.transform.localScale = StartScale * ScalePercent;
            else
                Canvas.transform.localScale = StartScale * 1;

            Debug.Log("NowFlame:" + NowFlame);
            Debug.Log("ScalePercent:" + ScalePercent);
            Debug.Log("キャンバスサイズ:"+Canvas.transform.localScale);
            Debug.Log("オブジェクトの状態" + Canvas.activeSelf);
            //Debug.Log("Docsサイズ:" + DocsWindow.instance.transform.localScale);
            NowFlame++;
        }
    }

    public static IEnumerator HiddenAnimation(GameObject Canvas)
    {
        int NowFlame = 0;
        float ScalePercent = 1;
        Vector3 StartScale = new Vector3(1, 1, 1);
        while (NowFlame <= HIDDENTIME)  //指定フレームまで繰り返す
        {
            yield return null;
            ScalePercent -= 1.0f / DISPLAYTIME;     //毎フレーム小さくなっていく

            if (NowFlame != HIDDENTIME)    //計算誤差対策で場合分け
                Canvas.transform.localScale = StartScale * ScalePercent;
            else
                Canvas.transform.localScale = StartScale * 0;

            NowFlame++;
        }
    }
}

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


    //アーカイブのリスト変数
    private List<string[]> archivesList = new List<string[]>();
    private int nowListIndex;

    //ウィンドウがオンかどうか
    bool activeFlag;

    //ウィンドウがオンになってからのフレーム数を記録する変数。
    private int activeFrame;

    //ウィンドウがオフになってからのフレーム数を記録
    private int disappearFrame;

    //プレイヤーの入力を受け付けるかどうかのフラグ
    private bool InputReceptionFlag;

    //フェーズを切り替えるかどうかのフラグ
    private bool phaseChangeFlag;


    //リスク判定が行われているかどうかのマークのヤツ。
    public TextMeshProUGUI TMPJudgedMark;


    /* Unity側で自動で呼び出される特殊な関数群 **********************************************************/

    // Start is called before the first frame update
    void Start()
    {
        //インスタンスの初期化
        if (instance == null)
        {
            instance = this;
        }

        
        // Start()の呼ばれるタイミングの問題で、
        // 他オブジェクトの関数を関数をStart()で呼ぶのは止めた方が良さそう。
        //archivesList.Add(DetailInfoWindow.instance.GetData(1));

        // アーカイブリストが空だとまずいので、初期化用のデータを突っ込む。
        // AddArchivesを初回呼んだ時にこのデータは削除されるようになっている。
        string nowTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string[] initData = { "none", "None", "0", "データが追加されていません", nowTime };
        archivesList.Add(initData);

        //インデックス番号の初期化
        nowListIndex = 0;


        WindowDrawInit();

        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (activeFlag == true)
        {
            //ウィンドウのアニメーションが動いている間は入力を禁止する。
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




    /* 自作関数群 ****************************************************************************************/


    /**** アーカイブ機能に関連 **********************************************************/

    // アーカイブにデータを追加する。
    public bool AddArchives(string[] datas)
    {
        bool rt = false;
        //同名のデータが無い場合は、アーカイブに追加。
        if (SeachData(datas[0]) == -1)
        {
            //初期化の際に追加したnoneの情報がある時、そいつを削除。
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

    // 引数のオブジェクト名のデータを探す
    private int SeachData(string obj_name)
    {
        int rtIndexNum = -1;


        for (int i = 0; i < archivesList.Count; i++)
        {
            //同じ名前が見つかったら、その時のIndex番号を取得。処理から抜け出す。
            if (archivesList[i][0] == obj_name)
            {
                rtIndexNum = i;
                break;
            }
        }
        //発見されれば正の値が、未発見なら-1が返る。
        return rtIndexNum;
    }

    // 表示する情報を次に進める。リストの最後に行くと最初に戻る。
    public void IndexNext()
    {
        nowListIndex++;
        if (archivesList.Count <= nowListIndex)
        {
            nowListIndex = 0;
        }


    }

    // 表示する情報を前に戻す。リストの最初に行くと最後に戻る。
    public void IndexBefore()
    {
        nowListIndex--;
        if (nowListIndex < 0)
        {
            nowListIndex = archivesList.Count - 1;
        }

    }

    //アーカイブされたデータをファイルに出力する。
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
                //MessageManager.instance.SetMessage("アーカイブファイル出力");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            MessageManager.instance.SetMessage("ファイル出力できませんでした");
        }

    }



    /**** Get関連 *******************************************************************************/

    //現在アーカイブに追加されているデータ数を返す
    public int GetArchivesCount()
    {
        return archivesList.Count;
    }

    //アーカイブの現在のデータを返す。
    public string[] GetNowData()
    {
        return archivesList[nowListIndex];
    }

    //プレイヤーからの入力を受け付けるかどうか
    public bool GetInputReception()
    {
        return InputReceptionFlag;
    }

    //フェーズを変える許可を出す
    public bool GetPhaseChange()
    {
        if (activeFlag == false && phaseChangeFlag == true)
        {
            return true;
        }
        else return false;
    }




    /**** 描画関連 *******************************************************************************/

    /* 描画用の変数 **********************************************************/

    //テキストを変更したりするための変数
    [SerializeField]
    private TextMeshProUGUI ObjNameJP, ObjDetail, TMPArchivesNumver;


    //スライドするアニメーション用
    [SerializeField]
    private RectTransform TitleWindow, InfoWindow;

    //スライドアニメーション用のポジション
    private Vector2 StartPosTitle, StartPosInfo;
    private Vector2 TitlePos, InfoPos;

    //背景の黒いやつの処理用変数
    [SerializeField]
    private Image backGround;

    private Color bgColor;
    private Vector3 bgStartScale;

    // ウィンドウ全体のの大きさを制御するやつ
    private RectTransform thisWindowRect;
    private Vector3 startScale;
    private float scalePercent;


    //アーカイブが増えると出てくる左右の矢印
    [SerializeField]
    private Transform DeltaLeft, DeltaRight;
    private Vector3 LeftPos, RightPos, movePoint;


    float slideX = 2000;
    float slideY = 1200;

    //ウィンドウ描画系の初期化
    private void WindowDrawInit()
    {

        //背景のやつの最初の大きさと色を保存しておく。
        bgStartScale = backGround.transform.localScale;
        bgColor = backGround.color;

        //ウィンドウのアニメーション用の処理。
        //ウィンドウの初期位置を保存
        TitlePos = StartPosTitle = TitleWindow.localPosition;
        InfoPos = StartPosInfo = InfoWindow.localPosition;

        //初期位置からスライドさせる
        TitlePos.x = StartPosTitle.x + slideX;
        InfoPos.y = StartPosInfo.y - slideY;

        //位置を反映
        TitleWindow.localPosition = TitlePos;
        InfoWindow.localPosition = InfoPos;


        //左右の矢印の初期化
        LeftPos = DeltaLeft.localPosition;
        RightPos = DeltaRight.localPosition;


        //ウィンドウ全体をの大きさ処理など
        thisWindowRect = this.GetComponent<RectTransform>();
        startScale = thisWindowRect.localScale;
        scalePercent = 0;
        thisWindowRect.localScale = startScale * scalePercent;

    }


    // 現れる時のアニメーション処理。処理が終わるとfalseを返す。
    private bool AppearAnimation()
    {
        bool workFlag = false;

        int fadeInTime = 30;
        int slideTime = fadeInTime + 30;

        int endTime = slideTime;


        //アニメーション動作中はtrue、終わったらfalse;
        if (activeFrame <= endTime)
        {
            workFlag = true;
        }
        else
        {
            workFlag = false;
        }


        //背景を徐々に暗くしていく処理
        if (0 <= activeFrame && activeFrame <= fadeInTime)
        {
            //最初のフレームの処理
            if (activeFrame == 0)
            {
                bgColor.a = 0;
                backGround.color = bgColor;

            }


            //透明な黒い背景を徐々に濃くしていく
            bgColor.a += 1.0f / fadeInTime;

            if (activeFrame == fadeInTime)
            {
                bgColor.a = 1.0f;
            }

            backGround.color = bgColor;
        }

        //ウィンドウがスライドする処理
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


    // 消える時のアニメーション処理。処理が終わるとTrueを返す。
    private bool DisappearAnimation()
    {
        bool workFlag = false;

        int slideTime = 20;
        int fadeOutTime = slideTime + 20;

        int endTime = fadeOutTime;


        //アニメーション動作中はtrue、終わったらfalse;
        if (disappearFrame <= endTime)
        {
            workFlag = true;
        }
        else
        {
            workFlag = false;
        }
        //ウィンドウがスライドしていくアニメーション
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
        //背景が明るくなるアニメーション
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


    // 詳細情報のウィンドウのOn・OFFを切り替えるやつら。
    public void InfoOn()
    {
        activeFlag = true;      //処理をアクティブに
        activeFrame = 0;

        //ウィンドウ全体の大きさを通常サイズに。
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


    //左右に出てくる矢印の処理
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

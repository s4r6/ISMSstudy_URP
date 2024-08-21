using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using System;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine.UI;
using Unity.VisualScripting;

public class RiskDiscover : MonoBehaviour
{
    public static RiskDiscover instance;
    private GameData _gameData;

    private List<string> judgeObjList = new List<string>();


    private string judgeObjName;

    private int RiskFlag;

    private bool anserMarkFlag;

    private bool InputReceptionFlag;


    private bool activeFlag;

    private int activeFrame;
    
    private int disappearFrame;

    //private UDPConnection myUdpConnection;

    /*** Unity側で自動で呼ばれる関数群 ******************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _gameData = GameData.GetInstance(); //ゲームの進行状況管理クラスの取得
  
        /*WindowDrawInit();
        InfoOff();
        this.gameObject.SetActive(false);
        //myUdpConnection = GameObject.Find("udpClient").GetComponent<UDPConnection>();

        /*myUdpConnection.OnObjNameChanged
                       .Where(value => value != null)   //valueがnullじゃないとき
                       .Subscribe(value => OnValueChanged(value))
                       .AddTo(this);*/

        /*myUdpConnection.OnCountNumChanged
                       .Where(value => value != 0)  //0じゃないとき
                       .Subscribe(value => SetCountNum(value))
                       .AddTo(this);

        myUdpConnection.OnAnswerNumChanged
                       .Where(value => value != 0)
                       .Subscribe(value => JudgeAnswerNum(value))
                       .AddTo(this);*/
    }


    // Update is called once per frame
    void Update()
    {
        /*if (activeFlag == true)
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
            // フレーム数更新
            activeFrame++;
        }
        else
        {
            //ウィンドウのアニメーションが動いている間は入力を禁止する。
            if (DisappearAnimation() == true)
            {
                InputReceptionFlag = false;
            }
            else
            {
                InputReceptionFlag = true;
                this.gameObject.SetActive(false);
            }
            disappearFrame++;


        }*/

        
    }




    /*** 自作の関数群 ************************************************************************************/

    // 引数のオブジェクトデータからリスク発見できたか判定して画面表示を変更する
    public bool DiscoverJudge(string[] surveyObjData)
    {
        //すでにリスク発見コマンドを適用されていないか調べる
        bool judgedFlag = CheckDiscovered(surveyObjData[0]);

        //対象のオブジェクトが発見コマンドを適用されている場合
        if (judgedFlag == true)
        {
            return false;
        }
        //発見コマンドを適用されていない場合、リスク発見画面の成否を判定、表示。
        else
        {
            //アーカイブに何も保存してない時などは処理しない。
            if (surveyObjData[0] == "none")
            {
                return false;
            }

            //judgeObjList.Add(surveyObjData[0]);

            _gameData.SetObjList(surveyObjData[0]);
            //myUdpConnection.SendByte(prefix: "_Nm", msg: surveyObjData[0]); //判定対象のオブジェクトを送信

            judgeObjName = surveyObjData[1];
            TMPTargetobjName.text = judgeObjName;

            RiskFlag = Convert.ToInt32(surveyObjData[2]);//0の場合リスク無し。それ以外は有り
            
            TMPCommentaryDetail.text = surveyObjData[4];
            if (RiskFlag == 0)
            {
                Xmark.SetActive(true);
                anserMarkFlag = false;  //正解不正解のフラグ
                TMPJudgeText.text = "リスク発見：失敗…";
             

            }
            else if (RiskFlag > 0)
            {
                Omark.SetActive(true);
                anserMarkFlag = true;   //正解不正解のフラグ
                TMPJudgeText.text = "リスク発見：成功！";
              
                _gameData.CorrectCount = 1; //正解回数を+1
            }

            _gameData.JudgeCount = 1;   //判定回数を+1

            //myUdpConnection.SendByte(prefix: "_Ct", num: 1); //判定回数を送信
            //if (RiskFlag != 0)
            //myUdpConnection.SendByte(prefix: "_An", num: RiskFlag); //正解不正解を送信
            //UDPConnection.instance.SendGameData(_gameData);
            return true;


        }

    }

    // すでに発見コマンドが適用されたオブジェクトでないか調べる。
    // 適用されている場合：true,されてない場合：false
    public bool CheckDiscovered(string obj_name)
    {
        bool rt = false;
        for (int i = 0; i < _gameData.GetSurveiedObjList().Count; i++)
        {
            if (_gameData.GetSurveiedObjList().Contains(obj_name))
            {
                rt = true;
                break;
            }
        }

        return rt;
    }



    private void CutInObjSetActive(bool flag)
    {
        CutInBack.gameObject.SetActive(flag);
        DangerMark.gameObject.SetActive(flag);
        LoupeMark.gameObject.SetActive(flag);
        MessageText.gameObject.SetActive(flag);

    }

    public bool GetInputReception()
    {
        return InputReceptionFlag;
    }




    /*** ウィンドウの表示や処理を制御する処理 *****************************************************/


   [SerializeField]　public TextMeshProUGUI 
        TMPCommentaryDetail, TMPCommentaryTitle, TMPTargetobjName, TMPJudgeText, MessageText;

    [SerializeField]
    public GameObject Omark, Xmark;

    [SerializeField]
    private RectTransform CutInBack;
    private Vector2 CutInPos;
   

    public RectTransform DangerMark;
    private Vector2 DangerPos;
    private float dangerScale = 1.5f;

    public RectTransform LoupeMark;
    private Vector2 LoupePos;

    [SerializeField] private RectTransform RiskResultWindow;
    private Vector3 startScale;
    private float scalePercent;




    // スケール変えるヤツの初期化。
    private void WindowDrawInit()
    {
        CutInPos = CutInBack.localPosition;

        Omark.SetActive(false);
        Xmark.SetActive(false);
        CutInObjSetActive(false);

        startScale = RiskResultWindow.localScale;
       
        scalePercent = 0;       
        RiskResultWindow.localScale = startScale * scalePercent;
        
        activeFlag = false;
    }

    //現れるアニメーション処理
    private bool AppearAnimation()
    {
        bool workFlag = false;


        int endFrame = 180;


        //アニメーション動作中はtrue、終わったらfalse;
        if (activeFrame <= endFrame)
        {
            workFlag = true;
        }
        else
        {
            workFlag = false;
        }


        if (0 <= activeFrame && activeFrame < 10)
        {
            //危険マークとカットイン背景の表示と初期化
            if (activeFrame == 0)
            {
                CutInBack.gameObject.SetActive(true);
                CutInPos.x = 3580;

                DangerMark.gameObject.SetActive(true);
                DangerMark.localPosition = new Vector2(0, DangerMark.localPosition.y);
                dangerScale = 1.5f;

                InputReceptionFlag = false;
                AudioManager.instance.playSE(13);

            }
            //少しずつスケールを小さく
            DangerMark.localScale = Vector3.one * dangerScale;
            dangerScale -= 1.0f / 10.0f;

        }
        else if (10 <= activeFrame && activeFrame < 20)
        {
            //少しずつスケールを大きく
            DangerMark.localScale = Vector3.one * dangerScale;
            dangerScale += 0.5f / 10.0f;

        }
        else if (30 <= activeFrame && activeFrame < 50)
        {
            //虫眼鏡の初期化と表示
            if (activeFrame == 30)
            {
                LoupeMark.gameObject.SetActive(true);
                LoupePos.x = 650.0f;
                LoupePos.y = LoupeMark.localPosition.y;
            }
            //虫眼鏡のスライド　左方向
            LoupeMark.localPosition = LoupePos;
            LoupePos.x += -1300.0f / 20.0f;

        }
        else if (50 <= activeFrame && activeFrame < 60)
        {
            if (activeFrame == 50)
            {
                LoupePos.x = -650.0f;

            }
            //虫眼鏡のスライド　左方向から中心
            LoupeMark.localPosition = LoupePos;
            LoupePos.x += 650.0f / 10.0f;

        }

        else if (60 <= activeFrame && activeFrame < 70)
        {
            //座標がズレるので一応固定しておく
            if (activeFrame == 60)
            {
                LoupePos.x = 0;
                LoupeMark.localPosition = LoupePos;
            }
        }
        else if (70 <= activeFrame && activeFrame < 90)
        {

            if (activeFrame == 70)
            {
                DangerPos = DangerMark.localPosition;
            }

            //虫眼鏡と危険マークを左にスライド
            LoupeMark.localPosition = LoupePos;
            DangerMark.localPosition = DangerPos;

            LoupePos.x += (-540.0f) / 20.0f;
            DangerPos.x += (-540.0f) / 20.0f;

            if (LoupePos.x < -540.0f)
            {
                LoupePos.x = -540.0f;
            }
            if (DangerPos.x < -540.0f)
            {
                DangerPos.x = -540.0f;
            }
        }
        else if (100 <= activeFrame && activeFrame < 170)
        {
            //「リスク発見！」のテキストを表示
            if (activeFrame == 100)
            {
                MessageText.gameObject.SetActive(true);
                AudioManager.instance.playSE(14);
            }

        }
        else if (170 <= activeFrame && activeFrame < 180)
        {
            //カットインを閉じて、解説を表示する
            if (activeFrame == 170)
            {
                CutInObjSetActive(false);


            }
            //解説のウインドウを徐々に大きくする
            scalePercent += 1.0f / 10;
            if (scalePercent >= 1 )
            {
                scalePercent = 1;
            }
            RiskResultWindow.localScale = startScale * scalePercent;
        }
        else if (activeFrame == 180)
        {
            if (anserMarkFlag == true)
            {
                AudioManager.instance.playSE(1);
            }
            else
            {
                AudioManager.instance.playSE(2);
            }
            //プレイヤーの入力禁止を解く
            InputReceptionFlag = true;
        }

        //カットインアニメーションが動いている間、背景をスライドさせていく
        if (activeFrame < 180)
        {
            CutInPos.x -= (3580 * 2) / (60.0f * 4.0f);
           
            if (CutInPos.x < -3580)
            {
                CutInPos.x = 3580;
               
            }
            CutInBack.localPosition = CutInPos;
        }


        return workFlag;
    }

    private void OnValueChanged(string ObjName)
    {
        Debug.Log("OnValueChangedが呼び出されました");
        if (!judgeObjList.Contains(ObjName))   //受け取ったオブジェクト名がふくまれていなければ
        {
            Debug.Log("このオブジェクトはまだ発見されていません:"+ ObjName);
            Debug.Log("発見済みのオブジェクト数:" + judgeObjList.Count);
            judgeObjList.Add(ObjName);  //判定済みのリストにこのオブジェクトを追加
            for(int i = 0; i < judgeObjList.Count; i++)
            {
                Debug.Log("要素"+i+":"+judgeObjList[i]);
            }
            Debug.Log("更新後:" + judgeObjList.Count);
        }
        //myUdpConnection.SetObjNameProperty = null;
    }

    /*private void SetCountNum(int SetNum)
    {
        judgeCount += SetNum;   //相手側で判定した回数も反映
        myUdpConnection.SetCountNumProperty = 0;    //変数をリセット
    }*/

    /*private void JudgeAnswerNum(int AnswerNum)
    {
        correctCount++;
        Debug.Log(correctCount);
        myUdpConnection.SetAnswerNumProperty = 0;
    }*/

    //消えるアニメーション
    private bool DisappearAnimation()
    {
        bool workFlag = false;

        int endFrame = 6;

        //アニメーション動作中はtrue、終わったらfalse;
        if (disappearFrame <= endFrame)
        {
            workFlag = true;
        }
        else
        {
            workFlag = false;
        }
        if (0 <= disappearFrame && disappearFrame < endFrame)
        {
            scalePercent -= 1.0f / (float)endFrame;
            if (scalePercent <= 0)
            {
                scalePercent= 0;   
            }
            RiskResultWindow.localScale = startScale * scalePercent;

        }
        return workFlag;
    }



    // 詳細情報のウィンドウのOn・OFFを切り替えるやつら。
    public void InfoOn()
    {
        this.gameObject.SetActive(true);
        activeFlag = true;      //処理をアクティブに
        activeFrame = 0;
        //RiskResultWindow.localScale = startScale;

    }

    public void InfoOff()
    {
        activeFlag = false;
        disappearFrame = 0;
        Omark.SetActive(false);
        Xmark.SetActive(false);
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UniRx;
using TMPro;
using System;
using UnityEngine.Networking;

public class DetailInfoWindow : MonoBehaviour
{

    /***** Unityオブジェクトを制御する変数群 ***************************************************************/

    public static DetailInfoWindow instance; // 他のスクリプトから関数を呼び出せるようにするやつ。


    // 画面に配置されたテキストオブジェクトを弄るやつ。インスペクターで紐づけ必須 
    [SerializeField] private TextMeshProUGUI ObjNameJP, ObjDetail, TMPJudgedMark;



    /***** その他変数 ***********************************************************************************************/

    //ファイル名に拡張子を付けるか問題について
    //Unity上でアセットという風に登録されたファイルは拡張子を付けてはダメで
    //登録されてない外部のファイルは必要になるらしい。ややこしい
    //


    [SerializeField] private string FolderName = "/SmallOffice"; //ステージデータが入ってるフォルダー名。

    [SerializeField] string stageNameFilePath = "/StageNames.txt"; //拡張子に注意。

    private string[] StageDataFiles;   //ステージ情報が書かれたファイルの名前。ファイルから読みこむ。

    //List<string[]> SurveyObjList;   // CSVファイルから読み込んだ調査オブジェクトのデータ格納.


    private string hitName = "none";  // 現在プレイヤーの視点が合っているオブジェクト名
    private int hitObjNum = 0;      // hitNameと一致したリストのIndex番号

    private bool SeachDataFlag; //検索処理を繰り返さないようにするフラグ。
    //private int riskObjNum;     //読み込んだステージデータにあるリスクの数

    private bool activeFlag;           // ウィンドウがアクティブかどうか。

    protected bool InputReceptionFlag;  //プレイヤーの入力を受け付けるかのフラグ

    protected bool phaseChangeFlag;     //フェーズを変えてよいかのフラグ

    //private UDPConnection myUdpConnection;
    //private StageManager myStageManager;

    private const int DISCOVERSTATE = 1;
    private const int EXPLORESTATE = 2;



    /* Unity側で自動で呼び出される特殊な関数群 **********************************************************/

    // 初期化処理。Updateの直前に呼ばれる
    void Start()
    {
        //インスタンス生成
        if (instance == null)
        {
            instance = this;
        }


        // //ステージデータの名前をファイルから呼び出す
        //StageDataFiles = GetStageName(Application.streamingAssetsPath + FolderName + stageNameFilePath);

        // //ステージのナンバーをランダムで決定。
        // //最初の引数（最小値）は乱数に含まれるが後の引数（最大値は含まれない）
        // int stageNum =  UnityEngine.Random.Range(0, StageDataFiles.Length);
        // //stageNum = 0;

        // //ステージデータと解説データを読み込み
        // SurveyObjList = CsvDataLoad(Application.streamingAssetsPath + FolderName + StageDataFiles[stageNum]);

        //myUdpConnection = GameObject.Find("udpClient").GetComponent<UDPConnection>();
        //myStageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        //StartCoroutine(GetWebStageDataProcess());

        //var stageNum = myStageManager.GetStageNum;
        //Debug.Log(stageNum);
        //StartCoroutine(DownloadStageData(Application.streamingAssetsPath + FolderName + myStageManager.StageDataFiles[stageNum]));

        //ウィンドウの初期化＆非表示
        //WindowDrawInit();
        

        //ウィンドウを消す
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*//ウィンドウがアクティブな時
        if (activeFlag == true)
        {
            ActiveProcess();
            bool animeWork = AppearAnimation();

            if (animeWork == true)
            {
                InputReceptionFlag = false;
            }
            else
            {
                InputReceptionFlag = true;
            }

            activeFrame++;

        }
        else
        {
            bool animeWork = DisappearAnimation();

            if (animeWork == true)
            {
                phaseChangeFlag = false;
            }
            else
            {
                phaseChangeFlag = true;

                this.gameObject.SetActive(false);
            }

            disappearFrame++;
        }

        //ウィンドウを徐々に大きく(小さく)する処理。
        //ScaleChangeProcess();*/

    }

    //ウィンドウを表示する
    public IEnumerator DisplayDetailInfoWindow()
    {
        this.gameObject.SetActive(true);    //ウィンドウ有効化
        ActiveProcess();    //有効化するオブジェクトのデータを取得
        StartCoroutine(DisplayWindow.AppearAnimation(this.gameObject)); //ウィンドウをだんだん大きく表示する
        yield return null;
    }

    private void ActiveProcess()
    {
        Debug.Log("ActiveProcess");

        //視界の判定にヒットしているオブジェクトの名前を取得
        hitName = CameraRayCast.instance.GetHitObjectName();
        
        Debug.Log(hitName);
        
        //ヒットしているオブジェクトをリストから検索
        //hitObjNum = SeachData(hitName);

        Debug.Log("SeachData EXE");

        var SurveyObject = StageData.GetObjNum(hitName);
        //表示に反映する
        ObjNameJP.text = SurveyObject[1];
        ObjDetail.text = SurveyObject[3];

        Debug.Log("表示に反映");
        //リスク判定を行っているかどうかを更新
        TMPJudgedMark.text = JudgedMarkProcess(hitName);


        //}
    }


    public IEnumerator HiddenDetailInfoWindow()
    {
        IEnumerator DisAppear = DisplayWindow.HiddenAnimation(this.gameObject); //ウィンドウ表示が非表示になるまで待機
        yield return DisAppear;
        this.gameObject.SetActive(false);
    }

    public int ActionWindow()
    {
        Debug.Log("入力待ち");
        if (GameInput.instance._BackAction.triggered == true)
        {
            Debug.Log("ウィンドウ終了");
            AudioManager.instance.playSE(0);
            StartCoroutine(HiddenDetailInfoWindow());
            return EXPLORESTATE;
        }
        // L1ボタンが押されたら、今表示されている情報をアーカイブに追加する。
        if (GameInput.instance._ArchivesAction.triggered == true)
        {
            // 詳細情報ウィンドウに表示されている情報をアーカイブに追加する
            // 追加成功でtrue すでに追加されている場合はfalse
            bool addFlag = ArchivesManager.instance.AddArchives(StageData.GetSurveyingObj());
            if (addFlag == true)
            {
                MessageManager.instance.SetMessage("アーカイブに追加しました");
                AudioManager.instance.playSE(9);
            }
            else
            {
                MessageManager.instance.SetMessage("すでに追加されています");
                AudioManager.instance.playSE(10);
            }
        }
        //リスク発見コマンド
        if (GameInput.instance._RiskAction.triggered == true)
        {

            // リスク発見画面のプログラムの方に現在のデータを送る。
            // すでにリスク発見されているオブジェの場合はfalseが返ってくる
            bool phaseChangeFlag = RiskDiscover.instance.DiscoverJudge(StageData.GetSurveyingObj());

            //まだリスク発見されていないオブジェクトの場合
            if (phaseChangeFlag == true)
            {
                // 詳細情報ウィンドウをオフ
                StartCoroutine(HiddenDetailInfoWindow());

                // リスク発見画面をオン
                RiskDiscover.instance.InfoOn();

                // リスク発見フェーズへ移行
                return DISCOVERSTATE;
            }
            //すでに発見コマンドを適用されている場合
            else
            {
                MessageManager.instance.SetMessage("リスク調査済みのオブジェクトです。");
                AudioManager.instance.playSE(10);
            }
        }
            return 0;
    }
    
    //リスク発見されているかどうか
    public string JudgedMarkProcess(string obj_name)
    {
        bool discoveredFlag = RiskDiscover.instance.CheckDiscovered(obj_name);

        if (discoveredFlag == true)
        {
            var Obj = StageData.GetObjNum(obj_name);
            int riskNum = Convert.ToInt32(Obj[2]);

            if (riskNum == 0)
            {
                //TMPJudgedMark.text = "無";
                return "無";
            }
            else
            {
                //TMPJudgedMark.text = "有";
                return "有";
            }
        }
        else
        {
            //TMPJudgedMark.text = "?";
            return "？";
        }
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UniRx;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    /*** 3Dオブジェクトを制御する変数 *******************************************************************/

    public CharacterController _cachController; //キャラクターの当たり判定キャッシュ
    public Transform _cashPlayerTransform;     //プレイヤーのオブジェクトのキャッシュ

    [SerializeField]
    public Camera CameraMain = null;

    [SerializeField]
    public Transform CameraL = null;

    [SerializeField]
    public Transform CameraR = null;

    [SerializeField]
    public Transform CameraFollower = null;

    [SerializeField]
    public Transform WebCameraSet = null;

    [SerializeField]
    public Transform DebugWindow = null;


    /*** 入力取得変数 **********************************************************************************/

    /* 以下　InputSystem用の変数 ***/
    /*public InputAction _MoveAction, _LookAction, _DecisionAction,
        _BackAction, _RiskAction, _GimicAction, _ArchivesAction,
        _JujiKeyAction, _AnyAction, _CameraResetAction;

    public InputAction _L2Action, _R2Action, _R1Action, _DebugMode;
    public Vector2 moveInput, lookInput, JujiKeyInput;

    public float rightButtonframe;  //十字キーの右が押されているフレーム数
    public float leftButtonframe;  //十字キー左が押されているフレーム数

    public bool debugModeFlag = false;
    */
    /*** プレイヤーのステータス変数 ********************************************************************/

    [SerializeField] public float speed = (float)70;
    [SerializeField] private float turnSpeed = (float)120;


    public float PlayerStartY;  //プレイヤーのY座標の初期を保存。重力の代わり

    public Vector3 moveDirection = Vector3.zero;//キャラが向いている方向

    //public DateTime startTime, endTime;

    //フェーズの定義
    public enum Phase
    {
        NONE,
        WAIT,
        EXPLORE,
        DETAILINFO,
        DISCOVER,
        ARCHIVES,
        RESULT,
        DOCUMENT
    }
    public Phase nowPhase, nextPhase;

    public bool rayCastFlag;   //カメラの当たり判定を行うかどうかのフラグ
    public bool IconFlag;      //アイコンを表示するかのフラグ
    public UDPConnection myUdpConnection;




    /*** Unity側で自動で呼ばれる関数群 ******************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerMove Start");
        if (instance == null)
        {
            instance = this;
        }
        PlayerStartY = this.transform.position.y;
        
        _cachController = GetComponent<CharacterController>();

        _cashPlayerTransform = this.transform;

        //InputInit();

        PhaseChangeProcess(Phase.WAIT);

        myUdpConnection = GameObject.Find("udpClient").GetComponent<UDPConnection>();

        /*myUdpConnection.OnJudgeFlagChanged
            .ObserveOnMainThread()
            .Where(value => value == true)
            .Subscribe(_ => JudgeEnd())
            .AddTo(this);*/

        Debug.Log("PlayerMove End");
    }

    // Update is called once per frame
    void Update()
    {
        //InputUpdate();
        rayCastFlag = false;
        IconFlag = false;

        
        //各フェーズ毎の処理
        /*switch (nowPhase)
        {
            case Phase.WAIT:
                if (_AnyAction.triggered == true && WaitPhaseScripts.instance.GetInputReception() == true)
                {
                    WaitPhaseScripts.instance.PushAny();
                }

                if (WaitPhaseScripts.instance.GetNextPhase() == true)
                {

                    startTime = DateTime.Now;   //現在時刻を保存

                    WaitPhaseScripts.instance.StopWaitPhaseWindow();  //waitPhaseのオブジェクトを停止する

                    PhaseChangeProcess(Phase.EXPLORE);      //探索フェーズに移行

                }


                break;

            case Phase.EXPLORE:*/ /*** 探索している状態 *****/

                /*rayCastFlag = true;
                IconFlag = true;

                // プレイヤーの回転処理
                PlayerRotate();

                //プレイヤーの移動処理
                MoveProcess();

                //カメラリセットのボタンが押されたら
                if (_CameraResetAction.triggered == true)
                {
                    WebCameraSet.localEulerAngles = Vector3.zero;

                    CameraMain.transform.localEulerAngles = Vector3.zero;

                    CameraL.localEulerAngles = Vector3.zero;

                    CameraR.localEulerAngles = Vector3.zero;

                    vertiRotate = 0;

                }

                if (_R1Action.triggered == true)
                {
                    DocsWindow.instance.InfoOn();
                    PhaseChangeProcess(Phase.DOCUMENT);


                }

                //探索処理
                SearchPhaseProcess();

                break;

            case Phase.DETAILINFO:*/ /*** 情報を閲覧している状態 *****/

                /*DetailBrowsePhaseProcess();
                break;

            case Phase.DISCOVER://発見フェーズ

                if (RiskDiscover.instance.GetInputReception() == true)
                {

                    DiscoverPhaseProcess();
                }

                break;


            case Phase.ARCHIVES://アーカイブ閲覧フェーズ。


                ArchivesPhaseProcess();




                break;

            case Phase.RESULT://リザルトフェーズ

                if (ResultScript.instance.GetInputReception() == true)
                {
                    if (_DecisionAction.triggered == true)
                    {
                        //SceneManager.LoadScene("Title");
                    }
                }
                break;

            case Phase.DOCUMENT:

                if (DocsWindow.instance.GetInputReception() == true)
                {
                    if (_BackAction.triggered == true)
                    {
                        DocsWindow.instance.InfoOff();
                    }*/

                    /* 左スティックか十字キーの左右でページ送り。****************/

                    /*if (0 < JujiKeyInput.x || 0 < moveInput.x)
                    {
                        rightButtonframe++;
                        if (rightButtonframe % 60 == 1)//長押しにも対応できるような感じで。1秒に1回動く。
                        {
                            DocsWindow.instance.PageChange(true);

                            AudioManager.instance.playSE(9);
                        }

                    }
                    else if (JujiKeyInput.x < 0 || moveInput.x < 0)
                    {
                        leftButtonframe++;
                        if (leftButtonframe % 60 == 1)
                        {
                            DocsWindow.instance.PageChange(false);

                            AudioManager.instance.playSE(9);
                        }
                    }
                    else
                    {
                        rightButtonframe = 0; leftButtonframe = 0;
                    }



                }
                //DocsWindowが停止していてフェーズ変更許可が出ていれば探索フェーズに戻る。
                if (DocsWindow.instance.GetPhaseChange() == true && DocsWindow.instance.GetActiveFlag() == false)
                {
                    PhaseChangeProcess(Phase.EXPLORE);
                }


                break;

            default:
                break;

        }*/
    }


    /* 自作の関数群 ************************************************************************************/

    /***** フェーズの処理  ********************************************/

    // 探索フェーズの処理まとめ
    /*public void SearchPhaseProcess()
    {
        // 決定ボタンが押されたら
        if (GameInput.instance._DecisionAction.triggered == true)
        {
            Debug.Log("決定ボタン入力");
            //視界に調べられるオブジェクトがある時、詳細情報を表示するフェーズに移行。
            if (CameraRayCast.instance.GetHitObjectName() != "none")
            {
                Debug.Log("詳細表示");
                DetailInfoWindow.instance.OnWindow(); //オブジェクトの詳細情報ウィンドウをアクティブにする

                PhaseChangeProcess(Phase.DETAILINFO);
                AudioManager.instance.playSE(8);


            }
        }

        if (GameInput.instance._GimicAction.triggered == true)
        {
            string gimicName = CameraRayCast.instance.GetHitGimicName();

            //視界にギミックを発動できるオブジェクトがある時
            if (gimicName != "none")
            {
                //対象のギミックオブジェを探して、そいつの持つアクションを実行する
                GameObject.Find(gimicName).GetComponent<GimicObjBase>().Action();

            }
        }

        // アーカイブ画面に移行
        if (GameInput.instance._ArchivesAction.triggered == true)
        {
            ArchivesManager.instance.InfoOn();

            PhaseChangeProcess(Phase.ARCHIVES);


        }
    }*/

    // 詳細情報フェーズの処理まとめ
    public void DetailBrowsePhaseProcess()
    {
        // キャンセルボタンが押されたら、探索フェーズに戻る。
        /*if (GameInput.instance._BackAction.triggered == true)
        {
            AudioManager.instance.playSE(0);

            DetailInfoWindow.instance.OffWindow();

            PhaseChangeProcess(Phase.EXPLORE);

        }*/

        // L1ボタンが押されたら、今表示されている情報をアーカイブに追加する。
        /*if (GameInput.instance._ArchivesAction.triggered == true)
        {
            // 詳細情報ウィンドウに表示されている情報をアーカイブに追加する
            // 追加成功でtrue すでに追加されている場合はfalse
            bool addFlag = ArchivesManager.instance.AddArchives(DetailInfoWindow.instance.GetNowData());
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

        }*/


        //リスク発見コマンド
        /*if (GameInput.instance._RiskAction.triggered == true)
        {

            // リスク発見画面のプログラムの方に現在のデータを送る。
            // すでにリスク発見されているオブジェの場合はfalseが返ってくる
            bool phaseChangeFlag = RiskDiscover.instance.DiscoverJudge(DetailInfoWindow.instance.GetNowData());

            //まだリスク発見されていないオブジェクトの場合
            if (phaseChangeFlag == true)
            {
                // 詳細情報ウィンドウをオフ
                DetailInfoWindow.instance.OffWindow();

                // リスク発見画面をオン
                RiskDiscover.instance.InfoOn();

                // リスク発見フェーズへ移行
                PhaseChangeProcess(Phase.DISCOVER);


            }
            //すでに発見コマンドを適用されている場合
            else
            {
                MessageManager.instance.SetMessage("リスク調査済みのオブジェクトです。");
                AudioManager.instance.playSE(10);
            }

        }
        */
    }

    //リスク発見コマンドの処理
    public void DiscoverPhaseProcess()
    {
        /*Manual.instance.WindowOff();
        Manual.instance.BackCommandActive();


        //if (Input.GetButtonDown("CancelButton"))
        if (GameInput.instance._BackAction.triggered == true)
        {
            myUdpConnection.SendByte(prefix: "_Fn", msg: "Pls_Judge");

            AudioManager.instance.playSE(0);
            RiskDiscover.instance.InfoOff();

            //全てのリスクを発見したかどうか判定
            if (RiskDiscover.instance.GetCorrectCount() == StageData.GetRiskNum())
            {
                //終了時間を取得し、クリアまでのタイムを計算して送る
                //endTime = DateTime.Now;
                //ResultScript.instance.SetLapsedTime(endTime - startTime);
                ResultScript.instance.InfoOn();
                ArchivesManager.instance.ArchivesFileSave();

                //リザルトに移行
                PhaseChangeProcess(Phase.RESULT);


            }
            else
            {
                //探索に移行
                PhaseChangeProcess(Phase.EXPLORE);

            }
        }*/
    }

    /*public void JudgeEnd()
    {
        //全てのリスクを発見したかどうか判定
        if (RiskDiscover.instance.GetCorrectCount() == StageData.GetRiskNum())
        {
            //終了時間を取得し、クリアまでのタイムを計算して送る
            //endTime = DateTime.Now;
            //ResultScript.instance.SetLapsedTime(endTime - startTime);
            ResultScript.instance.InfoOn();
            ArchivesManager.instance.ArchivesFileSave();

            //リザルトに移行
            PhaseChangeProcess(Phase.RESULT);
        }
        myUdpConnection.ResetJudgeFlag();
    }*/

    // アーカイブ閲覧フェーズの処理まとめ
    public void ArchivesPhaseProcess()
    {
        // 入力を受け付ける時
        if (ArchivesManager.instance.GetInputReception() == true)
        {

            //キャンセルボタンで探索フェーズ(phase:0)に戻る。
            if (GameInput.instance._BackAction.triggered == true)
            {
                AudioManager.instance.playSE(0);
                ArchivesManager.instance.InfoOff();

                PhaseChangeProcess(Phase.EXPLORE);
                //nextPhase = Phase.EXPLORE;
            }

            //リスク発見コマンド
            //if (Input.GetButtonDown("RectButton"))
            if (GameInput.instance._RiskAction.triggered == true)
            {
                // リスク発見画面のプログラムの方に現在のデータを送る。
                // すでにリスク発見されているオブジェの場合はfalseが返ってくる
                bool phaseChangeFlag = RiskDiscover.instance.DiscoverJudge(ArchivesManager.instance.GetNowData());

                // まだリスク発見されていないオブジェクトの場合
                if (phaseChangeFlag == true)
                {
                    // 詳細情報ウィンドウをオフ
                    ArchivesManager.instance.InfoOff();

                    // リスク発見画面をオン
                    RiskDiscover.instance.InfoOn();

                    // リスク発見フェーズへ移行
                    PhaseChangeProcess(Phase.DISCOVER);

                }
                //すでに発見コマンドを適用されている場合
                else
                {
                    MessageManager.instance.SetMessage("リスク調査済みのオブジェクトです。");
                    AudioManager.instance.playSE(10);
                }

            }

            /* 左スティックか十字キーの左右でページ送り。****************/

            if (0 < GameInput.instance.JujiKeyInput.x || 0 < GameInput.instance.moveInput.x)
            {
                GameInput.instance.rightButtonframe++;
                if (GameInput.instance.rightButtonframe % 60 == 1)//長押しにも対応できるような感じで。1秒に1回動く。
                {
                    ArchivesManager.instance.IndexNext();
                    if (ArchivesManager.instance.GetArchivesCount() > 1)
                    {
                        AudioManager.instance.playSE(9);
                    }


                }

            }
            else if (GameInput.instance.JujiKeyInput.x < 0 || GameInput.instance.moveInput.x < 0)
            {
                GameInput.instance.leftButtonframe++;
                if (GameInput.instance.leftButtonframe % 60 == 1)
                {
                    ArchivesManager.instance.IndexBefore();
                    if (ArchivesManager.instance.GetArchivesCount() > 1)
                    {
                        AudioManager.instance.playSE(9);
                    }
                }
            }
            else
            {
                GameInput.instance.rightButtonframe = 0; GameInput.instance.leftButtonframe = 0;
            }
        }
    }


    //フェーズ変える処理まとめ。
    void PhaseChangeProcess(Phase next_phase)
    {
        nowPhase = next_phase;
        switch (next_phase)
        {

            case Phase.WAIT:
                break;


            case Phase.EXPLORE:
                Manual.instance.WindowOff();
                Manual.instance.ExploreActive();
                break;


            case Phase.DETAILINFO: /*** 情報を閲覧している状態 *****/
                Manual.instance.WindowOff();
                Manual.instance.DetailActive();

                break;

            case Phase.DISCOVER://発見フェーズ
                Manual.instance.WindowOff();

                break;


            case Phase.ARCHIVES://アーカイブ閲覧フェーズ。
                Manual.instance.WindowOff();
                Manual.instance.ArchiveActive();

                break;

            case Phase.RESULT://リザルトフェーズ
                Manual.instance.WindowOff();
                break;

            case Phase.DOCUMENT://ドキュメント閲覧フェーズ
                Manual.instance.WindowOff();

                break;

            default:
                break;
        }
    }

    
    /**** 入力処理 ******************************************************/

    //入力系の初期化
    /*public void InputInit()
    {
        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        var actionMap = pInput.currentActionMap;
        Debug.Log(actionMap.name);
        //アクションマップからアクションを取得
        _MoveAction = actionMap["Move"];
        Debug.Log(_MoveAction);
        _LookAction = actionMap["Look"];

        _DecisionAction = actionMap["Decision"];
        _BackAction = actionMap["Back"];
        _RiskAction = actionMap["Risk"];
        _ArchivesAction = actionMap["Archives"];
        _GimicAction = actionMap["Gimic"];
        _JujiKeyAction = actionMap["JujiKey"];
        _AnyAction = actionMap["Any"];
        _CameraResetAction = actionMap["CameraReset"];

        _L2Action = actionMap["L2Action"];
        _R2Action = actionMap["R2Action"];

        _R1Action = actionMap["R1Action"];

        _DebugMode = actionMap["DebugMode"];

        debugModeFlag = false;
        DebugWindow.gameObject.SetActive(debugModeFlag);

    }
    */
    //入力系の更新
   /* void InputUpdate()
    {
        //Debug.Log("InputUpdate");
        moveInput = _MoveAction.ReadValue<Vector2>();
        //Debug.Log(moveInput);
        lookInput = _LookAction.ReadValue<Vector2>();

        JujiKeyInput = _JujiKeyAction.ReadValue<Vector2>();

        //if (_DebugMode.triggered)
        //{
        //    debugModeFlag = !debugModeFlag;
        //    DebugWindow.gameObject.SetActive(debugModeFlag);
        //}



    }*/



    public bool L2Pressed()
    {
        return GameInput.instance._L2Action.IsPressed();
    }

    public bool R2Pressed()
    {
        return GameInput.instance._R2Action.IsPressed();
    }

    public bool R1Triggered()
    {
        return GameInput.instance._R1Action.triggered;
    }

    public bool GetDebugMode()
    {
        return GameInput.instance.debugModeFlag;
    }

    /***** プレイヤーの移動関連 *****************************************/

    public float vertiRotate = 0.0f;
    private float MinLimit = -50.0f;    //縦回転の↑方向の制限
    private float MaxLimit = 70.0f;     //縦回転↓方向の制限

    Vector3 newVec = new Vector3();


    public void Move()
    {
        rayCastFlag = true;
        IconFlag = true;

        PlayerRotate();
        MoveProcess();
    }

    public void ResetCamera()
    {
        WebCameraSet.localEulerAngles = Vector3.zero;

        CameraMain.transform.localEulerAngles = Vector3.zero;

        CameraL.localEulerAngles = Vector3.zero;

        CameraR.localEulerAngles = Vector3.zero;

        vertiRotate = 0;
    }

    //プレイヤーの回転制御。
    private void PlayerRotate()
    {

        //水平方向の回転
        float turn = GameInput.instance.lookInput.x * turnSpeed * Time.deltaTime;
        _cashPlayerTransform.Rotate(0, turn, 0);

        //垂直方向の回転 webCameraSetを直接回転させるやつ。
        //インスペクター上の数値と、変数の値が異なっているのが判明している。
        //インスペクター上では-180～180の数値だが、変数上は0～360の間に収まっているような感じがする。

        vertiRotate += -GameInput.instance.lookInput.y * turnSpeed * Time.deltaTime;
        vertiRotate = Mathf.Clamp(vertiRotate, MinLimit, MaxLimit);
        WebCameraSet.localEulerAngles = new Vector3(vertiRotate, 0.0f, 0.0f);

    }

    //プレイヤーの移動制御
    public void MoveProcess()
    {
        //移動方向の取得
        moveDirection = new Vector3(GameInput.instance.moveInput.x, 0.0f, GameInput.instance.moveInput.y);

        //↓これだとキャラが空中に浮いてしまう
        moveDirection = CameraFollower.TransformDirection(moveDirection);


        //移動量を掛ける
        moveDirection = moveDirection * speed;

        /* Time.deltaTimeは1フレーム間の時間。
         * 時間量を掛けることで時間に対する移動量が変わらないように出来るらしい*/
        _cachController.Move(moveDirection * Time.deltaTime);

        //カメラが向いている方向に移動する場合に、キャラクターが中に浮いてしまう問題があり
        //その対処としてy座標を固定している。良い方法があれば書き換えたい。
        _cashPlayerTransform.position = new Vector3(_cashPlayerTransform.position.x, PlayerStartY, _cashPlayerTransform.position.z);

    }
    public bool GetRayCastFlag()
    {
        return rayCastFlag;
    }

    public bool GetIconFlag()
    {
        return IconFlag;
    }



}

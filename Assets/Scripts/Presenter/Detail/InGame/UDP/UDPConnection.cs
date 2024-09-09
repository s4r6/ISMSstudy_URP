using UnityEngine;
using System;
using UniRx;
using System.Collections.Generic;
using System.Collections;
//using UnityEngine.UI;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine.SceneManagement;

public class UDPConnection
{
    //public static UDPConnection instance;

    /*[DllImport("__Internal")]
    private static extern void Jslib_InitializeWebSocket(OnOpen onOpen, OnMessage onMessage, Connecting Connecting, SetRoomInfo SetRoomInfoFunc, JoinClient JoinClientFunc);

    [DllImport("__Internal")]
    private static extern void Jslib_ConnectWebSocket();

    [DllImport("__Internal")]
    private static extern void Jslib_SendWebSocketMessage(string str);

    [DllImport("__Internal")]
    private static extern void Jslib_CreateNewRoom();

    [DllImport("__Internal")]
    private static extern void Jslib_GetRoomInfo();

    [DllImport("__Internal")]
    private static extern void Jslib_JoinRoom(int RoomID);
    [DllImport("__Internal")]
    private static extern void Jslib_JoinClient(string json);
    
    [SerializeField]
    private RoomListWindow myRoomListWindow;
    //public static UDPConnection instance;
    public string host;
    private int port = 9000;
    //private UdpClient udpClient;
    //private Subject<string> subject = new Subject<string>();

    private ReactiveProperty<bool> Finish_GetRoomInfo = new ReactiveProperty<bool>(false);

    public IObservable<bool> OnFinish_GetRoomInfoChanged
    {
        get { return Finish_GetRoomInfo; }
    }

    private bool ConnectEndFlag = false;
    public bool GetConnectEndFlag
    {
        get { return ConnectEndFlag; }
    }


    private ReactiveProperty<int> StageNum = new ReactiveProperty<int>(100);

    public IObservable<int> OnStageNumChanged
    {
        get { return StageNum; }
    }

    private ReactiveProperty<string> ObjName = new ReactiveProperty<string>(null);
    public string SetObjNameProperty
    {
        set
        {
            ObjName.Value = value;
        }
    }
    public IObservable<string> OnObjNameChanged
    {
        get { return ObjName; }
    }

    private ReactiveProperty<int> CountNum = new ReactiveProperty<int>(0);
    public int SetCountNumProperty
    {
        set { CountNum.Value = value; }
    }
    public IObservable<int> OnCountNumChanged
    {
        get { return CountNum; }
    }

    private ReactiveProperty<int> AnswerNum = new ReactiveProperty<int>(0);
    public int SetAnswerNumProperty
    {
        set
        {
            AnswerNum.Value = value;
        }
    }
    public IObservable<int> OnAnswerNumChanged
    {
        get { return AnswerNum; }
    }

    private ReactiveProperty<bool> JudgeFlag = new ReactiveProperty<bool>(false);

    public IObservable<bool> OnJudgeFlagChanged
    {
        get { return JudgeFlag; }
    }
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        Debug.Log("これはエディターです");
#elif UNITY_WEBGL
        #region
        Debug.Log("debug");
        Jslib_InitializeWebSocket(OnOpenFunc, OnMessageFunc, ConnectingFunc, SetRoomInfoFunc, JoinClientFunc);
        #endregion
#endif

        udpClient = new UdpClient(9000);    //9000番ポートを送受信ポートとして解放
        udpClient.BeginReceive(OnReceived, udpClient);    //受信を開始
        
        subject
            .ObserveOnMainThread()
            .Subscribe(msg => {
                message.text = msg;
            }).AddTo(this);
        Debug.Log("Insutans");
    }

    public void StartCreateRoomCoroutine()
    {
        //StartCoroutine(CreateRoom());
    }
    
    private IEnumerator CreateRoom()    //部屋作成要求を送信
    {
        ConnectToServer();
        yield return new WaitUntil(() => ConnectEndFlag == true);   //接続されるまで待機
        Jslib_CreateNewRoom();
        yield return new WaitForSeconds(2f);
        DisplayAllRoom();
        PlayerData.M_Authority = PlayerAuthority.RoomHost;
        SceneManager.LoadScene("VRMainHTC_harf");

    }

    public void SendSelectRoom(int RoomID)  //参加する部屋を送信
    {
        Jslib_JoinRoom(RoomID);
        PlayerData.M_Authority = PlayerAuthority.RoomClient;
    }

    public void StartGetRoomServerCoroutine()
    {
        //StartCoroutine(GetRoomServer());
    }

    private IEnumerator GetRoomServer()
    {
        ConnectToServer();
        yield return new WaitUntil(() => ConnectEndFlag == true);   //接続されるまで待機
        Debug.Log("接続完了");
        Jslib_GetRoomInfo();
    }
    public void ConnectToServer()
    {
        #if UNITY_WEBGL
        Jslib_ConnectWebSocket();
        #endif
    }

    private void OnReceived(System.IAsyncResult result)
    {
        Debug.Log("受信を開始しました。");
        UdpClient getUdp = (UdpClient)result.AsyncState;
        IPEndPoint ipEnd = null;

        byte[] getByte = getUdp.EndReceive(result, ref ipEnd);  //受信

        var message = Encoding.UTF8.GetString(getByte);
        Debug.Log(message);
        if (message.Contains("_St"))  //ステージデータが送られてきたとき
        {
            StageNum.Value = SetStageNum(message);
        }
        else if (message.Contains("_Nm")) //オブジェクトの名前が送られてきたとき
        {
            ObjName.Value = SetObjName(message);
        }
        else if (message.Contains("_Ct")) //判定した回数が送られてきたとき
        {
            CountNum.Value = SetCountNum(message);
        }
        else if (message == "Pls_Judge") //終了判定の要求が送られてきたとき
        {
            JudgeFlag.Value = true;
            //PlayerMove.instance.JudgeEnd();
        }
        else if (message.Contains("_An")) //リスクの有無が送られてきたとき
        {
            AnswerNum.Value = SetAnswerNum(message);
        }
        else
        {
            Debug.Log("受信に失敗しました");
        }

        //subject.OnNext(message);
        Debug.Log("a");

        getUdp.BeginReceive(OnReceived, getUdp);    //繰り返し受信する機能(おそらく)
    }
    
    public void SendStageData()
    {
        StageDataPakcet stagepacket = new StageDataPakcet();
        stagepacket.PacketID = (int)PacketId.StageDataPacket;
        stagepacket.StageID = StageData.StageID;
        string json = JsonUtility.ToJson(stagepacket);
        Debug.Log(json);
        Jslib_SendWebSocketMessage(json);
    }

    public void SendGameData(GameData _gameData)
    {
        DefaultPacket gamepacket = new DefaultPacket();
        gamepacket.PacketID = (int)PacketId.DefaultPacket;
        gamepacket.SurveiedObjName = _gameData.GetLastSurveiedObjName();
        gamepacket.JudgeCount = _gameData.JudgeCount;
        gamepacket.correctCount = _gameData.CorrectCount;
        string json = JsonUtility.ToJson(gamepacket);
        Debug.Log(json);
        Jslib_SendWebSocketMessage(json);
    }
    public void SendByte(string msg = null)
    {
        string SendString = "NoMessage";
        if (msg != null)
            SendString = prefix + msg;
        else if (num != -1)
            SendString = prefix + num.ToString();
        

        //Jslib_SendWebSocketMessage(SendString);
    }

    public void SendByte(int SendNum)   //送られてきたint型をstring型に変換して送信
    {
        var SendString = SendNum.ToString();
        var Num = Encoding.UTF8.GetBytes("St" + SendString);
        udpClient.Send(Num, Num.Length);
    }

    public void SendByte(string prefix, string SendObj)    //string型を送信
    {
        Debug.Log(prefix + SendObj);
        var ObjName = Encoding.UTF8.GetBytes(prefix + SendObj);
        udpClient.Send(ObjName, ObjName.Length);

    }

    public void SendCount(string Count) //判定するたびに送信
    {
        var Num = Encoding.UTF8.GetBytes("Ct" + Count);
        udpClient.Send(Num, Num.Length);
    }

    public void SendAnswer(string Answer)
    {
        var AnswerNum = Encoding.UTF8.GetBytes("An" + Answer);
        udpClient.Send(AnswerNum, AnswerNum.Length);
    }
    
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            Jslib_ConnectWebSocket();
        }
    
    }

    public void StartMatching()
    {
        udpClient.Connect(host, port); //リモートエンドポイントと経路を確保
        ConnectEndFlag = true;
        Debug.Log(ConnectEndFlag);
    }
    
    private void SetReceivedMessage(string message)
    {
        Debug.Log(message);
        string Temp;
        if (message.Contains("_St"))  //ステージデータが送られてきたとき
        {
            Temp = message.Replace("_St", "");   //修飾語を取り除いてIntに変換
            StageNum.Value = int.Parse(Temp);
        }
        else if (message.Contains("_Nm")) //オブジェクトの名前が送られてきたとき
        {
            Temp = message.Replace("_Nm", "");
            ObjName.Value = Temp;
        }
        else if (message.Contains("_Ct")) //判定した回数が送られてきたとき
        {
            Temp = message.Replace("_Ct", "");
            CountNum.Value = int.Parse(Temp);
        }
        else if (message.Contains("_Fn")) //終了判定の要求が送られてきたとき
        {
            JudgeFlag.Value = true;
            //PlayerMove.instance.JudgeEnd();
        }
        else if (message.Contains("_An")) //リスクの有無が送られてきたとき
        {
            Temp = message.Replace("_An", "");
            AnswerNum.Value = int.Parse(Temp);
        }
        else
        {
            Debug.Log("受信に失敗しました:"+message);
        }
    
    }

    private int SetStageNum(string StageString) //データの装飾語を取り除いてint型に変換
    {
        StageString = StageString.Replace("_St", "");
        return int.Parse(StageString);
    }

    private string SetObjName(string NameString) //データの装飾語を取り除く
    {
        Debug.Log(NameString);
        NameString = NameString.Replace("_Nm", "");
        return NameString;
    }

    private int SetCountNum(string CountString)
    {
        CountString = CountString.Replace("_Ct", "");
        return int.Parse(CountString);
    }

    private int SetAnswerNum(string AnswerString)
    {
        AnswerString = AnswerString.Replace("_An", "");
        return int.Parse(AnswerString);
    }
    private void OnDestroy()
    {
        udpClient.Close();
    }



    public void DisplayAllRoom()
    {
        var RoomList = RoomData.GetRoomList();  //ルームのリストを取得
        foreach(KeyValuePair<int, int> room in RoomList)
        {
            Debug.Log("RoomID:" + room.Key + " " + "PlayerNum:" + room.Value);
        }
    }

    public void ResetJudgeFlag()
    {
        JudgeFlag.Value = false;
        Debug.Log("offにししたよ");
    }

    private string Serializer()
    {
        StageDataPakcet stagepacket = new StageDataPakcet();
        stagepacket.PacketID = (int)PacketId.StageDataPacket;
        stagepacket.StageID = StageData.StageID;
        string json = JsonUtility.ToJson(stagepacket);
        Debug.Log(json);
        return json;
    }

    private delegate void OnOpen();

    private delegate void OnMessage(IntPtr ptr, int size);

    private delegate void Connecting();

    private delegate void SetRoomInfo(int RoomID, int PlayerNum);

    private delegate void JoinClient(int _stageID);

    [MonoPInvokeCallback(typeof(JoinClient))]
    private static void JoinClientFunc(int _stageID)
    {
        StageData.StageID = _stageID;
        SceneManager.LoadScene("VRMainHTC_harf");
    }

    [MonoPInvokeCallback(typeof(OnOpen))]
    private static void OnOpenFunc()
    {
        Debug.Log("Connected");
    }

    [MonoPInvokeCallback(typeof(OnMessage))]
    private static void OnMessageFunc(IntPtr ptr, int size)
    {
        var message = new byte[size];
        Marshal.Copy(ptr, message, 0, size);
        var Packet = System.Text.Encoding.UTF8.GetString(message);
        //instance.SetReceivedMessage(System.Text.Encoding.UTF8.GetString(message));
        Debug.Log("受け取ったメッセージ:" + Packet);
        //instance.ObjName.Value = Packet;
    }

    [MonoPInvokeCallback(typeof(Connecting))]
    private static void ConnectingFunc()
    {
        //instance.ConnectEndFlag = true;
    }

    [MonoPInvokeCallback(typeof(SetRoomInfo))]
    private static void SetRoomInfoFunc(int RoomID, int PlayerNum)
    {
        Debug.Log("RoomID:" + RoomID + "PlayerNum:" + PlayerNum);
        RoomData.SetRoomInfo(RoomID, PlayerNum);
        //instance.Finish_GetRoomInfo.Value = true;
    }*/
}
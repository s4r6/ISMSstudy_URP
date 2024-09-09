using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using AOT;
using ISMS.Connecter;
using UniRx;
namespace ISMS.Connecter.Detail
{
    /*public class WebSocket :IWebSocket
    {
        //[DllImport("__Internal")]
        //private static extern void Jslib_InitializeWebSocket(OnOpen onOpen, OnMessage onMessage, Connecting Connecting, SetRoomInfo SetRoomInfoFunc, JoinClient JoinClientFunc);

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

        //public static UDPConnection instance;
        public string host;
        private int port = 9000;
        //private UdpClient udpClient;
        //private Subject<string> subject = new Subject<string>();

        WebSocket myInstance;


        public WebSocket()
        {
            //this.myInstance = this;
            //this.MyState.Value = readyState.DisConnect;
        }


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

        public void Connect()
        {
            Jslib_ConnectWebSocket();   //接続処理

        }

        public void CreateRoom()
        { }

        public void JoinRoom()
        {

        }

        private IEnumerator CreateRoom()    //部屋作成要求を送信
        {
            Jslib_ConnectWebSocket();   //接続処理
            //yield return new WaitUntil(() => ConnectEndFlag == true);   //接続されるまで待機
            Jslib_CreateNewRoom();
            yield return new WaitForSeconds(2f);
            DisplayAllRoom();
            PlayerData.M_Authority = PlayerAuthority.RoomHost;
            SceneManager.LoadScene("VRMainHTC_harf");

        }

        public void SendSelectRoom(int RoomID)  //参加する部屋を送信
        {
            Jslib_JoinRoom(RoomID);
            //PlayerData.M_Authority = PlayerAuthority.RoomClient;
        }

        public void StartGetRoomServerCoroutine()
        {
            //StartCoroutine(GetRoomServer());
        }

        private IEnumerator GetRoomServer()
        {
            ConnectToServer();
            //yield return new WaitUntil(() => ConnectEndFlag == true);   //接続されるまで待機
            Debug.Log("接続完了");
            Jslib_GetRoomInfo();
            yield return null;
        }
        public void ConnectToServer()
        {
#if UNITY_WEBGL
            
#endif
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



        public void DisplayAllRoom()
        {
            var RoomList = RoomData.GetRoomList();  //ルームのリストを取得
            foreach (KeyValuePair<int, int> room in RoomList)
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
            //WebSocket.Set();
            //MyState.Value
            //Debug.Log("Connected");
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
        }
    }
}*/
}


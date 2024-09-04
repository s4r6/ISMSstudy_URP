using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UniRx;

public class RoomListWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject RoomListCanvas;
    [SerializeField]
    private GameObject ListElement;
    [SerializeField]
    private Transform List;
    // Start is called before the first frame update
    [SerializeField]
    private UDPConnection myUDPConnection;
    void Start()
    {
        RoomListCanvas.SetActive(false);
        /*myUDPConnection.OnFinish_GetRoomInfoChanged
            .ObserveOnMainThread()
            .Where(value => value == true)
            .Subscribe(_ => DisplayRoomList())
            .AddTo(this);*/
    }

    private void DisplayRoomList()  //すべての部屋を表示する
    {
        Debug.Log("Room情報表示開始");
        //Transform list = transform.Find("List");
        Dictionary<int, int> RoomDic = RoomData.GetRoomList();
        RoomListCanvas.SetActive(true);
        foreach (KeyValuePair<int, int> kvp in RoomDic)
        {
            int RoomID = kvp.Key;
            int PlayerNum = kvp.Value;
            var Element = Instantiate(ListElement, List);
            //GameObject Element = Instantiate(ListElement);
            //Element.transform.SetParent(List, false);
            LayoutRebuilder.ForceRebuildLayoutImmediate(Element.GetComponent<RectTransform>());
            Element.transform.Find("Num").GetComponent<Text>().text = PlayerNum+"/2";
            var Room = Element.transform.GetChild(1).gameObject;
            Room.GetComponent<Button>().onClick.AddListener(ReturnSelectRoomID);
            Room.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Room" + RoomID;
            Debug.Log(Room.name);
        }
    }

    public void ReturnSelectRoomID()    //参加する部屋を返す関数
    {
        var eventSystem = EventSystem.current;
        var button_obj = eventSystem.currentSelectedGameObject;
        string ID =button_obj.transform.Find("RoomName").gameObject.GetComponent<Text>().text;
        ID = ID.Replace("Room", "");
        var RoomID = int.Parse(ID);
        //myUDPConnection.SendSelectRoom(RoomID);
    }
    

}

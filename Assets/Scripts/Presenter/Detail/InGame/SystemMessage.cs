using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail
{
    public enum SystemCode
    {
        Locked,
        Surveyed,
        FileIOFailed
    }
    public class SystemMessage : MonoBehaviour
    {
        static Dictionary<SystemCode, string> message = new()
        {
            [SystemCode.Locked] = "鍵がかかっています。",
            [SystemCode.Surveyed] = "リスク調査済みオブジェクトです。",
            [SystemCode.FileIOFailed] = "ファイル出力できませんでした。"
        };
    }
}

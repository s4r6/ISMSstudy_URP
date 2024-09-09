using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail
{
    /// <summary>
    /// プレイヤーの状態を表す
    /// </summary>
    public enum PlayerState
    {
        Loading,
        Wait,
        Explore,
        DetailInfo,
        Discover,
        Archive,
        Result,
        Document
    }
}

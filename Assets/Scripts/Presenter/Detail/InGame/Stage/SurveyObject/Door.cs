using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail.Stage
{
    /// <summary>
    /// ドアのアニメーションや空いているか閉まっているかの状態を管理
    /// </summary>
    public class Door : BaseSurveyObject, IActionable
    {
        Transform _cashThisTransform;
        [SerializeField]
        GimicObjBase DoorMain;    //実際に動くオブジェクト

        Vector3 startPosition = new Vector3(-90f, 180f, -180f);
        Vector3 afterPosition = new Vector3(-90f, 180f, -70f);

        bool openFlag = false;

        [SerializeField]
        bool LockFlag = false;

        public void Action()
        {
            DoorMain.Action();
        }
    }
}

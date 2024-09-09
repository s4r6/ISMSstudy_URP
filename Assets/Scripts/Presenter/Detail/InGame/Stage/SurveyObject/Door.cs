using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail.Stage
{
    /// <summary>
    /// �h�A�̃A�j���[�V������󂢂Ă��邩�܂��Ă��邩�̏�Ԃ��Ǘ�
    /// </summary>
    public class Door : BaseSurveyObject, IActionable
    {
        Transform _cashThisTransform;
        [SerializeField]
        GimicObjBase DoorMain;    //���ۂɓ����I�u�W�F�N�g

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

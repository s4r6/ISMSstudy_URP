using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail.Stage
{
    /// <summary>
    /// 引き出しのアニメーションや、開いている閉まっているなどの状態を管理
    /// </summary>
    public class Cabine : MonoBehaviour, IActionable
    {

        //float cabineStartX = 3.15f;
        [SerializeField]
        private float cabineMaxZ;

        bool cabineOpen = false;
        Vector3 startPosition;
        Vector3 openedPosition;

        private Transform _cashThisTransform;

        [SerializeField]
        private bool LockFlag = false;


        // Start is called before the first frame update
        void Start()
        {
            _cashThisTransform = this.transform;
            startPosition = this.transform.localPosition;
            openedPosition = startPosition + new Vector3(0, 0.0f, cabineMaxZ);
        }



        public void Action()
        {
            Debug.Log("開ける");
            if (LockFlag == false)
            {
                cabineOpen = !cabineOpen;
                if (cabineOpen == true)
                {
                    _cashThisTransform.localPosition = openedPosition;
                    AudioManager.instance.playSE(6);
                }
                else
                {
                    _cashThisTransform.localPosition = startPosition;
                    AudioManager.instance.playSE(7);
                }

            }
            else
            {
                SystemMessage.SetMessage(SystemCode.Locked);
                AudioManager.instance.playSE(3);
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public partial class PlayerState
{
    public class WaitState : PlayerStateBase
    {
        public override void OnEnter(PlayerState owner, PlayerStateBase prevState)
        {
            Debug.Log("Wait State Start");
        }
        public override void OnUpdate(PlayerState owner)
        {
            if (GameInput.instance._AnyAction.triggered == true)
            {
                owner.anim.SetBool("PressedAnyButton", true);
            }

            if (owner.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Debug.Log("遷移");
                //PlayerMove.instance.startTime = DateTime.Now;   //現在時刻を保存

                //WaitPhaseScripts.instance.StopWaitPhaseWindow();  //waitPhaseのオブジェクトを停止する

                owner.ChangeState(Explorestate);     //探索フェーズに移行

            }
        }
    }
}


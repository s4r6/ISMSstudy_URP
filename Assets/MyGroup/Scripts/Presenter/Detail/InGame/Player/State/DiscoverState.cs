using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerState
{
    public class DiscoverState : PlayerStateBase
    {
        public override void OnUpdate(PlayerState owner)
        {
            if (RiskDiscover.instance.GetInputReception() == true)
            {

                Manual.instance.WindowOff();
                Manual.instance.BackCommandActive();

                if (GameInput.instance._BackAction.triggered == true)
                {
                    //myUdpConnection.SendByte(prefix: "_Fn", msg: "Pls_Judge");

                    AudioManager.instance.playSE(0);
                    RiskDiscover.instance.InfoOff();

                    //全てのリスクを発見したかどうか判定
                    if (owner._gameData.CorrectCount == StageData.GetRiskNum())
                    {
                        //終了時間を取得し、クリアまでのタイムを計算して送る
                        ResultScript.instance.SetLapsedTime(owner._gameData.GetClearTime());
                        ResultScript.instance.InfoOn();
                        ArchivesManager.instance.ArchivesFileSave();

                        //リザルトに移行
                        owner.ChangeState(Resultstate);


                    }
                    else
                    {
                        //探索に移行
                        owner.ChangeState(Explorestate);
                    }
                }
            }
        }
    }
}

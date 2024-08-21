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

                    //�S�Ẵ��X�N�𔭌��������ǂ�������
                    if (owner._gameData.CorrectCount == StageData.GetRiskNum())
                    {
                        //�I�����Ԃ��擾���A�N���A�܂ł̃^�C�����v�Z���đ���
                        ResultScript.instance.SetLapsedTime(owner._gameData.GetClearTime());
                        ResultScript.instance.InfoOn();
                        ArchivesManager.instance.ArchivesFileSave();

                        //���U���g�Ɉڍs
                        owner.ChangeState(Resultstate);


                    }
                    else
                    {
                        //�T���Ɉڍs
                        owner.ChangeState(Explorestate);
                    }
                }
            }
        }
    }
}

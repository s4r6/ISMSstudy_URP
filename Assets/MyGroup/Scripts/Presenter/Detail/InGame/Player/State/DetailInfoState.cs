using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerState
{
    public class DetailInfoState : PlayerStateBase
    {
        private const int DISCOVERSTATE = 1;
        private const int EXPLORESTATE = 2;
        public override void OnEnter(PlayerState owner, PlayerStateBase prevState)
        {
            AudioManager.instance.playSE(8);
            owner.StartCoroutine(owner.myDetailInfoWindow.DisplayDetailInfoWindow());
        }
        public override void OnUpdate(PlayerState owner)
        {
            int returnValue = owner.myDetailInfoWindow.ActionWindow();
            if (returnValue == EXPLORESTATE)
            {
                owner.ChangeState(Explorestate);
                //PlayerMove.instance.DetailBrowsePhaseProcess();
            }
            else if(returnValue == DISCOVERSTATE)
            {
                owner.ChangeState(Discoverstate);
            }
        }
    }
}

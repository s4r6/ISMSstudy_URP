using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerState
{
    public class ArchivesState : PlayerStateBase
    {
        public override void OnUpdate(PlayerState owner)
        {
            PlayerMove.instance.ArchivesPhaseProcess();
        }
    }
}

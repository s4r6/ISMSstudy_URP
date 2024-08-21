using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerState
{
    public class DocumentState : PlayerStateBase
    {
        public override void OnEnter(PlayerState owner, PlayerStateBase prevState)
        {
            owner.StartCoroutine(owner.myDocsWindow.DisplayDocumentWindow());
        }
        public override void OnUpdate(PlayerState owner)
        {
            if(owner.myDocsWindow.ActionWindow())
            {
                owner.ChangeState(Explorestate);
            }
        }
    }
}

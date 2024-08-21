using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerState
{
    public class ResultState : PlayerStateBase
    {
        public override void OnUpdate(PlayerState owner)
        {
            if (ResultScript.instance.GetInputReception() == true)
            {
                if (GameInput.instance._DecisionAction.triggered == true)
                {
                    //SceneManager.LoadScene("Title");
                }
            }
        }
    }
}

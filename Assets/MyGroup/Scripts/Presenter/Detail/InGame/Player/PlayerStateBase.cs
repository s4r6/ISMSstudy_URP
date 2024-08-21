using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase
{
    public virtual void OnEnter(PlayerState owner, PlayerStateBase prevState) { }

    public virtual void OnUpdate(PlayerState owner) { }

    public virtual void OnExit(PlayerState owner, PlayerStateBase nextState) { } 
}

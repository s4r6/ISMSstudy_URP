using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public partial class PlayerState
{
    public class LoadingState : PlayerStateBase
    {
        public override void OnEnter(PlayerState owner, PlayerStateBase prevState)
        {
            owner.myStageLoader.OnLoadEndFlagChanged
            .Where(value => value == true)
            .Subscribe(value => {
                owner._gameData = GameData.GetInstance(); //ゲームデータのインスタンス作成
                owner.ChangeState(Waitstate);
                })
            .AddTo(owner);
            Debug.Log("LoadingState");
        }
    }
}

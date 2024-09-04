using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleanArchtecture;
using ISMS.Domain.Presenter;
using Zenject;

namespace ISMSInGame.Domain.UseCase.Detail
{
    public class InGameUseCase : IUseCase
    {
        IPlayerPresenter _player;
        public InGameUseCase(IPlayerPresenter player)
        {
            _player = player;
        }

        public void Begin()
        {
            //ネットワーク接続
            //ステージ初期化
            _player.InGameInitialize();
            //各種Bind
            //PlayerState初期化
        }
    }
}


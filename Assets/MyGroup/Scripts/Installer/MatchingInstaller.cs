using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ISMS.Presenter.Detail;
using ISMS.Connecter.Detail;
using ISMS.Presenter;
using ISMS.Connecter;

using ISMS.Domain.Presenter;
using ISMS.Domain.Connecter;

using ISMS.Matching.Main;

namespace ISMS.Matching.Installer
{
    public class MatchingInstaller : MonoInstaller
    {
        [SerializeField] RoomView roomView;
        [SerializeField] StageSelectView stageView;
        WebSocket webSocket = new WebSocket();

        public override void InstallBindings()
        {
            Container
                .Bind<IMatchingPresenter>()
                .FromInstance(new RoomPresenter(roomView))
                .AsCached();
            Container
                .Bind<ISocketConnecter>()
                .FromInstance(new SocketConnecter(webSocket))
                .AsCached();
            Container
                .Bind<IStagePresenter>()
                .FromInstance(new StagePresenter(stageView))
                .AsCached();

        }
    }
}


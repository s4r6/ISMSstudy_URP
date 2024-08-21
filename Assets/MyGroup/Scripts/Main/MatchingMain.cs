using UnityEngine;
using CleanArchtecture;
using ISMS.Domain.UseCase.Matching;
using ISMS.Domain.UseCase;
using ISMS.Domain.Presenter;
using ISMS.Domain.Connecter;
using Zenject;
using ISMS.Matching.Installer;
using ISMS.Domain.Entity;


namespace ISMS.Matching.Main
{
    public class MatchingMain : MonoBehaviour
    {
        IUseCase matchingUseCase;

        [Inject]
        void ConstructUseCase(IMatchingPresenter presenter, ISocketConnecter connecter, IStagePresenter s_presenter)
        {
            var entity = new StageEntity();
            matchingUseCase = new MatchingUseCase(presenter, connecter, entity, s_presenter);
        }

        private void Awake()
        {
            matchingUseCase.Begin();
        }
    }
}


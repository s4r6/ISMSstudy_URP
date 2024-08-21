using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Domain.UseCase;
using ISMS.Domain.Presenter;
using ISMS.Domain.Connecter;
using UniRx;
using UnityEngine.SceneManagement;
using ISMS.Domain.Entity;
using Cysharp.Threading.Tasks;
using UnityEditor;

namespace ISMS.Domain.UseCase.Matching
{
    public class MatchingUseCase : CleanArchtecture.IUseCase
    {
        IMatchingPresenter matchingPresenter;
        ISocketConnecter socketConnecter;
        StageEntity stageEntity;
        IStagePresenter stagePresenter;

        public MatchingUseCase(IMatchingPresenter _matchingPresenter, ISocketConnecter _socketConnecter, StageEntity _entity, IStagePresenter _stagePresenter)
        {
            this.matchingPresenter = _matchingPresenter;
            this.stagePresenter = _stagePresenter;
            this.socketConnecter = _socketConnecter;
            this.stageEntity = _entity;

        }

        public void Begin()
        {
            matchingPresenter
                .StartButtonClicked
                .Subscribe(_ => Debug.Log("ƒ‹[ƒ€ì¬"));

            matchingPresenter
                .JoinButtonClicked
                .Subscribe(_ => Debug.Log("ƒ‹[ƒ€Q‰Á"));

            matchingPresenter
                .SoloModeButtonClicked
                .Subscribe(_ =>
                {
                    /*await UniTask.WaitUntil(() => stageEntity.stageID != -1);
                    SceneManager.LoadScene(stageEntity.stageID);*/
                    stagePresenter.ActivateStageList();
                });


            stageEntity.OnChangeStageName
                .Where(x => x != null)
                .Subscribe(x => SceneManager.LoadScene(x));
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using ISMS.Presenter.Detail.Player;

namespace ISMS.Presenter.Detail.Stage
{
    public class RepositoryPresenter : MonoBehaviour
    {
        [SerializeField]
        SurveyObjectManager _objManager;
        [SerializeField]
        PlayerCore _playerCore;

        void Awake()
        {
            //await _objManager.Initialize();
        }

        void Start()
        {
            //_playerCore.ChangeCurrentPlayerState(PlayerState.Wait);    
        }

    }
}

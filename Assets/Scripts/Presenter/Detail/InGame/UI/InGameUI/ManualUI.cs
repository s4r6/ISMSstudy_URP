using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Presenter.Detail.Player;
using UniRx;

namespace ISMS.Presenter.Detail.UI
{
    public class ManualUI : MonoBehaviour
    {
        [SerializeField]
        GameObject BackGround;
        [SerializeField]
        GameObject ExploreWindow;
        [SerializeField]
        GameObject DetailWindow;
        [SerializeField]
        GameObject DetailInfoArchives;
        [SerializeField]
        GameObject DetailInfoMove;
        [SerializeField]
        GameObject BackCommand;

        [SerializeField]
        PlayerCore _state;

        void Start()
        {
            _state.CurrentPlayerState
                .Subscribe(x =>
                {
                    WindowOff();
                    if (x == PlayerState.Explore)
                        ExploreManual();
                    else if (x == PlayerState.DetailInfo)
                        DetailManual();
                    else if (x == PlayerState.Archive)
                        ArchiveManual();
                    else if (x == PlayerState.Discover)
                        BackCommandManual();
                }).AddTo(this);
        }
        void WindowOff()
        {
            BackGround.SetActive(false);
            ExploreWindow.gameObject.SetActive(false);
            DetailWindow.gameObject.SetActive(false);
            BackCommand.gameObject.SetActive(false);
            DetailInfoMove.gameObject.SetActive(false);
            DetailInfoArchives.gameObject.SetActive(false);
        }
        void ExploreManual()
        {
            ExploreWindow.SetActive(true);
        }

        void DetailManual()
        {
            DetailWindow.SetActive(true);
            BackCommand.SetActive(true);
            DetailInfoArchives.SetActive(true);
        }

        void ArchiveManual()
        {
            DetailWindow.gameObject.SetActive(true);
            BackCommand.gameObject.SetActive(true);
            DetailInfoMove.gameObject.SetActive(true);
        }

        void BackCommandManual()
        {
            BackCommand.gameObject.SetActive(true);
        }
    }
}

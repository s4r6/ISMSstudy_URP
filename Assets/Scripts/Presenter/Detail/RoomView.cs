using UnityEngine;
using UnityEngine.UI;

namespace ISMS.Presenter.Detail
{
    public class RoomView : MonoBehaviour, IRoomView
    {
        [SerializeField]
        Button startButton;
        [SerializeField]
        Button joinButton;
        [SerializeField]
        Button soloModeButton;

        public Button StartButton => startButton;
        public Button JoinButton => joinButton;
        public Button SoloModeButton => soloModeButton;
    }
}



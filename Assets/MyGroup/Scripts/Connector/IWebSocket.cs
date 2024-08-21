using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using CleanArchtecture;
using ISMS.Data.State;

namespace ISMS.Connecter
{

    public interface IWebSocket:ISocket
    {
        ReactiveProperty<readyState> MyState { get; }
        void CreateRoom();
        void JoinRoom();
        void Connect();

    }
}


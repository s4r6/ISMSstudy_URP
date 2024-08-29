using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using CleanArchtecture;

namespace ISMS.Connecter
{

    public interface IWebSocket:ISocket
    {
        void CreateRoom();
        void JoinRoom();
        void Connect();

    }
}


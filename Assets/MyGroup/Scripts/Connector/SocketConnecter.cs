using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Domain.Connecter;
using System;
using UniRx;
using ISMS.Data.State;

namespace ISMS.Connecter
{
    public class SocketConnecter:ISocketConnecter
    {
        IWebSocket socket;
        public IObservable<readyState> OnStateChanged => socket.MyState;
        public SocketConnecter(IWebSocket _socket)
        {
            this.socket = _socket;
        }

        
    }
}


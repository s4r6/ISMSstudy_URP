using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Domain.Connecter;
using System;
using UniRx;

namespace ISMS.Connecter
{
    public class SocketConnecter:ISocketConnecter
    {
        IWebSocket socket;
        public SocketConnecter(IWebSocket _socket)
        {
            this.socket = _socket;
        }

        
    }
}


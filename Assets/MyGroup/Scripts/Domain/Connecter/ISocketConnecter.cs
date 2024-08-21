using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleanArchtecture;
using System;
using UniRx;
using ISMS.Data.State;

namespace ISMS.Domain.Connecter
{
    public interface ISocketConnecter : IConnecter
    {
       IObservable<readyState> OnStateChanged { get; }
    }
}


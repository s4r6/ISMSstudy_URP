using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : IPlayerPresenter
{
    IPlayerView _view;
    public PlayerPresenter(IPlayerView view)
    {
        _view = view;
    }

    public void InGameInitialize()
    {

    }
}

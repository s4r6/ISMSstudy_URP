using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerAuthority
{
    RoomHost,
    RoomClient
}
public static class PlayerData
{
    private static bool setAuthority = false;
    public static bool SetAuthority
    {
        get
        {
            return setAuthority;
        }
    }
    private static PlayerAuthority m_authority; //é©êgÇÃå†å¿
    public static PlayerAuthority M_Authority
    {
        get
        {
            return m_authority;
        }
        set
        {
            m_authority = value;
            setAuthority = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Defines a network manager with all functionality tied to events.
/// </summary>
public class EventNetworkManager : NetworkManager
{
    #region Singleton
    public static EventNetworkManager instance;
    private void Start()
    {
        instance = this;
    }
    #endregion

    #region Events
    public delegate void _OnNetworkConnectionDelegate(NetworkConnection conn);
    public event _OnNetworkConnectionDelegate ServerPlayerJoined = delegate { };
    public event _OnNetworkConnectionDelegate ServerPlayerReady = delegate { };
    public event _OnNetworkConnectionDelegate ServerPlayerLeft = delegate { };
    #endregion

    #region Callback Methods
    /// <summary>
    /// Called on the server when a client reports that it is ready to receive messages.
    /// </summary>
    public override void OnServerReady(NetworkConnection conn)
    {
        Debug.Log("On Ready.");

        base.OnServerReady(conn);
        ServerPlayerReady.Invoke(conn);
    }
    /// <summary>
    /// Called on server when a client initially connects.
    /// </summary>
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        ServerPlayerJoined.Invoke(conn);
    }
    /// <summary>
    /// Called on server when a client disconnects
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        ServerPlayerLeft.Invoke(conn);
    }
    #endregion
}


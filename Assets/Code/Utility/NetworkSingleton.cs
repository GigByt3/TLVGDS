using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Like a monosingleton, but for network stuff!
/// </summary>
public class NetworkSingleton : NetworkBehaviour
{
    private static NetworkSingleton instance = null;

    /// <summary>
    /// Awake is called pre-initialization.
    /// </summary>
    protected virtual void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Returns the reference of the singleton if it exists, errors out otherwise.
    /// </summary>
    public static NetworkSingleton GetSingleton()
    {
        if (instance == null) { Debug.LogError("MonoSingleton requested but it doesn\'t exist in the scene!"); }
        return instance;
    }
}


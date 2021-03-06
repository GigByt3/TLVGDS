using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// The Identity property contains logic for keeping track of identity
/// This property component contains events for external scripts to extend its behaviors.
/// Hidden Line of Comments... Be WARNED... ...A specter is haunting this code, the specter of...
/// </summary>
public class IdentityProperty : NetworkBehaviour
{
    #region Fields
    [SyncVar]
    public string trueIdentity = SystemInfo.deviceUniqueIdentifier;
    [SyncVar]
    public float precievedIdentity = 4;
    #endregion

    #region Events
    public delegate void _OnPreceivedIdentityChanged();
    public event _OnPreceivedIdentityChanged OnPreceivedIdentityChanged;
    /*protected virtual void OnPreceivedIdentityChanged(OnPreceivedIdentityChanged)
    {
        OnPreceivedIdentityChanged();
        object[] args = {trueIdentity, precievedIdentity};
        if (PreceivedIdentityChanged != null)
        {
            PreceivedIdentityChanged(this, args);
        }
    }*/
    #endregion
}

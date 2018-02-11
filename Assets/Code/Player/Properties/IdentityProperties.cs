using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// The Identity property contains logic for keeping track of identity
/// This property component contains events for external scripts to extend its behaviors.
/// </summary>
/*public class IdentityProperty : NetworkBehaviour
{
    #region Fields
    [SyncVar]
    public string trueIdentity = SystemInfo.deviceUniqueIdentifier;
    [SyncVar]
    //public float precievedIdentity = ;
    #endregion

    #region Events
    public delegate void _OnPreceivedIdentityChanged(object source, eventArgs args);
    public event _OnPreceivedIdentityChanged OnPreceivedIdentityChanged;
    protected vitrual void OnPreceivedIdentityChanged()
    {
        if (PreceivedIdentityChanged != null)
        {
            PreceivedIdentityChanged(this, [trueIdentity, precievedIdentity])
        }
    }
    #endregion
}
*/

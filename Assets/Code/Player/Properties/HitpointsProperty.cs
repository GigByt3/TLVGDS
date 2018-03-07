using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// The Hitpoints property contains logic for a simple hitpoints system.
/// This property component contains events for external scripts to extend its behaviors.
/// </summary>
public class HitpointsProperty : NetworkBehaviour
{
    #region Fields
    [SyncVar]
    public float hitpoints = 100;
    #endregion

    #region Events
    // EVERYONE Event fired when the property takes damage.
    public delegate void _OnDamaged(float amount);
    public event _OnDamaged OnDamaged = delegate { };
    public delegate void _IDead();
    public event _IDead IDead = delegate { };
    #endregion

    #region Main Methods
    /// <summary>
    /// Called from the server, this deals damage to the player.
    /// </summary>
    [ClientRpc]
    public void RpcDealDamage(float damage)
    {
        hitpoints -= damage;
        OnDamaged.Invoke(damage);
        if(hitpoints <= 1 && isServer) 
        {
            IDead.Invoke();
        }
        Debug.Log("Damaged!");
    }
    [ClientRpc]
    public void RpcDeath()
    {
    //    this.transform.position() = new Vector3(1000, 1000, -1);
    }
    #endregion
}

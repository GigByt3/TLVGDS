using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Defines a skeleton for an attack behaviour.
/// </summary>
/// <remarks>
/// The default behaviour for BasePlayerAttack is an invisible, raycasted attack.
/// </remarks>
public class BasePlayerAttack : NetworkBehaviour
{
    #region Fields
    // Base Config
    public float damagePerHit = 10f;
    public float range = 10f;

    // Attack info.
    public float currentAttackCooldown = 3;
    public float maxAttackCooldown = -1;
    private bool hasCooledDownDebounce = false;
    #endregion

    #region Events
    // OnAttacked is fired when the player attacks using this weapon. EVERYONE
    public delegate void _OnAttacked(Vector3 attackDirection);
    public event _OnAttacked OnAttacked = delegate { };

    // OnRechargeComplete is called when the power recharges and is ready to fire again. EVERYONE.
    public delegate void _OnRechargeComplete();
    public event _OnRechargeComplete OnRechangeComplete = delegate { };
    #endregion

    #region Cooldown
    /// <summary>
    /// Awake is called pre-initialization.
    /// </summary>
    protected virtual void Awake()
    {
        maxAttackCooldown = currentAttackCooldown;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    protected virtual void Update()
    {
        // Decrease cooldown.
        currentAttackCooldown -= Time.deltaTime;
        currentAttackCooldown = Mathf.Clamp(currentAttackCooldown, 0, maxAttackCooldown);
        if(currentAttackCooldown == 0 && !hasCooledDownDebounce)
        {
            hasCooledDownDebounce = true;
            OnRechangeComplete.Invoke(); // Notify listeners that reload is complete.
        }
    }
    #endregion

    #region Attack Handling / Logic
    /// <summary>
    /// CmdAttack is the actual command attack sent from the client to the server.
    /// </summary>
    [Command]
    public void CmdAttack(Vector3 aimingVector)
    {
        // If we can't fire yet, don't.
        if(currentAttackCooldown > 0) { return; }
        
        // Notify clients.
        RpcOnAttacked(aimingVector);

        // Handle actual attack.
        ServerAttackMethod(aimingVector);
    }

    /// <summary>
    /// Called on the server, use this method to implement your own attack behaviors!
    /// </summary>
    /// <remarks>
    /// This section of the code contains the attack logic isolated from the rest of weapon functionality.
    /// Edit this if you only want to change how the attack works.
    /// </remarks>
    public virtual void ServerAttackMethod(Vector3 aimingVector)
    {
        // Process the actual damage stats on the server.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimingVector, range);

        // Hit logic
        if (hit.transform != null)
        {
            // How to damage players from the server: 
            // 1. Search for a HitpointsProperty component
            HitpointsProperty hitpointsProperty = hit.transform.GetComponent<HitpointsProperty>();

            // 2. If one exists, just damage it. Simple!
            if(hitpointsProperty != null)
            {
                hitpointsProperty.RpcDealDamage(damagePerHit);
            }
        }
    }

    /// <summary>
    /// RpcOnAttacked is fired from the server to clients when the player fires an attack.
    /// </summary>
    [ClientRpc]
    public void RpcOnAttacked(Vector3 aimingVector)
    {
        currentAttackCooldown = maxAttackCooldown; // Resets attack cooldown for clients.
        hasCooledDownDebounce = false; // Allows recharge event to fire again.
        OnAttacked.Invoke(aimingVector); // Invokes event
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IvoryAttack : BasePlayerAttack
{
    public override void ServerAttackMethod(Vector3 aimingVector)
    { 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimingVector, range);
        
        int poison = (2)EffectType.HealthPerSecond;
        // Hit logic
        if (hit.transform != null)
        {
            // How to damage players from the server but using effects on contact: 
            // 1. Search for a PlayerEffectsManager:
            PlayerEffectsManager playerEffectsManager = hit.transform.GetComponent<PlayerEffectsManager.cs>();

            // 2. If one exists, just damage it. Simple!
            if(playerEffectsManager != null)
            {
                  playerEffectsManager.RpcAddEffect(PlayerEffect poison);
            }
       }
    }
}

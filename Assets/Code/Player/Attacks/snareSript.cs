using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class snare : NetworkBehavior
{
    
    if(!isServer) { return; }
     void OnTriggerEnter2D(Collider2D other) {
         if(GetComponent<PlayerEffectsManager> =! null )
         {
            int stasis = (int)EffectType.Speed 0;
            PlayerEffectsManager.RpcAddEffect(PlayerEffect stasis);
         }
    }
}
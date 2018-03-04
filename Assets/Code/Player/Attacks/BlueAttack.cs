using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BlueAttack : BasePlayerAttack
{
    //Snare

    public override ServerAttackMethod(Vector3 aimingVector)
    {
        Instantiate(snare, aimingVector, Quaternion.identity)        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


// At present this class inherits the BasePlayerAttack as the functionality of the attack is very similar
public class IdentitySwitch : BasePlayerAttack
{
    public override void ServerAttackMethod(Vector3 aimingVector)
    { 
        RaycastHit2D getID = Physics2D.Raycast(transform.position, aimingVector, range);
        if (getID.transform != null)
        {
            // Searching for Identity Property
            IdentityProperty identityProperty = getID.transform.GetComponent<IdentityProperties.cs>();
            string ourIdentity = this.GetComponent<IdentityProperties.cs>().precievedIdentity;

            // Switching identitys
            if(identityProperty != null)
            {
                string storedPrecievedIdentity = identityProperty.precievedIdentity;
                identityProperty.precievedIdentity = ourIdentity;
                ourIdentity = storedPrecievedIdentity;
            }
        }
    }
}
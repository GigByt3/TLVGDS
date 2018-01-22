using UnityEngine;

public class charecterIdentity : NetworkBehavior
{
    public void createIdentity()
    {
        Random rnd = new Random(type);
        int id = NetworkIdentity.netId
        if(type == true) {return(id)};
        int preveived_id = rnd.Next(12);
        return(preveived_id);    
    }
}
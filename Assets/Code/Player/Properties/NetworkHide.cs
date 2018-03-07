using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHide : NetworkBehavior
{
    [SyncVar];
    boolean hidden = false;
    [SyncVar]
    int time = Time.deltaTime;

    #region Methods
    private void Update()
    {
        time = Time.deltaTime - time;
    }

    public void Reveal(float seconds)
    {
        hidden = false;
        OnReveal()
        int startClock = time;
        while(hidden == false)
        {
            if(startClock == seconds)
            {
                hidden = true;
                OnHide();
            }
        }
    }
    #endregion
}

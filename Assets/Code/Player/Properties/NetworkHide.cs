using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHide : NetworkBehaviour
{
    [SyncVar]
    bool hidden = false;
    [SyncVar]
    float time = Time.deltaTime;

    #region Methods
    private void Update()
    {
        time = Time.deltaTime - time;
    }

    public void Reveal(float seconds)
    {
        hidden = false;
        OnReveal();
        float startClock = time;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHideVisualController : NetworkBehaviour
{
    public Sprite Assasin;
    public Sprite Normmy;
    public AudioSource RevealSound;
    public AudioSource HideSound;


    #region Events
    public delegate void _OnHide();
    public delegate void _OnReveal();
//  public event _OnHide OnHide = delegate { };
//  public event _OnReveal OnReveal = delegate { };
    [ClientRpc]
    protected virtual void OnHide()
    {
        SpriteRenderer.sprite() = Assasin;
        RevealSound.Play();
    }
    [ClientRpc]
    protected virtual void OnReveal()
    {
        SpriteRenderer.sprite = Normmy;
        RevealSound.Play();
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHideVisualController : NetworkBehaviour
{
    public Sprite Assasin;
    public Sprite Normmy;
    private SpriteRenderer SneakySpriteRenderer = this.GetComponent<SpriteRenderer>();
    public AudioSource RevealSound;
    public AudioSource HideSound;

 
    #region Events  
    public delegate void _OnHideThis();
    public static event _OnHideThis OnHideThis;
    [ClientRpc]
    protected virtual void OnHide()
    {
        OnHideThis();
        SneakySpriteRenderer.Sprite = Normmy;
        HideSound.Play();
    }  

    public delegate void _OnRevealThis();
    public event _OnRevealThis OnRevealThis;
    [ClientRpc]
    protected virtual void OnReveal()
    {
        OnRevealThis();
        SneakySpriteRenderer.Sprite = Assasin;
        RevealSound.Play();
    } 
    #endregion
}
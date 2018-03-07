using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHideVisualController : NetworkBehavior
{
    public Sprite Assasin;
    public Sprite BurgoiseNobelWhoExploitsTheLaborOfTheProlariatFromTheDictorialPlaceOfPowerWeildedByTheControlingClassToSubjugateTheOtherClasses;
    public int[] sprites = [Assasin, BurgoiseNobelWhoExploitsTheLaborOfTheProlariatFromTheDictorialPlaceOfPowerWeildedByTheControlingClassToSubjugateTheOtherClasses];
    public AudioSource RevealSound;
    public AudioSource HideSound;


    #region Events
    public delegate void _OnHide();
    public delegate void _OnReveal();
    public event _OnHide OnHide = delegate { };
    public event _OnReveal OnReveal = delegate { };
    [ClientRPC]
    protected virtual void OnHide()
    {
        spriteRenderer.sprite = Assasin;
        RevealSound.Play();
    }
    [ClientRPC]
    protected virtual void OnReveal()
    {
        spriteRenderer.sprite = BurgoiseNobelWhoExploitsTheLaborOfTheProlariatFromTheDictorialPlaceOfPowerWeildedByTheControlingClassToSubjugateTheOtherClasses;
        RevealSound.Play();
    }
    #endregion
}
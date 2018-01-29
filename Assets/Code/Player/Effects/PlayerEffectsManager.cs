using System;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// PlayerEffectsManager is responsible for adding, storing, replicating, and removing effects from players.
/// </summary>
public class PlayerEffectsManager : NetworkBehaviour
{
    #region Fields
    public List<PlayerEffect> PlayerEffects { get; private set; } // Contains a list of all active player effects.

    // EVERYONE event, Fired when player effects change.
    public delegate void _OnPlayerEffectsChanged();
    public event _OnPlayerEffectsChanged OnPlayerEffectsChanged = delegate { };

    // EVERYONE event, Fired when a new player effect is added.
    public delegate void _OnPlayerEffectsAdded(PlayerEffect effect);
    public event _OnPlayerEffectsAdded OnPlayerEffectsAdded = delegate { };
    #endregion

    #region Main Methods
    /// <summary>
    /// Awake is fired pre-initialization.
    /// </summary>
    private void Awake()
    {
        PlayerEffects = new List<PlayerEffect>();
    }

    /// <summary>
    /// Update is called every frame.
    /// </summary>
    private void Update()
    {

        // This is trash. Refactor earlier than 3 a.m.

        // Decrement duration values
        List<PlayerEffect> newPlayerEffectsList = new List<PlayerEffect>();

        foreach(PlayerEffect effect in PlayerEffects)
        {
            // If the duration will be above zero, re-add to the collection. Dump it out otherwise.
            if(effect.duration - Time.deltaTime > 0)
            {
                PlayerEffect modified = new PlayerEffect(effect.effectType, effect.duration - Time.deltaTime, effect.modifierFloat, effect.modifierString);
                newPlayerEffectsList.Add(modified);
            }
            else
            {
                // Notify event listeners of changes.
                OnPlayerEffectsChanged.Invoke();
            }
            
        }

        PlayerEffects = newPlayerEffectsList;
    }

    /// <summary>
    /// RpcAddEffect is called by the server to add an effect to this manager.
    /// </summary>
    [ClientRpc]
    public void RpcAddEffect(PlayerEffect effect)
    {
        // Add it.
        PlayerEffects.Add(effect);

        // Notify event listeners of changes.
        OnPlayerEffectsChanged.Invoke();
        OnPlayerEffectsAdded.Invoke(effect);
    }
    #endregion
}

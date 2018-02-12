using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine.Networking;
using UnityEngine;


/// <summary>
/// MatchClient stores client-specific match data (Username, score, etc.) and provides
/// an outlet for players to send commands to the MatchController.
/// </summary>
/// <remarks>
/// This should be attached to a player object that is persistent between rounds.
/// </remarks>
public class MatchClient : NetworkBehaviour
{
    #region Get Match Controller
    private MatchController matchController;

    /// <summary>
    /// Awake is called at pre-initialization. We use it to report the construction of this instance
    /// to the MatchController.
    /// </summary>
    private void Awake()
    {
        // First, get the match controller.
        matchController = MatchController.GetSingleton() as MatchController;

        // Next, tell the match controller that we exist.
        matchController.AddMatchClient(this);

        // Finally, for organization, place this game object as a child of the match controller.
        transform.parent = matchController.transform;
    }

    /// <summary>
    /// OnDestroy is called right before the object is destroyed. This is used to report the destruction of this
    /// instance to the MatchController.
    /// </summary>
    private void OnDestroy()
    {
        matchController.RemoveMatchClient(this);
    }
    #endregion

    #region SyncVars
    [SyncVar]
    public string nickname = "Player";
    [SyncVar]
    public int characterChoice = 0;
    [SyncVar]
    public int score = 0;
    [SyncVar]
    public int kills = 0;
    [SyncVar]
    public int deaths = 0;
    [SyncVar]
    public bool isSpectator = false;
    [SyncVar]
    public NetworkIdentity playerIdentity = null;
    #endregion

    #region Commands
    /// <summary>
    /// Sent from Client to Server to change the nickname of the player.
    /// </summary>
    [Command]
    public void CmdChangeNickname(string newNickname)
    {
        nickname = newNickname;
    }

    /// <summary>
    /// Sent from Client to Server to change the selected character for this round.
    /// </summary>
    [Command]
    public void CmdChangeChosenCharacter(int newCharacterChoice)
    {
        characterChoice = Mathf.Clamp(newCharacterChoice, 0, matchController.GetMaxCharacterSelectionId());
    }
    #endregion

    #region Server Methods
    /// <summary>
    /// Called from the server, this method instantiates the player's character object based on his/her choice of character.
    /// Additionally, it will not spawn if the player already has a character.
    /// </summary>
    public void ServerInstantiatePlayerObject()
    {
        if(playerIdentity == null)
        {
            // Spawn new player
            GameObject newPlayer = (GameObject)Instantiate(matchController.characterList[characterChoice], Vector3.zero, Quaternion.identity);
            NetworkServer.SpawnWithClientAuthority(newPlayer, connectionToClient);
            

            // Set player
            playerIdentity = newPlayer.GetComponent<NetworkIdentity>();

            // Debug!
            Debug.Log("Spawned client\'s character!");
        }
    }
    #endregion
}


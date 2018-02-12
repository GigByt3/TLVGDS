using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// MatchController is a joint client-server NetworkBehaviour that controls logic around the match itself.
/// </summary>
public class MatchController : NetworkSingleton
{
    #region Config
    public GameObject[] characterList; // List of playable character GO's.
    public float roundTimeLength = 120; // Length of each round.
    #endregion

    #region Fields
    public List<MatchClient> clients = new List<MatchClient>(); // List of MatchClient subordinates in the game.
    public float networkTimer = 120; // The timer. The time is kept on clients, and its initial state is broadcasted by the server at start and on join-in-progress.
    #endregion

    #region SyncVars
    // Game State -- 0 = Setup, 1 = In Progress, 2 = Finished
    [SyncVar(hook = "OnGameStateChanged")]
    public int gameState = 0;
    #endregion

    #region Join In Progress Synchronization
    /// <summary>
    /// Start is called on initialization.
    /// </summary>
    private void Start()
    {
        EventNetworkManager.instance.ServerPlayerReady += FireJoinInProgressSyncRPCs;
    }

    /// <summary>
    /// This event listener fires all join in progress updating RPCs when a new player connects.
    /// </summary>
    public void FireJoinInProgressSyncRPCs(NetworkConnection conn)
    {
        TargetSyncNetworkTimer(conn, networkTimer);
    }

    /// <summary>
    /// This RPC is sent by the server to joining clients to synchronize the server clock.
    /// </summary>
    [TargetRpc]
    public void TargetSyncNetworkTimer(NetworkConnection conn, float timer)
    {
        networkTimer = timer;
    }
    #endregion

    #region Match Control
    /// <summary>
    /// Fired when the game state changes on everyone.
    /// </summary>
    private void OnGameStateChanged(int newState)
    {
        gameState = newState; // Update state for all.

        Debug.Log("State changed to: " + newState.ToString());

        if (newState == 0) // Changed to game setup...
        {
            networkTimer = roundTimeLength; // Reset the time for everyone.
        }
        else if (newState == 1) // Changed to game running...
        {
            Debug.Log(isServer);
            if(isServer) { SpawnPlayers(); } // For the server, add all player objects at round start.
        }
        else if (newState == 2) // Changed to game ended...
        {
            networkTimer = roundTimeLength; // Reset the time for everyone.

            if (isServer) { CleanupPlayers(); } // For the server, cleanup all objects.
        }
    }
    
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if(gameState == 0) // Game setup...
        {
            
        }
        else if(gameState == 1) // Game running...
        {
            networkTimer -= Time.deltaTime; // Decrement timer for everyone.

            if (isServer) // End round logic for server.
            {
                if(networkTimer <= 0)
                {
                    gameState = 2;
                }
            }
        }
        else if(gameState == 2) // Game ended...
        {
            
        }
    }
    #endregion

    #region Main Methods
    /// <summary>
    /// Called by new MatchClients, this adds the MatchClient to the list of match clients.
    /// </summary>
    public void AddMatchClient(MatchClient clientAdding)
    {
        clients.Add(clientAdding);
    }

    /// <summary>
    /// Called by MatchClients whose owners left the game, this removes the MatchClient from the list of match clients.
    /// </summary>
    public void RemoveMatchClient(MatchClient clientRemoving)
    {
        clients.Remove(clientRemoving);
    }
    
    /// <summary>
    /// Called by the server, this spawns all of the players' objects.
    /// </summary>
    public void SpawnPlayers()
    {
        foreach (MatchClient client in clients)
        {
            client.ServerInstantiatePlayerObject();
        }
    }

    /// <summary>
    /// Called by the server, this cleans up all of the players' objects.
    /// </summary>
    public void CleanupPlayers()
    {
        foreach (MatchClient client in clients)
        {
            if(client.playerIdentity != null)
            {
                NetworkServer.Destroy(client.playerIdentity.gameObject);
            }
        }
    }
    #endregion

    #region Other Methods
    /// <summary>
    /// Returns the maximum array index of characterList. Used for clamping / sanity checks
    /// when clients request to change characters.
    /// </summary>
    public int GetMaxCharacterSelectionId()
    {
        return characterList.Length - 1;
    }
    #endregion

    #region Debug
    public bool isDebug = false; // When true, debug is enabled.
    private Rect debugWindowRect = new Rect(0, 0, 100, 200);

    /// <summary>
    /// Used to draw debug information.
    /// </summary>
    private void OnGUI()
    {
        if (!isDebug) { return; }
        debugWindowRect = GUI.Window(0, debugWindowRect, DebugMatchStatusWindow, "MatchController");
    }

    /// <summary>
    /// Used to draw what is within the debug window.
    /// </summary>
    private void DebugMatchStatusWindow(int id)
    {
        switch (gameState)
        {
            case 0: // Setup
                GUILayout.Label("Game Setup");
                if(isServer)
                {
                    if (GUILayout.Button("Begin Round"))
                    {
                        gameState = 1;
                    }
                }
                break;
            case 1:
                GUILayout.Label("Game Running");
                GUILayout.Label("Time Left: " + (Mathf.Round(networkTimer * 10) / 10).ToString());
                if (isServer && GUILayout.Button("End Game Early"))
                {
                    gameState = 2;
                }
                break;
            case 2:
                GUILayout.Label("Game Finished");
                if (isServer && GUILayout.Button("Finish"))
                {
                    gameState = 0;
                }
                break;
        }

        GUI.DragWindow(); // Makes window draggable
    }
    #endregion
}

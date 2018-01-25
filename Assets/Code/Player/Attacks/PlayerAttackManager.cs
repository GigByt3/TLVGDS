using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

/// <summary>
/// PlayerAttackManager manages the local player's attacks. It
/// has been written to be as easy as possible for designers to swap
/// values in and out.
/// </summary>
public class PlayerAttackManager : NetworkBehaviour
{
    public PlayerAttackDefinition[] attackDefinitions;
    public Camera playerCamera;

    /// <summary>
    /// Start is called at initialization.
    /// </summary>
    private void Start()
    {
        playerCamera = Camera.main;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if(!isLocalPlayer) { return; } // Do not check for inputs on other players.

        // Calculate current rotation between mouse cursor position and player game object, which we will imply is the parent object.
        Vector3 mousePosWorldCoords = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 directionToMouse = mousePosWorldCoords - transform.position;
        directionToMouse = new Vector3(directionToMouse.x, directionToMouse.y, 0);

        // Go through each definition, and pass the attack on to the BasePlayerAttack instance if the corresponding button is down.
        foreach (PlayerAttackDefinition definition in attackDefinitions)
        {
            if(Input.GetButton(definition.inputKey))
            {
                definition.attack.CmdAttack(directionToMouse);
            }
        }

        Debug.DrawRay(transform.position, directionToMouse);
    }
}

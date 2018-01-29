using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// A Class that allows players to navigate around the world without running into obstacles.
/// </summary>
public class PlayerMovementController : NetworkBehaviour
{
    #region Fields
    // Input Axes
    public string horizontalMovementAxis = "Horizontal"; // Defines the input manager axis to read horizontal input.
    public string verticalMovementAxis = "Vertical"; // Defines the input manager axis to read horizontal input.

    public float characterRadius = .5f; // Character movement collision size.

    public float maxMovementSpeed = 5f; // Defines the maximum speed this character can move.
    public float acceleration = 2f; // Defines the acceleration, or how fast the character speeds up.
    #endregion

    #region Main Methods
    /// <summary>
    /// FixedUpdate is called once per physics frame
    /// </summary>
    void FixedUpdate()
    {
        if(!isLocalPlayer) { return; } // Yield control if not local player.

        // Get input
        float horizontalAxisValue = Input.GetAxis(horizontalMovementAxis) * Time.fixedDeltaTime;
        float verticalAxisValue = Input.GetAxis(verticalMovementAxis) * Time.fixedDeltaTime;

        // Get move vector for this frame.
        Vector2 translationVectorThisFrame = Vector2.ClampMagnitude(new Vector2(horizontalAxisValue * maxMovementSpeed * acceleration, verticalAxisValue * maxMovementSpeed * acceleration), maxMovementSpeed);

        // Collision check for going through walls.
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, characterRadius, translationVectorThisFrame, translationVectorThisFrame.magnitude);
        if(hit.transform != null)
        {
            // Bump math! Bump bump bump!
            translationVectorThisFrame = translationVectorThisFrame + hit.normal * translationVectorThisFrame.magnitude;
        }
        
        // Skkrrt! This moves the object based on the displacement vector we calculated.
        transform.Translate(translationVectorThisFrame);
    }
    #endregion
}

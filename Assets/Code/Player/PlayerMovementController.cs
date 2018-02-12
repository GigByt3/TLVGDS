using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// A Class that allows players to navigate around the world without running into obstacles.
/// </summary>
[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovementController : NetworkBehaviour
{
    #region Fields
    // 2D Character Controller
    private CharacterController2D controller = null;

    // Input Axes
    public string horizontalMovementAxis = "Horizontal"; // Defines the input manager axis to read horizontal input.
    public string verticalMovementAxis = "Vertical"; // Defines the input manager axis to read horizontal input.
    public float acceleration = 2f; // Defines the acceleration, or how fast the character speeds up.
    #endregion

    #region Main Methods
    /// <summary>
    /// Start is called at initialization.
    /// </summary>
    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    /// <summary>
    /// FixedUpdate is called once per physics frame
    /// </summary>
    private void FixedUpdate()
    {
        if(!hasAuthority) { return; }

        controller.Move(new Vector2(Input.GetAxis(horizontalMovementAxis) * acceleration * Time.fixedDeltaTime, Input.GetAxis(verticalMovementAxis) * acceleration * Time.fixedDeltaTime));
    }
    #endregion
}

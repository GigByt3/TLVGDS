using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

/// <summary>
/// Defines a top-down character controller for two-dimensional game environments.
/// </summary>
public class CharacterController2D : MonoBehaviour
{
    [Range(0, 10f)]
    public float characterRadius = .5f; // Character movement collision size.
    public float maxMovementSpeed = 5f; // Defines the maximum speed this character can move.

    /// <summary>
    /// Moves the character in the vector direction. Collides with walls / colliders if it can't move in that direction.
    /// Call this in FixedUpdate, not update because this uses physics.
    /// </summary>
    public void Move(Vector2 targetMoveVector)
    {
        // Get move vector for this frame.
        Vector2 translationVectorThisFrame = Vector2.ClampMagnitude(targetMoveVector, maxMovementSpeed);

        // Collision check for going through walls.
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, characterRadius, translationVectorThisFrame, translationVectorThisFrame.magnitude);
        if (hit.transform != null)
        {
            // Bump math! Bump bump bump!
            translationVectorThisFrame = translationVectorThisFrame + hit.normal * translationVectorThisFrame.magnitude;
        }

        // Skkrrt! This moves the object based on the displacement vector we calculated.
        transform.Translate(translationVectorThisFrame);
    }
}


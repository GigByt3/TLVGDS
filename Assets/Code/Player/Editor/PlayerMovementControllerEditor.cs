using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR

[CustomEditor(typeof(PlayerMovementController))]
public class PlayerMovementControllerEditor : Editor
{
    private const float CIRCLE_VISUALIZATION_INCREMENT = 36;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the regular inspector.

        // Draws the outline of the character.
        for(float i = Mathf.PI/ CIRCLE_VISUALIZATION_INCREMENT; i < Mathf.PI*2; i += Mathf.PI / CIRCLE_VISUALIZATION_INCREMENT)
        {
            float lastI = i - Mathf.PI / CIRCLE_VISUALIZATION_INCREMENT;
            PlayerMovementController targetController = (PlayerMovementController)target;
            Transform targetTransform = targetController.transform;

            Debug.DrawLine(targetTransform.position + new Vector3(Mathf.Cos(lastI), Mathf.Sin(lastI), 0) * targetController.characterRadius, targetTransform.position + new Vector3(Mathf.Cos(i), Mathf.Sin(i)) * targetController.characterRadius, Color.green, 1f);
        }
    }
}

#endif
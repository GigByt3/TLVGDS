#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// A utility that automatically links all visible nodes and constructs the node graph.
/// </summary>
[CustomEditor(typeof(PathfindingService))]
public class PathfindingServiceEditor : Editor
{
    private bool shouldDrawNodeLinks = false;

    /// <summary>
    /// OnInspectorGUI is called every time the inspector is drawn.
    /// </summary>
    public override void OnInspectorGUI()
    {
        // Option to draw node links.
        GUILayout.BeginHorizontal();
        GUILayout.Label("View Node Links");
        shouldDrawNodeLinks = GUILayout.Toggle(shouldDrawNodeLinks, "");
        GUILayout.EndHorizontal();

        // Option to calculate node graph.
        if (GUILayout.Button("Recalculate Scene Node Graph"))
        {
            SetupPathfindingSystem();
        }
    }

    /// <summary>
    /// OnSceneGUI is called when the scene's GUI updates.
    /// </summary>
    private void OnSceneGUI()
    {
        // Draw all node links
        if (shouldDrawNodeLinks)
        {
            PathfindingService pathfindingService = PathfindingService.GetSingleton() as PathfindingService;
            foreach (Node node in pathfindingService.graph)
            {
                // Draw node outlines
                Debug.DrawLine(node.transform.position + new Vector3(.5f, .5f), node.transform.position + new Vector3(-.5f, .5f));
                Debug.DrawLine(node.transform.position + new Vector3(-.5f, .5f), node.transform.position + new Vector3(-.5f, -.5f));
                Debug.DrawLine(node.transform.position + new Vector3(-.5f, -.5f), node.transform.position + new Vector3(.5f, -.5f));
                Debug.DrawLine(node.transform.position + new Vector3(.5f, -.5f), node.transform.position + new Vector3(.5f, .5f));

                // Draw links
                foreach (KeyValuePair<Node, float> otherNode in node.neighborNodes)
                {
                    Debug.DrawLine(node.transform.position, otherNode.Key.transform.position, Color.green, Time.deltaTime);
                }
            }
        }
    }

    /// <summary>
    /// Sets up the entire scene's pathfinding system.
    /// </summary>
    /// <remarks>
    /// This is a crappy o(n^2) solution, so it can take a while.
    /// </remarks>
    private void SetupPathfindingSystem()
    {
        // Clear all preexisting relationships in the node setup.
        PathfindingService pathfindingService = PathfindingService.GetSingleton() as PathfindingService;

        pathfindingService.CalculateNodeGraphAdjacencies();
    }
}
#endif

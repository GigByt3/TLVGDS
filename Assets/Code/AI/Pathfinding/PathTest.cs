using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A utility class used to test the pathing behaviors
/// </summary>
public class PathTest : MonoBehaviour {

    // Editor-accessible fields
    public Node[] path = new Node[0];
    public Node to;
    public Node from;

    // Unaccessible fields
    PathfindingService service;

    /// <summary>
    /// Start is called at initialization
    /// </summary>
    void Start () {
        service = PathfindingService.GetSingleton() as PathfindingService;
	}

    /// <summary>
    /// OnGUI is called every time the UI is rendered.
    /// </summary>
    private void OnGUI()
    {
        if(GUILayout.Button("Repath"))
        {
            path = service.GetPath(from, to);
            if(path == null)
            {
                path = new Node[0];
                Debug.Log("No Path.");
            }
            else
            {
                Debug.Log("Found Path.");
            }
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {

        for (int i = 1; i < path.Length; i++)
        {
            Debug.DrawLine(path[i - 1].transform.position, path[i].transform.position, Color.red, Time.deltaTime);
        }
    }
}

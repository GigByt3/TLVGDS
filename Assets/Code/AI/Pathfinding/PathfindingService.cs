using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PathfindingService oversees all nodes in the game world.
/// </summary>
public class PathfindingService : MonoSingleton
{
    public List<Node> graph = new List<Node>(); // Collection of all nodes in the world.

    #region Unity Functions
    /// <summary>
    /// Start is called at initialization.
    /// </summary>
    private void Start()
    {
        CalculateNodeGraphAdjacencies();
    }
    #endregion

    #region Main Methods
    /// <summary>
    /// Adds the node to the node graph.
    /// </summary>
    /// <remarks>
    /// Only used by Node classes to report their existence to the Pathfinding service.
    /// </remarks>
    public void AddNodeToGraph(Node node)
    {
        graph.Add(node);
    }

    /// <summary>
    /// Populates each node with its adjacency data. Takes a short while to calculate.
    /// </summary>
    /// <remarks>
    /// Crap-tier code.
    /// </remarks>
    public void CalculateNodeGraphAdjacencies()
    {
        // Get pathfinding service & all gameobjects with a Node component.
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<Node> allNodes = new List<Node>();

        foreach (GameObject go in allObjects)
        {
            Node node = go.GetComponent<Node>();
            if (go.activeInHierarchy && node != null)
            {
                allNodes.Add(node);
            }
        }

        // Clear all preexisting relationships in the node setup.
        PathfindingService pathfindingService = PathfindingService.GetSingleton() as PathfindingService;
        pathfindingService.graph.Clear();
        foreach (Node node in allNodes)
        {
            node.neighborNodes.Clear();
        }

        // For each node, cast a ray to see if the nodes are visible. If they are, link them together.
        foreach (Node node in allNodes)
        {
            // Add the node to the graph.
            pathfindingService.graph.Add(node);

            foreach (Node otherNode in allNodes)
            {
                // Skip if identical node or already linked.
                if (node == otherNode || node.neighborNodes.ContainsKey(otherNode)) { continue; }

                Vector3 vectorToOtherNode = (otherNode.transform.position - node.transform.position);
                RaycastHit2D hit = Physics2D.Raycast(node.transform.position, vectorToOtherNode.normalized);

                if (hit.transform == null)
                {
                    node.neighborNodes.Add(otherNode, vectorToOtherNode.magnitude);
                }
            }
        }

        Debug.Log("Recalculation done.");
    }

    /// <summary>
    /// Calculates a path from the FROM node to the TO node.
    /// </summary>
    /// <remarks>
    /// Uses a crappy implementation of Djikstra's.
    /// </remarks>
    /// <returns>
    /// An array containing the sequence of nodes in which to travel to reach the destination.
    /// Returns null if the TO node is unreachable from the FROM node.
    /// </returns>
    public Node[] GetPath(Node from, Node to)
    {
        // Initialize collections
        List<Node> unvisitedNodes = new List<Node>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>(); // Key = node, Value = previous node when travelling from FROM node.
        Dictionary<Node, float> distance = new Dictionary<Node, float>(); // Key = node, Value = distance to node when travelling from FROM node.

        foreach(Node node in graph)
        {
            unvisitedNodes.Add(node);
            previous.Add(node, null);
            distance.Add(node, float.MaxValue);
        }

        // Set the intial node with a distance of zero so it's evaluated first.
        distance[from] = 0;

        // Algorithm.
        while(unvisitedNodes.Count > 0)
        {
            // Get closest node by distance.
            Node u = null;
            float minDist = float.MaxValue;
            foreach(Node node in unvisitedNodes)
            {
                if(distance[node] < minDist)
                {
                    minDist = distance[node];
                    u = node;
                }
            }

            // Mark Node u as visited.
            unvisitedNodes.Remove(u);

            // Calculate potential shorter paths to each neighboring node.
            foreach(KeyValuePair<Node, float> neighborNode in u.neighborNodes)
            {


                float altDist = distance[u] + neighborNode.Value;
                if(altDist < distance[neighborNode.Key]) // Shorter path found.
                {
                    distance[neighborNode.Key] = altDist;
                    previous[neighborNode.Key] = u;
                }
            }

            // If u is the TO node, a path has been found!
            if (u == to)
            {
                break;
            }
        }

        // If there is no previous node for the TO node, we never evaluated it and thus it is unreachable.
        if(previous[to] == null)
        {
            return null;
        }

        // Initialize collections and local variables for path construction.
        List<Node> path = new List<Node>();
        Node eval = to;

        while (previous[eval] != null)
        {
            path.Add(previous[eval]);
            eval = previous[eval];
        }

        // Reverse the path so it's readable in the right direction.
        path.Reverse();

        return path.ToArray(); // Return, finally.
    }
    #endregion
}

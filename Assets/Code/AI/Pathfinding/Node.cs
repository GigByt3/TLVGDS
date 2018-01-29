using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a node on a node graph.
/// </summary>
public class Node : MonoBehaviour
{
    [SerializeField]
    public Dictionary<Node, float> neighborNodes = new Dictionary<Node, float>(); // Collection of neighbor nodes
}

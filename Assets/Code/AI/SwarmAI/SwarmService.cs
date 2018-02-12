using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// SwarmService tracks and calls methods on all subordinates and groups.
/// </summary>
public class SwarmService : NetworkBehaviour
{


    // Collections of active objects.
    public LinkedList<SubordinateGroup> activeGroups = new LinkedList<SubordinateGroup>();
    public LinkedList<Subordinate> activeSubordinates = new LinkedList<Subordinate>();


}

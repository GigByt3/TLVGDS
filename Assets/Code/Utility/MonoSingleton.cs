using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

/// <summary>
/// A Singleton class for monobehaviors, allows monobehaviors to be singletons.
/// </summary>
/// <remarks>
/// The difference between Singleton and MonoSingleton is that MonoSingleton assumes a 
/// monobehaviour and thus does not intialize a new Monobehaviour class if none such class
/// exists. However, the drawback is that the class will error out if there is no instance
/// to assign as the singleton.
/// </remarks>
[ExecuteInEditMode]
public class MonoSingleton : MonoBehaviour
{
    private static MonoSingleton instance = null;

    /// <summary>
    /// Awake is called pre-initialization.
    /// </summary>
    protected virtual void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Returns the reference of the singleton if it exists, errors out otherwise.
    /// </summary>
    public static MonoSingleton GetSingleton()
    {
        if(instance == null) { Debug.LogError("MonoSingleton requested but it doesn\'t exist in the scene!"); }
        return instance;
    }
}


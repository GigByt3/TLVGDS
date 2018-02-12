using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Just a mono with a wrapper for Application.Quit.
/// </summary>
public class ExitGameButtonFunc : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

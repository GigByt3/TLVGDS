using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// PlayerEffect is a data structure used to store information describing effects.
/// </summary>
public struct PlayerEffect
{
    public int effectType;
    public float modifierFloat;
    public string modifierString;
    public float duration; // The length of the 

    /// <summary>
    /// Standard constructor for Struct.
    /// </summary>
    /// <param name="effectType">Defines the integer ID representing the effect.</param>
    /// <param name="duration">After this duration, the effect will be removed from the player.</param>
    /// <param name="modifierFloat">An abstract datapoint to represent a float modifier. This could be used, for instance, to boost or reduce walk speed. This is useless on its own and is just an input to external scripts.</param>
    /// <param name="modifierString">An abstract datapoint to represent a string modifier. This could describe many different effects. This is useless on its own and is just an input to external scripts.</param>
    public PlayerEffect(int effectType, float duration, float modifierFloat, string modifierString)
    {
        this.effectType = effectType;
        this.duration = duration;
        this.modifierFloat = modifierFloat;
        this.modifierString = modifierString;
    }
}


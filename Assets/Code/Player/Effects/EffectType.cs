using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// EffectType gives human-friendly names to effect identification numbers.  
/// </summary>
/// <usage>
/// (int)EffectType.Speed is the same as zero, so substitute wherever.
/// </usage>
public enum EffectType : int
{
    Speed = 0,
    HealthPerSecond = 1,
}


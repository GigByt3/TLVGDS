using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Defines an attack so that it can be viewed in the editor
/// </summary>
[Serializable]
public struct PlayerAttackDefinition
{
    public string inputKey;
    public BasePlayerAttack attack;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public interface IAttackAction
    {
        Vector2 GetAttackVector();
        float GetForce();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public interface IAttackAction
    {
        Vector3 GetAttackVector();
        float GetForce();
    }
}
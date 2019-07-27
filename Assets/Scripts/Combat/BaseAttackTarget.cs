using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public abstract class BaseAttackTarget : MonoBehaviour
    {
       public abstract void OnHit(IAttackAction attacker, IExternalForceReceiver attackerForceReceiver);
    }
}

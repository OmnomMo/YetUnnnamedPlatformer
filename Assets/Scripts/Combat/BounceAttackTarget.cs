using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MoUnity
{
    public class BounceAttackTarget : BaseAttackTarget
    {
        public override void OnHit(IAttackAction attacker, IExternalForceReceiver attackerForceReceiver)
        {
            Debug.LogError(attacker.GetAttackVector().ToString());
            attackerForceReceiver.ApplyForce(attacker.GetForce() * attacker.GetAttackVector() * -1);
        }
    }
}

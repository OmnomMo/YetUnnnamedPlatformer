using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MoUnity
{
    public class BounceAttackTarget : BaseAttackTarget
    {
        public override void OnHit(IAttackAction attacker, IExternalForceReceiver attackerForceReceiver)
        {
            DebugHelperDrawer.Instance.SpawnVectorHelperPrefab(attacker.GetForce() /1000 * attacker.GetAttackVector() * -1, PlayerController.Instance.playerObject.transform.position);

            attackerForceReceiver.ResetVelocity();
            
            attackerForceReceiver.ApplyForce(attacker.GetForce() * attacker.GetAttackVector() * -1);
        }
    }
}

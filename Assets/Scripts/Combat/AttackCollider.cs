using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public class AttackCollider : MonoBehaviour
    {
         void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.layer == Definitions.ENVIRONMENT_ATTACKTARGET_LAYER)
            {

                BaseAttackTarget target = collider.gameObject.GetComponent<BaseAttackTarget>();
                if ( target !=  null)
                {
                    target.OnHit(PlayerController.Instance.debugAttack, PlayerController.Instance.debugMovement);
                }
            }
        }
    }
}

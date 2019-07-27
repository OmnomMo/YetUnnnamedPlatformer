using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public interface IExternalForceReceiver
    {
        void ApplyForce(Vector3 force);
    }
}

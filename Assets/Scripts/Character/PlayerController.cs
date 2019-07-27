using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public class PlayerController : MonoBehaviour
    {

        private static PlayerController _instance;
        public static PlayerController Instance { get { return _instance; } }

        public GameObject playerObject;

        public DebugMovement debugMovement;
        public DebugTargeting debugTargeting;
        public DebugAttack debugAttack;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
    }
}

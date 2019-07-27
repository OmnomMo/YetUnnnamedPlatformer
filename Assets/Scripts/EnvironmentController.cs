using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public class EnvironmentController : MonoBehaviour
    {
        private static EnvironmentController _instance;
        public static EnvironmentController Instance { get { return _instance; } }


        public Transform levelCenter;
        public float distanceFromLevelCenter;
        public float cameraDistanceFromLevelCenter;


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


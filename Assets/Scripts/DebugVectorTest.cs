using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public class DebugVectorTest : MonoBehaviour
    {

        public GameObject vectorIndicatorPrefab;
        public Transform levelCenter;
        public List<Transform> testPositionParents;


        private void Start()
        {
            DoForAllCubes(InstantiatevectorIndicators);
            DoForAllCubes(HideCube);
        }

        void Update()
        {
            DoForAllCubes(ShowTangets);
        }

        public void InstantiatevectorIndicators(Transform cube)
        {
            GameObject newIndicator = Instantiate(vectorIndicatorPrefab);
            newIndicator.transform.localScale = new Vector3(1, 1, 1);
            newIndicator.transform.parent = cube;
            newIndicator.transform.localPosition = Vector3.zero;
        }

        public void HideCube(Transform cube)
        {
            cube.GetComponent<MeshRenderer>().enabled = false;
        }

        public void ShowTangets(Transform cube)
        {
            Vector3 tangentVector = VectorUtilities.PositionToTangentVector(cube.position, levelCenter.position);
            cube.rotation = Quaternion.LookRotation(tangentVector, Vector3.up);
        }

        public void DoForAllCubes(System.Action<Transform> CubeAction)
        {
            foreach (Transform cube in testPositionParents)
            {
                CubeAction(cube);
            }
        }
    }
}

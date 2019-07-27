using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity {
    public class CameraFollowPlayer : MonoBehaviour
    {



        // Update is called once per frame
        void Update()
        {
            Vector3 playerPosition = PlayerController.Instance.playerObject.transform.position;
            Vector3 centerPosition = VectorUtilities.GetFlatVector3AtHeight(EnvironmentController.Instance.levelCenter.position, playerPosition.y);

            SetCameraPosition(playerPosition, centerPosition);
            SetCameraRotation(playerPosition, centerPosition);
        }

        private void SetCameraPosition(Vector3 playerPosition, Vector3 centerPosition)
        {
            Vector3 radiusVector = Vector3.Normalize(playerPosition - centerPosition);

            transform.position =
                centerPosition +
                radiusVector * EnvironmentController.Instance.cameraDistanceFromLevelCenter;
        }

        private void SetCameraRotation(Vector3 playerPosition, Vector3 centerPosition)
        {
            transform.rotation = Quaternion.LookRotation(-1 * Vector3.Normalize(
               playerPosition - centerPosition));
        }
    }
}

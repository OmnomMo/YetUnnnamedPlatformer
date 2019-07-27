using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.PlayerInput;

namespace MoUnity {
    public class DebugTargeting : MonoBehaviour
    {


        public Vector2 targetVector;
        public Transform arrowRotationParent;
        public GameObject arrowGameObject;

        public bool isTargeting;
        private DefaultControls input;


        private void Awake()
        {

            input = new DefaultControls();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }


        private void Update()
        {

            Vector2 aimVector = (input.Player.TwinstickAim.ReadValue<Vector2>());

            isTargeting = (aimVector.x > 0.1f || aimVector.y > 0.1f || aimVector.x < -0.1f || aimVector.y < -0.1f);

            SetArrowVisibility(isTargeting);

            if (isTargeting)
            {
                SetTargetArrow(aimVector);
            } else
            {
                SetTargetArrow(new Vector2(PlayerController.Instance.debugMovement.velocity.x, 0));
            }
        }

        private void SetTargetArrow(Vector2 aimVector)
        {
            RotateTargetArrow(aimVector);
            targetVector = aimVector;
        }

        private void RotateTargetArrow(Vector2 inputDirection)
        {
            arrowRotationParent.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, VectorUtilities.Vector2ToAngle(inputDirection)));
        }

        private void SetArrowVisibility(bool visibility)
        {
            arrowGameObject.SetActive(visibility);
        }


    }
}

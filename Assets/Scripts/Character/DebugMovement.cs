using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.PlayerInput;

namespace MoUnity
{
    public class DebugMovement : MonoBehaviour
    {
        [Header("Movement Stats")]
        public int acceleration;
        public int speed;
        public int jumpPower;
        [Range(0,1)]
        public float airControl;





        [Header("Debug Output")]
        public Transform motionVectorDebugObject;
        public bool isGrounded;
        public Vector3 velocity;


        //Private variables

            private int groundObjectContactCount;
        private DefaultControls input;

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void Awake()
        {
            input = new DefaultControls();
        }
        // Start is called before the first frame update
        void Start()
        {
            input.Player.Jump.performed += ExecuteJump;
            // input.Player.Move.performed += ExecuteMove;
        }

        private void OnDestroy()
        {
            input.Player.Jump.performed -= ExecuteJump;
            //input.Player.Move.performed -= ExecuteMove;

        }

        void FixedUpdate()
        {
           
        }

        private void LateUpdate()
        {
            ConstrainDistanceFromLevelCenter();
            RotatePlayerVelocityAlongTangent();
            ClampSpeed();
            SetPlayerRotation();
        }


        private void Update()
        {
            isGrounded = groundObjectContactCount >= 1;
            float xMove = input.Player.Move.ReadValue<Vector2>().x;

            if (xMove > 0.1f || xMove < -0.1f)
            {
                if (isGrounded)
                {
                    ExecuteMove(xMove);
                } else
                {
                    ExecuteMove(xMove * airControl);

                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.layer == Definitions.ENVIRONMENT_FLOOR_LAYER)
            {
                groundObjectContactCount++;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.gameObject.layer == Definitions.ENVIRONMENT_FLOOR_LAYER)
            {
                groundObjectContactCount--;
            }
        }

        public void ClampSpeed()
        {
            float speedFactor = speed / GetPlayerHorizontalSpeed();
            if (speedFactor < 1)
            {
                Vector3 flatVelocity = VectorUtilities.GetFlatVector3(GetComponent<Rigidbody>().velocity);
                GetComponent<Rigidbody>().velocity = new Vector3(flatVelocity.x * speedFactor, GetComponent<Rigidbody>().velocity.y, flatVelocity.z * speedFactor);
            }
        }

        public float GetPlayerHorizontalSpeed()
        {
            Vector3 flatVelocity = VectorUtilities.GetFlatVector3(GetComponent<Rigidbody>().velocity);
            return flatVelocity.magnitude;
        }

        private void RotatePlayerVelocityAlongTangent()
        {

            Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
            Vector3 currentFlatVelocity = VectorUtilities.GetFlatVector3(currentVelocity);
            Vector3 currentFlatTangent = VectorUtilities.PositionToTangentVector(transform.position, EnvironmentController.Instance.levelCenter.position);

            float velocityOffsetAngle = Vector2.Angle(new Vector2(currentFlatTangent.x, currentFlatTangent.z), new Vector2(currentVelocity.x, currentVelocity.z));

            float flatVelocity = 0;

            if (velocityOffsetAngle <= 90)
            {
                flatVelocity = currentFlatVelocity.magnitude;
            } else
            {
                flatVelocity = -1*currentFlatVelocity.magnitude;
            }
            GetComponent<Rigidbody>().velocity = new Vector3(currentFlatTangent.x * flatVelocity, currentVelocity.y, currentFlatTangent.z * flatVelocity);
            velocity = GetComponent<Rigidbody>().velocity;
            
        }


        private void SetPlayerRotation()
        {
            transform.rotation = Quaternion.LookRotation(VectorUtilities.PositionToTangentVector(transform.position, EnvironmentController.Instance.levelCenter.position), Vector3.up);
        }

        private void ExecuteMove(float xMove)
        {
            float moveForce = xMove * Time.deltaTime * acceleration;
            GetComponent<Rigidbody>().AddForce(moveForce * VectorUtilities.PositionToTangentVector(transform.position, EnvironmentController.Instance.levelCenter.position));
        }

        private void ExecuteJump(InputAction.CallbackContext context)
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpPower, 0));
            }
        }

        private void ConstrainDistanceFromLevelCenter()
        {

            Vector3 flatLevelCenterPosition = VectorUtilities.GetFlatVector3AtHeight(EnvironmentController.Instance.levelCenter.position, transform.position.y);
            Vector3 radiusVector = transform.position - flatLevelCenterPosition;
     

            Vector3 translationVector = flatLevelCenterPosition + Vector3.Normalize(radiusVector) * EnvironmentController.Instance.distanceFromLevelCenter;

            transform.position = new Vector3(translationVector.x, transform.position.y, translationVector.z);
        }

     

    }
}

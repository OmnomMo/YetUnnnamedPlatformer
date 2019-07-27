using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MoUnity {
    public class DebugAttack : MonoBehaviour, IAttackAction
    {

        public static readonly float TIMEATTACKINDICATOR_SHOWN = 0.3f;

        public float attackForce;

        public Vector2 targetVector;
        private DefaultControls input;
        public GameObject attackIndicator;
        public Transform attackIndicatorRotationParent;
        private IEnumerator hideAttackIndicatorCoroutine;

        private void Awake()
        {
            input = new DefaultControls();
        }

        void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }


        private void Start()
        {
            input.Player.Attack.performed += ExecuteAttack;
            hideAttackIndicatorCoroutine = HideAttackIndicator(TIMEATTACKINDICATOR_SHOWN);
            attackIndicator.SetActive(false);
        }


        private void OnDestroy()
        {
            input.Player.Attack.performed -= ExecuteAttack;

        }
        private void ExecuteAttack(InputAction.CallbackContext context)
        {
            StopCoroutine(hideAttackIndicatorCoroutine);
            SetTargetIndicator(PlayerController.Instance.debugTargeting.targetVector);
            hideAttackIndicatorCoroutine = HideAttackIndicator(TIMEATTACKINDICATOR_SHOWN);
            StartCoroutine(hideAttackIndicatorCoroutine);
        }

        private void SetTargetIndicator(Vector2 targetDirection)
        {
            targetVector = targetDirection;
            attackIndicatorRotationParent.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, VectorUtilities.Vector2ToAngle(targetDirection)));
            attackIndicator.SetActive(true);
        }

        private IEnumerator HideAttackIndicator(float timeAttackIndicatorShown)
        {
            yield return new WaitForSeconds(timeAttackIndicatorShown);
            attackIndicator.SetActive(false);
        }

        public Vector2 GetAttackVector()
        {
            return targetVector;
        }

        public float GetForce()
        {
            return attackForce;
        }
    }
}

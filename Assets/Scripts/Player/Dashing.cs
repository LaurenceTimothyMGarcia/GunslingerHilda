using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInput
{
    public class Dashing : MonoBehaviour
    {
        [Header("References")]
        public Transform orientation;
        public Transform playerCam;
        private Rigidbody rb;
        private PlayerController pc;

        [Header("Dashing")]
        public float dashForce;
        public float dashUpwardForce;
        public float dashDuration;

        [Header("Cooldown")]
        public float dashCd;
        private float dashCdTimer;

        [Header("Settings")]
        public bool useCameraForward = true;
        public bool allowAllDirections = true;
        public bool disableGravity = false;
        public bool resetVel = true;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            pc = GetComponent<PlayerController>();
        }

        void Update()
        {
            if (PlayerInputManager.Instance.dashPressed())
            {
                Dash();
            }

            if (dashCdTimer > 0)
            {
                dashCdTimer -= Time.deltaTime;
            }
        }

        private void Dash()
        {
            if (dashCdTimer > 0)
            {
                return;
            }
            else
            {
                dashCdTimer = dashCd;
            }

            pc.dashing = true;

            Transform forwardT;

            if (useCameraForward)
            {
                forwardT = playerCam;
            }
            else
            {
                forwardT = orientation;
            }

            Vector3 direction = GetDirection(forwardT);

            Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

            if (disableGravity)
            {
                rb.useGravity = false;
            }

            delayedForceToApply = forceToApply;
            Invoke(nameof(DelayedDashForce), 0.025f);

            Invoke(nameof(ResetDash), dashDuration);
        }

        private Vector3 delayedForceToApply;
        private void DelayedDashForce()
        {
            if (resetVel)
            {
                rb.velocity = Vector3.zero;
            }

            rb.AddForce(delayedForceToApply, ForceMode.Impulse);
        }

        private void ResetDash()
        {
            pc.dashing = false;

            if (disableGravity)
            {
                rb.useGravity = true;
            }
        }

        private Vector3 GetDirection(Transform forwardT)
        {
            float horizontalInput = PlayerInputManager.Instance.getMovement().x;
            float verticalInput = PlayerInputManager.Instance.getMovement().y;

            Vector3 direction = new Vector3();

            if (allowAllDirections)
            {
                direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
            }
            else
            {
                direction = forwardT.forward;
            }

            if (verticalInput == 0 && horizontalInput == 0)
            {
                direction = forwardT.forward;
            }

            return direction.normalized;
        }
    }
}


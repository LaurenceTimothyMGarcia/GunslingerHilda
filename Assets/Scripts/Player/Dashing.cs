using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInput
{

    /***
    *   PlayerInput namespace deals with any script related to player actions
    */

    public class Dashing : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Player orientation to where the player is facing forward")]
        public Transform orientation;
        [Tooltip("Transform info of the player camera")]
        public Transform playerCam;
        private Rigidbody rb;
        private PlayerController pc;

        [Header("Dashing")]
        [Tooltip("Force applied to the dash")]
        public float dashForce;
        public float dashUpwardForce;
        [Tooltip("How long the dash lasts for")]
        public float dashDuration;
        private Vector3 delayedForceToApply;


        [Header("Cooldown")]
        [Tooltip("Dash cooldown time")]
        public float dashCd;
        private float dashCdTimer;

        [Header("Settings")]
        [Tooltip("Dash where the camera is facing")]
        public bool useCameraForward = true;
        [Tooltip("Dash in all directions")]

        public bool allowAllDirections = true;
        [Tooltip("No Gravity while dashing")]

        public bool disableGravity = false;
        [Tooltip("Reset velocity after dash")]

        public bool resetVel = true;


        void Start()
        {
            /***
                Runs before first frame
                Pulls rigidbody and playercontroller script from object
            */

            rb = GetComponent<Rigidbody>();
            pc = GetComponent<PlayerController>();
        }

        void Update()
        {
            /***
                Runs every frame
            */

            // Checks if player pressed the dash button
            if (PlayerInputManager.Instance.dashPressed())
            {
                Dash();
            }

            // If timer is greater than 0, count down
            if (dashCdTimer > 0)
            {
                dashCdTimer -= Time.deltaTime;
            }
        }

        private void Dash()
        {
            /***
                Primary Dash Script
            */

            // Cool down still on timer, then can't dash
            if (dashCdTimer > 0)
            {
                return;
            }
            // Resets timer if less than 0
            else
            {
                dashCdTimer = dashCd;
            }

            // Updates player controller state to dashing
            pc.dashing = true;

            Transform forwardT;

            // Determines what direction is forward
            if (useCameraForward)
            {
                forwardT = playerCam;
            }
            else
            {
                forwardT = orientation;
            }

            // Direction of the player
            Vector3 direction = GetDirection(forwardT);

            // Creates a force in the direction of the player
            Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

            if (disableGravity)
            {
                rb.useGravity = false;
            }

            delayedForceToApply = forceToApply;
            Invoke(nameof(DelayedDashForce), 0.025f);

            Invoke(nameof(ResetDash), dashDuration);
        }

        private void DelayedDashForce()
        {
            /***
                Resets velocity of dash
            */

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
            /***
                Gets direction based on the player input and direction
            */

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


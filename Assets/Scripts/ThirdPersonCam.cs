using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class ThirdPersonCam : MonoBehaviour
    {
        [Header("References")]
        public Transform orientation;
        public Transform player;
        public Transform playerObj;
        public Rigidbody rb;

        public float rotationSpeed;

        public Transform combatLookAt;

        [Header("Camera Styles")]
        public GameObject standardCam;
        public GameObject combatCam;
        public GameObject topDownCam;

        public CameraStyle currentStyle;

        public enum CameraStyle
        {
            Standard,
            Combat,
            Topdown
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            // Swap styles
            if (!PlayerInputManager.Instance.aimPressed())
            {
                SwitchCameraStyle(CameraStyle.Standard);
            }
            if (PlayerInputManager.Instance.aimPressed())
            {
                SwitchCameraStyle(CameraStyle.Combat);
            }

            // Rotate orientation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;

            // Standard camera style
            if (currentStyle == CameraStyle.Standard || currentStyle == CameraStyle.Topdown)
            {
                // rotate player object
                float horizontalInput = PlayerInputManager.Instance.getMovement().x;
                float verticalInput = PlayerInputManager.Instance.getMovement().y;

                Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

                if (inputDir != Vector3.zero)
                {
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                }
            }
            // Combat camera style
            else if (currentStyle == CameraStyle.Combat)
            {
                Vector3 dirToCombat = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
                orientation.forward = dirToCombat.normalized;

                playerObj.forward = dirToCombat.normalized;
            }
        }

        private void SwitchCameraStyle(CameraStyle newStyle)
        {
            standardCam.SetActive(false);
            combatCam.SetActive(false);
            topDownCam.SetActive(false);

            if (newStyle == CameraStyle.Standard)
            {
                standardCam.SetActive(true);
            }
            if (newStyle == CameraStyle.Combat)
            {
                combatCam.SetActive(true);
            }
            if (newStyle == CameraStyle.Topdown)
            {
                topDownCam.SetActive(true);
            }

            currentStyle = newStyle;
        }
    }
}
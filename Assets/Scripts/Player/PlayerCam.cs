using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerCam : MonoBehaviour
    {
        //Mouse/Controller Sensitivity
        [Header("Mouse/Controller Sensitivity")]
        [SerializeField] private float sensX = 50f; 
        [SerializeField] private float sensY = 50f; 

        //Current orientation of player
        [Header("Player Orientation")]
        [SerializeField] private Transform orientation;

        // Private floats for X and Y rotation of camera
        private float xRotation;
        private float yRotation;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        private void Update()
        {
            if (!PlayerInputManager.Instance.isLookingPressed())
            {
                Debug.Log("Not moving");
                return;
            }

            // Debug.Log("Looking Player:" + PlayerInputManager.Instance.getLooking());
            // Debug.Log("Looking Player Raw:" + PlayerInputManager.Instance.getLookingRaw());

            float controllerX = PlayerInputManager.Instance.getLooking().x * Time.deltaTime * sensX;
            float controllerY = PlayerInputManager.Instance.getLooking().y * Time.deltaTime * sensY;
            
            // Debug.Log("Controller X:" + controllerX);
            // Debug.Log("Controller Y:" + controllerY);

            yRotation += controllerX;
            xRotation -= controllerY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*** 
 * Processes the keyboard input from the player
***/

namespace PlayerInput
{
    public class KeyboardInput : MonoBehaviour
    {

        [SerializeField] private InputController input;

        private Vector2 currentMovement;


        void Awake()
        {
            input = new InputController();

            input.Player.Move.performed += ctx => {
                //Reads the current stick/wasd value
                currentMovement = ctx.ReadValue<Vector2>();
            };
        }


        void Update()
        {
            //Want to implement Unity's new movement system
            //Easily allows for controller + keyboard movement

        }

    }
}


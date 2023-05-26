using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*** 
 * Script holder that manages boolean values for each movement state
***/

namespace PlayerInput
{
    public class PlayerInputManager : MonoBehaviour
    {
        //Singleton instance of the input manager
        public static PlayerInputManager Instance = null;

        //Input controller
        private InputController input;
        private InputAction moveAction;
        private InputAction sprintAction;
        private InputAction crouchAction;
        private InputAction lookAction;
        private InputAction jumpAction;

        //Basic Movement
        private Vector2 movement;
        private bool movementPressed;
        private bool sprint;
        [SerializeField] private bool sprintHold;
        private bool crouch;
        [SerializeField] private bool crouchHold;

        //Looking
        private Vector2 looking;
        private bool lookingPressed;

        //Jumping Movement
        private bool jump;
        private bool wallJump;

        //Singleton for only one Input Manager
        private void Awake()
        {
            //Initialize singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(this.gameObject);
            }


            //Initialize Input
            input = new InputController();
            moveAction = input.Player.Move;
            sprintAction = input.Player.Sprint;
            crouchAction = input.Player.Crouch;
            lookAction = input.Player.Look;
            jumpAction = input.Player.Jump;

            //May not need this since its in OnEnable() method as well
            moveAction.Enable();
            sprintAction.Enable();
            crouchAction.Enable();
            lookAction.Enable();
            jumpAction.Enable();

            //Sets player input values with listeners

            //MOVEMENT
            moveAction.performed += ctx => {

                //Reads the current stick/wasd value
                movement = ctx.ReadValue<Vector2>();

                //Sets boolean if stick is moved on either axis
                movementPressed = movement.x != 0 || movement.y != 0;
            };

            //Stops the movement when released
            moveAction.canceled += ctx => movement = Vector2.zero;


            //Checks for Sprinting button
            //Current version of sprint is holding it down to run
            sprintAction.performed += setSprint;
            //Set a toggle bool for sprint
            //True - Hold Down to run
            //False - Toggle to run
            if (sprintHold)
            {
                sprintAction.canceled += setSprint;
            }


            //Crouch Button
            crouchAction.performed += setCrouch;
            //Set a toggle bool for crouch
            //True - Hold Down to crouch
            //False - Toggle to crouch
            if (crouchHold)
            {
                crouchAction.canceled += setCrouch;
            }

            //ROTATION AND LOOKING AROUND WITH CAMERA
            //Deals with player looking around
            lookAction.performed += ctx => {

                //Reads current stick/mouse value
                looking = ctx.ReadValue<Vector2>();

                //Sets boolean if stick/mouse is looking around
                lookingPressed = looking.x != 0 || looking.y != 0;
            };

            //Stops the look when released
            lookAction.canceled += ctx => looking = Vector2.zero;


            //JUMPS
            jumpAction.performed += setJump;
            jumpAction.canceled += setJump;
        }

        //MOVEMENT SETTER AND GETTER
        //Returns the current value of the player movement
        public Vector2 getMovement()
        {
            return movement;
        }
        //Movement but just 1 -1 or 0
        public Vector2 getMovementRaw()
        {
            //Raw of X
            if (movement.x < 0)
            {
                movement.x = -1;
            }
            else if (movement.x > 0)
            {
                movement.x = 1;
            }

            //Raw of Y
            if (movement.y < 0)
            {
                movement.y = -1;
            }
            else if (movement.x > 0)
            {
                movement.y = 1;
            }

            return movement;
        }

        //Returns value if the movement stick was moved or not
        public bool isMovePressed()
        {
            return movementPressed;
        }

        //SPRINT SETTER AND GETTER
        public void setSprint(InputAction.CallbackContext ctx)
        {
            if (sprint)
            {
                sprint = false;
            }
            else
            {
                sprint = true;
            }
        }
        public bool sprintPressed()
        {
            return sprint;
        }

        //Sets Crouch boolean to true or false
        public void setCrouch(InputAction.CallbackContext ctx)
        {
            if (crouch)
            {
                crouch = false;
            }
            else
            {
                crouch = true;
            }
        }
        public bool crouchPressed()
        {
            return crouch;
        }


        //LOOKING SETTER AND GETTER
        //Returns looking value from player
        public Vector2 getLooking()
        {
            return looking;
        }
        //Looking but just 1 -1 or 0
        public Vector2 getLookingRaw()
        {
            //Raw of X
            if (looking.x < 0)
            {
                looking.x = -1;
            }
            else if (looking.x > 0)
            {
                looking.x = 1;
            }

            //Raw of Y
            if (looking.y < 0)
            {
                looking.y = -1;
            }
            else if (looking.x > 0)
            {
                looking.y = 1;
            }

            return looking;
        }
        public bool isLookingPressed()
        {
            return lookingPressed;
        }


        //JUMP SETTER AND GETTER
        public void setJump(InputAction.CallbackContext ctx)
        {
            //Sets jump based on current state of jump
            if (jump)
            {
                jump = false;
            }
            else
            {
                jump = true;
            }
        }
        //Get Jump
        public bool jumpPressed()
        {
            return jump;
        }


        //When script is enabled and disabled
        private void OnEnable()
        {
            moveAction.Enable();
            sprintAction.Enable();
            crouchAction.Enable();
            lookAction.Enable();
            jumpAction.Enable();
        }
        void OnDisable()
        {
            moveAction.Disable();
            sprintAction.Disable();
            crouchAction.Disable();
            lookAction.Disable();
            jumpAction.Disable();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*** 
 * Script holder that manages boolean values for each movement state
***/

namespace PlayerInput
{

    /***
    *   PlayerInput namespace deals with any script related to player actions
    */

    public class PlayerInputManager : MonoBehaviour
    {
        //Singleton instance of the input manager
        public static PlayerInputManager Instance = null;

        //Input controller
        private InputController input;
        private InputAction moveAction;
        private InputAction dashAction;
        private InputAction crouchAction;
        private InputAction lookAction;
        private InputAction jumpAction;
        private InputAction fireAction;
        private InputAction aimAction;
        private InputAction reloadAction;

        //Basic Movement
        private Vector2 movement;
        private bool movementPressed;
        private bool dash;
        [Header("Determines if player needs to hold or tap to trigger")]
        [SerializeField] private bool dashHold;
        private bool crouch;
        [SerializeField] private bool crouchHold;

        //Looking
        private Vector2 looking;
        private bool lookingPressed;

        //Jumping Movement
        private bool jump;
        private bool wallJump;

        // Fire/shooting
        private bool fire;
        [SerializeField] private bool fireHold;

        // Aim
        private bool aim;
        [SerializeField] private bool aimHold;

        // Reload
        private bool reload;

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
            dashAction = input.Player.Dash;
            crouchAction = input.Player.Crouch;
            lookAction = input.Player.Look;
            jumpAction = input.Player.Jump;
            fireAction = input.Player.Fire;
            aimAction = input.Player.Aim;
            reloadAction = input.Player.Reload;

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


            //Checks for dash button
            //Current version of dash is holding it down to run
            dashAction.performed += setDash;
            dashAction.canceled += setDash;
            //Set a toggle bool for dash
            //True - Hold Down to run
            //False - Toggle to run
            if (dashHold)
            {
                dashAction.canceled += setDash;
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

            //Fire Button
            fireAction.performed += setFire;
            fireAction.canceled += setFire;
            //Set a toggle bool for fire
            //True - Hold Down to fire
            //False - Toggle to fire
            if (fireHold)
            {
                fireAction.canceled += setFire;
            }

            //Aim Button
            aimAction.performed += setAim;
            //Set a toggle bool for Aim
            //True - Hold Down to Aim
            //False - Toggle to Aim
            if (aimHold)
            {
                aimAction.canceled += setAim;
            }

            //JUMPS
            reloadAction.performed += setReload;
            reloadAction.canceled += setReload;
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

        //dash SETTER AND GETTER
        public void setDash(InputAction.CallbackContext ctx)
        {
            if(!PauseMenu.isPaused)
            {
                if (dash)
                {
                    dash = false;
                }
                else
                {
                    dash = true;
                }
            }
        }
        public bool dashPressed()
        {
            return dash;
        }

        //Sets Crouch boolean to true or false
        public void setCrouch(InputAction.CallbackContext ctx)
        {
            if(!PauseMenu.isPaused)
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

        //Sets fire boolean to true or false
        public void setFire(InputAction.CallbackContext ctx)
        {
            if(!PauseMenu.isPaused)
            {
                if (fire)
                {
                    fire = false;
                }
                else
                {
                    fire = true;
                }
            } 
        }
        public bool firePressed()
        {
            return fire;
        }

        //Sets aim boolean to true or false
        public void setAim(InputAction.CallbackContext ctx)
        {
            if(!PauseMenu.isPaused)
            {
                if (aim)
                {
                    aim = false;
                }
                else
                {
                    aim = true;
                }
            }
        }
        public bool aimPressed()
        {
            return aim;
        }

        //Reload SETTER AND GETTER
        public void setReload(InputAction.CallbackContext ctx)
        {
            if(!PauseMenu.isPaused)
            {
                //Sets Reload based on current state of Reload
                if (reload)
                {
                    reload = false;
                }
                else
                {
                    reload = true;
                }
            }
        }
        //Get reload
        public bool reloadPressed()
        {
            return reload;
        }


        //When script is enabled and disabled
        private void OnEnable()
        {
            input.Enable();
        }
        void OnDisable()
        {
            input.Disable();
        }
    }
}

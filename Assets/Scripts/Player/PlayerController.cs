using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    * Player controller
*/

namespace PlayerInput
{

    /***
    *   PlayerInput namespace deals with any script related to player actions
    */

    public class PlayerController : MonoBehaviour
    {

        [Header("Movement")]
        //Base movement speed of the player
        [SerializeField] private float aimSpeed;
        [SerializeField] private float sprintSpeed;
        private float moveSpeed;

        public float dashSpeed;
        public float dashSpeedChangeFactor;

        //Friction so the speed doesn't go on forever
        [SerializeField] private float groundDrag;


        [Header("Jumping")]
        //Force needed to jump
        [SerializeField] private float jumpForce;
        //Cooldown between jumps
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float airMultiplier;
        private bool readyToJump;


        [Header("Crouching")]
        [SerializeField] private float crouchSpeed;
        [SerializeField] private float crouchYScale;
        private float startYScale;


        [Header("Ground Check")]
        [SerializeField] private float playerHeight;
        [SerializeField] private LayerMask whatIsGround;
        private bool grounded;


        [Header("Slope Handling")]
        [SerializeField] private float maxSlopeAngle;
        private RaycastHit slopeHit;
        private bool exitSlope;


        [SerializeField] private Transform orientation;


        private float horizontalInput;
        private float verticalInput;

        private Vector3 moveDirection;

        private Rigidbody rb;

        //Current state of the movement state machine
        public MovementState state;
        //State machine for movement
        public enum MovementState
        {
            aiming,
            sprinting,
            crouching,
            dashing,
            air
        }

        public bool dashing;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            readyToJump = true;

            startYScale = transform.localScale.y;
        }

        private void Update()
        {

            //Ground check in order to see if player is touching ground
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            //Input function that pulls getter methods from PlayerInputManager
            Input();

            //Handles speed of player
            SpeedControl();

            //State management system
            StateHandler();

            //Add in the drag
            if (state == MovementState.sprinting || state == MovementState.aiming || state == MovementState.crouching)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0;
            }
        }

        //Any physics calculations go here
        private void FixedUpdate()
        {
            MovePlayer();
        }

        //Takes and holds input values from the instances
        private void Input()
        {
            //Movement information pulled from PlayerInputManager
            horizontalInput = PlayerInputManager.Instance.getMovement().x;
            verticalInput = PlayerInputManager.Instance.getMovement().y;

            //Jump fucntion
            if (PlayerInputManager.Instance.jumpPressed() && readyToJump && grounded)
            {

                readyToJump = false;

                Jump();

                //Resets ready to jump to true after certain amount of time
                Invoke(nameof(ResetJump), jumpCooldown);
            }

            //Crouch
            if (PlayerInputManager.Instance.crouchPressed())
            {
                //Shrinks down player - probably need to change if third person and using animations
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            }

            if (!PlayerInputManager.Instance.crouchPressed())
            {
                //Resets player scale when crouch is gone
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            }
        }

        private float desiredMoveSpeed;
        private float lastDesiredMoveSpeed;
        private MovementState lastState;
        private bool keepMomentum;

        //Statemanagement System
        private void StateHandler()
        {
            // State - Dashing
            if (dashing)
            {
                state = MovementState.dashing;
                desiredMoveSpeed = dashSpeed;
                speedChangeFactor = dashSpeedChangeFactor;
            }

            // State - Crouching
            else if (PlayerInputManager.Instance.crouchPressed())
            {
                state = MovementState.crouching;
                desiredMoveSpeed = crouchSpeed;
            }

            // State - Aiming
            else if (grounded && PlayerInputManager.Instance.aimPressed())
            {
                state = MovementState.aiming;
                desiredMoveSpeed = aimSpeed;
            }

            // State - Sprinting
            else if (grounded)
            {
                state = MovementState.sprinting;
                desiredMoveSpeed = sprintSpeed;
            }

            // State - Air
            else
            {
                state = MovementState.air;

                desiredMoveSpeed = sprintSpeed;
            }

            bool desiredMoveSpeedChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
            if (lastState == MovementState.dashing)
            {
                keepMomentum = true;
            }

            if (desiredMoveSpeedChanged)
            {
                if (keepMomentum)
                {
                    StopAllCoroutines();
                    StartCoroutine(SmoothlyLerpMoveSpeed());
                }
                else
                {
                    StopAllCoroutines();
                    moveSpeed = desiredMoveSpeed;
                }
            }

            lastDesiredMoveSpeed = desiredMoveSpeed;
            lastState = state;
        }

        private float speedChangeFactor;
        private IEnumerator SmoothlyLerpMoveSpeed()
        {
            // lerp move speed to desired value
            float time = 0;
            float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
            float startValue = moveSpeed;

            float boostFactor = speedChangeFactor;

            while (time < difference)
            {
                moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
                time += Time.deltaTime * boostFactor;

                yield return null;
            }

            moveSpeed = desiredMoveSpeed;
            speedChangeFactor = 1f;
            keepMomentum = false;
        }

        //Moves the player
        private void MovePlayer()
        {
            if (state == MovementState.dashing) return;

            //Calculate movement direction 
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (OnSlope() && !exitSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

                if (rb.velocity.y > 0)
                {
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
                }
            }

            //On ground
            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            //in air
            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }

            rb.useGravity = !OnSlope();
        }

        //Limits speed of player
        private void SpeedControl()
        {
            //Limiting speed on slope
            if (OnSlope() && !exitSlope)
            {
                if (rb.velocity.magnitude > moveSpeed)
                {
                    rb.velocity = rb.velocity.normalized * moveSpeed;
                }
            }

            //Limit speed in ground or air
            else
            {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                //limit velocity if needd
                //If faster than movement speed, calculate max velcoty then apply it.
                if (flatVel.magnitude > moveSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }
        }


        //Jump function
        private void Jump()
        {
            exitSlope = true;

            //Reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
            exitSlope = false;
        }

        //Slope function
        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
            {
                //Returns how steep the slope is
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        //Have move direction change to the slope
        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
        }
    }
}


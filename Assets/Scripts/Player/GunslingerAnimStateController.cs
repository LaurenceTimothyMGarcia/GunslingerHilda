using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class GunslingerAnimStateController : MonoBehaviour
    {
        [Header("Ground Check")]
        [SerializeField] private float playerHeight;
        [SerializeField] private LayerMask whatIsGround;
        private bool grounded;

        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //Ground check in order to see if player is touching ground
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            bool isWalking = !PlayerInputManager.Instance.getMovement().Equals(Vector2.zero);
            bool isJumping = PlayerInputManager.Instance.jumpPressed();

            if (isJumping)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", true);
            }

            if (!grounded)
            {
                animator.SetBool("isJumping", false);
            }

            if (isWalking && !isJumping)
            {
                animator.SetBool("isWalking", true);
            }
            
            if (!isWalking && !isJumping)
            {
                animator.SetBool("isWalking", false);
            }

            WeaponSwap();
        }

        void WeaponSwap()
        {
            if (PlayerInputManager.Instance.swapRevolverPressed())
            {
                animator.SetBool("useRevolver", true);
                animator.SetBool("useShotgun", false);
                animator.SetBool("useSniper", false);
            }

            if (PlayerInputManager.Instance.swapShotgunPressed())
            {
                animator.SetBool("useRevolver", false);
                animator.SetBool("useShotgun", true);
                animator.SetBool("useSniper", false);
            }

            if (PlayerInputManager.Instance.swapSniperPressed())
            {
                animator.SetBool("useRevolver", false);
                animator.SetBool("useShotgun", false);
                animator.SetBool("useSniper", true);
            }

            if (PlayerInputManager.Instance.swapHandPressed())
            {
                animator.SetBool("useRevolver", false);
                animator.SetBool("useShotgun", false);
                animator.SetBool("useSniper", false);
            }
        }
    }
}

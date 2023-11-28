using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class GunslingerAnimStateController : MonoBehaviour
    {
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            bool isWalking = !PlayerInputManager.Instance.getMovement().Equals(Vector2.zero);
            bool isJumping = PlayerInputManager.Instance.jumpPressed();

            if (isJumping)
            {
                // animator.SetBool("isWalking", false);
                // animator.SetBool("isJumping", true);
            }

            if (isWalking && !isJumping)
            {
                animator.SetBool("isWalking", true);
            }
            
            if (!isWalking && !isJumping)
            {
                animator.SetBool("isWalking", false);
            }

            
        }
    }
}

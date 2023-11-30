using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInput
{
    public class GunSwap : MonoBehaviour
    {
        public GameObject revolver;
        public GameObject shotgun;
        public GameObject sniper;

        // Start is called before the first frame update
        void Start()
        {
            revolver.SetActive(false);
            shotgun.SetActive(false);
            sniper.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerInputManager.Instance.swapRevolverPressed())
            {
                Debug.Log("Revolver Selected");
                revolver.SetActive(true);
                shotgun.SetActive(false);
                sniper.SetActive(false);
            }

            if (PlayerInputManager.Instance.swapShotgunPressed())
            {
                Debug.Log("Shotgun Selected");
                revolver.SetActive(false);
                shotgun.SetActive(true);
                sniper.SetActive(false);
            }

            if (PlayerInputManager.Instance.swapSniperPressed())
            {
                Debug.Log("Sniper Selected");
                revolver.SetActive(false);
                shotgun.SetActive(false);
                sniper.SetActive(true);
            }

            if (PlayerInputManager.Instance.swapHandPressed())
            {
                Debug.Log("Hand Selected");
                revolver.SetActive(false);
                shotgun.SetActive(false);
                sniper.SetActive(false);
            }
        }
    }
}


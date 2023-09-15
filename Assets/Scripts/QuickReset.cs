using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickReset : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        /***
            If player crouches then it resets scene making a new terrain
        */

        if (PlayerInput.PlayerInputManager.Instance.crouchPressed())
        {
            SceneManager.LoadScene("ProcTerrain");
        }
    }
}

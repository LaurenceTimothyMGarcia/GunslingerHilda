using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickReset : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.PlayerInputManager.Instance.crouchPressed())
        {
            SceneManager.LoadScene("ProcTerrain");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lauoai : MonoBehaviour
{
    private bool canWarp = false;

    void Update()
    {
        if (canWarp)
        {
            if (Input.GetKeyDown(KeyCode.E)) // Ideally this should be a part of PlayerInput and not hardcoded to E
                Warp();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(
            $"{this}: {other.gameObject} entered collision circle (tag {other.gameObject.tag})"
        );

        if (other.gameObject.tag == "Player")
        {
            canWarp = true;
            Debug.Log($"{this}: canWarp set to {canWarp}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(
            $"{this}: {other.gameObject} exited collision circle (tag {other.gameObject.tag})"
        );

        if (other.gameObject.tag == "Player")
        {
            canWarp = false;
            Debug.Log($"{this}: canWarp set to {canWarp}");
        }
    }

    void Warp()
    {
        // For now, this just reloads the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

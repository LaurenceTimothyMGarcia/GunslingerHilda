using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// All of this was nabbed from https://www.youtube.com/watch?v=_lREXfAMUcE - Easy Enemy Healthbars in Unity
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}

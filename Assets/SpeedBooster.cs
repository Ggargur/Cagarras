using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    [Header("Values")]
    [Range(0f, 2f)]
    public float SpeedIncrease = 1.5f;
    public float HeightIncrease = 2f;

    [Header("References")]
    [SerializeField] Flying PlayerMovement;
    private void OnValidate()
    {
        if (SpeedIncrease == 0 || HeightIncrease == 0) 
            Debug.LogWarning("Speed Increase and Height Increase can't be equal to zero.");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovement.ActivateSpeedEffect(true);
        PlayerMovement.constantSpeed *= SpeedIncrease;
        PlayerMovement.GainAltitude(new Vector3(0, -HeightIncrease));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovement.ActivateSpeedEffect(false);
        PlayerMovement.constantSpeed /= SpeedIncrease;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pooper : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputActionReference rightControllerReference;
    [SerializeField] InputActionReference leftControllerReference;

    private float rightSelectButtonState;
    private float leftSelectButtonState;

    private bool IsSelectDown;
    [Header("Poop")]
    [SerializeField] GameObject TemplatePoop;
    [SerializeField] Vector3 poopOffset;

    void Update()
    {
        rightSelectButtonState = rightControllerReference.action.ReadValue<float>();
        leftSelectButtonState = leftControllerReference.action.ReadValue<float>();

        if((rightSelectButtonState != 0 && leftSelectButtonState != 0) && IsSelectDown == false)
        {
            GameObject poop = Instantiate(TemplatePoop);
            poop.transform.SetParent(TemplatePoop.transform);
            poop.transform.position = transform.position - poopOffset;
            poop.name = "Poop";
            IsSelectDown = true;
            AudioManager.PlayRandomSound(AudioManager.Sound.Poop);
        }
        else if((rightSelectButtonState == 0 && leftSelectButtonState == 0))
        {
            IsSelectDown = false;
        }
    }
}

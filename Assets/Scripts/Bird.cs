using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bird : MonoBehaviour
{
    public GameObject TemplatePoop;
    private GameObject poop;

    private Rigidbody Rigidbody;

    private InputDevice targetDevice;

    private float moveSpeed = 2.0f;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        List<InputDevice> devices = new List<InputDevice>(); 
        InputDevices.GetDevices(devices);


        if(devices.Count > 0)
        {
            targetDevice = devices[index:0];
        }
    }

    void Update()
    {
        transform.position += Camera.main.transform.forward * Time.deltaTime * moveSpeed;
        //Rigidbody.AddForce(transform.forward);

        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if(primaryButtonValue == true)
        {
            poop = GameObject.Instantiate(TemplatePoop);
            poop.transform.SetParent(TemplatePoop.transform);
            poop.transform.position = transform.position;
            poop.name = "poo";
        }
        if(Input.GetButtonDown("Fire1"))
        {
            poop = GameObject.Instantiate(TemplatePoop);
            poop.transform.SetParent(TemplatePoop.transform);
            poop.transform.position = transform.position;
            poop.name = "poo";
        }
    }
}

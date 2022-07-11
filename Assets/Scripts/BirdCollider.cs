using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollider : MonoBehaviour
{
    void Update()
    {
        transform.position = GameObject.Find("OVRCameraRig").transform.position;
    }
}

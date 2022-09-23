using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    private Quaternion _fixedRotation;
    void Start()
    {
        _fixedRotation = transform.rotation;
    }

    void Update()
    {
        if (transform.rotation != _fixedRotation)
            transform.rotation = _fixedRotation;
    }
}

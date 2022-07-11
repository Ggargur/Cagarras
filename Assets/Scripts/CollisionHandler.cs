using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Rigidbody _rb;
    private Flying _fly;
    private float normalConstantSpeed;

    [SerializeField] private float flopBoost = 100;
    [SerializeField] GameObject trackingReference;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _fly = GetComponent<Flying>();

        normalConstantSpeed = _fly.constantSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionX;
        _rb.constraints = RigidbodyConstraints.FreezePositionZ;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;

        trackingReference.transform.rotation = new Quaternion(trackingReference.transform.rotation.x, 0, trackingReference.transform.rotation.z, trackingReference.transform.rotation.w);
        _fly.wingFlapForce += flopBoost;
        _fly.constantSpeed = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        _rb.constraints = RigidbodyConstraints.None;
        _fly.constantSpeed = normalConstantSpeed;
        _fly.wingFlapForce -= flopBoost;
    }
}

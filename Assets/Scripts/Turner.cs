using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject mainCamera;
    [Header("Values")]
    [Range(30, 90)]
    [SerializeField] private float minimunAngleLeft = 45;
    [Range(-90, -30)]
    [SerializeField] private float minimunAngleRight = -45;
    [Range(0, 1)]
    [SerializeField] private float velocityMod = 0.7f;
    [SerializeField] private float angleVelocity = 1;
    private Rigidbody _rigidbody;

    private Vector3 torque;

    private Flying _componentFlying;
    private bool isTurning;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        torque = new Vector3(0, angleVelocity, 0);

        _componentFlying = GetComponent<Flying>();
    }

    void Update()
    {
        if (mainCamera.transform.eulerAngles.z >= minimunAngleLeft && mainCamera.transform.eulerAngles.z <= 180)
        {
            if(!isTurning)
            {
                _componentFlying.constantSpeed *= velocityMod;
                isTurning = true;
            }
            _rigidbody.AddRelativeTorque(-torque * (mainCamera.transform.eulerAngles.z - minimunAngleLeft));

        }
        else if (mainCamera.transform.eulerAngles.z - 360 <= minimunAngleRight && mainCamera.transform.eulerAngles.z>180)
        {
            if (!isTurning)
            {
                _componentFlying.constantSpeed *= velocityMod;
                isTurning = true;
            }
            _rigidbody.AddRelativeTorque(torque * -(mainCamera.transform.eulerAngles.z - 360 - minimunAngleRight));
        }
        else
        {
            if(isTurning)
            {
                _componentFlying.constantSpeed *= 1f / velocityMod;
                isTurning = false;
            }
            _rigidbody.angularVelocity -= _rigidbody.angularVelocity;
        }
    }
}

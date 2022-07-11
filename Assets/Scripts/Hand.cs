using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    [SerializeField] private GameObject HandFollower;
    [SerializeField] private float FollowSpeed = 30f;
    [SerializeField] private float RotationSpeed = 100f;

    private Rigidbody _body;
    private Transform _follower;

    private Vector3 positionOffset;
    private Vector3 rotationOffset;

    void Start()
    {
        _follower = HandFollower.transform;

        _body = GetComponent<Rigidbody>();

        _body.position = _follower.position;
        _body.rotation = _follower.rotation;
        
    }

    void Update()
    {
        var positionWithOffset = _follower.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (_follower.position - transform.position).normalized * distance * FollowSpeed;


        var rotationWithOffset = _follower.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);  
        _body.angularVelocity = axis * angle * Mathf.Deg2Rad * RotationSpeed;

    }
}

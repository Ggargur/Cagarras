using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform rootObject;
    public GameObject followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headBodyOffset;
    private Flying playerMovement;
    public bool isHeadEnabled { get { return !playerMovement.IsGamePaused; } }

    private void Start()
    {
        playerMovement = followObject.GetComponent<Flying>();
    }

    private void LateUpdate()
    {
        if (isHeadEnabled)
        {
        rootObject.position = transform.position + headBodyOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.transform.forward, Vector3.up).normalized;

        transform.position = followObject.transform.TransformPoint(positionOffset);
        transform.rotation = followObject.transform.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
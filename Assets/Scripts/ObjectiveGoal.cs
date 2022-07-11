using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ObjectiveGoal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject Objective;
    [SerializeField] private GameObject Player;
    [Header("Values")]
    [SerializeField] private Vector3 objectiveDistanceOffset;
    private float MinimalDistance = 10.0f;
    private bool Concluded = false;
    private SphereCollider _sc;

    private void Awake()
    {
        _sc = GetComponent<SphereCollider>();
        _sc.radius = MinimalDistance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == Player.name && !Concluded)
        {
            print("Você Concluiu o Objetivo #2");
            Objective.GetComponent<ObjectiveScript>().IsBeingHeld = false;
            Objective.transform.position = transform.position + objectiveDistanceOffset;
            Concluded = true;
        }
    }
}

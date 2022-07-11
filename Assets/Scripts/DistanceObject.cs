using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DistanceObject : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool Concluded = false;
    public float MinimalDistance = 10;
    private SphereCollider _sc;

    private void Awake()
    {
        _sc = GetComponent<SphereCollider>();
        _sc.radius = MinimalDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Concluded)
        {
            print("Você Concluiu o Objetivo #1");
            Concluded = true;
        }
    }
}

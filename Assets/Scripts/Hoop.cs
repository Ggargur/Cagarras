using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Way Waymanager;
    public bool isNext = false;

    private int collided;
    private void Awake()
    {
        Waymanager = GetComponentInParent<Way>();
    }

    private void OnTriggerEnter(Collider other)
    {
        collided++;
    }

    private void OnTriggerExit(Collider other)
    {
        collided--;
    }

    private void Update()
    {
        if(collided == 2 && isNext)
        {
            Waymanager.ChangeNext();
        }
    }
}

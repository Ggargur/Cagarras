using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    private Way Waymanager;
    public bool isNext = false;

    private void Awake()
    {
        Waymanager = GetComponentInParent<Way>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            if (isNext)
            {
                Waymanager.ChangeNext();
            }
    }
}

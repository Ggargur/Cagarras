using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public int PoopValuePoint = 5;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandTag : MonoBehaviour
{
    [SerializeField] Transform Player;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Player.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopCollection : MonoBehaviour
{
    List<CollectatonHoop> Hoops;

    public bool HasDoneAll = false;
    void Start()
    {
        var hoops = FindObjectsOfType<CollectatonHoop>();
        foreach(var h in hoops)
            Hoops.Add(h);
    }

    void Update()
    {
        foreach (var h in Hoops)
        {
            if (!h.isDone)
                return;
        }
        HasDoneAll = true;
    }
}

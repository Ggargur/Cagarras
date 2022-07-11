using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopTarget : MonoBehaviour
{
    private bool Concluded = false;
    
    void OnCollisionEnter(Collision collision)
    {
        if(!Concluded && collision.collider.name == "poo")
        {
            Concluded = true;
            print("Você Concluiu o Objetivo #3");
        }
    }
}

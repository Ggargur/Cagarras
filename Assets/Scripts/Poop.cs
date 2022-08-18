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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objective"))
        {
            var island = other.gameObject.GetComponent<PoopableIsland>();
            if(island != null)
            {
                if (!island.wasPooped)
                {
                    ScoreManager.Instance.score += PoopValuePoint;
                    island.wasPooped = true;
                    AudioManager.PlayRandomSound(AudioManager.Sound.CompleteTask);
                }
            } 
        }
        Destroy(this.gameObject);
    }
}

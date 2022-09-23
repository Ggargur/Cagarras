using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopableIsland : MonoBehaviour
{
    public bool wasPooped = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Poop"))
        {
            GetPoints(collision.collider);
            StartCoroutine(DestroyDelayed(collision.collider));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poop"))
        {
            GetPoints(other);
            StartCoroutine(DestroyDelayed(other));
        }
    }

    void GetPoints(Collider other)
    {
            if (!wasPooped)
            {
                ScoreManager.Instance.score += other.GetComponent<Poop>().PoopValuePoint;
                wasPooped = true;
                AudioManager.PlayRandomSound(AudioManager.Sound.CompleteTask);
            }
    }

    IEnumerator DestroyDelayed(Object obj)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(obj);
    }
}

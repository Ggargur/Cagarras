using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectatonHoop : MonoBehaviour
{
    public Color InicialColor;
    public Color DoneColor;

    private Material _material;

    public bool isDone = false;

    private void Awake()
    {
        var renderer = GetComponent<MeshRenderer>();
        _material = renderer.material;
        _material.color = InicialColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.score++;
            _material.color = DoneColor;
            isDone = true;
            AudioManager.PlayRandomSound(AudioManager.Sound.CompleteTask);
            StartCoroutine(DisableOld());
        }
    }

    IEnumerator DisableOld()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}

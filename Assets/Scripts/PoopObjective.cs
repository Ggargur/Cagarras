using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopObjective : MonoBehaviour
{
    [SerializeField] private GameObject poop;
    [SerializeField] private Vector3 TurningSpeed;

    [SerializeField] private Color InitialColor;
    [SerializeField] private Color FinishedColor;

    private ParticleSystem _particleSystem;

    public bool HasBeenFinished;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == poop.name)
        {
            var trail = _particleSystem.trails;
            trail.colorOverLifetime = FinishedColor;
            AudioManager.PlaySound(AudioManager.Sound.CompleteTask, 0);
            HasBeenFinished = true;
        }
    }
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        var trail = _particleSystem.trails;
        trail.colorOverLifetime = InitialColor;
    }

    private void Update()
    {
        transform.Rotate(TurningSpeed * Time.deltaTime);
    }
}

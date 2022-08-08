using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopObjective : MonoBehaviour
{
    [SerializeField] private GameObject poop;
    [SerializeField] private Vector3 TurningSpeed;

    [SerializeField] private Color InitialColor;
    [SerializeField] private Color FinishedColor;

    private MeshRenderer _meshRenderer;

    public bool HasBeenFinished;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == poop.name)
        {
            var trail = _meshRenderer.material;
            trail.color = FinishedColor;
            AudioManager.PlaySound(AudioManager.Sound.CompleteTask, 0);
            HasBeenFinished = true;
        }
    }
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if(_meshRenderer != null)
        {
            var trail = _meshRenderer.material;
            trail.color = InitialColor;
        }
        else
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            var trail = _meshRenderer.material;
            trail.color = InitialColor;
        }
    }
}

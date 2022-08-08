using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Rigidbody _rb;
    private Flying _fly;
    private float normalConstantSpeed;
    private Vector3 _intialPosition;
    private Quaternion _initialRotation;

    [SerializeField] private float Cooldown = 3;
    [SerializeField] GameObject trackingReference;
    [SerializeField] GameObject _way;
    [SerializeField] LayerMask Layer;
    private Quaternion _rotation;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _fly = GetComponent<Flying>();
        UpdateRestartPosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Terrain"))
        {
            StartCoroutine(WaitToBeTeleported());
            AudioManager.PlaySound(AudioManager.Sound.Damage, 0);
        }
    }

    IEnumerator WaitToBeTeleported()
    {
        yield return new WaitForSeconds(Cooldown);
        transform.rotation = _initialRotation;
        transform.position = _intialPosition; 
    }

    public void UpdateRestartPosition()
    {
        _initialRotation = transform.rotation;
        _intialPosition = transform.position;
    }
}

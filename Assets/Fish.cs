using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Vector3 Direction;
    public int Value = 5; 
    [Header("Direction")]
    [SerializeField] float MaxAngle = 15f;
    [SerializeField] float Interval = 4f;

    [Header("Speed")]
    [SerializeField] float Speed = 2f;
    [SerializeField] float RotationSpeed = 1.5f;

    float _t = 0;
    Quaternion _rotationObjective;

    private void Start()
    {
        _rotationObjective = transform.rotation;
        StartCoroutine(ChangeDirection());
    }

    private void FixedUpdate()
    {
        _t += Time.deltaTime * RotationSpeed;
        transform.SetPositionAndRotation(
            transform.position + Direction * Time.deltaTime * Speed,
            Quaternion.Lerp(transform.rotation, _rotationObjective, _t));
    }

    IEnumerator ChangeDirection()
    {
        SetNewDirection(GetNewDirection());
        while (true)
        {
            var d = GetNewDirection();
            if (Vector3.Angle(d, Direction) < MaxAngle)
            {
                SetNewDirection(d);
                _t = 0;
            }
            yield return new WaitForSeconds(Interval);
        }
    }

    Vector3 GetNewDirection()
    {
        var newdirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        return newdirection;
    }

    void SetNewDirection(Vector3 direction)
    {
        Direction = direction;
        _rotationObjective = Quaternion.LookRotation(Direction, transform.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.score += Value; 
            gameObject.SetActive(false);
            AudioManager.PlayRandomSound(AudioManager.Sound.Eat);
        }
    }
}

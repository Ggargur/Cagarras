using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBird : MonoBehaviour
{
    private float _untilConclusion;
    public float Speed = 10f;
    [SerializeField] float MinDistance;
    Vector3 startPosition;
    [SerializeField] Vector3 destination;

    void Start()
    {
        startPosition = transform.position = GiveRandomPosition();
        destination = GiveRandomPosition();
    }

    void Update()
    {
        transform.LookAt(destination);
        transform.position = Vector3.Lerp(startPosition, destination, _untilConclusion);
        _untilConclusion += Time.deltaTime * Speed;

        if(Vector3.Distance(transform.position, destination) <= MinDistance)
        {
            destination = GiveRandomPosition();
            startPosition = transform.position;
            _untilConclusion = 0f;
        }
    }

    Vector3 GiveRandomPosition()
    {
        return new Vector3(Random.Range(0, 1500), Random.Range(0, 250), Random.Range(0, 1500));
    }
}

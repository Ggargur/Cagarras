using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBird : MonoBehaviour
{
    [Header("References")]
    public BackgroundBirdsConfig Settings;
    [SerializeField] GameObject Player;

    private Vector3 destinationDirection;
    private Vector3 destinationPosition;
    private Rigidbody _rigidbody;
    private float _distancePastframe;



    float Speed;

    void Start()
    {
        Speed = (Settings.MinSpeed + Settings.MaxSpeed) / 2;
        StartCoroutine(GetNewDirectionAndSpeed());
        _rigidbody = GetComponent<Rigidbody>();
        transform.position = GiveRandomPositionNearPlayer();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = destinationDirection * Speed;
        var distance = Vector3.Distance(transform.position, destinationPosition);

        if (distance > _distancePastframe)
            ChangeDirectionAndSpeed(true);

        if (distance <= Settings.MinDistance)
            ChangeDirectionAndSpeed(false);

        _distancePastframe = distance;
    }

    IEnumerator GetNewDirectionAndSpeed()
    {
        while (true)
        {
            ChangeDirectionAndSpeed(true);
            yield return new WaitForSeconds(Settings.timeBetweenChanges);
        }
    }

    void ChangeDirectionAndSpeed(bool isRandom)
    {
        Vector3 newdestination;
        if (isRandom)
            newdestination = GiveRandomPositionNearPlayer() + Player.transform.forward * Settings.offsetDistance;
        else
            newdestination = GiveRandomPosition();

        var oldDestination = destinationPosition;
        var angle = Vector3.Angle(oldDestination, newdestination);
        if (angle >= Settings.maxAngleBetweenDestinations)
            return;

        destinationPosition = newdestination;

        destinationDirection = (destinationPosition - transform.position).normalized;
        Speed *= Random.Range(1 - Settings.SpeedRange, 1 + Settings.SpeedRange);
        if (Speed < Settings.MinSpeed) Speed = Settings.MinSpeed;
        if (Speed > Settings.MaxSpeed) Speed = Settings.MaxSpeed;
        transform.LookAt(destinationPosition);
    }
    Vector3 GiveRandomPositionNearPlayer()
    {
        var XZtoYratio = 4f;
        return new Vector3(Random.Range(-Settings.Radius, Settings.Radius) + Player.transform.position.x, Random.Range(-Settings.Radius / XZtoYratio, Settings.Radius / XZtoYratio) + Player.transform.position.y, Random.Range(-Settings.Radius, Settings.Radius) + Player.transform.position.z);
    }

    Vector3 GiveRandomPosition()
    {
        var maxdistanceXZ = Settings.Radius * 2;
        var maxdistanceY = 5f;
        return new Vector3(transform.position.x + Random.Range(-maxdistanceXZ, maxdistanceXZ), transform.position.y + Random.Range(-maxdistanceY, maxdistanceY), transform.position.z + Random.Range(-maxdistanceXZ, maxdistanceXZ));
    }
}

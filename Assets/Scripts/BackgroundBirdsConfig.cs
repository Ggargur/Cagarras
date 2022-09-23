using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundBirdsSettings", menuName = "BackgroundBirdsSettings", order = 1)]
public class BackgroundBirdsConfig : ScriptableObject
{
    [Header("Speed")]
    [Range(0,1f)]
    public float SpeedRange;
    public float MinSpeed, MaxSpeed;
    [Header("Time")]
    public float timeBetweenChanges;
    [Header("Distance & Angles")]
    public float MinDistance;
    public float Radius = 5;
    public float offsetDistance;
    public float maxAngleBetweenDestinations = 45f;
}

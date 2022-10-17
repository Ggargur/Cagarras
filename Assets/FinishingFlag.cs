using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingFlag : MonoBehaviour
{
    [SerializeField] CountDownTimer Timer;
    private GameObject _audioSource;


    private void OnTriggerEnter(Collider other)
    {
        if (Timer.HasReachedGoal) return;

        Timer.HasReachedGoal = true;

        if(_audioSource == null)
            _audioSource = AudioManager.PlayRandomSound(AudioManager.Sound.CompleteTask);
    }
}

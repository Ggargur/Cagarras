using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private SoundAudioClip[] soundAudioClip;
    public static SoundAudioClip[] AudioClips;


    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioManager.Sound sound;
        public AudioClip[] audioClip;
    }

    private void Awake()
    {
        AudioClips = this.soundAudioClip;
    }
}

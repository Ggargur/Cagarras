using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public enum Sound
    {
        WingFlap,
        CompleteTask,
        Poop,
        PickupObject,
        DropObject,
        Damage,
    }

    public static GameObject PlayRandomSound(Sound sound)
    {
        GameObject soundgameObject = new GameObject("Sound");
        AudioSource audioSource = soundgameObject.AddComponent<AudioSource>();
        audioSource.clip = GetRandomAudioClip(sound);
        audioSource.volume = 0.1f;
        audioSource.Play();

        Object.Destroy(soundgameObject, audioSource.clip.length);
        return soundgameObject;
    }

    public static GameObject PlaySound(Sound sound, int index)
    {
        GameObject soundgameObject = new GameObject("Sound");
        AudioSource audioSource = soundgameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound, index);
        audioSource.volume = 0.1f;
        audioSource.Play();

        Object.Destroy(soundgameObject, audioSource.clip.length);
        return soundgameObject;
    }

    private static AudioClip GetRandomAudioClip(Sound sound)
    {
        foreach(Sounds.SoundAudioClip soundAudioClip in Sounds.AudioClips)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip[Random.Range(0, soundAudioClip.audioClip.Length-1)];
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    private static AudioClip GetAudioClip(Sound sound, int index)
    {
        foreach (Sounds.SoundAudioClip soundAudioClip in Sounds.AudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip[index];
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}

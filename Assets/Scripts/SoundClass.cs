using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundClass
{
    public string clipName;

    public AudioClip clip;

    public bool loop;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.5f, 2f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}

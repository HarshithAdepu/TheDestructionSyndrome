using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundClass[] sounds;
    public static AudioManager audioManagerInstance;
    void Awake()
    {
        if (audioManagerInstance == null)
            audioManagerInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(SoundClass s in sounds)                           //YO
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlaySound("8BitBackground");
    }
    public void PlaySound(string clipName)
    {
        SoundClass s = Array.Find(sounds, sound => sound.clipName == clipName);
        if(s==null)
        {
            Debug.Log("Sound file \""+clipName+"\" not found");
            return; 
        }
        s.source.Play();
    }
}

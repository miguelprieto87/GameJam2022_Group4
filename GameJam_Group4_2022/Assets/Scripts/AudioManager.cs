using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // lives through transitioning
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void Start()
    {
        Play("bg_music");
    }
    public void Play(string name)
    {
        Sound snd = Array.Find(sounds, sound => sound.name == name);
        try
        {
            snd.source.Play();
        }
        catch (Exception e)
        {
            Debug.LogWarning("sound not found");
        }


    }
}



[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector] public AudioSource source;
}

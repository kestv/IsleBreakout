using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
            var source = s.GetSource();
            source.clip = s.GetClip();
            source.volume = s.GetVolume();
            source.pitch = s.GetPitch();
            source.loop = s.GetLoop();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.GetName() == name);
        s.GetSource().Play();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.GetName() == name);
        return s.GetSource().isPlaying;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.GetName() == name);
        s.GetSource().Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField]AudioClip audioClip;
    [SerializeField]string clipName;
    [Range(0f,1f)]
    [SerializeField]float volume;
    [Range(.1f,3f)]
    [SerializeField]float pitch;
    [SerializeField]bool loop;
    [HideInInspector]
    [SerializeField]AudioSource source;

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    public void SetPitch(float pitch)
    {
        this.pitch = pitch;
    }

    public void SetAudioClip(AudioClip clip)
    {
        this.audioClip = clip;
    }

    public void SetClipName(string name)
    {
        this.clipName = name;
    }

    public void SetLoop(bool loop)
    {
        this.loop = loop;
    }

    public void SetSource(AudioSource source)
    {
        this.source = source;
    }

    public float GetVolume()
    {
        return this.volume;
    }

    public float GetPitch()
    {
        return this.pitch;
    }

    public AudioClip GetClip()
    {
        return this.audioClip;
    }

    public string GetName()
    {
        return this.clipName;
    }

    public bool GetLoop()
    {
        return this.loop;
    }

    public AudioSource GetSource()
    {
        return this.source;
    }
}

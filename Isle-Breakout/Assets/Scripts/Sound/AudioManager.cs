using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;
    [SerializeField]
    Sound[] background;
    int index;
    float clipLength;
    float clipStartTime;
    [SerializeField]
    bool dontDestroyOnLoad;
    [SerializeField]
    float spatialBlend;
    [SerializeField]
    AudioMixerGroup mixer;
    AudioSource bgSource;
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
            source.spatialBlend = spatialBlend;
            if (mixer != null)
                source.outputAudioMixerGroup = mixer;
        }
        if(dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
        bgSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        clipStartTime = 0;
        clipLength = 0;
        index = 0;
        InvokeRepeating("ControlBackground", 1f, 5f);
    }

    void ControlBackground()
    {
        if (Time.time - clipStartTime > clipLength)
        {
            var lastIndex = index;
            index = UnityEngine.Random.Range(0, background.Length - 1);
            if (index != lastIndex)
            {
                PlayBG(index);
            }
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

    void PlayBG(int index)
    {
        Sound s = background[index];
        s.SetSource(bgSource);
        var source = s.GetSource();
        source.clip = s.GetClip();
        source.volume = s.GetVolume();
        source.pitch = s.GetPitch();
        source.loop = s.GetLoop();
        source.spatialBlend = spatialBlend;
        if (mixer != null)
            source.outputAudioMixerGroup = mixer;
        source.Play();
        clipStartTime = Time.time;
        clipLength = source.clip.length;
    }
}

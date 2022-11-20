using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool instance;
    public AudioSource AudioSourcePrefab;
    public List<AudioSource> audioSources;

    private void Awake()
    {
        instance = this;
        audioSources = new List<AudioSource>();
    }

    public AudioSource GetSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        AudioSource new_source = GameObject.Instantiate(AudioSourcePrefab);
        audioSources.Add(new_source);
        return new_source;
    }
}

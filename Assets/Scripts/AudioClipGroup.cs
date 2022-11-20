using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AudioClipGroup")]

public class AudioClipGroup : ScriptableObject
{
    public List<AudioClip> AudioClips;

    [Range(0, 2)]
    public float VolumeMin = 1;
    [Range(0, 2)]
    public float VolumeMax = 1;
    [Range(0, 1)]
    public float PitchMin = 0;
    [Range(0, 1)]
    public float PitchMax = 0;
    public float Cooldown = 0;
    private static float nextPlayTime = 0;

    public void Play()
    {
        Play(AudioSourcePool.instance.GetSource());
    }

    public void PlayAtIndex(int index)
    {
        PlayAtIndex(AudioSourcePool.instance.GetSource(), index);
    }

    public void Play(AudioSource source)
    {
        if (nextPlayTime > Time.time) return;
        source.clip = AudioClips[Random.Range(0, AudioClips.Count)];
        source.volume = Random.Range(VolumeMin, VolumeMax);
        //source.pitch = Random.Range(PitchMin, PitchMax);
        source.PlayOneShot(source.clip);
        nextPlayTime = Time.time + Cooldown;
    }

    public void PlayAtIndex(AudioSource source, int index)
    {
        if (nextPlayTime > Time.time) return;
        source.clip = AudioClips[index];
        source.volume = Random.Range(VolumeMin, VolumeMax);
        //source.pitch = Random.Range(PitchMin, PitchMax);
        source.PlayOneShot(source.clip);
        nextPlayTime = Time.time + Cooldown;
    }
}

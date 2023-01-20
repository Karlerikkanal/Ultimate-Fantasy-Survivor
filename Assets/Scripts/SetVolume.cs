using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    private void OnEnable()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }
    }
}

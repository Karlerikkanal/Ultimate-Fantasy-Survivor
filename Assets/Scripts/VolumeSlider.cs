using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider _slider;
    void Start()
    {
        _slider.value = 0.99f;
        _slider.onValueChanged.AddListener(val => AudioListener.volume = val);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }

    private void OnEnable()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        _slider.value = PlayerPrefs.GetFloat("volume");
    }
}

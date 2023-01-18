using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider _slider;
    void Start()
    {
        _slider.onValueChanged.AddListener(val => AudioListener.volume = val);
    }
}

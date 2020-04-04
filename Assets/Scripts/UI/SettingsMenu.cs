using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider = null;
    [SerializeField] private Slider _effectsSlider = null;

    public static Action<float> OnMusicVolumeChanged { get; set; }
    public static Action<float> OnEffectsVolumeChanged { get; set; }

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(v =>
        {
            OnMusicVolumeChanged?.Invoke(v);
        });
        _effectsSlider.onValueChanged.AddListener(v =>
        {
            OnEffectsVolumeChanged?.Invoke(v);
        });
    }

    private void Start()
    {
        _musicSlider.value = 0.3f;
        _effectsSlider.value = 0.3f;
    }
}

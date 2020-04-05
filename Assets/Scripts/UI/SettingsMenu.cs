using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private static float _musicVolume = 0.3f;
    private static float _effectsVolume = 0.3f;

    public static Action<float> OnMusicVolumeChanged { get; set; }
    public static Action<float> OnEffectsVolumeChanged { get; set; }

    [SerializeField] private Slider _musicSlider = null;
    [SerializeField] private Slider _effectsSlider = null;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(v =>
        {
            _musicVolume = v;
            OnMusicVolumeChanged?.Invoke(v);
        });
        _effectsSlider.onValueChanged.AddListener(v =>
        {
            _effectsVolume = v;
            OnEffectsVolumeChanged?.Invoke(v);
        });
    }

    private void Start()
    {
        _musicSlider.value = _musicVolume;
        _effectsSlider.value = _effectsVolume;
    }
}


using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private static float _musicVolume = 1.0f;
    private static float _effectsVolume = 1.0f;
    private static List<AudioSource> _effectSources = new List<AudioSource>();
    private static List<AudioSource> _musicSources = new List<AudioSource>();

    static SoundManager()
    {
        SettingsMenu.OnMusicVolumeChanged += v =>
        {
            var oldVolume = _musicVolume;
            var newVolume = v;
            _musicVolume = newVolume;
            foreach (var source in _musicSources)
            {
                source.volume = _musicVolume;
            }
        };
        SettingsMenu.OnEffectsVolumeChanged += v =>
        {
            var oldVolume = _effectsVolume;
            var newVolume = v;
            _effectsVolume = newVolume;
            foreach (var source in _effectSources)
            {
                source.volume = _effectsVolume;
            }
        };
    }

    public static AudioSource CreateAudioSource(string clipKey)
    {
        var source = GameObject.Instantiate(new GameObject()).AddComponent<AudioSource>();
        var clip = Resources.Load<AudioClip>(clipKey);
        if (clip == null)
        {
            Debug.Log("Couldn't find clip " + clipKey + ".");
        }
        source.clip = clip;
        return source;
    }

    public static void PlayMusic(string clip)
    {
        var audioSource = CreateAudioSource("Sounds/Music/" + clip);
        audioSource.volume = _musicVolume;
        audioSource.loop = true;
        _musicSources.Add(audioSource);
        audioSource.Play();
    }
    public static void PlayEffect(string clip)
    {
        var audioSource = CreateAudioSource("Sounds/Effects/" + clip);
        audioSource.volume = _effectsVolume;
        _effectSources.Add(audioSource);
        audioSource.Play();
    }
}
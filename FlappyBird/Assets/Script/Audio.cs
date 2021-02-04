using System.Collections.Generic;
using UnityEngine;

public enum AudioEnum
{
    Die,
    Hit,
    Point,
    Swooshing,
    Wing,
}

public class Audio
{
    private static readonly Dictionary<AudioEnum, string> _audioMap = new Dictionary<AudioEnum, string>();

    private static AudioSource _audioSource;

    private static AudioSource audioSource
    {
        get
        {
            if (_audioSource == null)
            {
                _audioSource = new GameObject("Audio").AddComponent<AudioSource>();
            }
            return _audioSource;
        }
    }

    static Audio()
    {
        _audioMap.Add(AudioEnum.Die, "Audio/Die");
        _audioMap.Add(AudioEnum.Hit, "Audio/Hit");
        _audioMap.Add(AudioEnum.Point, "Audio/Point");
        _audioMap.Add(AudioEnum.Swooshing, "Audio/Swooshing");
        _audioMap.Add(AudioEnum.Wing, "Audio/Wing");
    }

    private static AudioClip GetAudioClip(AudioEnum audioName)
    {
        string path = _audioMap[audioName];
        return Resources.Load<AudioClip>(path);
    }

    public static void PlayOneShot(AudioEnum audioEnum)
    {
        var clip = GetAudioClip(audioEnum);
        audioSource.PlayOneShot(clip);
    }

    public static void Stop()
    {
        if (audioSource.isPlaying == true) { audioSource.Stop(); }
    }
}
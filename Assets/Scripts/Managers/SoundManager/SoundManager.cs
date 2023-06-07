using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{

    private static string SettingsPath = "SoundManagerSettings";


    private SoundManagerSettings soundManagerSettings;

    private static SoundManager instance;
    private void Awake()
    {

        MakeSingleton();
        GameObject audioSourceEffectGO = new GameObject("AudioSourceEffects");
        audioSourceEffectGO.transform.SetParent(transform);
        _audioSourceEffects = audioSourceEffectGO.AddComponent<AudioSource>();

        GameObject audioSourceBackgroundGO = new GameObject("AudioSourceBackground");
        audioSourceBackgroundGO.transform.SetParent(transform);
        _audioSourceBackgroundMusic = audioSourceBackgroundGO.AddComponent<AudioSource>();
        _audioSourceBackgroundMusic.loop = true;

        if(_nameClipPairs == null || _nameClipPairs.Length == 0)
        {
            soundManagerSettings = Resources.Load<SoundManagerSettings>(SettingsPath);
            if(soundManagerSettings != null )
            {
                _nameClipPairs = soundManagerSettings.NameClipPairs;
                _backgroundMusicName = soundManagerSettings.BackgroundMusicName;
                _musicMultiplier = soundManagerSettings.MusicVolumeMul;
                _effectMultiplier = soundManagerSettings.EffectVolumeMul;
            }
        }

        _audioClipDictionary = new Dictionary<string, AudioClip>();
        foreach (NameClipPair item in _nameClipPairs)
        {
            _audioClipDictionary.Add(item.Name, item.AudioClip);
        }

        if (!string.IsNullOrEmpty(_backgroundMusicName))
        {
            PlayBackgroundMusic(_backgroundMusicName);
        }

    }


    private float _musicMultiplier=1f;
    private float _effectMultiplier=1f;


    private AudioSource _audioSourceEffects;

    private AudioSource _audioSourceBackgroundMusic;

    private string _backgroundMusicName;


    private NameClipPair[] _nameClipPairs;
    private Dictionary<string, AudioClip> _audioClipDictionary;

    private void Start()
    {

        MusicVolume = 0.5f;
        ArrangeMusicVolume();

        EffectsVolume = 0.5f;
        ArrangeEffectVolume();

    }


    public void ArrangeMusicVolume()
    {
        _audioSourceBackgroundMusic.volume = MusicVolume * _musicMultiplier;
    }
    public void ArrangeMusicVolume(float val)
    {
        MusicVolume = val;
        _audioSourceBackgroundMusic.volume = MusicVolume * _musicMultiplier;
    }

    public void ArrangeEffectVolume()
    {
        _audioSourceEffects.volume = EffectsVolume * _effectMultiplier;
    }
    public void ArrangeEffectVolume(float val)
    {
        EffectsVolume = val;
        _audioSourceEffects.volume = EffectsVolume * _effectMultiplier;
    }


    public void PlaySoundClip(string name)
    {

        _audioSourceEffects.PlayOneShot(_audioClipDictionary[name]);

    }

    public void PlaySoundClip(string name,float vol)
    {

        _audioSourceEffects.PlayOneShot(_audioClipDictionary[name],vol);

    }

    public void PlayBackgroundMusic(string name = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            if (!_audioSourceBackgroundMusic.isPlaying)
                _audioSourceBackgroundMusic.Play();
        }
        else
        {
            AudioClip clip = _audioClipDictionary[name];
            if (_audioSourceBackgroundMusic.clip != clip)
            {
                _audioSourceBackgroundMusic.clip = clip;
                _audioSourceBackgroundMusic.Play();
            }
        }
    }

    public void StopBackgroundMusic()
    {
        _audioSourceBackgroundMusic.Stop();
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    #region GetterSetter

    public static SoundManager Instance { get { 
            
            if (instance == null)
            {
                GameObject soundManager = new GameObject("SoundManager");
                instance = soundManager.AddComponent<SoundManager>();
            }
            
            return instance;
        } 
    }

    public float MusicVolume{ get; set; }

    public float EffectsVolume{ get; set;}


    #endregion




}
[Serializable]
public struct NameClipPair
{
    public string Name;
    public AudioClip AudioClip;
}



public static class SoundManagerExtensions
{

    public static void PlaySoundClip(this string name)
    {
        SoundManager.Instance.PlaySoundClip(name);
    }
    public static void PlaySoundClip(this string name,float volume)
    {
        SoundManager.Instance.PlaySoundClip(name,volume);
    }
    public static void PlayBackgroundMusic(this string name)
    {
        SoundManager.Instance.PlayBackgroundMusic(name);
    }


}
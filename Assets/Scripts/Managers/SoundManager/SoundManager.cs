using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;
    private void Awake()
    {

        MakeSingleton();
        GameObject audioSourceEffectGO = new GameObject("AudioSourceEffects");
        audioSourceEffectGO.transform.SetParent(transform);
        audioSourceEffects = audioSourceEffectGO.AddComponent<AudioSource>();

        audioClipDictionary = new Dictionary<string, AudioClip>();
        foreach (NameClipPair item in nameClipPairs)
        {
            audioClipDictionary.Add(item.Name,item.AudioClip);
        }


    }

    [Range(0f, 1f)]
    public float musicMultiplier=0.5f;


    private AudioSource audioSourceEffects;

    public AudioSource audioSourceBackgroundMusic;

    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider effectsSlider;

    [SerializeField]
    private NameClipPair[] nameClipPairs;
    private Dictionary<string, AudioClip> audioClipDictionary;

    private void Start()
    {

        musicSlider?.onValueChanged.AddListener(delegate { ArrangeMusicVolume(); });
        effectsSlider?.onValueChanged.AddListener(delegate { ArrangeEffectVolume(); });
        SoundVolume = 0.5f;
        ArrangeMusicVolume();

        EffectsVolume = 0.5f;
        ArrangeEffectVolume();

    }


    public void ArrangeMusicVolume()
    {
        if(audioSourceBackgroundMusic != null)
            audioSourceBackgroundMusic.volume = SoundVolume*musicMultiplier;

    }

    public void ArrangeEffectVolume()
    {
        float volume = EffectsVolume;
        audioSourceEffects.volume = volume;

    }


    public void PlaySoundClip(string name)
    {

        audioSourceEffects.PlayOneShot(audioClipDictionary[name]);

    }

    public void PlaySoundClip(string name,float vol)
    {

        audioSourceEffects.PlayOneShot(audioClipDictionary[name],vol);

    }

    public void PlayMusic()
    {
        if (!audioSourceBackgroundMusic.isPlaying)
            audioSourceBackgroundMusic.Play();
    }

    public void StopMusic()
    {
        audioSourceBackgroundMusic.Stop();
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

    public static SoundManager Instance { get => instance; set => instance = value; }

    public float SoundVolume
    {
        get
        {
            if (musicSlider != null)
                return musicSlider.value;
            else
                return 0.5f;
        }
        set
        {
            if(musicSlider != null)
                musicSlider.value = value;
        }
    }

    public float EffectsVolume
    {
        get
        {
            if (effectsSlider != null)
                return effectsSlider.value;
            else
                return 0.5f;
        }
        set
        {
            if(effectsSlider!=null)
                effectsSlider.value = value;
        }
    }

    public Slider MusicSlider {
        get { 
            if(musicSlider == null)
            {
                musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
            }
            return musicSlider; 
        }
        set => musicSlider = value; 
    }
    public Slider EffectsSlider
    {
        get
        {
            if (effectsSlider == null)
            {
                effectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();
            }
            return effectsSlider;
        }
        set => effectsSlider = value; }

    #endregion


    [Serializable]
    struct NameClipPair
    {
        public string Name;
        public AudioClip AudioClip;
    }

}

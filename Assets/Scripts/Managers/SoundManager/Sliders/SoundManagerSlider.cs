using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerSlider : MonoBehaviour
{

    [SerializeField]
    private SliderType _sliderType;


    private Slider _slider;


    private void Awake()
    {
        _slider = GetComponent<Slider>();
        
        if(_sliderType == SliderType.Music)
        {
            _slider.onValueChanged.AddListener(ArrangeMusic);
        }
        else if(_sliderType == SliderType.Effect)
        {
            _slider.onValueChanged.AddListener(ArrangeEffect);
        }

    }


    private void OnEnable()
    {
        if(_sliderType == SliderType.Music)
        {
            _slider.value = SoundManager.Instance.MusicVolume;
        }
        else if(_sliderType == SliderType.Effect)
        {
            _slider.value = SoundManager.Instance.EffectsVolume;
        }
    }

    private void ArrangeMusic(float val)
    {
        SoundManager.Instance.ArrangeMusicVolume(val);

    }

    private void ArrangeEffect(float val)
    {
        SoundManager.Instance.ArrangeEffectVolume(val);
    }



    private enum SliderType { Music,Effect}

}

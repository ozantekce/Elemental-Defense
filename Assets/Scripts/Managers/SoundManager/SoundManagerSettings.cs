using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundManagerSettings", menuName = "SoundManager/SoundManagerSettings")]
public class SoundManagerSettings : ScriptableObject
{

    [SerializeField]
    private string _backgroundMusicName;

    [SerializeField] 
    private float _musicVolumeMul,_effectVolumeMul;

    [SerializeField]
    private NameClipPair[] _nameClipPairs;
    


    public NameClipPair[] NameClipPairs { get => _nameClipPairs; set => _nameClipPairs = value; }
    public string BackgroundMusicName { get => _backgroundMusicName; set => _backgroundMusicName = value; }
    public float MusicVolumeMul { get => _musicVolumeMul; set => _musicVolumeMul = value; }
    public float EffectVolumeMul { get => _effectVolumeMul; set => _effectVolumeMul = value; }
}

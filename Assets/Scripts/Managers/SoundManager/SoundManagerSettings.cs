using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundManagerSettings", menuName = "SoundManager/SoundManagerSettings")]
public class SoundManagerSettings : ScriptableObject
{

    [SerializeField]
    private string _backgroundMusicName;

    [SerializeField]
    private NameClipPair[] _nameClipPairs;
    


    public NameClipPair[] NameClipPairs { get => _nameClipPairs; set => _nameClipPairs = value; }
    public string BackgroundMusicName { get => _backgroundMusicName; set => _backgroundMusicName = value; }
}

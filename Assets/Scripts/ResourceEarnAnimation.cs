using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ResourceEarnAnimation : MonoBehaviour, Poolable
{
    [SerializeField]
    private string _poolableKey;
    private bool _pooled;

    private Poolable _poolable;

    
    private Tween _tween;
    private TextMeshPro _textMeshPro;



    public void InitResourceEarnAnimation(Vector3 startPos
        , Vector3 endPos, float duration, int amount)
    {

        transform.position = startPos;
        if(_textMeshPro == null) _textMeshPro = GetComponentInChildren<TextMeshPro>();
        _textMeshPro.text = "+"+amount.NumberToLetter();

        _tween = transform.DOMove(endPos, duration)
            .SetEase(Ease.OutBack)
            .OnComplete(_poolable.AddToPool);

    }

    public void OnCreate()
    {
        _poolable = this;
    }


    public static void CreateResourceEarnAnimation(string key, Vector3 startPos
        ,Vector3 endPos, float duration, int amount)
    {

        ResourceEarnAnimation animation 
            = Poolable.GetFromPool<ResourceEarnAnimation>(key);
        animation.InitResourceEarnAnimation(startPos,endPos,duration,amount);

    }





    #region GetterSetter
    public string Key { get => _poolableKey; set => _poolableKey=value; }

    public MonoBehaviour MonoBehaviour => this;

    public bool Pooled { get => _pooled; set => _pooled=value; }

    #endregion

}

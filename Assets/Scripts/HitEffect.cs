using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour, Poolable
{
    [SerializeField]
    private string _poolableKey;
    private bool _pooled;
    private Poolable _poolable;

    [SerializeField]
    private float _lifeTime;

    [SerializeField]
    private bool _followTarget;

    private Transform _target;

    private void Awake()
    {
        _poolable = this;
    }

    private void Update()
    {

        if (_pooled) return;

        if (_followTarget && _target!=null)
        {
            transform.position = _target.position;
        }

    }

    public void InitHitEffect(Transform target)
    {
        _target = target;
        transform.position = target.position;
        Invoke("SendToPool", _lifeTime);
    }

    private void SendToPool()
    {
        _poolable.SendToPool();
    }


    #region GetterSetter
    public string Key { get => _poolableKey; set => _poolableKey = value; }

    public MonoBehaviour MonoBehaviour => this;

    public bool Pooled { get => _pooled; set => _pooled=value; }

    #endregion


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour, Poolable
{

    [SerializeField]
    private string _poolableKey;
    private bool _pooled;
    private Poolable _poolable;

    protected MyEffectData _effectData;


    private void Awake()
    {
        _poolable = this;
    }



    protected void Update()
    {

        if (_pooled) return;

        if (FollowTarget)
        {
            transform.position = Target;
        }

    }


    public virtual void InitEffect(MyEffectData effectData)
    {
        _effectData = effectData;
        transform.position = _effectData.startPosition;
        if(effectData.lifeTime>0)
            Invoke("SendToPool", effectData.lifeTime);

        //InitEffectImpl();
    }




    protected void SendToPool()
    {
        _poolable.AddToPool();
    }




    #region GetterSetter
    public string Key { get => _poolableKey; set => _poolableKey = value; }

    public MonoBehaviour MonoBehaviour => this;

    public bool Pooled { get => _pooled; set => _pooled = value; }


    public bool FollowTarget { get => _effectData.followTarget; }

    private Vector3 _lastTargetPos;
    public Vector3 Target { get {

            if (_effectData.usingTransform)
            {
                if(_effectData.targetTransform == null) return _lastTargetPos;
                _lastTargetPos = _effectData.targetTransform.position;
                 return _effectData.targetTransform.position;
            }
            else
            {
                return _effectData.targetPosition;
            }

        } 
    }




    #endregion





}

public struct MyEffectData
{
    public float lifeTime;
    public Vector3 startPosition;

    public bool usingTransform;
    public Transform targetTransform;
    public Vector3 targetPosition;

    public bool followTarget;


    public MyEffectData(Vector3 startPosition, float lifeTime, Transform target, bool followTarget = false)
    {
        this.startPosition = startPosition;
        this.usingTransform = true;
        this.lifeTime = lifeTime;
        this.targetTransform = target;
        this.targetPosition = Vector3.zero;
        this.followTarget = followTarget;
    }

    public MyEffectData(Vector3 startPosition,Transform target, bool followTarget = false)
    {
        this.startPosition = startPosition;
        this.usingTransform = true;
        this.lifeTime = -1;
        this.targetTransform = target;
        this.targetPosition = Vector3.zero;
        this.followTarget = followTarget;
    }

}
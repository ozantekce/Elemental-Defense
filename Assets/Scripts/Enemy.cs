using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// It can be poolable but not implemented yet
public class Enemy : MonoBehaviour/*, Poolable*/
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _currentMovSpeed;

    private float _maxHP,_currentHP;

    [SerializeField]
    private EnemyStatus _status;

    [SerializeField]
    private GameObject _visual;
    private EnemyPathFollower _pathFollower;


    [SerializeField]
    private HPBar hpBar;

    private CooldownManualReset _slowCD;
    private CooldownManualReset _stunCD;


    private Renderer _renderer;
    [SerializeField]
    private Material _normalMaterial;
    [SerializeField]
    private Material _slowedMaterial;
    [SerializeField]
    private Material _stunedMaterail;

    /*
    private Poolable _poolable;
    private bool _pooled;
    public string _poolableKey;
    */
    private void Start()
    {
        _pathFollower = new EnemyPathFollower(this);
        _maxHP = Local.Instance.EnemyHP;
        _currentHP = _maxHP;

        _slowCD = new CooldownManualReset(Local.WaterEffectDuration*1000f);
        _stunCD = new CooldownManualReset(Local.EarthEffectDuration*1000f);

        _renderer = _visual.GetComponentInChildren<Renderer>();

        //_poolable = this;
    }

    /*
    public void InitEnemy()
    {
        _pathFollower.Init();
        _maxHP = Local.Instance.EnemyHP;
        _currentHP = _maxHP;

        _slowCD.ResetTimer();
        _stunCD.ResetTimer();

        _visual.transform.SetParent(transform,false);
        if (animator == null) animator = _visual.GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.Normal;
        animator.SetTrigger("Run");
    }
    */

    private void Update()
    {
        //if (Pooled) return;
        CalculateCurrentSpeed();
        _pathFollower.Tick();
    }

    private void CalculateCurrentSpeed()
    {
        if (Status == EnemyStatus.Slowed &&  _slowCD.TimeOver())
        {
            Status = EnemyStatus.None;
        }
        if (Status == EnemyStatus.Stunned && _stunCD.TimeOver())
        {
            Status = EnemyStatus.None;
        }

        CurrentMovSpeed = _movementSpeed;

        if(Status == EnemyStatus.Stunned)
        {
            CurrentMovSpeed = 0;
            return;
        }

        float movSpeed = _movementSpeed;
        if (Status == EnemyStatus.Slowed)
        {
            movSpeed *= (1f - Local.Instance.ElementEffect(Element.Water));
            CurrentMovSpeed = movSpeed;
        }

    }



    public void TakeDamage(float amount)
    {

        if (_currentHP <= 0) return;
        _currentHP -= amount;
        if (_currentHP <= 0)
        {
            DestroyedByTower();
            hpBar.SetHPPercentage(0);
        }
        else{

            hpBar.SetHPPercentage(_currentHP/_maxHP);

        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }

    private Animator animator;
    private void DestroyedByTower()
    {
        Local.Instance.Gold += Local.Instance.GoldDrop;
        ResourceEarnAnimation.CreateResourceEarnAnimation("GoldEarnAnimation"
            , transform.position
            , transform.position +Vector3.up *50
            , 1.6f
            , Local.Instance.GoldDrop);
        float r = Random.Range(0, 1f);
        if (r < Local.Instance.EssenceChange)
        {
            // Essence 
            Local.Instance.Essence+=Local.Instance.EssenceDrop;
            ResourceEarnAnimation.CreateResourceEarnAnimation("EssenceEarnAnimation"
            , transform.position - Vector3.up * 10
            , transform.position + Vector3.up * 75
            , 2.2f
            , Local.Instance.EssenceDrop);
        }

        _visual.transform.SetParent(null);
        if(animator == null) animator = _visual.GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        animator.SetTrigger("Die");
        Destroy(_visual.gameObject, 2f);

        Destroy(gameObject, 0.1f);



    }


    private void ReachedToBase(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            // reset wave
            GameManager.Instance.ResetWave();

            //Destroy(gameObject,0.1f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        ReachedToBase(other);
    }


    #region GetterSetter
    public float CurrentMovSpeed { get => _currentMovSpeed; set => _currentMovSpeed = value; }

    public EnemyStatus Status
    {
        get => _status;

        set
        {
            if (_status == EnemyStatus.Stunned && value == EnemyStatus.Slowed)
            {
                return;
            }
            
            if (value == EnemyStatus.Stunned)
            {
                _renderer.material = _stunedMaterail;
                if(Status!=EnemyStatus.Stunned)
                {
                    StatusEffect statusEffect
                        = Poolable.GetFromPool<StatusEffect>("StunStatusEffect");
                    statusEffect.InitEffect(EnemyStatus.Stunned, this, new MyEffectData(
                        startPosition: transform.position,
                        target: this.transform,
                        true
                        ));
                }

                _stunCD.ResetTimer();
            }
            if (value == EnemyStatus.Slowed)
            {
                _renderer.material = _slowedMaterial;
                if(Status!=EnemyStatus.Slowed )
                {
                    StatusEffect statusEffect
                        = Poolable.GetFromPool<StatusEffect>("SlowStatusEffect");
                    statusEffect.InitEffect(EnemyStatus.Slowed, this, new MyEffectData(
                        startPosition: transform.position,
                        target: this.transform,
                        true
                        ));
                }
                _slowCD.ResetTimer();
            }
            if(value==EnemyStatus.None && _status != EnemyStatus.None)
            {
                _renderer.material = _normalMaterial;
            }
            _status = value;
        }


    }
    public CooldownManualReset SlowCD { get => _slowCD; set => _slowCD = value; }
    //public string Key { get => _poolableKey; set => _poolableKey = value; }

    //public MonoBehaviour MonoBehaviour => this;

    //public bool Pooled { get => _pooled; set => _pooled= value; }
    #endregion


    private class EnemyPathFollower
    {

        private Enemy _parent;

        public Enemy Parent { get => _parent; set => _parent = value; }
        public Transform[] Path { get => _path; set => _path = value; }

        private Transform _target;

        private Transform[] _path;
        private int _pathIndex;

        private bool _pathOver;

        private Vector3 _dir;


        public EnemyPathFollower(Enemy parent)
        {
            Parent = parent;
            Path = GameManager.Instance.EnemyFollowPath;
            _target = _path[0];
            _dir = (_target.position - Parent.transform.position).normalized;
        }

        public void Tick()
        {
            if (_pathOver /*|| Parent.Pooled*/) return;


            FindDirection();

            if (ControlReachToTarget())
            {
                OnReachedToTarget();
                if (_pathOver) return;
            }

            Move();
            Rotate();

        }

        private void FindDirection()
        {
            _dir = (_target.position - Parent.transform.position);
            _dir.y = 0;
            _dir.Normalize();
        }

        private void Move()
        {
            // Move
            float distance
                = Vector3.SqrMagnitude(_posXYCache - _targetXYCache);

            float moveMag = GameManager.DeltaTime() * Parent.CurrentMovSpeed;

            if(moveMag*moveMag < distance)
            {
                Parent.transform.Translate(_dir * GameManager.DeltaTime() * Parent.CurrentMovSpeed);
            }
            else
            {
                Parent.transform.Translate(_dir * Mathf.Sqrt(distance));
            }

            
        }

        private void Rotate()
        {
            Vector3 forward = Parent._visual.transform.forward;
            if (0.3f > Vector3.SqrMagnitude(forward - _dir))
            {
                Parent._visual.transform.forward = _dir;
                return;
            }
            
            Parent._visual.transform.forward
                = Vector3.Lerp(forward
                , _dir, 3f * GameManager.DeltaTime());

        }

        private void OnReachedToTarget()
        {
            _pathIndex++;
            if(_pathIndex >= _path.Length)
            {
                _pathOver = true;
                return;
            }
            _target = _path[_pathIndex];
            
        }


        private Vector2 _posXYCache;
        private Vector2 _targetXYCache;
        private bool ControlReachToTarget()
        {
            _posXYCache.x = Parent.transform.position.x;
            _posXYCache.y = Parent.transform.position.z;

            _targetXYCache.x = _target.position.x;
            _targetXYCache.y = _target.position.z;
            
            float distance 
                = Vector3.SqrMagnitude(_posXYCache - _targetXYCache);

            if (distance < 4f) return true;

            return false;
        }


    }


}

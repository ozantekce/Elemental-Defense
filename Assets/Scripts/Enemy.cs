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
    private Image _hpBarFill;

    [SerializeField]
    private Transform hpBarFill;

    private CooldownManualReset _slowCD;
    private CooldownManualReset _stunCD;

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

        _slowCD = new CooldownManualReset(1000f);
        _stunCD = new CooldownManualReset(300f);

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
        if (_status == EnemyStatus.slowed &&  _slowCD.TimeOver())
        {
            _status = EnemyStatus.none;
        }
        if (_status == EnemyStatus.stunned && _stunCD.TimeOver())
        {
            _status = EnemyStatus.none;
        }

        CurrentMovSpeed = _movementSpeed;

        if(_status == EnemyStatus.stunned)
        {
            CurrentMovSpeed = 0;
            return;
        }

        float movSpeed = _movementSpeed;
        if (Status == EnemyStatus.slowed)
        {
            movSpeed *= (1f - Local.Instance.WaterEffect);
            CurrentMovSpeed = movSpeed;
        }

    }


    private Vector3 hpBarCache;
    private Quaternion hpBarRotation = new Quaternion(0, 0, 0, 1);//Quaternion(0,0,0,1)
    public void TakeDamage(float amount)
    {
        hpBarCache = hpBarFill.transform.localScale;
        if (_currentHP <= 0) return;
        _currentHP -= amount;
        if (_currentHP <= 0)
        {
            DestroyedByTower();
            hpBarCache.z = 0;
            hpBarFill.transform.localScale = hpBarCache;
        }
        else{
            hpBarCache.z = _currentHP / _maxHP;
            hpBarFill.transform.localScale = hpBarCache;
        }
        hpBarFill.rotation = hpBarRotation;
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }

    private Animator animator;
    private void DestroyedByTower()
    {
        Local.Instance.Gold += Local.Instance.GoldDrop;
        int r = Random.Range(0, 101);
        if (r < Local.Instance.EssenceChange)
        {
            // Essence 
            Local.Instance.Essence++;
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
            if (_status == EnemyStatus.stunned && value == EnemyStatus.slowed)
            {
                return;
            }
            if (value == EnemyStatus.stunned)
            {
                _stunCD.ResetTimer();
            }
            if (value == EnemyStatus.slowed)
            {
                _slowCD.ResetTimer();
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

            float moveMag = Time.deltaTime * Parent.CurrentMovSpeed;

            if(moveMag*moveMag < distance)
            {
                Parent.transform.Translate(_dir * Time.deltaTime * Parent.CurrentMovSpeed);
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
                , _dir, 3f * Time.deltaTime);

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

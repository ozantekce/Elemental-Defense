using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// It can be poolable but not implemented yet
public class Enemy : MonoBehaviour, Poolable
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
    private CooldownManualReset _slowCD;
    private CooldownManualReset _stunCD;


    private Poolable _poolable;
    private bool _pooled;
    public string _poolableKey;

    private void Start()
    {
        _pathFollower = gameObject.AddComponent<EnemyPathFollower>();
        _pathFollower.Parent = this;
        _pathFollower.Init();
        _maxHP = Local.Instance.EnemyHP;
        _currentHP = _maxHP;

        _slowCD = new CooldownManualReset(1000f);
        _stunCD = new CooldownManualReset(300f);

        _poolable = this;
    }


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


    private void Update()
    {
        if (Pooled) return;
        CalculateCurrentSpeed();
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

    public void TakeDamage(float amount)
    {
        if (_currentHP <= 0) return;
        _currentHP -= amount;
        if (_currentHP <= 0)
        {
            DestroyedByTower();
            _hpBarFill.fillAmount = 0;
        }
        else{_hpBarFill.fillAmount = _currentHP / _maxHP;}
            
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
    public string Key { get => _poolableKey; set => _poolableKey = value; }

    public MonoBehaviour MonoBehaviour => this;

    public bool Pooled { get => _pooled; set => _pooled= value; }
    #endregion


    private class EnemyPathFollower : MonoBehaviour
    {

        private Enemy _parent;

        public Enemy Parent { get => _parent; set => _parent = value; }
        public Transform[] Path { get => _path; set => _path = value; }

        private Transform _target;

        private Transform[] _path;
        private int _pathIndex;

        private bool _pathOver;

        private Vector3 _dir;

        public void Init()
        {
            Path = GameManager.Instance.EnemyFollowPath;
            _target = _path[0];
            _dir = (_target.position - transform.position).normalized;
        }


        private void Update()
        {
            if (_pathOver || Parent.Pooled) return;

            if (ControlReachToTarget())
            {
                OnReachedToTarget();
                if (_pathOver) return;
            }

            Move();
            Rotate();

        }

        private void Move()
        {
            _dir = (_target.position - Parent.transform.position);
            _dir.y = 0;
            _dir.Normalize();
            // Move
            Parent.transform.Translate(_dir * Time.deltaTime * Parent.CurrentMovSpeed);
        }

        private void Rotate()
        {
            Vector3 forward = Parent._visual.transform.forward;
            Parent._visual.transform.forward
                = Vector3.Lerp(forward
                , _dir, 3f * Time.deltaTime);

            if (0.3f > Vector3.SqrMagnitude(forward - _dir))
            {
                Parent._visual.transform.forward = _dir;
            }
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

        private bool ControlReachToTarget()
        {
            Vector2 pos = new Vector2(Parent.transform.position.x, Parent.transform.position.z);
            Vector2 targetPos = new Vector2(_target.position.x, _target.position.z);
            float distance 
                = Vector3.SqrMagnitude(pos - targetPos);

            if (distance < 1f) return true;

            return false;
        }


    }


}

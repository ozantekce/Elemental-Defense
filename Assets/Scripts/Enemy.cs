using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private float _movementSpeed;
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




    private void Start()
    {
        GameObject pathFollowerGO = new GameObject("PathFollower");
        pathFollowerGO.transform.SetParent(transform);
        pathFollowerGO.transform.localPosition = Vector3.zero;
        _pathFollower = pathFollowerGO.AddComponent<EnemyPathFollower>();
        _pathFollower.Parent = this;
        _pathFollower.Path = GameManager.Instance.EnemyFollowPath;
        _maxHP = Local.Instance.EnemyHP;
        _currentHP = _maxHP;

        _slowCD = new CooldownManualReset(1000f);
        _stunCD = new CooldownManualReset(300f);

    }

    private void Update()
    {
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
        if (_currentHP <= 0)
            return;
        _currentHP -= amount;
        if (_currentHP <= 0)
        {
            DestroyedByTower();
            _hpBarFill.fillAmount = 0;
        }
        else
        {
            _hpBarFill.fillAmount = _currentHP / _maxHP;
        }
            
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }

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
        Animator animator = _visual.GetComponent<Animator>();
        animator.animatePhysics = true;
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
        //ChangeDirection(other);
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

        private void Start()
        {
            _target = _path[0];
            _dir = (_target.position - transform.position).normalized;
        }

        private void Update()
        {
            if (_pathOver) return;

            if (ControlReachToTarget())
            {
                ReachedToTarget();
                if (_pathOver) return;
            }

            Move();
            Rotate();

        }

        private void Move()
        {
            _dir = (_target.position - Parent.transform.position).normalized;
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

        private void ReachedToTarget()
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
            float distance 
                = Vector3.SqrMagnitude(_target.position - Parent.transform.position);

            if (distance < 1f) return true;

            return false;
        }


    }


}

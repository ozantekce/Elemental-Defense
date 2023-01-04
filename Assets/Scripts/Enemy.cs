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

    private float _maxHP,_currentHP;

    [SerializeField]
    private Direction _direction;

    [SerializeField]
    private EnemyStatus _status;

    private Dictionary<Direction, Vector3> _directionDic = new Dictionary<Direction, Vector3>()
    {
        { Direction.up, new Vector3(-1,0,0)},
        { Direction.down, new Vector3(1,0,0)},
        { Direction.left, new Vector3(0,0,-1)},
        { Direction.right, new Vector3(0,0,1)},
    };

    public Direction Direction { get => _direction; set => _direction = value; }
    public EnemyStatus Status { 
        
        get => _status;

        set { 
            

            if(_status==EnemyStatus.stunned && value == EnemyStatus.slowed)
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

    private ChangeDirectionController _changeDirectionController;


    private CooldownManualReset _slowCD;
    private CooldownManualReset _stunCD;

    [SerializeField]
    private GameObject _visual;


    [SerializeField]
    private Image _hpBarFill;

    private void Start()
    {
        GameObject changeDirectionGO = new GameObject("ChangeDirection");
        changeDirectionGO.transform.SetParent(transform);
        changeDirectionGO.transform.localPosition = Vector3.zero;
        _changeDirectionController = changeDirectionGO.AddComponent<ChangeDirectionController>();
        _changeDirectionController.Parent = this;

        _maxHP = Local.Instance.EnemyHP;
        _currentHP = _maxHP;

        _slowCD = new CooldownManualReset(1000f);
        _stunCD = new CooldownManualReset(300f);

    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {

        if (_status == EnemyStatus.slowed &&  _slowCD.TimeOver())
        {
            _status = EnemyStatus.none;
        }
        if (_status == EnemyStatus.stunned && _stunCD.TimeOver())
        {
            _status = EnemyStatus.none;
        }

        if(_status == EnemyStatus.stunned)
        {
            return;
        }

        if (_direction != Direction.none)
        {
            float movSpeed = _movementSpeed;
            if(Status == EnemyStatus.slowed)
            {
                movSpeed *= (1f-Local.Instance.WaterEffect);
            }

            transform.Translate(_directionDic[_direction] * Time.deltaTime * movSpeed);
        }
            



        _visual.transform.forward = Vector3.Lerp(_visual.transform.forward
            , _directionDic[_direction], 3f*Time.deltaTime);

    }

    public void TakeDamage(float amount)
    {
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

    private void ChangeDirection(Collider other)
    {
        if (other.CompareTag("ChangeDirection/Up"))
        {
            _direction = Direction.up;
        }
        else if (other.CompareTag("ChangeDirection/Down"))
        {
            _direction = Direction.down;
        }
        else if (other.CompareTag("ChangeDirection/Left"))
        {
            _direction = Direction.left;
        }
        else if (other.CompareTag("ChangeDirection/Right"))
        {
            _direction = Direction.right;
        }
    }



    private void OnTriggerEnter(Collider other)
    {


        //ChangeDirection(other);
        ReachedToBase(other);


    }



    private class ChangeDirectionController : MonoBehaviour
    {

        private Enemy _parent;
        private SphereCollider _collider;

        public Enemy Parent { get => _parent; set => _parent = value; }

        private void Start()
        {
            _collider = gameObject.AddComponent<SphereCollider>();
            _collider.radius = 0.3f;
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _parent.ChangeDirection(other);
        }

    }


}

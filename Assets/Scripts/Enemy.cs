using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private float movementSpeed;

    private float maxHP,currentHP;

    [SerializeField]
    private Direction direction;

    [SerializeField]
    private EnemyStatus status;

    private Dictionary<Direction, Vector3> directionDic = new Dictionary<Direction, Vector3>()
    {
        { Direction.up, new Vector3(-1,0,0)},
        { Direction.down, new Vector3(1,0,0)},
        { Direction.left, new Vector3(0,0,-1)},
        { Direction.right, new Vector3(0,0,1)},
    };

    public Direction Direction { get => direction; set => direction = value; }


    private ChangeDirectionController _changeDirectionController;

    private void Start()
    {
        GameObject changeDirectionGO = new GameObject("ChangeDirection");
        changeDirectionGO.transform.SetParent(transform);
        changeDirectionGO.transform.localPosition = Vector3.zero;
        _changeDirectionController = changeDirectionGO.AddComponent<ChangeDirectionController>();
        _changeDirectionController.Parent = this;

        maxHP = Local.Instance.EnemyHP;
        currentHP = maxHP;


    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(direction!=Direction.none)
            transform.Translate(directionDic[direction] * Time.deltaTime * movementSpeed);
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
            DestroyedByTower();

    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }

    private void DestroyedByTower()
    {
        Local.Instance.Gold += Local.Instance.GoldDrop;
        Destroy(gameObject, 0.1f);
    }

    private void ReachedToBase(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            // reset wave
            Destroy(gameObject,0.1f);
        }
    }

    private void ChangeDirection(Collider other)
    {
        if (other.CompareTag("ChangeDirection/Up"))
        {
            direction = Direction.up;
        }
        else if (other.CompareTag("ChangeDirection/Down"))
        {
            direction = Direction.down;
        }
        else if (other.CompareTag("ChangeDirection/Left"))
        {
            direction = Direction.left;
        }
        else if (other.CompareTag("ChangeDirection/Right"))
        {
            direction = Direction.right;
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

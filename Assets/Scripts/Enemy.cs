using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
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

    private void Start()
    {
        
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

    private void DestroyedByTower()
    {
        Destroy(gameObject, 0.1f);
    }

    private void ReachedToBase(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            // increase player hp
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

        ChangeDirection(other);
        ReachedToBase(other);

    }

}

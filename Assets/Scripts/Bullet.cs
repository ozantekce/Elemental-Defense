using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject _source;
    [SerializeField]
    private Enemy _destination;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _attackPower;

    private bool _destroying = false;

    public Enemy Destination { get => _destination; set => _destination = value; }
    public GameObject Source { get => _source; set => _source = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float AttackPower { get => _attackPower; set => _attackPower = value; }

    void Update()
    {
        if(_destination == null)
        {
            if (!_destroying)
            {
                Destroy(gameObject,1f);
                _destroying = true;
            }

        }

        MoveToDestination();

        
    }

    private Vector3 _destinationLastPos;
    private void MoveToDestination()
    {


        Vector3 directionVector;
        if(Destination != null)
        {
            _destinationLastPos = Destination.transform.position;
            directionVector = _destination.transform.position - transform.position;
        }
        else
        {
            directionVector = _destinationLastPos - transform.position;
        }
        
        directionVector = directionVector.normalized;
        transform.Translate(directionVector * Time.deltaTime * _speed);


    }



    private void OnTriggerEnter(Collider other)
    {
        if(_destroying || _destination == null)
        {
            return;
        }

        if (other.gameObject == _destination.gameObject)
        {
            _destination.TakeDamage(_attackPower);
            Destroy(gameObject, 0.5f);
            _destroying = true;
        }
    }

}

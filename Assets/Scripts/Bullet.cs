using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject source;
    [SerializeField]
    private Enemy destination;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float attackPower;

    private bool destroying = false;

    public Enemy Destination { get => destination; set => destination = value; }
    public GameObject Source { get => source; set => source = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AttackPower { get => attackPower; set => attackPower = value; }

    void Update()
    {
        if(destination == null)
        {
            if (!destroying)
            {
                Destroy(gameObject);
                destroying = true;
            }

        }
        else
        {
            MoveToDestination();
        }

        
    }


    private void MoveToDestination()
    {

        Vector3 directionVector = destination.transform.position - transform.position;
        directionVector = directionVector.normalized;
        transform.Translate(directionVector * Time.deltaTime * speed);

    }


    private void OnTriggerEnter(Collider other)
    {
        if(destroying || destination == null)
        {
            return;
        }

        if (other.gameObject == destination.gameObject)
        {
            destination.TakeDamage(attackPower);
            Destroy(gameObject, 0.5f);
            destroying = true;
        }
    }

}

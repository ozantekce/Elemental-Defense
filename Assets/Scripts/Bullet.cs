using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Tower _source;
    [SerializeField]
    private Enemy _destination;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _attackPower;

    

    public Enemy Destination { get => _destination; set => _destination = value; }
    public Tower Source { get => _source; set => _source = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    
    private bool _selfDestroying = false;
    void Update()
    {
        if(_destination == null)
        {
            if (!_selfDestroying)
            {
                gameObject.SetActive(false);
                Destroy(gameObject,1f);
                _selfDestroying = true;
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
        if(_selfDestroying || _destination == null)
        {
            return;
        }

        if (other.gameObject == _destination.gameObject)
        {
            _destination.TakeDamage(_attackPower);

            if(Source.TowerType == TowerType.air)
            {
                int layerMask = 1 << LayerMask.NameToLayer("Enemy");
                Collider[] hitColliders 
                    = Physics.OverlapSphere(transform.position, Local.AirEffectRange, layerMask);
                //Debug.Log(hitColliders.Length);
                foreach (Collider hitCollider in hitColliders)
                {

                    hitCollider.GetComponent<Enemy>().TakeDamage(_attackPower * Local.Instance.AirEffect);

                }
            }

            if(Source.TowerType == TowerType.water)
            {
                _destination.Status = EnemyStatus.slowed;
            }
            if (Source.TowerType == TowerType.earth)
            {
                int r = Random.Range(0, 101);
                if(r <= 100f*Local.Instance.EarthEffect)
                {
                    _destination.Status = EnemyStatus.stunned;
                }
                

            }

            Destroy(gameObject, 0.5f);
            _selfDestroying = true;
        }
    }

}

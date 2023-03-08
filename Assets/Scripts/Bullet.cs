using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, Poolable
{
    [SerializeField]
    private Tower _source;
    [SerializeField]
    private Enemy _destination;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _attackPower;

    [SerializeField]
    private string _poolableKey;


    public Enemy Destination { get => _destination; set => _destination = value; }
    public Tower Source { get => _source; set => _source = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    public string Key { get => _poolableKey; set => _poolableKey = value; }
    public MonoBehaviour MonoBehaviour { get => this; }
    public bool Pooled { get => _pooled; set => _pooled = value; }

    //private bool _selfDestroying = false;

    private bool _pooled;

    private Poolable _poolable;

    private void Awake()
    {
        _poolable = this;
    }

    void Update()
    {
        if (_pooled)
        {
            return;
        }

        if(_destination == null)
        {
            _poolable.SendToPool();
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
            _poolable.SendToPool();
        }
    }


}

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
    private HitEffect _hitEffect;

    [SerializeField]
    private string _poolableKey;
    private bool _pooled;
    private Poolable _poolable;
    private Queue<IBulletCommand> _commandQueue;



    private void Awake()
    {
        _poolable = this;
        _commandQueue = new Queue<IBulletCommand>();
    }

    public void InitBullet(Vector3 spawnPoint, float damage,bool isCritical, Tower source, Enemy destination)
    {

        transform.position = spawnPoint;
        AttackPower = damage;
        Source = source;
        Destination = destination;

        _commandQueue.Clear();
        _commandQueue.Enqueue(new BulletDamageCommand(_destination, _attackPower,isCritical));
        if (Source.TowerType == TowerType.Water)
        {
            _commandQueue.Enqueue(new BulletSlowCommand(_destination));
        }
        else if (Source.TowerType == TowerType.Earth)
        {
            _commandQueue.Enqueue(new BulletStunCommand(_destination));
        }
        else if (Source.TowerType == TowerType.Air)
        {
            _commandQueue.Enqueue(new BulletMessyAttackCommand(_destination, _attackPower, isCritical));
        }

    }


    void Update()
    {
        if (_pooled)
        {
            return;
        }

        if(_destination == null)
        {
            _poolable.AddToPool();
            return;
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
        transform.Translate(directionVector * GameManager.DeltaTime() * _speed,Space.World);

    }





    private void OnTriggerEnter(Collider other)
    {
        if (_poolable.Pooled || _destination==null) return;

        if (other.gameObject == _destination.gameObject)
        {
            while(_commandQueue.Count > 0) _commandQueue.Dequeue().Execute();
            _poolable.AddToPool();
            HitEffect(other.transform);
        }
    }


    private void HitEffect(Transform target)
    {
        if (_hitEffect == null)
        {
            return;
        }
        HitEffect hitEffect = Poolable.GetFromPool(_hitEffect);
        hitEffect.InitHitEffect(target);

    }


    #region GetterSetter
    public Enemy Destination { get => _destination; set => _destination = value; }
    public Tower Source { get => _source; set => _source = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    public string Key { get => _poolableKey; set => _poolableKey = value; }
    public MonoBehaviour MonoBehaviour { get => this; }
    public bool Pooled { get => _pooled; set => _pooled = value; }
    #endregion


}

public interface IBulletCommand
{
    void Execute();

}


public class BulletSlowCommand : IBulletCommand
{
    private Enemy _enemy;

    public BulletSlowCommand(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Execute()
    {
        _enemy.Status = EnemyStatus.Slowed;
    }

}


public class BulletStunCommand : IBulletCommand
{
    private Enemy _enemy;

    public BulletStunCommand(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Execute()
    {
        int r = Random.Range(0, 101);
        if (r <= 100f * Local.Instance.ElementEffect(Element.Earth))
        {
            _enemy.Status = EnemyStatus.Stunned;
        }
    }

}


public class BulletMessyAttackCommand : IBulletCommand
{
    private Enemy _enemy;
    private float _damage;
    private bool _isCritical;
    public BulletMessyAttackCommand(Enemy enemy,float damage, bool isCritical)
    {
        this._enemy = enemy;
        this._damage = damage * Local.Instance.ElementEffect(Element.Air);
        _isCritical = isCritical;
    }

    public void Execute()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] hitColliders
            = Physics.OverlapSphere(_enemy.transform.position, Local.AirEffectRange, layerMask);
        string text = _damage.ToString("F0");
        if (_isCritical) { text = "<color=red>" + text + "</color>"; }
        foreach (Collider hitCollider in hitColliders)
        {
            Enemy e = hitCollider.GetComponent<Enemy>();
            if (e == _enemy) continue;
            e.TakeDamage(_damage );
            DamageTextManager.Instance.CreateText(text, hitCollider.transform.position + Vector3.up * 2f);
        }
    }
}


public class BulletDamageCommand : IBulletCommand
{
    private Enemy _enemy;
    private float _damage;
    private bool _isCritical;
    public BulletDamageCommand(Enemy enemy, float damage,bool isCritical)
    {
        this._enemy = enemy;
        this._damage = damage;
        this._isCritical = isCritical;
    }

    public void Execute()
    {
        _enemy.TakeDamage(_damage);
        string text = _damage.ToString("F0");
        if(_isCritical) { text = "<color=red>" + text + "</color>"; }
        DamageTextManager.Instance.CreateText(text, _enemy.transform.position+Vector3.up*2f);
    }
}
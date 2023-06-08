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
    private bool _isCritical;
    [SerializeField]
    private HitEffect _hitEffect;


    [SerializeField]
    private string _poolableKey;
    private bool _pooled;
    private Poolable _poolable;
    
    private Queue<IBulletCommand> _commandQueue;


    private bool _exploded;

    private void Awake()
    {
        _poolable = this;
        _commandQueue = new Queue<IBulletCommand>();
    }

    public void InitBullet(Vector3 spawnPoint, float damage,bool isCritical, Tower source, Enemy destination)
    {
        _exploded = false;
        transform.position = spawnPoint;
        AttackPower = damage;
        Source = source;
        Destination = destination;
        IsCritical = isCritical;

        _commandQueue.Clear();
        _commandQueue.Enqueue(new BulletDamageCommand(this));
        if (Source.TowerType == TowerType.Water)
        {
            _commandQueue.Enqueue(new BulletSlowCommand(this));
        }
        else if (Source.TowerType == TowerType.Earth)
        {
            _commandQueue.Enqueue(new BulletStunCommand(this));
        }
        else if (Source.TowerType == TowerType.Air)
        {
            _commandQueue.Enqueue(new BulletMessyAttackCommand(this));
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
            if (!_waitingToAddPool && TryFindNewDestination()) ;
            else
            {
                _poolable.AddToPool();
                return;
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
        transform.Translate(directionVector * GameManager.DeltaTime() * _speed,Space.World);

    }


    private bool TryFindNewDestination()
    {
        Destination = FindEnemy();
        
        return Destination==null ? false:true;
    }


    private Enemy FindEnemy()
    {
        float minDistance = float.MaxValue;
        Enemy target = null;
        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            float dist = Vector3.Distance(_destinationLastPos, enemy.transform.position);
            if (dist <= Source.Range && dist < minDistance)
            {
                minDistance = dist;
                target = enemy;
            }
        }

        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_poolable.Pooled || _destination==null || _exploded) return;

        if (other.gameObject == _destination.gameObject)
        {
            while(_commandQueue.Count > 0) _commandQueue.Dequeue().Execute();
            
            _exploded = true;
            
            StartCoroutine(AddToPool());

            HitEffect(other.transform);
        }
    }


    private bool _waitingToAddPool;
    private WaitForSeconds _wait;
    private IEnumerator AddToPool()
    {
        _waitingToAddPool = true;
        if (_wait == null) _wait = new WaitForSeconds(0.1f);
        
        yield return _wait;

        _poolable.AddToPool();
        _waitingToAddPool = false;
    }

    private void HitEffect(Transform target)
    {
        if (_hitEffect == null)
        {
            return;
        }
        HitEffect hitEffect = Poolable.GetFromPool(_hitEffect);
        hitEffect.InitEffect(new MyEffectData(target.position,1f,target,true));

    }


    #region GetterSetter
    public Enemy Destination { get => _destination; set => _destination = value; }
    public Tower Source { get => _source; set => _source = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    public string Key { get => _poolableKey; set => _poolableKey = value; }
    public MonoBehaviour MonoBehaviour { get => this; }
    public bool Pooled { get => _pooled; set => _pooled = value; }
    public bool IsCritical { get => _isCritical; set => _isCritical = value; }
    #endregion


}




#region BulletCommands

public interface IBulletCommand
{
    Bullet Bullet { get; set; }
    void Execute();

}


public class BulletSlowCommand : IBulletCommand
{

    private Bullet _bullet;
    public BulletSlowCommand(Bullet bullet)
    {
        this._bullet = bullet;
    }

    public Bullet Bullet { get => _bullet; set => _bullet=value; }

    public void Execute()
    {
        Bullet.Destination.Status = EnemyStatus.Slowed;
    }

}


public class BulletStunCommand : IBulletCommand
{

    private Bullet _bullet;
    public BulletStunCommand(Bullet bullet)
    {
        this._bullet = bullet;
    }

    public Bullet Bullet { get => _bullet; set => _bullet = value; }
    public void Execute()
    {
        int r = Random.Range(0, 101);
        if (r <= 100f * Local.Instance.ElementEffect(Element.Earth))
        {
            Bullet.Destination.Status = EnemyStatus.Stunned;
        }
    }

}


public class BulletMessyAttackCommand : IBulletCommand
{
    private Bullet _bullet;
    public BulletMessyAttackCommand(Bullet bullet)
    {
        this._bullet = bullet;
    }

    public Bullet Bullet { get => _bullet; set => _bullet = value; }
    public Enemy Enemy { get => Bullet.Destination; }
    public float Damage { get => Bullet.AttackPower * Element.Air.ElementEffect(); }
    public bool IsCritical { get => Bullet.IsCritical; }

    public void Execute()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] hitColliders
            = Physics.OverlapSphere(Enemy.transform.position, Local.AirEffectRange, layerMask);
        string text = Damage.ToString("F0");
        if (IsCritical) { text = "<color=red>" + text + "!</color>"; }
        foreach (Collider hitCollider in hitColliders)
        {
            Enemy e = hitCollider.GetComponent<Enemy>();
            if (e == Enemy) continue;
            e.TakeDamage(Damage);
            InfoTextManager.Instance.CreateText(text, hitCollider.transform.position + Vector3.up * 2f);
        }
    }
}


public class BulletDamageCommand : IBulletCommand
{
    private Bullet _bullet;
    public BulletDamageCommand(Bullet bullet)
    {
        this._bullet = bullet;
    }

    public Bullet Bullet { get => _bullet; set => _bullet = value; }
    public Enemy Enemy { get => Bullet.Destination; }
    public float Damage { get => Bullet.AttackPower; }
    public bool IsCritical { get => Bullet.IsCritical; }

    public void Execute()
    {
        Enemy.TakeDamage(Damage);
        string text = Damage.ToString("F0");
        if(IsCritical) { text = "<color=red>" + text + "!</color>"; }
        InfoTextManager.Instance.CreateText(text, Enemy.transform.position+Vector3.up*2f);
    }
}


#endregion



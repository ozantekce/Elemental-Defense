using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{


    [SerializeField]
    private string _name;
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private Bullet _bulletPrefab;


    [SerializeField]
    private TowerType _towerType;


    #region BaseValues
    public float baseRange = 500f;
    public float baseAttackPower = 1;
    public float baseAttackPerSecond = 1;
    public float baseCriticalChange = 1;
    public float baseCriticalDamage = 1;
    #endregion


    #region CurrentValues
    [SerializeField]
    private float _range;
    [SerializeField]
    private float _attackPower;
    [SerializeField]
    private float _attackPerSecond;
    [SerializeField]
    private float _criticalChange;
    [SerializeField]
    private float _criticalDamage;
    #endregion


    [SerializeField]
    private int _currentLevel;

    private CooldownDynamic _attackCD;

    private void Start()
    {
        _attackCD = new CooldownDynamic();
        UpdateValues();
    }


    private Enemy currentEnemy;

    private void Update()
    {
        UpdateValues();
        
        if(currentEnemy == null
            || Vector3.Distance(transform.position,currentEnemy.transform.position)>=_range)
            currentEnemy = FindEnemy();

        if(_attackCD.Ready(1000 / _attackPerSecond) && currentEnemy != null)
        {
            SendBullet(currentEnemy);
        }

    }


    private const int UpdateRate = 10;
    private int _currentUpdate = 11;

    private void UpdateValues()
    {
        _currentUpdate++;
        if(_currentUpdate >= UpdateRate)
        {
            _currentUpdate = 0;
        }
        else
        {
            return;
        }

        _range = baseRange * Local.Instance.Range;
        _attackPower = baseAttackPower * Local.Instance.Damage;
        _attackPerSecond = baseAttackPerSecond * Local.Instance.AttackSpeed;
        _criticalChange = baseCriticalChange * Local.Instance.CriticalHitChange;
        _criticalDamage = baseCriticalDamage * Local.Instance.CriticalHitDamage;

    }



    private void SendBullet(Enemy target)
    {

        float damage = _attackPower;
        int r = Random.Range(0, 101);
        if (r < CriticalChange)
        {
            // Critical Attack
            damage *= (1+_criticalDamage);
        }

        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.AttackPower = damage;
        bullet.Source = gameObject;
        bullet.Destination = target;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Enemy FindEnemy()
    {
        float minDistance = float.MaxValue;
        Enemy target = null;
        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if(dist<=_range &&dist < minDistance)
            {
                minDistance = dist;
                target = enemy;
            }
        }

        return target;
    }




    private bool UpdateTower()
    {


        return false;
    }

    private void UpdateUpdateCost()
    {

    }


    private void ShowRange()
    {


    }



    #region GetterSetter

    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    public float Range { get => _range; set => _range = value; }
    public float AttackPerSecond { get => _attackPerSecond; set => _attackPerSecond = value; }
    public float CriticalChange { get => _criticalChange; set => _criticalChange = value; }
    public float CriticalDamage { get => _criticalDamage; set => _criticalDamage = value; }
    public TowerType TowerType { get => _towerType; set => _towerType = value; }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }


    #endregion


}

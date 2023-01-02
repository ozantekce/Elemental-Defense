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
    public float baseRange = 150f;
    public float baseAttackPower = 3;
    public float baseAttackPerSecond = 1.5f;
    public float baseCriticalChange = 10f;
    public float baseCriticalDamage = 1;
    #endregion

    #region UpdateIncreaseValues
    private float increaseRange = 1f;
    private float increaseAttackPower = 0.5f;
    private float increaseAttackPerSecond = 0.05f;
    private float increaseCriticalChange = 0.1f;
    private float increaseCriticalDamage = 0.1f;
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

        _range = (baseRange + increaseRange * _currentLevel) * Local.Instance.Range;
        _attackPower = (baseAttackPower + increaseAttackPower * _currentLevel ) * Local.Instance.Damage;
        _attackPerSecond = (baseAttackPerSecond + increaseAttackPerSecond * _currentLevel) * Local.Instance.AttackSpeed;
        _criticalChange = (baseCriticalChange + increaseCriticalChange * _currentLevel) * Local.Instance.CriticalHitChange;
        _criticalDamage = (baseCriticalDamage + increaseCriticalDamage * _currentLevel) * Local.Instance.CriticalHitDamage;

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




    public void UpdateTower()
    {
        _currentLevel++;

        _currentUpdate = 11;
        UpdateValues();

    }


    private void ShowRange()
    {


    }


    public string GetTowerInfo()
    {
        string info = "";

        info += "Attack Power:" + AttackPower;
        info += "\n" + "Attack Speed:" + AttackPerSecond;
        info += "\n" + "Critical Hit Change:" + CriticalChange+"%";
        info += "\n" + "Critical Hit Damage:" + 100*CriticalDamage+"%";
        info += "\n" + "Range:" + Range;

        return info;
    }



    #region GetterSetter

    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    public float Range { get => _range; set => _range = value; }
    public float AttackPerSecond { get => _attackPerSecond; set => _attackPerSecond = value; }
    public float CriticalChange { get => _criticalChange; set => _criticalChange = value; }
    public float CriticalDamage { get => _criticalDamage; set => _criticalDamage = value; }
    public TowerType TowerType { get => _towerType; set => _towerType = value; }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public string Name { get => _name; set => _name = value; }


    #endregion


}

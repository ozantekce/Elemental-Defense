using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    const int MaxLevel = 99999;

    [SerializeField]
    private string _name;
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private Bullet _bulletPrefab;
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
    [SerializeField]
    private int _currentLevel;
    [SerializeField]
    private int _updateCost;

    private CooldownDynamic _attackCD;

    private void Start()
    {
        _attackCD = new CooldownDynamic();
    }


    private Enemy currentEnemy;

    private void Update()
    {
        
        if(currentEnemy == null
            || Vector3.Distance(transform.position,currentEnemy.transform.position)>=_range)
            currentEnemy = FindEnemy();

        if(_attackCD.Ready(1000 / _attackPerSecond) && currentEnemy != null)
        {
            SendBullet(currentEnemy);
        }

    }

    private void SendBullet(Enemy target)
    {

        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.AttackPower = _attackPower;
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


    #endregion


}

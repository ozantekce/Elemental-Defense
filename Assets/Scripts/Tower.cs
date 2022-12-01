using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private static int MaxLevel;

    [SerializeField]
    private string name;
    [SerializeField]
    private Transform bulleySpawnPoint;
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private float range;
    [SerializeField]
    private float attackPower;
    [SerializeField]
    private float attackPerSecond;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int updateCost;

    private Cooldown attackCD;

    private void Start()
    {

        attackCD = new Cooldown(1000/attackPerSecond);

    }

    private Enemy currentEnemy;

    private void Update()
    {
        
        if(currentEnemy == null|| Vector3.Distance(transform.position,currentEnemy.transform.position)>=range)
            currentEnemy = FindEnemy();

        if(attackCD.Ready()&& currentEnemy != null)
        {
            SendBullet(currentEnemy);
        }

    }

    private void SendBullet(Enemy target)
    {

        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulleySpawnPoint.position;
        bullet.AttackPower = attackPower;
        bullet.Source = gameObject;
        bullet.Destination = target;

    }

    
    private Enemy FindEnemy()
    {
        float minDistance = float.MaxValue;
        Enemy target = null;
        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if(dist<=range &&dist < minDistance)
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



}

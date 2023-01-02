using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal : MonoBehaviour
{
    [SerializeField]
    private float spawnCD = 1000, waitForFirstSpawn = 5000;

    private Cooldown spawnCooldown;
    private CooldownManualReset firstSpawnCooldown;

    public Direction enemyStartDirection;
    

    private GameObject enemiesGO;

    [SerializeField]
    private Enemy _enemyType1;


    void Start()
    {

        enemiesGO = new GameObject("Enemies");

        spawnCooldown = new Cooldown(spawnCD);
        firstSpawnCooldown = new CooldownManualReset(waitForFirstSpawn);

    }


    void Update()
    {

        if (!firstSpawnCooldown.TimeOver())
        {
            return;
        }

        if (!_waveRunnnig)
        {
            
            WaveStart();
        }


        if (_enemyCount>0 && spawnCooldown.Ready())
        {
            SpawnEnemy();
        }

        if(_waveRunnnig&&GameManager.Instance.EnemyList.Count == 0)
        {
            WaveOver();
        }

    }

    
    private int _enemyCount;
    private bool _waveRunnnig;

    private void WaveStart()
    {
        _waveRunnnig = true;
        _enemyCount = Local.Instance.EnemyCount;

    }

    private void WaveOver()
    {
        _waveRunnnig = false;
        firstSpawnCooldown.ResetTimer();

        Local.Instance.Wave++;

    }

    public void ResetWave()
    {
        _waveRunnnig = false;
        firstSpawnCooldown.ResetTimer();

    }


    private void SpawnEnemy()
    {
        _enemyCount--;

        Enemy enemy = _enemyType1;

        enemy = GameObject.Instantiate(enemy);
        enemy.transform.position = transform.position+ new Vector3(0,enemy.transform.lossyScale.y,0);
        enemy.Direction = enemyStartDirection;
        enemy.transform.SetParent(enemiesGO.transform);

        GameManager.Instance.AddEnemy(enemy);

    }






}

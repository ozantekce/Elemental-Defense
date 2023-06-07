using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal : MonoBehaviour
{
    [SerializeField]
    private float spawnCD = 1000, waitForFirstSpawn = 5000;


    private CooldownDynamic spawnCooldown;
    private CooldownManualReset firstSpawnCooldown;
    
    private GameObject enemiesGO;

    [SerializeField]
    private Enemy _enemyPrefab;
    [SerializeField]
    private Enemy _bossPrefab;

    void Start()
    {

        enemiesGO = new GameObject("Enemies");

        spawnCooldown = new CooldownDynamic();
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


        if (_enemyCount>0 && spawnCooldown.Ready(spawnCD/Local.Instance.GameSpeed))
        {
            SpawnEnemy();
        }
        if(_bossCount > 0 && _enemyCount ==0 && spawnCooldown.Ready(spawnCD / Local.Instance.GameSpeed) )
        {
            SpawnBoss();
        }

        if(_waveRunnnig && GameManager.Instance.EnemyList.Count == 0 && _enemyCount==0)
        {
            WaveOver();
        }

    }

    
    private int _enemyCount;
    private int _bossCount;
    private bool _waveRunnnig;

    private void WaveStart()
    {
        if (Local.Instance.NumberOfAllTowers <= 0) return;
        _waveRunnnig = true;
        _enemyCount = Local.Instance.EnemyCount;

        _bossCount = Local.Instance.Wave % Local.BossWave==0 ? 1 : 0;

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

        Enemy enemy = _enemyPrefab;

        enemy = GameObject.Instantiate(enemy);
        enemy.transform.position = transform.position+ new Vector3(0,enemy.transform.lossyScale.y,0);
        enemy.transform.SetParent(enemiesGO.transform);

        GameManager.Instance.AddEnemy(enemy);

    }


    private void SpawnBoss()
    {

        _bossCount--;

        Enemy enemy = _bossPrefab;

        enemy = GameObject.Instantiate(enemy);
        enemy.transform.position = transform.position + new Vector3(0, enemy.transform.lossyScale.y, 0);
        enemy.transform.SetParent(enemiesGO.transform);

        GameManager.Instance.AddEnemy(enemy);
    }





}

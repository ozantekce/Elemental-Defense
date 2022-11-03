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


    private int sublevel = 0;
    public List<SubLevel> sublevels;
    

    private GameObject enemiesGO;

    void Start()
    {

        enemiesGO = new GameObject("Enemies");

        spawnCooldown = new Cooldown(spawnCD);
        firstSpawnCooldown = new CooldownManualReset(waitForFirstSpawn);

    }


    void Update()
    {

        if (sublevel >= sublevels.Count|| !firstSpawnCooldown.TimeOver())
        {
            return;
        }

        if (spawnCooldown.Ready())
        {
            SpawnEnemy();
        }

        if (enemyCountPairsIndex >= sublevels[sublevel].enemyCountPairs.Length
            && enemiesGO.transform.childCount==0)
        {
            SubLevelOver();
            return;
        }


    }

    private int enemyCountPairsIndex = 0;
    private void SpawnEnemy()
    {
        if (enemyCountPairsIndex >= sublevels[sublevel].enemyCountPairs.Length)
        {
            return;
        }

        Enemy enemy = sublevels[sublevel].enemyCountPairs[enemyCountPairsIndex].enemy;

        sublevels[sublevel].enemyCountPairs[enemyCountPairsIndex].count--;
        if (sublevels[sublevel].enemyCountPairs[enemyCountPairsIndex].count <= 0)
        {
            enemyCountPairsIndex++;
        }

        enemy = GameObject.Instantiate(enemy);
        enemy.transform.position = transform.position;
        enemy.Direction = enemyStartDirection;
        enemy.transform.SetParent(enemiesGO.transform);

    }


    private void SubLevelOver()
    {
        enemyCountPairsIndex = 0;
        sublevel++;
        firstSpawnCooldown.ResetTimer();
        if (sublevel >= sublevels.Count)
        {
            LevelOver();
        }
    }

    private void LevelOver()
    {
        // change scene
    }


    [Serializable]
    public struct EnemyCountPair
    {
        public Enemy enemy;
        public int count;
    }

    [Serializable]
    public struct SubLevel
    {
        public EnemyCountPair[] enemyCountPairs;
    }


}

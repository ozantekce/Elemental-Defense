using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    
    private List<Enemy> _enemyList = new List<Enemy>();

    [SerializeField]
    private Portal _portal;


    [SerializeField]
    private GameObject _rangeArea;

    [SerializeField]
    private Transform[] _enemyFollowPath;


    private void Awake()
    {
        Time.timeScale = 1;
        _instance = this;
    }


    public void ResetWave()
    {
        _portal.ResetWave();
        for (int i = 0; i < _enemyList.Count; i++)
        {
            Destroy(_enemyList[i].gameObject);
            
        }
        _enemyList.Clear();


    }


    public void AddEnemy(Enemy enemy)
    {
        _enemyList.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy);
    }


    public void GameSpeed1X()
    {
        Local.Instance.GameSpeedLevel = 1;
    }
    public void GameSpeed2X()
    {
        Local.Instance.GameSpeedLevel = 2;
    }
    public void GameSpeed4X()
    {
        Local.Instance.GameSpeedLevel = 6;
    }


    public static float DeltaTime()
    {
        return Time.deltaTime*Local.Instance.GameSpeed;
    }

    public static GameManager Instance { get => _instance; set => _instance = value; }
    public List<Enemy> EnemyList { get => _enemyList; set => _enemyList = value; }
    public Portal Portal { get => _portal; set => _portal = value; }
    public GameObject RangeArea { get => _rangeArea; set => _rangeArea = value; }
    public Transform[] EnemyFollowPath { get => _enemyFollowPath; set => _enemyFollowPath = value; }
}

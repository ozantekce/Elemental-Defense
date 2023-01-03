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

    private void Awake()
    {
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

    public static GameManager Instance { get => _instance; set => _instance = value; }
    public List<Enemy> EnemyList { get => _enemyList; set => _enemyList = value; }
    public Portal Portal { get => _portal; set => _portal = value; }
    public GameObject RangeArea { get => _rangeArea; set => _rangeArea = value; }
}

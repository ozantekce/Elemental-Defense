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
        Time.timeScale = 1;
    }
    public void GameSpeed2X()
    {
        Time.timeScale = 2;
    }
    public void GameSpeed4X()
    {
        Time.timeScale = 4;
    }
    public void GameSpeed8X()
    {
        Time.timeScale = 8;
    }


    public void UpdateFireLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ElementCost(Local.Instance.FireLevel))
        {
            Local.Instance.Essence -= Local.Instance.ElementCost(Local.Instance.FireLevel);
            Local.Instance.FireLevel++;
        }
    }

    public void UpdateWaterLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ElementCost(Local.Instance.WaterLevel))
        {
            Local.Instance.Essence -= Local.Instance.ElementCost(Local.Instance.WaterLevel);
            Local.Instance.WaterLevel++;
        }
    }

    public void UpdateEarthLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ElementCost(Local.Instance.EarthLevel))
        {
            Local.Instance.Essence -= Local.Instance.ElementCost(Local.Instance.EarthLevel);
            Local.Instance.EarthLevel++;
        }
    }

    public void UpdateAirLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ElementCost(Local.Instance.AirLevel))
        {
            Local.Instance.Essence -= Local.Instance.ElementCost(Local.Instance.AirLevel);
            Local.Instance.AirLevel++;
        }
    }


    public void UpdateDamageLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.DamageLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.DamageLevel);
            Local.Instance.DamageLevel++;
        }
    }

    public void UpdateAttackSpeedLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.AttackSpeedLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.AttackSpeedLevel);
            Local.Instance.AttackSpeedLevel++;
        }
    }

    public void UpdateCriticalHitChanceLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.CriticalHitChangeLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.CriticalHitChangeLevel);
            Local.Instance.CriticalHitChangeLevel++;
        }
    }

    public void UpdateCriticalHitDamageLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.CriticalHitDamageLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.CriticalHitDamageLevel);
            Local.Instance.CriticalHitDamageLevel++;
        }
    }


    public void UpdateRangeLevel()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.RangeLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.RangeLevel);
            Local.Instance.RangeLevel++;
        }
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

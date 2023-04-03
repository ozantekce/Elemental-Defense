using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Local : MonoBehaviour
{

    private static Local instance;



    private void Awake()
    {
        MakeSingleton();
    }


    [ContextMenu("Reset Local Data")]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [ContextMenu("Add Gold")]
    public void AddGold()
    {
        Gold += 1000;
    }
    [ContextMenu("Add Essence")]
    public void AddEssence()
    {
        Essence += 1000;
    }



    private const int BaseGoldDrop = 1;
    private const int BaseEssenceChange = 5;
    private const float BaseEnemyHP = 10;


    private const int BaseTowerCost = 10;
    private const int TowerCostIncrease = 10;


    public int FireTowerCost
    {
        get { return BaseTowerCost 
                + TowerCostIncrease * NumberOfFireTowers
                + TowerCostIncrease/4 * (NumberOfAllTowers-NumberOfFireTowers)
                ; 

        }
    }

    public int WaterTowerCost
    {
        get
        {
            return BaseTowerCost
              + TowerCostIncrease * NumberOfWaterTowers
              + TowerCostIncrease / 4 * (NumberOfAllTowers - NumberOfWaterTowers)
              ;

        }
    }

    public int EarthTowerCost
    {
        get
        {
            return BaseTowerCost
              + TowerCostIncrease * NumberOfEarthTowers
              + TowerCostIncrease / 4 * (NumberOfAllTowers - NumberOfEarthTowers)
              ;

        }
    }

    public int AirTowerCost
    {
        get
        {
            return BaseTowerCost
              + TowerCostIncrease * NumberOfAirTowers
              + TowerCostIncrease / 4 * (NumberOfAllTowers - NumberOfAirTowers)
              ;

        }
    }



    private const int BaseUpdateCost = 2;

    public int FireTowerUpdateCost(int level)
    {
        return (int)(BaseUpdateCost +  Mathf.Pow(level,1.2f) * BaseUpdateCost);
    }

    public int WaterTowerUpdateCost(int level)
    {
        return (int)(BaseUpdateCost + Mathf.Pow(level, 1.2f) * BaseUpdateCost);
    }

    public int EarthTowerUpdateCost(int level)
    {
        return (int)(BaseUpdateCost + Mathf.Pow(level, 1.2f) * BaseUpdateCost);
    }

    public int AirTowerUpdateCost(int level)
    {
        return (int)(BaseUpdateCost + Mathf.Pow(level, 1.2f) * BaseUpdateCost);
    }



    public int FireTowerSellPrice(int level)
    {
        return 1+(level * (level+1))/10 * BaseUpdateCost;
    }

    public int WaterTowerSellPrice(int level)
    {
        return 1+(level * (level + 1)) /10 * BaseUpdateCost;
    }

    public int EarthTowerSellPrice(int level)
    {
        return 1+(level * (level + 1)) / 10 * BaseUpdateCost;
    }

    public int AirTowerSellPrice(int level)
    {
        return 1+(level * (level + 1)) / 10 * BaseUpdateCost;
    }



    public int NumberOfFireTowers
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("numOfFireTowers", value);
        }
        get
        {
            return PlayerPrefs.GetInt("numOfFireTowers", 0);
        }
    }

    public int NumberOfWaterTowers
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("numOfWaterTowers", value);
        }
        get
        {
            return PlayerPrefs.GetInt("numOfWaterTowers", 0);
        }
    }

    public int NumberOfEarthTowers
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("numOfEarthTowers", value);
        }
        get
        {
            return PlayerPrefs.GetInt("numOfEarthTowers", 0);
        }
    }

    public int NumberOfAirTowers
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("numOfAirTowers", value);
        }
        get
        {
            return PlayerPrefs.GetInt("numOfAirTowers", 0);
        }
    }

    public int NumberOfAllTowers
    {
        get { return NumberOfFireTowers + NumberOfWaterTowers + NumberOfEarthTowers + NumberOfAirTowers; }
    }


    #region Resources

    public int Gold
    {
        set
        {
            PlayerPrefs.SetInt("gold", value);
        }
        get
        {
            return PlayerPrefs.GetInt("gold", 100);
        }
    }

    public int Essence
    {
        set
        {
            PlayerPrefs.SetInt("essence", value);
        }
        get
        {
            return PlayerPrefs.GetInt("essence", 10);
        }
    }

    public int RebornPoint
    {
        set
        {
            PlayerPrefs.SetInt("rebornPoint", value);
        }
        get
        {
            return PlayerPrefs.GetInt("rebornPoint", 0);
        }
    }

    #endregion


    #region Elements

    

    private const float FireEffectIncrease = 12f;
    private const float WaterEffectIncrease = 1f;
    private const float EarthEffectIncrease = 0.5f;
    private const float AirEffectIncrease = 1f;


    private const float FireEffectBase = 5f;
    private const float WaterEffectBase = 2f;
    private const float EarthEffectBase = 1f;
    private const float AirEffectBase = 6f;


    public const float AirEffectRange = 50f;

    private const int EssenceIncreaseForElements = 3;
    public int ElementCost(int level)
    {
        return level * EssenceIncreaseForElements;
    }

    public int FireLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("fireLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("fireLvl", 1);
        }
    }

    public int WaterLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("waterLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("waterLvl", 1);
        }
    }

    public int EarthLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("earthLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("earthLvl", 1);
        }
    }

    public int AirLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("airLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("airLvl", 1);
        }
    }


    public float FireEffect
    {
        get
        {
            return (100f + FireEffectBase + FireLevel * FireEffectIncrease) / 100f;
        }
    }

    public float WaterEffect
    {
        get
        {
            return (WaterEffectBase + WaterLevel * WaterEffectIncrease) / 100f;
        }
    }

    public float EarthEffect
    {
        get
        {
            return (EarthEffectBase + EarthLevel * EarthEffectIncrease) / 100f;
        }
    }


    public float AirEffect
    {
        get
        {
            return (AirEffectBase + AirLevel * AirEffectIncrease)/100f;
        }
    }



    #endregion


    #region Research

    public const float DamageIncrease = 7;
    public const float AttackSpeedIncrease = 1f;
    public const float CriticalHitChangeIncrease = 0.5f;
    public const float CriticalHitDamageIncrease = 1;
    public const float RangeIncrease = 0.5f;


    private const int EssenceIncreaseForResearch = 2;
    public int ResearchCost(int level)
    {
        return level * EssenceIncreaseForResearch;
    }

    public int DamageLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("damageLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("damageLvl", 1);
        }
    }

    public int AttackSpeedLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("attackSpeedLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("attackSpeedLvl", 1);
        }
    }

    public int CriticalHitChangeLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("criticalHitChangeLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("criticalHitChangeLvl", 1);
        }
    }

    public int CriticalHitDamageLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("criticalHitDamageLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("criticalHitDamageLvl", 1);
        }
    }

    public int RangeLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("rangeLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("rangeLvl", 1);
        }
    }


    public float Damage
    {
        get
        {
            return (100f + DamageLevel * DamageIncrease)/100f;
        }
    }

    public float AttackSpeed
    {
        get
        {
            return (100f + AttackSpeedLevel * AttackSpeedIncrease) / 100f;
        }
    }

    public float CriticalHitChange
    {
        get
        {
            return (100f + CriticalHitChangeLevel * CriticalHitChangeIncrease) / 100f;
        }
    }

    public float CriticalHitDamage
    {
        get
        {
            return (100f + CriticalHitDamageLevel * CriticalHitDamageIncrease) / 100f;
        }
    }

    public float Range
    {
        get
        {
            return (100f + RangeLevel * RangeIncrease) / 100f;
        }
    }



    #endregion


    #region Reborn

    private const int GoldDropIncrease = 1;
    private const int EssenceChangeIncrease = 1;
    private const float EnemyHpDecrease = 1;

    private const int RebornPointIncrease= 1;
    public int RebornPointCost(int level)
    {
        return level * RebornPointIncrease;
    }

    public int EssenceChangeLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("essenceChangeLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("essenceChangeLvl", 1);
        }

    }

    public int EnemyHPLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("enemyHPLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("enemyHPLvl", 1);
        }

    }

    public int GoldDropLevel
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("goldDropLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("goldDropLvl", 1);
        }

    }





    #endregion



    public int GoldDrop
    {
        get
        {
            return BaseGoldDrop + (GoldDropLevel-1) * GoldDropIncrease;
        }
    }


    public int EssenceChange
    {
        get
        {
            return BaseEssenceChange + EssenceChangeLevel * EssenceChangeIncrease;
        }
    }


    public float EnemyHP
    {
        get
        {
            return (BaseEnemyHP + Wave/20f) * EnemyHPMultiplier / (EnemyHPLevel*EnemyHpDecrease);
        }
    }




    public int Wave
    {
        set
        {
            Tower.UpdateTowerValues();
            PlayerPrefs.SetInt("wave", value);
        }
        get
        {
            return PlayerPrefs.GetInt("wave", 1);
        }
    }

    public int EnemyCount
    {
        get
        {
            return 5 +(Wave/20)+ ((Wave%20)*1);
        }

    }

    public float EnemyHPMultiplier
    {
        get
        {
            return ((1f + Wave * 0.04f) * ( 1f + Wave * 0.005f )) ;
        }
    }






    public static Local Instance { get => instance; set => instance = value; }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}

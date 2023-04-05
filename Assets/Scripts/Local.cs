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
        Application.targetFrameRate = 300;
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

    [ContextMenu("Add RP")]
    public void AddRP()
    {
        RebornPoint += 1000;
    }

    #region TowerFeatureData
    private TowerFeatureData _baseFireTowerFeatureData = new TowerFeatureData(
        range:100f,
        attackPower:5f,
        attackPerSecond:1f,
        criticalChange:10f,
        criticalDamage:1f
        );
    private TowerFeatureData _baseWaterTowerFeatureData = new TowerFeatureData(
        range: 100f,
        attackPower: 5f,
        attackPerSecond: 1f,
        criticalChange: 10f,
        criticalDamage: 1f
        );
    private TowerFeatureData _baseEarthTowerFeatureData = new TowerFeatureData(
        range: 100f,
        attackPower: 5f,
        attackPerSecond: 1f,
        criticalChange: 10f,
        criticalDamage: 1f
        );
    private TowerFeatureData _baseAirTowerFeatureData = new TowerFeatureData(
        range: 100f,
        attackPower: 5f,
        attackPerSecond: 1f,
        criticalChange: 10f,
        criticalDamage: 1f
        );

    public TowerFeatureData GetBaseFeatureData(Tower tower)
    {
        if(tower.TowerType==TowerType.fire)return _baseFireTowerFeatureData;
        else if(tower.TowerType==TowerType.water)return _baseWaterTowerFeatureData;
        else if(tower.TowerType==TowerType.earth)return _baseEarthTowerFeatureData;
        else if(tower.TowerType==TowerType.air)return _baseAirTowerFeatureData;
        else return null;
    }


    private TowerFeatureData _increaseFireTowerFeatureData = new TowerFeatureData(
        range: 0.7f,
        attackPower: 0.5f,
        attackPerSecond: 0.01f,
        criticalChange: 0f,
        criticalDamage: 0f
        );
    private TowerFeatureData _increaseWaterTowerFeatureData = new TowerFeatureData(
        range: 0.7f,
        attackPower: 0.5f,
        attackPerSecond: 0.01f,
        criticalChange: 0f,
        criticalDamage: 0f
        );
    private TowerFeatureData _increaseEarthTowerFeatureData = new TowerFeatureData(
        range: 0.7f,
        attackPower: 0.5f,
        attackPerSecond: 0.01f,
        criticalChange: 0f,
        criticalDamage: 0f
        );
    private TowerFeatureData _increaseAirTowerFeatureData = new TowerFeatureData(
        range: 0.7f,
        attackPower: 0.5f,
        attackPerSecond: 0.01f,
        criticalChange: 0f,
        criticalDamage: 0f
        );

    public TowerFeatureData GetIncreaseFeatureData(Tower tower)
    {
        if (tower.TowerType == TowerType.fire) return _increaseFireTowerFeatureData;
        else if (tower.TowerType == TowerType.water) return _increaseWaterTowerFeatureData;
        else if (tower.TowerType == TowerType.earth) return _increaseEarthTowerFeatureData;
        else if (tower.TowerType == TowerType.air) return _increaseAirTowerFeatureData;
        else return null;
    }

    #endregion


    private const int BaseGoldDrop = 1;
    private const float BaseEssenceChange = 0.05f;
    private const float BaseEnemyHP = 10;
    private const float BaseGameSpeed = 1f;


    private const int BaseTowerCost = 10;
    private const int TowerCostIncrease = 10;


    public int FireTowerCost
    {
        get { return (int)(BaseTowerCost 
                + TowerCostIncrease * Mathf.Pow(NumberOfFireTowers,2f) 
                + 2f * TowerCostIncrease * Mathf.Pow(NumberOfFireTowers,(NumberOfFireTowers/4f))
                + TowerCostIncrease/4 * (NumberOfAllTowers-NumberOfFireTowers));
                ; 

        }
    }

    public int WaterTowerCost
    {
        get
        {
            return (int)(BaseTowerCost
                + TowerCostIncrease * Mathf.Pow(NumberOfWaterTowers, 2f)
                + 2f * TowerCostIncrease * Mathf.Pow(NumberOfWaterTowers, (NumberOfWaterTowers / 4f))
              + TowerCostIncrease / 4 * (NumberOfAllTowers - NumberOfWaterTowers))
              ;

        }
    }

    public int EarthTowerCost
    {
        get
        {
            return (int)(BaseTowerCost
                + TowerCostIncrease * Mathf.Pow(NumberOfEarthTowers, 2f)
                + 2f * TowerCostIncrease * Mathf.Pow(NumberOfEarthTowers, (NumberOfEarthTowers / 4f))
              + TowerCostIncrease / 4 * (NumberOfAllTowers - NumberOfEarthTowers))
              ;

        }
    }

    public int AirTowerCost
    {
        get
        {
            return (int)(BaseTowerCost
                + TowerCostIncrease * Mathf.Pow(NumberOfAirTowers, 2f)
                + 2f * TowerCostIncrease * Mathf.Pow(NumberOfAirTowers, (NumberOfAirTowers / 4f))
              + TowerCostIncrease / 4 * (NumberOfAllTowers - NumberOfAirTowers))
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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

    public const int MaxElemetsLevel = 50;

    private const float FireEffectIncrease = 0.0425f;
    private const float WaterEffectIncrease = 0.011f;
    private const float EarthEffectIncrease = 0.010f;
    private const float AirEffectIncrease = 0.035f;


    private const float FireEffectBase = 0.10f;
    private const float WaterEffectBase = 0.25f;
    private const float EarthEffectBase = 0.10f;
    private const float AirEffectBase = 0.25f;


    public const float AirEffectRange = 50f;

    public const float WaterEffectTime = 1f;
    public const float EarthEffectTime = 1f;

    private const int EssenceIncreaseForElements = 3;
    public int ElementCost(int level)
    {
        return level * EssenceIncreaseForElements;
    }

    public int FireLevel
    {
        set
        {
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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
            return (1f + FireEffectBase + (FireLevel * FireEffectIncrease));
        }
    }

    public float WaterEffect
    {
        get
        {
            return (WaterEffectBase + (WaterLevel * WaterEffectIncrease));
        }
    }

    public float EarthEffect
    {
        get
        {
            return (EarthEffectBase + (EarthLevel * EarthEffectIncrease));
        }
    }


    public float AirEffect
    {
        get
        {
            return (AirEffectBase + (AirLevel * AirEffectIncrease));
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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
            Tower.UpdateAllTowersFeatureData();
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

    public const int MinWavetoReborn = 50;

    public const int MaxEssenceChangeLevel = 50;
    public const int MaxGameSpeedLevel = 10;
    public const int MaxEnemyHpDecreaseLvl = 50;

    private const int GoldDropIncrease = 1;
    private const float EssenceChangeIncrease = 0.0125f;
    private const float GameSpeedIncrease = 0.333333333f;
    
    private const float EnemyHpDecrease = 0.015f;


    private const float RebornPointIncrease = 1.2f;
    public int RebornPointCost(int level)
    {
        return (int)(Mathf.Pow(level,1.5f) * RebornPointIncrease);
    }

    public int EssenceChangeLevel
    {
        set
        {
            Tower.UpdateAllTowersFeatureData();
            PlayerPrefs.SetInt("essenceChangeLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("essenceChangeLvl", 1);
        }

    }

    public int EnemyHPDecreaseLevel
    {
        set
        {
            Tower.UpdateAllTowersFeatureData();
            PlayerPrefs.SetInt("enemyHPDecreaseLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("enemyHPDecreaseLvl", 1);
        }

    }

    public int GoldDropLevel
    {
        set
        {
            Tower.UpdateAllTowersFeatureData();
            PlayerPrefs.SetInt("goldDropLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("goldDropLvl", 1);
        }

    }

    public int GameSpeedLevel
    {
        set
        {
            Tower.UpdateAllTowersFeatureData();
            PlayerPrefs.SetInt("gameSpeedLvl", value);
        }
        get
        {
            return PlayerPrefs.GetInt("gameSpeedLvl", 1);
        }
    }


    public float EnemyHPDecreasePercent
    {
        get { return (1-(EnemyHpDecrease * (EnemyHPDecreaseLevel - 1))); }
    }


    public int CanEarnRP
    {
        get { return (int)((Wave / 25f) + ( Mathf.Pow(Wave, 0.65f) * ((Wave - 45) / 100f))); }
    }

    #endregion



    public int GoldDrop
    {
        get
        {
            return BaseGoldDrop + (GoldDropLevel-1) * GoldDropIncrease;
        }
    }


    public float EssenceChange
    {
        get
        {
            return BaseEssenceChange + (EssenceChangeLevel-1) * EssenceChangeIncrease;
        }
    }


    public float EnemyHP
    {
        get
        {
            return (BaseEnemyHP + Wave/5f) * EnemyHPMultiplier
                * EnemyHPDecreasePercent;
        }
    }

    public float GameSpeed
    {
        get
        {
            return (BaseGameSpeed + (GameSpeedLevel-1) * GameSpeedIncrease);
        }
    }




    public int Wave
    {
        set
        {
            Tower.UpdateAllTowersFeatureData();
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

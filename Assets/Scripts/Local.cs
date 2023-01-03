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


    private const int BaseGoldDrop = 1;
    private const int BaseEssenceChange = 3;
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



    private const int BaseUpdateCost = 10;

    public int FireTowerUpdateCost(int level)
    {
        return BaseUpdateCost + level * BaseUpdateCost;
    }

    public int WaterTowerUpdateCost(int level)
    {
        return BaseUpdateCost + level * BaseUpdateCost;
    }

    public int EarthTowerUpdateCost(int level)
    {
        return BaseUpdateCost + level * BaseUpdateCost;
    }

    public int AirTowerUpdateCost(int level)
    {
        return BaseUpdateCost + level * BaseUpdateCost;
    }



    public int FireTowerSellPrice(int level)
    {
        return 5+(level * (level+1))/5 * BaseUpdateCost;
    }

    public int WaterTowerSellPrice(int level)
    {
        return 5+(level * (level + 1)) / 5 * BaseUpdateCost;
    }

    public int EarthTowerSellPrice(int level)
    {
        return 5+(level * (level + 1)) / 5 * BaseUpdateCost;
    }

    public int AirTowerSellPrice(int level)
    {
        return 5+(level * (level + 1)) / 5 * BaseUpdateCost;
    }



    public int NumberOfFireTowers
    {
        set
        {
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

    

    private const float FireEffectIncrease = 10;
    private const float WaterEffectIncrease = 1;
    private const float EarthEffectIncrease = 0.5f;
    private const float AirEffectIncrease = 1;

    private const int EssenceIncreaseForElements = 10;
    public int ElementCost(int level)
    {
        return level * EssenceIncreaseForElements;
    }

    public int FireLevel
    {
        set
        {
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
            return FireLevel * FireEffectIncrease;
        }
    }

    public float WaterEffect
    {
        get
        {
            return WaterLevel * WaterEffectIncrease;
        }
    }

    public float EarthEffect
    {
        get
        {
            return EarthLevel * EarthEffectIncrease;
        }
    }


    public float AirEffect
    {
        get
        {
            return AirLevel * AirEffectIncrease;
        }
    }

    #endregion


    #region Research

    public const float DamageIncrease = 10;
    public const float AttackSpeedIncrease = 1;
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
            return BaseGoldDrop + GoldDropLevel * GoldDropIncrease;
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
            return BaseEnemyHP * EnemyHPMultiplier / (EnemyHPLevel*EnemyHpDecrease);
        }
    }




    public int Wave
    {
        set
        {
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
            return 5 + ((Wave%10)*1);
        }

    }

    public float EnemyHPMultiplier
    {
        get
        {
            return 1 + ((Wave / 5)) * 0.5f;
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

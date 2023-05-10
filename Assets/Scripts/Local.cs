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
        Gold += 10000;
    }
    [ContextMenu("Add Essence")]
    public void AddEssence()
    {
        Essence += 10000;
    }

    [ContextMenu("Add RP")]
    public void AddRP()
    {
        RebornPoint += 10000;
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
        if(tower.TowerType==TowerType.Fire)return _baseFireTowerFeatureData;
        else if(tower.TowerType==TowerType.Water)return _baseWaterTowerFeatureData;
        else if(tower.TowerType==TowerType.Earth)return _baseEarthTowerFeatureData;
        else if(tower.TowerType==TowerType.Air)return _baseAirTowerFeatureData;
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
        if (tower.TowerType == TowerType.Fire) return _increaseFireTowerFeatureData;
        else if (tower.TowerType == TowerType.Water) return _increaseWaterTowerFeatureData;
        else if (tower.TowerType == TowerType.Earth) return _increaseEarthTowerFeatureData;
        else if (tower.TowerType == TowerType.Air) return _increaseAirTowerFeatureData;
        else return null;
    }

    #endregion


    private const int Base_GoldDrop = 1;
    private const float Base_EssenceChange = 0.05f;
    private const float Base_EnemyHP = 10;
    private const float Base_GameSpeed = 1f;

    private const int Base_TowerUpdateCost = 2;
    private const int Base_TowerCost = 10;
    private const int Increase_TowerCost = 10;
    private const int Max_Towers = 12;



    public int NewTowerCost(TowerType type)
    {
        return (int)(
              Increase_TowerCost * Mathf.Pow(NumberOfTowerType(type), 3.6f)
            + Increase_TowerCost * NumberOfAllTowers
            + Base_TowerCost)
            ;
    }

    public int TowerUpdateCost(int level)
    {
        return (int)(Base_TowerUpdateCost + Mathf.Pow(level, 1.2f) * Base_TowerUpdateCost);
    }

    public int TowerSellPrice(int level)
    {
        return 1 + (level * (level + 1)) / 10 * Base_TowerUpdateCost;
    }

    public int NumberOfTowerType(TowerType type)
    {
        if (type == TowerType.Fire) return NumberOfFireTowers;
        if (type == TowerType.Water) return NumberOfWaterTowers;
        if (type == TowerType.Earth) return NumberOfEarthTowers;
        if (type == TowerType.Air) return NumberOfAirTowers;
        Debug.Log("HATA  : " + type);
        return 0;
    }

    public void IncreaseNumberOfTower(TowerType type)
    {
        if(type == TowerType.Fire) NumberOfFireTowers++;
        if(type == TowerType.Water) NumberOfWaterTowers++;
        if(type == TowerType.Earth) NumberOfEarthTowers++;
        if(type == TowerType.Air) NumberOfAirTowers++;
    }

    public void DecreaseNumberOfTower(TowerType type)
    {
        if (type == TowerType.Fire) NumberOfFireTowers--;
        if (type == TowerType.Water) NumberOfWaterTowers--;
        if (type == TowerType.Earth) NumberOfEarthTowers--;
        if (type == TowerType.Air) NumberOfAirTowers--;
    }

    private int NumberOfFireTowers
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

    private int NumberOfWaterTowers
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

    private int NumberOfEarthTowers
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

    private int NumberOfAirTowers
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

    private int NumberOfAllTowers
    {
        get { return NumberOfFireTowers + NumberOfWaterTowers + NumberOfEarthTowers + NumberOfAirTowers; }
    }


    #region Resources

    public float Gold
    {
        set
        {
            PlayerPrefs.SetFloat("gold", value);
        }
        get
        {
            return PlayerPrefs.GetFloat("gold", 100f);
        }
    }

    public float Essence
    {
        set
        {
            PlayerPrefs.SetFloat("essence", value);
        }
        get
        {
            return PlayerPrefs.GetFloat("essence", 10);
        }
    }

    public float RebornPoint
    {
        set
        {
            PlayerPrefs.SetFloat("rebornPoint", value);
        }
        get
        {
            return PlayerPrefs.GetFloat("rebornPoint", 0);
        }
    }

    #endregion


    #region Elements

    public const int MaxElemetsLevel = 50;


    private readonly Dictionary<Element, float> Increase_ElementEffect = new Dictionary<Element, float>()
    {
        { Element.Fire , 0.0425f },
        { Element.Water , 0.011f },
        { Element.Earth , 0.010f },
        { Element.Air , 0.035f },
    };

    private readonly Dictionary<Element, float> Base_ElementEffect = new Dictionary<Element, float>() 
    {
        { Element.Fire , 1.10f },
        { Element.Water , 0.25f },
        { Element.Earth , 0.10f },
        { Element.Air , 0.25f },
    };

    public const float AirEffectRange = 35f;

    public const float WaterEffectDuration = 1f;
    public const float EarthEffectDuration = 1f;


    public int ElementCost(int level)
    {
        if (level >= MaxElemetsLevel) return -1;
        return (int)(
                Mathf.Log(level, (5f / level) + 1) * 15 + 5 + Mathf.Pow(1.175f, level)
            )
            ;
    }


    public int ElementLevel(Element element)
    {
        if (element == Element.Fire) return FireLevel;
        if (element == Element.Water) return WaterLevel;
        if (element == Element.Earth) return EarthLevel;
        if (element == Element.Air) return AirLevel;
        return 0;
    }

    public void IncreaseElementLevel(Element element)
    {
        if (ElementLevel(element) >= MaxElemetsLevel) return;
        if (element == Element.Fire) FireLevel++;
        else if (element == Element.Water)  WaterLevel++;
        else if (element == Element.Earth)  EarthLevel++;
        else if (element == Element.Air)  AirLevel++;
    }

    public void SetElementLevel(Element element, int level)
    {
        if (element == Element.Fire) FireLevel=level;
        else if (element == Element.Water) WaterLevel=level;
        else if (element == Element.Earth) EarthLevel=level;
        else if (element == Element.Air) AirLevel=level;
    }
    public void SetElementsLevels(Element[] elements
        ,int[] levels)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            SetElementLevel(elements[i], levels[i]);
        }
    }

    private int FireLevel
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

    private int WaterLevel
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

    private int EarthLevel
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

    private int AirLevel
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


    public float ElementEffect(Element element)
    {
        return Base_ElementEffect[element] + (ElementLevel(element) * Increase_ElementEffect[element]);
    }


    #endregion


    #region Research

    private readonly Dictionary<Research, float> Increase_ResearchEffect = new Dictionary<Research, float>()
    {
        { Research.Damage , 3f },
        { Research.AttackSpeed , 0.5f },
        { Research.CriticalHitChange , 60f },
        { Research.CriticalHitDamage , 0.5f },
        { Research.Range , 0.25f }
    };


    public int ResearchCost(int level)
    {
        return (int)(
                Mathf.Log(level, (50f / level) + 1) * 5 + 4 + Mathf.Pow(1.05f, level)
            );
    }

    public int ResearchCost(Research research)
    {
        return ResearchCost(ResearchLevel(research));
    }

    public int ResearchLevel(Research research)
    {
        if (research == Research.Damage) return DamageLevel;
        if (research == Research.AttackSpeed) return AttackSpeedLevel;
        if (research == Research.CriticalHitChange) return CriticalHitChangeLevel;
        if (research == Research.CriticalHitDamage) return CriticalHitDamageLevel;
        if (research == Research.Range) return RangeLevel;
        return 0;
    }

    public void IncreaseResearchLevel(Research research)
    {
        if (research == Research.Damage) DamageLevel++;
        else if (research == Research.AttackSpeed) AttackSpeedLevel++;
        else if (research == Research.CriticalHitChange) CriticalHitChangeLevel++;
        else if (research == Research.CriticalHitDamage) CriticalHitDamageLevel++;
        else if (research == Research.Range) RangeLevel++;
    }

    public void SetResearchLevel(Research research, int level)
    {
        if (research == Research.Damage) DamageLevel = level;
        else if (research == Research.AttackSpeed) AttackSpeedLevel = level;
        else if (research == Research.CriticalHitChange) CriticalHitChangeLevel = level;
        else if (research == Research.CriticalHitChange) CriticalHitDamageLevel = level;
        else if (research == Research.Range) RangeLevel = level;
    }
    public void SetResearchsLevels(Research[] researches
        , int[] levels)
    {
        for (int i = 0; i < researches.Length; i++)
        {
            SetResearchLevel(researches[i], levels[i]);
        }
    }

    private int DamageLevel
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

    private int AttackSpeedLevel
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

    private int CriticalHitChangeLevel
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

    private int CriticalHitDamageLevel
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

    private int RangeLevel
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


    public float ResearchEffect(Research research)
    {
        return (100f + ResearchLevel(research) * Increase_ResearchEffect[research]) / 100f;
    }


    #endregion


    #region Reborn

    public const int MinWavetoReborn = 50;

    public const int MaxEssenceChangeLevel = 50;
    public const int MaxGameSpeedLevel = 10;
    public const int MaxEnemyHpDecreaseLvl = 50;

    private const int Increase_GoldDrop = 1;
    private const float Increase_EssenceChange = 0.0125f;
    private const float Increase_GameSpeed = 0.333333333f;
    
    private const float Decrease_EnemyHp = 0.015f;


    private const float Increase_RebornPoint = 1.2f;
    public int RebornPointCost(int level)
    {
        return (int)(Mathf.Pow(level,1.5f) * Increase_RebornPoint);
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
        get { return (1-(Decrease_EnemyHp * (EnemyHPDecreaseLevel - 1))); }
    }


    public int CanEarnRP
    {
        get { return (int)(Mathf.Log(Wave, (6f / Wave) + 1) - 30); }
    }

    #endregion



    public int GoldDrop
    {
        get
        {
            return Base_GoldDrop + (GoldDropLevel-1) * Increase_GoldDrop;
        }
    }


    public float EssenceChange
    {
        get
        {
            return Base_EssenceChange + (EssenceChangeLevel-1) * Increase_EssenceChange;
        }
    }


    public float EnemyHP
    {
        get
        {
            return Base_EnemyHP + Base_EnemyHP * EnemyHPMultiplier
                * EnemyHPDecreasePercent;
        }
    }

    public float GameSpeed
    {
        get
        {
            return (Base_GameSpeed + (GameSpeedLevel-1) * Increase_GameSpeed);
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
            return (int)(5 + Mathf.Log(Wave, (128f / Wave) + 1));
        }

    }

    public float EnemyHPMultiplier
    {
        get
        {
            return (Mathf.Log(Wave, (32f / Wave) + 1));
        }
    }



    #region PassiveIncome

    public const float IncomeTime = 5f;

    private const int Base_IncomeUpdateCost = 5;

    private const float Base_PassiveGold = 1f;
    private const float Base_PassiveEssence = 1f;
    private const float Base_PassiveRP = 0f;

    private const float PassiveGoldByLevel = 1.6666f;
    private const float PassiveEssenceByLevel = 0.25f;
    private const float PassiveRPByLevel = 1f/12;



    public int PassiveIncomeLevel(PassiveIncome passiveIncome)
    {
        if (passiveIncome == PassiveIncome.Gold) return PassiveGoldLevel;
        if (passiveIncome == PassiveIncome.Essence) return PassiveEssenceLevel;
        if (passiveIncome == PassiveIncome.RP) return PassiveRPLevel;
        return 0;
    }

    private int PassiveGoldLevel
    {
        set
        {
            PlayerPrefs.SetInt("passiveGoldLevel", value);
        }
        get
        {
            return PlayerPrefs.GetInt("passiveGoldLevel", 1);
        }
    }

    public int PassiveEssenceLevel
    {
        set
        {
            PlayerPrefs.SetInt("passiveEssenceLevel", value);
        }
        get
        {
            return PlayerPrefs.GetInt("passiveEssenceLevel", 1);
        }
    }

    public int PassiveRPLevel
    {
        set
        {
            PlayerPrefs.SetInt("passiveRPLevel", value);
        }
        get
        {
            return PlayerPrefs.GetInt("passiveRPLevel", 1);
        }
    }


    public void SetIncomeLevel(PassiveIncome income, int level)
    {
        if (income == PassiveIncome.Gold) PassiveGoldLevel = level;
        else if (income == PassiveIncome.Essence) PassiveEssenceLevel = level;
        else if (income == PassiveIncome.RP) PassiveRPLevel = level;
    }
    public void SetIncomesLevels(PassiveIncome[] incomes
        , int[] levels)
    {
        for (int i = 0; i < incomes.Length; i++)
        {
            SetIncomeLevel(incomes[i], levels[i]);
        }
    }


    public float PassiveIncomeAmount(PassiveIncome passiveIncome)
    {
        if(passiveIncome == PassiveIncome.Gold) { return PassiveGoldIncome; }
        if(passiveIncome == PassiveIncome.Essence) { return PassiveEssenceIncome; }
        if(passiveIncome == PassiveIncome.RP) { return PassiveRPIncome; }
        return 0;
    }

    private float PassiveGoldIncome
    {
        get
        {
            return Base_PassiveGold + (PassiveGoldLevel-1) * PassiveGoldByLevel;
        }
    }

    private float PassiveEssenceIncome
    {
        get
        {
            return Base_PassiveEssence + (PassiveEssenceLevel - 1) * PassiveEssenceByLevel;
        }
    }

    private float PassiveRPIncome
    {
        get
        {
            return Base_PassiveRP + (PassiveRPLevel - 1) * PassiveRPByLevel;
        }
    }


    public int PassiveIncomeUpdateCost(int level)
    {
        return (int)(Mathf.Pow(level, 1.2f) * Base_IncomeUpdateCost) -2;
    }

    public int PassiveIncomeUpdateCost(PassiveIncome passiveIncome)
    {
        return PassiveIncomeUpdateCost(PassiveIncomeLevel(passiveIncome));
    }

    public void IncreaseIncomeLevel(PassiveIncome passiveIncome)
    {
        if (passiveIncome == PassiveIncome.Gold) { PassiveGoldLevel++; }
        if (passiveIncome == PassiveIncome.Essence) { PassiveEssenceLevel++; }
        if (passiveIncome == PassiveIncome.RP) { PassiveRPLevel++; }
    }

    #endregion

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

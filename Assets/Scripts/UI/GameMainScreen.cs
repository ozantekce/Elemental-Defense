using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ScreenManagerNS;
using Screen = ScreenManagerNS.Screen;

public class GameMainScreen : Screen
{


    private void Start()
    {


        // Screen Texts
        "WaveInfo".SetTextMethod("Wave",()=>Local.Instance.Wave);
        "EnemyCountInfo".SetTextMethod("EnemyCount", ()=>Local.Instance.EnemyCount);
        "EnemyHPInfo".SetTextMethod("EnemyHP", () => ((int)Local.Instance.EnemyHP));

        "GoldInfo".SetTextMethod("Gold", () => (int)Local.Instance.Gold);
        "EssenceInfo".SetTextMethod("Essence", () => (int)Local.Instance.Essence);
        "RebornInfo".SetTextMethod("RebornPoint", () => (int)Local.Instance.RebornPoint);
        // End


        // ElementsPopUp Texts

        //Titles
        "FireTitleText".SetTextMethod("FireLevel", () => Local.Instance.ElementLevel(Element.Fire));
        "WaterTitleText".SetTextMethod("WaterLevel", () => Local.Instance.ElementLevel(Element.Water));
        "EarthTitleText".SetTextMethod("EarthLevel", () => Local.Instance.ElementLevel(Element.Earth));
        "AirTitleText".SetTextMethod("AirLevel", () => Local.Instance.ElementLevel(Element.Air));
        //


        // Element Info
        "FireInfoText".SetTextMethod("FireInfo", () => Local.Instance.ElementEffect(Element.Fire) * 100);
        "WaterInfoText".SetTextMethod("WaterInfo", () => Local.Instance.ElementEffect(Element.Water) * 100);
        "EarthInfoText".SetTextMethod("EarthInfo", () => Local.Instance.ElementEffect(Element.Earth) * 100);
        "AirInfoText".SetTextMethod("AirInfo", () => Local.Instance.ElementEffect(Element.Air) * 100);
        //

        // Buttons
        "FireButtonText".SetTextMethod("Essence2", () => Local.Instance.ElementCost(Local.Instance.ElementLevel(Element.Fire)));
        "WaterButtonText".SetTextMethod("Essence2", () => Local.Instance.ElementCost(Local.Instance.ElementLevel(Element.Water)));
        "EarthButtonText".SetTextMethod("Essence2", () => Local.Instance.ElementCost(Local.Instance.ElementLevel(Element.Earth)));
        "AirButtonText".SetTextMethod("Essence2", () => Local.Instance.ElementCost(Local.Instance.ElementLevel(Element.Air)));
        //

        // End


        // ResearchPopUp

        // Titles
        "DamageTitleText".SetTextMethod("DamageLevel", () => Local.Instance.ResearchLevel(Research.Damage));
        "AttackSpeedTitleText".SetTextMethod("AttackSpeedLevel", () => Local.Instance.ResearchLevel(Research.AttackSpeed));
        "CriticalHitChanceTitleText".SetTextMethod("CriticalHitChanceLevel", () => Local.Instance.ResearchLevel(Research.CriticalHitChange));
        "CriticalHitDamageTitleText".SetTextMethod("CriticalHitDamageLevel", () => Local.Instance.ResearchLevel(Research.CriticalHitDamage));
        "RangeTitleText".SetTextMethod("RangeLevel", () => Local.Instance.ResearchLevel(Research.Range));
        //

        // Info
        "DamageInfoText".SetTextMethod("DamageInfo", () => Local.Instance.ResearchEffect(Research.Damage) * 100);
        "AttackSpeedInfoText".SetTextMethod("AttackSpeedInfo", () => Local.Instance.ResearchEffect(Research.AttackSpeed) * 100);
        "CriticalHitChanceInfoText".SetTextMethod("CriticalHitChanceInfo", () => Local.Instance.ResearchEffect(Research.CriticalHitChange));
        "CriticalHitDamageInfoText".SetTextMethod("CriticalHitDamageInfo", () => Local.Instance.ResearchEffect(Research.CriticalHitDamage) * 100);
        "RangeInfoText".SetTextMethod("RangeInfo", () => Local.Instance.ResearchEffect(Research.Range) * 100);
        //


        // Button
        "DamageButtonText".SetTextMethod("Essence2", () => Local.Instance.ResearchCost(Research.Damage));
        "AttackSpeedButtonText".SetTextMethod("Essence2", () => Local.Instance.ResearchCost(Research.AttackSpeed));
        "CriticalHitChanceButtonText".SetTextMethod("Essence2", () => Local.Instance.ResearchCost(Research.CriticalHitChange));
        "CriticalHitDamageButtonText".SetTextMethod("Essence2", () => Local.Instance.ResearchCost(Research.CriticalHitDamage));
        "RangeButtonText".SetTextMethod("Essence2", () => Local.Instance.ResearchCost(Research.Range));
        //

        // End



        // Base

        // Titles
        
        "PassiveGoldTitleText".SetTextMethod("GoldLevel", () => Local.Instance.PassiveIncomeLevel(PassiveIncome.Gold));
        "PassiveEssenceTitleText".SetTextMethod("EssenceLevel", () => Local.Instance.PassiveIncomeLevel(PassiveIncome.Essence));
        "PassiveRPTitleText".SetTextMethod("RPLevel", () => Local.Instance.PassiveIncomeLevel(PassiveIncome.RP));

        //

        // Info

        "PassiveGoldInfoText".SetTextMethod("GoldIncome", () => Local.Instance.PassiveIncomeAmount(PassiveIncome.Gold));
        "PassiveEssenceInfoText".SetTextMethod("EssenceIncome", () => Local.Instance.PassiveIncomeAmount(PassiveIncome.Essence));
        "PassiveRPInfoText".SetTextMethod("RPIncome", () => Local.Instance.PassiveIncomeAmount(PassiveIncome.RP));

        //

        // Button

        "PassiveGoldButtonText".SetTextMethod("Essence2", () => Local.Instance.PassiveIncomeUpdateCost(PassiveIncome.Gold));
        "PassiveEssenceButtonText".SetTextMethod("Essence2", () => Local.Instance.PassiveIncomeUpdateCost(PassiveIncome.Essence));
        "PassiveRPButtonText".SetTextMethod("Essence2", () => Local.Instance.PassiveIncomeUpdateCost(PassiveIncome.RP));

        //

        //End
        


        // Slot Empty
        "NewFireTowerButtonText".SetTextMethod("Gold2", () => Local.Instance.NewTowerCost(TowerType.Fire));
        "NewWaterTowerButtonText".SetTextMethod("Gold2", () => Local.Instance.NewTowerCost(TowerType.Water));
        "NewEarthTowerButtonText".SetTextMethod("Gold2", () => Local.Instance.NewTowerCost(TowerType.Earth));
        "NewAirTowerButtonText".SetTextMethod("Gold2", () => Local.Instance.NewTowerCost(TowerType.Air));
        //End

    }




}

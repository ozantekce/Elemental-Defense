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
        "FireTitleText".SetTextMethod("FireLevel", () => Element.Fire.ElementLevel());
        "WaterTitleText".SetTextMethod("WaterLevel", () => Element.Water.ElementLevel());
        "EarthTitleText".SetTextMethod("EarthLevel", () => Element.Earth.ElementLevel());
        "AirTitleText".SetTextMethod("AirLevel", () => Element.Air.ElementLevel());
        //


        // Element Info
        "FireInfoText".SetTextMethod("FireInfo", () => Element.Fire.ElementEffect()* 100);
        "WaterInfoText".SetTextMethod("WaterInfo", () => Element.Water.ElementEffect()* 100);
        "EarthInfoText".SetTextMethod("EarthInfo", () => Element.Earth.ElementEffect()* 100);
        "AirInfoText".SetTextMethod("AirInfo", () => Element.Air.ElementEffect() * 100);
        //

        // Buttons
        "FireButtonText".SetTextMethod("Essence2", () => Element.Fire.ElementCost());
        "WaterButtonText".SetTextMethod("Essence2", () => Element.Water.ElementCost());
        "EarthButtonText".SetTextMethod("Essence2", () => Element.Earth.ElementCost());
        "AirButtonText".SetTextMethod("Essence2", () => Element.Air.ElementCost());
        //

        // End


        // ResearchPopUp

        // Titles
        "DamageTitleText".SetTextMethod("DamageLevel", () => Research.Damage.ResearchLevel());
        "AttackSpeedTitleText".SetTextMethod("AttackSpeedLevel", () => Research.AttackSpeed.ResearchLevel());
        "CriticalHitChanceTitleText".SetTextMethod("CriticalHitChanceLevel", () => Research.CriticalHitChange.ResearchLevel());
        "CriticalHitDamageTitleText".SetTextMethod("CriticalHitDamageLevel", () => Research.CriticalHitDamage.ResearchLevel());
        "RangeTitleText".SetTextMethod("RangeLevel", () => Research.Range.ResearchLevel());
        //

        // Info
        "DamageInfoText".SetTextMethod("DamageInfo", () => Research.Damage.ResearchEffect() * 100);
        "AttackSpeedInfoText".SetTextMethod("AttackSpeedInfo", () => Research.AttackSpeed.ResearchEffect() * 100);
        "CriticalHitChanceInfoText".SetTextMethod("CriticalHitChanceInfo", () => Research.CriticalHitChange.ResearchEffect());
        "CriticalHitDamageInfoText".SetTextMethod("CriticalHitDamageInfo", () => Research.CriticalHitDamage.ResearchEffect() * 100);
        "RangeInfoText".SetTextMethod("RangeInfo", () => Research.Range.ResearchEffect() * 100);
        //


        // Button
        "DamageButtonText".SetTextMethod("Essence2", () => Research.Damage.ResearchCost());
        "AttackSpeedButtonText".SetTextMethod("Essence2", () => Research.AttackSpeed.ResearchCost());
        "CriticalHitChanceButtonText".SetTextMethod("Essence2", () => Research.CriticalHitChange.ResearchCost());
        "CriticalHitDamageButtonText".SetTextMethod("Essence2", () => Research.CriticalHitDamage.ResearchCost());
        "RangeButtonText".SetTextMethod("Essence2", () => Research.Range.ResearchCost());
        //

        // End



        // Base

        // Titles
        
        "PassiveGoldTitleText".SetTextMethod("GoldLevel", () => PassiveIncome.Gold.PassiveIncomeLevel());
        "PassiveEssenceTitleText".SetTextMethod("EssenceLevel", () => PassiveIncome.Essence.PassiveIncomeLevel());
        "PassiveRPTitleText".SetTextMethod("RPLevel", () => PassiveIncome.RP.PassiveIncomeLevel());

        //

        // Info

        "PassiveGoldInfoText".SetTextMethod("GoldIncome", () => PassiveIncome.Gold.PassiveIncomeAmount());
        "PassiveEssenceInfoText".SetTextMethod("EssenceIncome", () => PassiveIncome.Essence.PassiveIncomeAmount());
        "PassiveRPInfoText".SetTextMethod("RPIncome", () => PassiveIncome.RP.PassiveIncomeAmount());

        //

        // Button

        "PassiveGoldButtonText".SetTextMethod("Essence2", () => PassiveIncome.Gold.PassiveIncomeUpdateCost());
        "PassiveEssenceButtonText".SetTextMethod("Essence2", () => PassiveIncome.Essence.PassiveIncomeUpdateCost());
        "PassiveRPButtonText".SetTextMethod("Essence2", () => PassiveIncome.RP.PassiveIncomeUpdateCost());

        //

        //End
        


        // Slot Empty
        "NewFireTowerButtonText".SetTextMethod("Gold2", () => TowerType.Fire.NewTowerCost());
        "NewWaterTowerButtonText".SetTextMethod("Gold2", () => TowerType.Water.NewTowerCost());
        "NewEarthTowerButtonText".SetTextMethod("Gold2", () => TowerType.Earth.NewTowerCost());
        "NewAirTowerButtonText".SetTextMethod("Gold2", () => TowerType.Air.NewTowerCost());
        //End

    }




}

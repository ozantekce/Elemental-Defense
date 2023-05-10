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
        ExtendedText
            .SetTextMethod("WaveInfo", "Wave",()=>Local.Instance.Wave);
        ExtendedText
            .SetTextMethod("EnemyCountInfo", "EnemyCount", ()=>Local.Instance.EnemyCount);
        ExtendedText
            .SetTextMethod("EnemyHPInfo", "EnemyHP", () => ((int)Local.Instance.EnemyHP));

        ExtendedText
            .SetTextMethod("GoldInfo", "Gold", () => Local.Instance.Gold);
        ExtendedText
            .SetTextMethod("EssenceInfo", "Essence", () => Local.Instance.Essence);
        ExtendedText
            .SetTextMethod("RebornInfo", "RebornPoint", () => Local.Instance.RebornPoint);
        // End


        // ElementsPopUp Texts

        //Titles
        ExtendedText
            .SetTextMethod("FireTitleText","FireLevel", () => Local.Instance.ElementLevel(Element.Fire));
        ExtendedText
            .SetTextMethod("WaterTitleText", "WaterLevel", () => Local.Instance.ElementLevel(Element.Water));
        ExtendedText
            .SetTextMethod("EarthTitleText", "EarthLevel", () => Local.Instance.ElementLevel(Element.Earth));
        ExtendedText
            .SetTextMethod("AirTitleText", "AirLevel", () => Local.Instance.ElementLevel(Element.Air));
        //


        // Element Info
        ExtendedText
            .SetTextMethod("FireInfoText", "FireInfo", () => Local.Instance.ElementEffect(Element.Fire) * 100);
        ExtendedText
            .SetTextMethod("WaterInfoText", "WaterInfo", () => Local.Instance.ElementEffect(Element.Water) * 100);
        ExtendedText
            .SetTextMethod("EarthInfoText", "EarthInfo", () => Local.Instance.ElementEffect(Element.Earth) * 100);
        ExtendedText
            .SetTextMethod("AirInfoText", "AirInfo", () => Local.Instance.ElementEffect(Element.Air) * 100);
        //

        // Buttons
        ExtendedText
            .SetTextMethod("FireButtonText", "Essence2", () => Local.Instance.ElementCost(Local.Instance.ElementLevel(Element.Fire)));
        ExtendedText
            .SetTextMethod("WaterButtonText", "Essence2", () => Local.Instance.ElementCost(Local.Instance.ElementLevel(Element.Water)));
        ExtendedText
            .SetTextMethod("EarthButtonText", "Essence2", () => Local.Instance.ElementCost(Local.Instance.ElementLevel(Element.Earth)));
        ExtendedText
            .SetTextMethod("AirButtonText", "Essence2", () => Local.Instance.ElementCost(Local.Instance.ElementLevel(Element.Air)));
        //

        // End


        // ResearchPopUp

        // Titles
        ExtendedText
            .SetTextMethod("DamageTitleText", "DamageLevel", () => Local.Instance.ResearchLevel(Research.Damage));
        ExtendedText
            .SetTextMethod("AttackSpeedTitleText", "AttackSpeedLevel", () => Local.Instance.ResearchLevel(Research.AttackSpeed));
        ExtendedText
            .SetTextMethod("CriticalHitChanceTitleText", "CriticalHitChanceLevel", () => Local.Instance.ResearchLevel(Research.CriticalHitChange));
        ExtendedText
            .SetTextMethod("CriticalHitDamageTitleText", "CriticalHitDamageLevel", () => Local.Instance.ResearchLevel(Research.CriticalHitDamage));
        ExtendedText
            .SetTextMethod("RangeTitleText", "RangeLevel", () => Local.Instance.ResearchLevel(Research.Range));
        //

        // Info
        ExtendedText
            .SetTextMethod("DamageInfoText", "DamageInfo", () => Local.Instance.ResearchEffect(Research.Damage) * 100);
        ExtendedText
            .SetTextMethod("AttackSpeedInfoText", "AttackSpeedInfo", () => Local.Instance.ResearchEffect(Research.AttackSpeed) * 100);
        ExtendedText
            .SetTextMethod("CriticalHitChanceInfoText", "CriticalHitChanceInfo", () => Local.Instance.ResearchEffect(Research.CriticalHitChange) * 100);
        ExtendedText
            .SetTextMethod("CriticalHitDamageInfoText", "CriticalHitDamageInfo", () => Local.Instance.ResearchEffect(Research.CriticalHitDamage) * 100);
        ExtendedText
            .SetTextMethod("RangeInfoText", "RangeInfo", () => Local.Instance.ResearchEffect(Research.Range) * 100);
        //


        // Button
        ExtendedText
            .SetTextMethod("DamageButtonText", "Essence2", () => Local.Instance.ResearchCost(Research.Damage));
        ExtendedText
            .SetTextMethod("AttackSpeedButtonText", "Essence2", () => Local.Instance.ResearchCost(Research.AttackSpeed));
        ExtendedText
            .SetTextMethod("CriticalHitChanceButtonText", "Essence2", () => Local.Instance.ResearchCost(Research.CriticalHitChange));
        ExtendedText
            .SetTextMethod("CriticalHitDamageButtonText", "Essence2", () => Local.Instance.ResearchCost(Research.CriticalHitDamage));
        ExtendedText
            .SetTextMethod("RangeButtonText", "Essence2", () => Local.Instance.ResearchCost(Research.Range));
        //

        // End



        // Base

        // Titles
        
        ExtendedText
            .SetTextMethod("PassiveGoldTitleText", "GoldLevel" ,() => Local.Instance.PassiveIncomeLevel(PassiveIncome.Gold));
        ExtendedText
            .SetTextMethod("PassiveEssenceTitleText", "EssenceLevel", () => Local.Instance.PassiveIncomeLevel(PassiveIncome.Essence));
        ExtendedText
            .SetTextMethod("PassiveRPTitleText", "RPLevel", () => Local.Instance.PassiveIncomeLevel(PassiveIncome.RP));

        //

        // Info

        ExtendedText
            .SetTextMethod("PassiveGoldInfoText", "GoldIncome", () => Local.Instance.PassiveIncomeAmount(PassiveIncome.Gold));
        ExtendedText
            .SetTextMethod("PassiveEssenceInfoText", "EssenceIncome", () => Local.Instance.PassiveIncomeAmount(PassiveIncome.Essence));
        ExtendedText
            .SetTextMethod("PassiveRPInfoText", "RPIncome", () => Local.Instance.PassiveIncomeAmount(PassiveIncome.RP));

        //

        // Button

        ExtendedText
            .SetTextMethod("PassiveGoldButtonText", "Essence2", () => Local.Instance.PassiveIncomeUpdateCost(PassiveIncome.Gold));
        ExtendedText
            .SetTextMethod("PassiveEssenceButtonText", "Essence2", () => Local.Instance.PassiveIncomeUpdateCost(PassiveIncome.Essence));
        ExtendedText
            .SetTextMethod("PassiveRPButtonText", "Essence2", () => Local.Instance.PassiveIncomeUpdateCost(PassiveIncome.RP));

        //

        //End


    }




}

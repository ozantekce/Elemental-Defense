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
            .SetText("WaveInfo", ()=>UI_TEXT_FORMATS.Execute("Wave",Local.Instance.Wave));
        ExtendedText
            .SetText("EnemyCountInfo", ()=>UI_TEXT_FORMATS.Execute("EnemyCount", Local.Instance.EnemyCount));
        ExtendedText
            .SetText("EnemyHPInfo", ()=>UI_TEXT_FORMATS.Execute("EnemyHP", ((int)Local.Instance.EnemyHP)));

        ExtendedText
            .SetText("GoldInfo", () => UI_TEXT_FORMATS.Execute("Gold", Local.Instance.Gold));
        ExtendedText
            .SetText("EssenceInfo", () => UI_TEXT_FORMATS.Execute("Essence", Local.Instance.Essence));
        ExtendedText
            .SetText("RebornInfo", () => UI_TEXT_FORMATS.Execute("RebornPoint", Local.Instance.RebornPoint));
        // End


        // ElementsPopUp Texts

        //Titles
        ExtendedText
            .SetText("FireTitleText", () => UI_TEXT_FORMATS.Execute("FireLevel", Local.Instance.FireLevel));
        ExtendedText
            .SetText("WaterTitleText", () => UI_TEXT_FORMATS.Execute("WaterLevel", Local.Instance.WaterLevel));
        ExtendedText
            .SetText("EarthTitleText", () => UI_TEXT_FORMATS.Execute("EarthLevel", Local.Instance.EarthLevel));
        ExtendedText
            .SetText("AirTitleText", () => UI_TEXT_FORMATS.Execute("AirLevel", Local.Instance.AirLevel));
        //


        // Element Info
        ExtendedText
            .SetText("FireInfoText", () => UI_TEXT_FORMATS.Execute("FireInfo", Local.Instance.FireEffect * 100));
        ExtendedText
            .SetText("WaterInfoText", () => UI_TEXT_FORMATS.Execute("WaterInfo", Local.Instance.WaterEffect*100));
        ExtendedText
            .SetText("EarthInfoText", () => UI_TEXT_FORMATS.Execute("EarthInfo", Local.Instance.EarthEffect*100));
        ExtendedText
            .SetText("AirInfoText", () => UI_TEXT_FORMATS.Execute("AirInfo", Local.Instance.AirEffect*100));
        //

        // Buttons
        ExtendedText
            .SetText("FireButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.FireLevel)));
        ExtendedText
            .SetText("WaterButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.WaterLevel)));
        ExtendedText
            .SetText("EarthButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.FireLevel)));
        ExtendedText
            .SetText("AirButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.AirLevel)));
        //

        // End


        // ResearchPopUp

        // Titles
        ExtendedText
            .SetText("DamageTitleText", () => UI_TEXT_FORMATS.Execute("DamageLevel", Local.Instance.DamageLevel));
        ExtendedText
            .SetText("AttackSpeedTitleText", () => UI_TEXT_FORMATS.Execute("AttackSpeedLevel", Local.Instance.AttackSpeedLevel));
        ExtendedText
            .SetText("CriticalHitChanceTitleText", () => UI_TEXT_FORMATS.Execute("CriticalHitChanceLevel", Local.Instance.CriticalHitChangeLevel));
        ExtendedText
            .SetText("CriticalHitDamageTitleText", () => UI_TEXT_FORMATS.Execute("CriticalHitDamageLevel", Local.Instance.CriticalHitDamageLevel));
        ExtendedText
            .SetText("RangeTitleText", () => UI_TEXT_FORMATS.Execute("RangeLevel", Local.Instance.RangeLevel));
        //

        // Info
        ExtendedText
            .SetText("DamageInfoText", () => UI_TEXT_FORMATS.Execute("DamageInfo", Local.Instance.Damage * 100));
        ExtendedText
            .SetText("AttackSpeedInfoText", () => UI_TEXT_FORMATS.Execute("AttackSpeedInfo", Local.Instance.AttackSpeed * 100));
        ExtendedText
            .SetText("CriticalHitChanceInfoText", () => UI_TEXT_FORMATS.Execute("CriticalHitChanceInfo", Local.Instance.CriticalHitChange * 100));
        ExtendedText
            .SetText("CriticalHitDamageInfoText", () => UI_TEXT_FORMATS.Execute("CriticalHitDamageInfo", Local.Instance.CriticalHitDamage * 100));
        ExtendedText
            .SetText("RangeInfoText", () => UI_TEXT_FORMATS.Execute("RangeInfo", Local.Instance.Range * 100));
        //


        // Button
        ExtendedText
            .SetText("DamageButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.DamageLevel)));
        ExtendedText
            .SetText("AttackSpeedButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.AttackSpeedLevel)));
        ExtendedText
            .SetText("CriticalHitChanceButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.CriticalHitChangeLevel)));
        ExtendedText
            .SetText("CriticalHitDamageButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.CriticalHitDamageLevel)));
        ExtendedText
            .SetText("RangeButtonText", () => UI_TEXT_FORMATS.Execute("Essence2", Local.Instance.ElementCost(Local.Instance.RangeLevel)));
        //

        // End


    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TEXT_FORMATS
{



    private static Dictionary<string, Format> NameFormatPair = new Dictionary<string, Format>()
    {
        { "Wave" , new Format("Wave:") },
        { "EnemyCount" , new Format("Enemy:") },
        { "EnemyHP" , new Format("EnemyHP:") },
        { "Gold" , new Format("G:") },
        { "Gold2" , new Format("Gold(",")") },
        { "Essence" , new Format("E:") },
        { "Essence2" , new Format("Essence(",")") },
        { "RebornPoint" , new Format("RP:") },
        { "RebornPoint2" , new Format("RP(",")") },
        { "FireLevel" , new Format("Fire(Lv." , ")") },
        { "WaterLevel" , new Format("Water(Lv." , ")") },
        { "EarthLevel" , new Format("Earth(Lv." , ")") },
        { "AirLevel" , new Format("Air(Lv." , ")") },
        { "FireInfo" , new Format("Increased fire towers damage by <color=red>" , "%</color>") },
        { "WaterInfo" , new Format("Increased water towers slow by <color=red>" , "%</color>") },
        { "EarthInfo" , new Format("Increased earth towers stun change by <color=red>" , "%</color>") },
        { "AirInfo" , new Format("Increased air towers messy attack rate by <color=red>" , "%</color>") },


        { "DamageLevel" , new Format("Damage(Lv." , ")") },
        { "AttackSpeedLevel" , new Format("Attack Speed(Lv.", ")")},
        { "CriticalHitChanceLevel" , new Format("Critical Hit Chance(Lv." , ")") },
        { "CriticalHitDamageLevel" , new Format("Critical Hit Damage(Lv." , ")") },
        { "RangeLevel" , new Format("Range(Lv." , ")") },

        { "DamageInfo" , new Format("Increased towers damage by <color=red>" , "%</color>") },
        { "AttackSpeedInfo" , new Format("Increased towers attack speed by <color=red>","%</color>") },
        { "CriticalHitChanceInfo" , new Format("Increased towers critical hit chance by <color=red>" , "%</color>") },
        { "CriticalHitDamageInfo" , new Format("Increased towers critical hit damage by <color=red>" , "%</color>") },
        { "RangeInfo" , new Format("Increased towers range by <color=red>" , "%</color>") },


        { "GoldLevel" , new Format("Gold(Lv." , ")") },
        { "EssenceLevel" , new Format("Essence(Lv." , ")") },
        { "RPLevel" , new Format("RP(Lv." , ")") },

        { "GoldIncome" , new Format("<color=red>" , "</color> Gold by hour") },
        { "EssenceIncome" , new Format("<color=red>" , "</color> Essence by hour") },
        { "RPIncome" , new Format("<color=red>" , "</color> RP by hour") },


    };



    public static string Execute(string formatName, string text)
    {
        return NameFormatPair[formatName].ExecuteFormat(text);
    }

    public static string Execute(string formatName, float val)
    {
        return NameFormatPair[formatName].ExecuteFormat(NumberFormat(val));
    }

    public static string Execute(string formatName, int val)
    {
        return NameFormatPair[formatName].ExecuteFormat(NumberFormat(val));
    }

    public static string NumberFormat(float number)
    {
        if (number >= 1e18)
            return (number / 1e18).ToString("F2") + "E";
        else if (number >= 1e15)
            return (number / 1e15).ToString("F2") + "P";
        else if (number >= 1e12)
            return (number / 1e12).ToString("F2") + "T";
        else if (number >= 1e9)
            return (number / 1e9).ToString("F2") + "G";
        else if (number >= 1e6)
            return (number / 1e6).ToString("F2") + "M";
        else if (number >= 1e3)
            return (number / 1e3).ToString("F2") + "K";
        else
            return number.ToString("F2");
    }

    public static string NumberFormat(int number)
    {
        if (number >= 1e18)
            return (number / 1e18).ToString("F2") + "E";
        else if (number >= 1e15)
            return (number / 1e15).ToString("F2") + "P";
        else if (number >= 1e12)
            return (number / 1e12).ToString("F2") + "T";
        else if (number >= 1e9)
            return (number / 1e9).ToString("F2") + "G";
        else if (number >= 1e6)
            return (number / 1e6).ToString("F2") + "M";
        else if (number >= 1e3)
            return (number / 1e3).ToString("F2") + "K";
        else
            return number.ToString();
    }


    private class Format
    {

        private string front;
        private string end;

        public Format(string front="", string end="")
        {
            this.front = front;
            this.end = end;
        }
        
        public virtual string ExecuteFormat(string text)
        {
            return front+text+end;
        }

    }


}

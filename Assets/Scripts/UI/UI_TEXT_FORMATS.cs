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
        { "Essence" , new Format("E:") },
        { "Essence2" , new Format("Essence(",")") },
        { "RebornPoint" , new Format("RP:") },
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



    };



    public static string Execute(string formatName, string text)
    {
        return NameFormatPair[formatName].ExecuteFormat(text);
    }

    public static string Execute(string formatName, float val)
    {
        return NameFormatPair[formatName].ExecuteFormat(val+"");
    }

    public static string Execute(string formatName, int val)
    {
        return NameFormatPair[formatName].ExecuteFormat(val + "");
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

        public string ExecuteFormat(string text)
        {
            return front+text+end;
        }

    }

}

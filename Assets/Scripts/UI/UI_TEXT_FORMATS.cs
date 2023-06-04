using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UI_TEXT_FORMATS
{

    public delegate string IntFormatDelegate(int number);
    public delegate string FloatFormatDelegate(float number);

    private static Dictionary<string, TextFormat> NameFormatPairs = new Dictionary<string, TextFormat>()
    {
        { "Wave" , new TextFormat("Wave:{0}") },
        { "EnemyCount" , new TextFormat("Enemy:{0}") },
        { "EnemyHP" , new TextFormat("EnemyHP:{0}") },
        { "Gold" , new TextFormat("G:{0}") },
        { "Gold2" , new TextFormat("Gold({0})") },
        { "Gold3" , new TextFormat("<sprite=. name=gold>:{0}") },
        { "Essence" , new TextFormat("E:{0}") },
        { "Essence2" , new TextFormat("Essence({0})") },
        { "Essence3" , new TextFormat("<sprite=. name=essence>:{0}") },
        { "RebornPoint" , new TextFormat("RP:{0}") },
        { "RebornPoint2" , new TextFormat("RP({0})") },
        { "RebornPoint3" , new TextFormat("<sprite=. name=rp>:{0}") },

        { "FireLevel" , new TextFormat("Fire(Lv.{0})")},
        { "WaterLevel" , new TextFormat("Water(Lv.{0})") },
        { "EarthLevel" , new TextFormat("Earth(Lv.{0})") },
        { "AirLevel" , new TextFormat("Air(Lv.{0})") },

        { "FireInfo" , new TextFormat("Increased fire towers damage by <color=red>{0}%</color>") },
        { "WaterInfo" , new TextFormat("Increased water towers slow by <color=red>{0}%</color>") },
        { "EarthInfo" , new TextFormat("Increased earth towers stun change by <color=red>{0}%</color>") },
        { "AirInfo" , new TextFormat("Increased air towers messy attack rate by <color=red>{0}%</color>") },


        { "DamageLevel" , new TextFormat("Damage(Lv.{0})") },
        { "AttackSpeedLevel" , new TextFormat("Attack Speed(Lv.{0})")},
        { "CriticalHitChanceLevel" , new TextFormat("Critical Hit Chance(Lv.{0})") },
        { "CriticalHitDamageLevel" , new TextFormat("Critical Hit Damage(Lv.{0})") },
        { "RangeLevel" , new TextFormat("Range(Lv.{0})") },

        { "DamageInfo" , new TextFormat("Increased towers damage by <color=red>{0}%</color>") },
        { "AttackSpeedInfo" , new TextFormat("Increased towers attack speed by <color=red>{0}%</color>") },
        { "CriticalHitChanceInfo" , new TextFormat("Increased towers critical hit chance by <color=red>{0}%</color>") },
        { "CriticalHitDamageInfo" , new TextFormat("Increased towers critical hit damage by <color=red>{0}%</color>") },
        { "RangeInfo" , new TextFormat("Increased towers range by <color=red>{0}%</color>") },


        { "GoldLevel" , new TextFormat("Gold(Lv.{0})") },
        { "EssenceLevel" , new TextFormat("Essence(Lv.{0})") },
        { "RPLevel" , new TextFormat("RP(Lv.{0})") },

        { "GoldIncome" , new TextFormat("<color=red>{0}</color> Gold per "+Local.IncomeTime+" minute") },
        { "EssenceIncome" , new TextFormat("<color=red>{0}</color> Essence per "+Local.IncomeTime+" minute")},
        { "RPIncome" , new TextFormat("<color=red>{0}</color> RP per "+Local.IncomeTime+" minute") },


        { "CurrentLevelTowerInfo" , new TextFormat("\t\tCurrent\nAttack Power: {0}\nAttack Speed: {1}\nCri. Hit Change: {2}%\nCri. Hit Damage: {3}%\nRange: {4}") },
        { "NextLevelTowerInfo" , new TextFormat("\t\tNext Level\nAttack Power: {0}\nAttack Speed: {1}\nCri. Hit Change: {2}%\nCri. Hit Damage: {3}%\nRange: {4}") },
        { "TowerInfo" , new TextFormat("\t\tCurrent\nAttack Power: {0}\nAttack Speed: {1}\nCri. Hit Change: {2}%\nCri. Hit Damage: {3}%\nRange: {4}\n\t\tNext Level\nAttack Power: {5}\nAttack Speed: {6}\nCri. Hit Change: {7}%\nCri. Hit Damage: {8}%\nRange: {9}") },

        {"MAX",new TextFormat("MAX") }

    };

    public static string ExecuteFormat(this string formatName,params string[] texts)
    {
        return NameFormatPairs[formatName].ExecuteFormat(texts);
    }

    public static string ExecuteFormat(this string formatName,params float[] vals)
    {
        return NameFormatPairs[formatName].ExecuteFormat(vals);
    }

    public static string ExecuteFormat(this string formatName,params int[] vals)
    {
        return NameFormatPairs[formatName].ExecuteFormat(vals);
    }

    public static string NumberToLetter(this float number)
    {
        if (number < 0) return "MAX";

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

    public static string NumberToLetter(this int number)
    {
        if (number < 0) return "MAX";

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


    private class TextFormat
    {
        private string _format;
        private FloatFormatDelegate _floatFormatDelegate = NumberToLetter;
        private IntFormatDelegate _intFormatDelegate = NumberToLetter;

        public TextFormat(string format)
        {
            _format = format;
        }

        public virtual string ExecuteFormat(params string[] texts)
        {
            if (texts[0].Equals("MAX")) return "MAX";
            return string.Format(this._format, texts);
        }

        public virtual string ExecuteFormat(params int[] vals)
        {
            return ExecuteFormat(vals.Select(v => _intFormatDelegate != null ? _intFormatDelegate(v) : v.ToString()).ToArray());
        }

        public virtual string ExecuteFormat(params float[] vals)
        {
            return ExecuteFormat(vals.Select(v => _floatFormatDelegate != null ? _floatFormatDelegate(v) : v.ToString()).ToArray());
        }
    }


}

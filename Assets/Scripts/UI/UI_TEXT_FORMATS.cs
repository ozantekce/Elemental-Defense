using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UI_TEXT_FORMATS
{


    public delegate string IntFormatDelegate(int number);
    public delegate string FloatFormatDelegate(float number);
    private delegate TextFormat AlternativeFormat();

    private static Dictionary<string, TextFormat> NameFormatPairs = new Dictionary<string, TextFormat>()
    {
        { "Wave" , new TextFormat("Wave:{0}") },
        { "EnemyCount" , new TextFormat("Enemy:{0}") },
        { "EnemyHP" , new TextFormat("EnemyHP:{0}") },
        { "Gold" , new TextFormat("G:{0}") },
        { "Gold2" , new TextFormat("Gold({0})") },
        { "Essence" , new TextFormat("E:{0}") },
        { "Essence2" , new TextFormat("Essence({0})") },
        { "RebornPoint" , new TextFormat("RP:{0}") },
        { "RebornPoint2" , new TextFormat("RP({0})") },

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
        private AlternativeFormat _alternativeFormat;
        private FloatFormatDelegate _floatFormatDelegate = NumberToLetter;
        private IntFormatDelegate _intFormatDelegate = NumberToLetter;

        public TextFormat(string format)
        {
            _format = format;
        }
        public TextFormat(string format, AlternativeFormat alternativeFormat) : this(format)
        {
            _alternativeFormat = alternativeFormat;
        }

        public virtual string ExecuteFormat(params string[] texts)
        {
            if(_alternativeFormat != null)
            {
                TextFormat alt = _alternativeFormat();
                if (alt!=null)
                {
                    return alt.ExecuteFormat(texts);
                }
            }
            if (texts[0].Equals("MAX")) return "MAX";
            return string.Format(this._format, texts);

        }

        public virtual string ExecuteFormat(IntFormatDelegate format, params int[] texts)
        {
            string[] strings = new string[texts.Length];
            for (int i = 0; i < texts.Length; i++)
            {
                strings[i] = format(texts[i]);
            }
            return ExecuteFormat(strings);
        }
        public virtual string ExecuteFormat(params int[] texts)
        {
            string[] strings = new string[texts.Length];
            for (int i = 0; i < texts.Length; i++)
            {
                if (_intFormatDelegate == null)
                    strings[i] = texts[i].ToString();
                else
                    strings[i] = _intFormatDelegate(texts[i]);
            }
            return ExecuteFormat(strings);
        }

        public virtual string ExecuteFormat(FloatFormatDelegate format, params float[] texts)
        {
            string[] strings = new string[texts.Length];
            for (int i = 0; i < texts.Length; i++)
            {
                strings[i] = format(texts[i]);
            }
            return ExecuteFormat(strings);
        }

        public virtual string ExecuteFormat(params float[] texts)
        {
            string[] strings = new string[texts.Length];
            for (int i = 0; i < texts.Length; i++)
            {
                if (_floatFormatDelegate == null)
                    strings[i] = texts[i].ToString();
                else
                    strings[i] = _floatFormatDelegate(texts[i]);
            }
            return ExecuteFormat(strings);
        }

    }


}

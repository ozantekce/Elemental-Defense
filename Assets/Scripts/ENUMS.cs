using System;

public enum Element
{
    Fire,Water,Earth,Air
}


public enum Research
{
    Damage, AttackSpeed, CriticalHitChange, CriticalHitDamage, Range
}

public enum Direction
{
    None,Up,Down,Left,Right
}

public enum EnemyStatus
{
    None,Slowed,Stunned
}


public enum TowerType
{
    Fire,Water,Earth,Air
}

public enum TowerAttackType
{
    Closed,
}

public enum PassiveIncome
{
    Gold,Essence,RP
}


public struct EnumFloatPair
{
    public string enumName;
    public float val;
    public EnumFloatPair(string enumName, float val)
    {
        this.enumName = enumName; this.val = val;
    }
}

public struct EnumIntPair
{
    public string enumName;
    public int val;
    public EnumIntPair(string enumName, int val)
    {
        this.enumName = enumName; this.val = val;
    }
}


public static class EnumHelper
{
    public static T StringToEnum<T>(this string value) where T : struct
    {
        if (Enum.TryParse(value, true, out T result))
        {
            return result;
        }
        else
        {
            throw new ArgumentException("Invalid enum value", nameof(value));
        }
    }

    public static int ElementLevel(this Element element)
    {
        return Local.Instance.ElementLevel(element);
    }

    public static int ElementCost(this Element element)
    {
        return Local.Instance.ElementCost(element);
    }

    public static float ElementEffect(this Element element)
    {
        return Local.Instance.ElementEffect(element);
    }


    public static int ResearchLevel(this Research research)
    {
        return Local.Instance.ResearchLevel(research);
    }

    public static int ResearchCost(this Research research)
    {
        return Local.Instance.ResearchCost(research);
    }

    public static float ResearchEffect(this Research research)
    {
        return Local.Instance.ResearchEffect(research);
    }


    public static int PassiveIncomeLevel(this PassiveIncome income)
    {
        return Local.Instance.PassiveIncomeLevel(income);
    }

    public static int PassiveIncomeUpdateCost(this PassiveIncome income)
    {
        return Local.Instance.PassiveIncomeUpdateCost(income);
    }

    public static float PassiveIncomeAmount(this PassiveIncome income)
    {
        return Local.Instance.PassiveIncomeAmount(income);
    }

    public static int NewTowerCost(this TowerType tower)
    {
        return Local.Instance.NewTowerCost(tower);
    }


}
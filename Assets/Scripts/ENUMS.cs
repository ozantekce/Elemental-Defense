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

public static class EnumHelper
{
    public static T StringToEnum<T>(string value) where T : struct
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
}
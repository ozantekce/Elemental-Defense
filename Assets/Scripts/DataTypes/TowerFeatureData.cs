using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerFeatureData
{

    public TowerFeatureData()
    {

    }

    public TowerFeatureData(float range,float attackPower, float attackPerSecond
        ,float criticalChange, float criticalDamage
        )
    {
        this.range = range;
        this.attackPower = attackPower;
        this.attackPerSecond = attackPerSecond;
        this.criticalChange = criticalChange;
        this.criticalDamage = criticalDamage;
    }

    public float range;
    public float attackPower;
    public float attackPerSecond;
    public float criticalChange;
    public float criticalDamage;


}

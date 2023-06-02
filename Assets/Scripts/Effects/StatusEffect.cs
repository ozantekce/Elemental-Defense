using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : Effect
{

    private Enemy _enemy;
    private EnemyStatus _status;


    public void InitEffect(EnemyStatus status, Enemy enemy, MyEffectData effectData)
    {
        this._enemy = enemy;
        this._status = status;
        base.InitEffect(effectData);
    }


    private void Update()
    {
        base.Update();
        if(_enemy==null || _enemy.Status != _status)
        {
            SendToPool();
        }
    }

}

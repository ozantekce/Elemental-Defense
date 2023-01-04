using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMainScreen : Screen
{


    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI enemyHpText;


    public TextMeshProUGUI goldText;
    public TextMeshProUGUI essenceText;
    public TextMeshProUGUI rebornPointText;



    private const int UpdateRate = 20;
    private int currentUpdate = 21;
    private void Update()
    {
        
        currentUpdate++;
        if(currentUpdate >= UpdateRate)
        {
            currentUpdate = 0;
            waveText.text = "Wave:"+Local.Instance.Wave;
            enemyCountText.text = "Enemy:"+Local.Instance.EnemyCount;
            enemyHpText.text = "EnemyHP:"+((int)Local.Instance.EnemyHP);

            goldText.text = "G:" + Local.Instance.Gold;
            essenceText.text = "E:" + Local.Instance.Essence;
            rebornPointText.text = "RP:" + Local.Instance.RebornPoint;

        }


    }


}

using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    
    private List<Enemy> _enemyList = new List<Enemy>();

    [SerializeField]
    private Portal _portal;


    [SerializeField]
    private GameObject _rangeArea;

    [SerializeField]
    private Transform[] _enemyFollowPath;


    private void Awake()
    {
        Time.timeScale = 1;
        _instance = this;
        string @string = PlayerPrefs.GetString("lastIncomeTime");
        if (@string != string.Empty)
        {
            _lastIncomeTime = DateTime.Parse(@string);
        }
        else
        {
            _lastIncomeTime = DateTime.Now;
            PlayerPrefs.SetString("lastIncomeTime", _lastIncomeTime.ToString());
        }

        _incomeCooldown = new Cooldown(1000 * 60 * 60);
        Income();
    }


    private void Update()
    {
        if (_incomeCooldown.Ready())
        {
            Income();
        }
    }

    private DateTime _lastIncomeTime; 
    private Cooldown _incomeCooldown;
    private void Income()
    {
        DateTime now = DateTime.Now;
        int elapsedTime = (now - _lastIncomeTime).Minutes;
        if (elapsedTime < 60)
        {
            return;
        }
        Local.Instance.Gold += (int)(elapsedTime / 60f * Local.Instance.PassiveIncomeAmount(PassiveIncome.Gold));
        Local.Instance.Essence += (int)(elapsedTime / 60f * Local.Instance.PassiveIncomeAmount(PassiveIncome.Essence));
        Local.Instance.RebornPoint += (int)(elapsedTime / 60f * Local.Instance.PassiveIncomeAmount(PassiveIncome.RP));
        _lastIncomeTime = now;
        PlayerPrefs.SetString("lastIncomeTime", _lastIncomeTime.ToString());
    }


    public void ResetWave()
    {
        _portal.ResetWave();
        for (int i = 0; i < _enemyList.Count; i++)
        {
            Destroy(_enemyList[i].gameObject);
            
        }
        _enemyList.Clear();


    }


    public void AddEnemy(Enemy enemy)
    {
        _enemyList.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy);
    }


    public void GameSpeed1X()
    {
        Time.timeScale = 1;
    }
    public void GameSpeed2X()
    {
        Time.timeScale = 2;
    }
    public void GameSpeed4X()
    {
        Time.timeScale = 4;
    }
    public void GameSpeed8X()
    {
        Time.timeScale = 8;
    }



    public void IncreaseElementLevel(Element element)
    {
        int elementLevel = Local.Instance.ElementLevel(element);
        int price = Local.Instance.ElementCost(elementLevel);
        if(Local.Instance.Essence >= price)
        {
            Local.Instance.Essence -= price;
            Local.Instance.IncreaseElementLevel(element);
        }
    }

    public void IncreaseElementLevel(string elementName)
    {
        Element element = EnumHelper.StringToEnum<Element>(elementName);
        IncreaseElementLevel(element);
    }

    public void IncreaseResearchLevel(Research research)
    {

        int researchLevel = Local.Instance.ResearchLevel(research);
        int price = Local.Instance.ResearchCost(researchLevel);

        if(Local.Instance.Essence >= price)
        {
            Local.Instance.Essence -= price;
            Local.Instance.IncreaseResearchLevel(research);
        }

    }

    public void IncreaseResearchLevel(string researchName)
    {
        Research research = EnumHelper.StringToEnum<Research>(researchName);
        IncreaseResearchLevel(research);
    }


    public void IncreaseIncomeLevel(PassiveIncome income)
    {
        int price = Local.Instance.PassiveIncomeUpdateCost(income);

        if (Local.Instance.Essence >= price)
        {
            Local.Instance.Essence -= price;
            Local.Instance.IncreaseIncomeLevel(income);
        }

    }

    public void IncreaseIncomeLevel(string incomeName)
    {
        PassiveIncome research = EnumHelper.StringToEnum<PassiveIncome>(incomeName);
        IncreaseIncomeLevel(research);
    }


    public static float DeltaTime()
    {
        return Time.deltaTime*Local.Instance.GameSpeed;
    }

    public static GameManager Instance { get => _instance; set => _instance = value; }
    public List<Enemy> EnemyList { get => _enemyList; set => _enemyList = value; }
    public Portal Portal { get => _portal; set => _portal = value; }
    public GameObject RangeArea { get => _rangeArea; set => _rangeArea = value; }
    public Transform[] EnemyFollowPath { get => _enemyFollowPath; set => _enemyFollowPath = value; }
}

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
    private PlayerBase _base;


    [SerializeField]
    private GameObject _rangeArea;

    [SerializeField]
    private Transform[] _enemyFollowPath;


    private void Awake()
    {
        Time.timeScale = 1;
        _instance = this;

    }

    private void Start()
    {
    GetLastIncome:
        string @string = PlayerPrefs.GetString("lastIncomeTime");
        if (!string.IsNullOrEmpty(@string))
        {
            _lastIncomeTime = DateTime.Parse(@string);
        }
        else
        {
            PlayerPrefs.SetString("lastIncomeTime", DateTime.Now.ToString());
            goto GetLastIncome;
        }

        _incomeCooldown = new Cooldown(1000 * 60 * Local.IncomeTime);
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
        float elapsedTime = (float)(now - _lastIncomeTime).TotalMinutes;
        Debug.Log(elapsedTime);
        if (elapsedTime < Local.IncomeTime)
        {
            return;
        }
        float gold = (elapsedTime / Local.IncomeTime * PassiveIncome.Gold.PassiveIncomeAmount());
        float essence = (elapsedTime / Local.IncomeTime * PassiveIncome.Essence.PassiveIncomeAmount());
        float rp = (elapsedTime / Local.IncomeTime * PassiveIncome.RP.PassiveIncomeAmount());

        Local.Instance.Gold += gold;
        Local.Instance.Essence += essence;
        Local.Instance.RebornPoint += rp;

        string tx = "<color=yellow>";
        tx += "+" + "Gold".ExecuteFormat(gold) +"\n";
        tx += "+" + "Essence".ExecuteFormat(essence) +"\n";
        tx += "+" + "RebornPoint".ExecuteFormat(rp) + "</color>";

        InfoTextManager.Instance.CreateText(tx,_base.transform.position+Vector3.up*30f,3f);
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
        int elementLevel = element.ElementLevel();
        if (elementLevel >= Local.MaxElemetsLevel) return;
        int price = element.ElementCost();
        if(Local.Instance.Essence >= price)
        {
            Local.Instance.Essence -= price;
            Local.Instance.IncreaseElementLevel(element);
        }

    }

    public void IncreaseElementLevel(string elementName)
    {
        Element element = elementName.StringToEnum<Element>();
        IncreaseElementLevel(element);
    }

    public void IncreaseResearchLevel(Research research)
    {

        int researchLevel = research.ResearchLevel();
        int price = research.ResearchCost();

        if(Local.Instance.Essence >= price)
        {
            Local.Instance.Essence -= price;
            Local.Instance.IncreaseResearchLevel(research);
        }

    }

    public void IncreaseResearchLevel(string researchName)
    {
        Research research = researchName.StringToEnum<Research>();
        IncreaseResearchLevel(research);
    }


    public void IncreaseIncomeLevel(PassiveIncome income)
    {
        int price = income.PassiveIncomeUpdateCost();

        if (Local.Instance.Essence >= price)
        {
            Local.Instance.Essence -= price;
            Local.Instance.IncreaseIncomeLevel(income);
        }

    }

    public void IncreaseIncomeLevel(string incomeName)
    {
        PassiveIncome research = incomeName.StringToEnum<PassiveIncome>();
        IncreaseIncomeLevel(research);
    }


    public static float DeltaTime()
    {
        return Time.deltaTime*Local.Instance.GameSpeed*1.15f;
    }

    public static GameManager Instance { get => _instance; set => _instance = value; }
    public List<Enemy> EnemyList { get => _enemyList; set => _enemyList = value; }
    public Portal Portal { get => _portal; set => _portal = value; }
    public GameObject RangeArea { get => _rangeArea; set => _rangeArea = value; }
    public Transform[] EnemyFollowPath { get => _enemyFollowPath; set => _enemyFollowPath = value; }
}

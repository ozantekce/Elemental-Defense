using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ScreenManagerNameSpace;
using TMPro;
using UnityEngine.UI;

public class RebornPopUp : PopUp
{

    [Header("Contract")]
    [SerializeField]
    private TextMeshProUGUI _contractInfo;
    [SerializeField]
    private Button _contractButton;

    [Header("Title")]
    [SerializeField]
    private TextMeshProUGUI _essenceChangeInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _goldDropInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _enemyHPInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _gameSpeedInfoTitleText;


    [Header("Info")]
    [SerializeField]
    private TextMeshProUGUI _essenceChangeInfoText;
    [SerializeField]
    private TextMeshProUGUI _goldDropInfoText;
    [SerializeField]
    private TextMeshProUGUI _enemyHPInfoText;
    [SerializeField]
    private TextMeshProUGUI _gameSpeedInfoText;


    [Header("ButtonText")]
    [SerializeField]
    private TextMeshProUGUI _essenceChangeButtonText;
    [SerializeField]
    private TextMeshProUGUI _goldDropButtonText;
    [SerializeField]
    private TextMeshProUGUI _enemyHPButtonText;
    [SerializeField]
    private TextMeshProUGUI _gameSpeedButtonText;


    [Header("Button")]
    [SerializeField]
    private Button _essenceChangeButton;
    [SerializeField]
    private Button _goldDropButton;
    [SerializeField]
    private Button _enemyHPButton;
    [SerializeField]
    private Button _gameSpeedButton;

    private void Start()
    {
        _essenceChangeButton.onClick.AddListener(OnClickEssenceChangeButton);
        _goldDropButton.onClick.AddListener(OnClickGoldDropButton);
        _enemyHPButton.onClick.AddListener(OnClickEnemyHPButton);
        _gameSpeedButton.onClick.AddListener(OnClickGameSpeedButton);

        _contractButton.onClick.AddListener(OnClickContractButton);

        UpdateTexts();
    }


    private void UpdateTexts()
    {
        // Update Contract

        if (Local.Instance.Wave < Local.MinWavetoReborn)
        {
            _contractInfo.text = "You must be at least wave "+Local.MinWavetoReborn;
            _contractButton.enabled = false;
        }
        else
        {
            _contractInfo.text = "This will reset your wave" +
                ", gold and towers but you gain <color=red>"
                + (Local.Instance.CanEarnRP) + "</color> Reborn Point";
            _contractButton.enabled = true;
        }


        //end

        // Update Buttons
        _essenceChangeButtonText.text = "RP(" + Local.Instance.RebornPointCost(Local.Instance.EssenceChangeLevel) + ")";
        _goldDropButtonText.text = "RP(" + Local.Instance.RebornPointCost(Local.Instance.GoldDropLevel) + ")";
        _enemyHPButtonText.text = "RP(" + Local.Instance.RebornPointCost(Local.Instance.EnemyHPDecreaseLevel) + ")";
        _gameSpeedButtonText.text = "RP(" + Local.Instance.RebornPointCost(Local.Instance.GameSpeedLevel) + ")";
        
        if (Local.Instance.EssenceChangeLevel >= Local.MaxEssenceChangeLevel)
        {
            _essenceChangeButtonText.text = "MAX";
            _essenceChangeButton.enabled = false;
        }
        if (Local.Instance.EnemyHPDecreaseLevel >= Local.MaxEnemyHpDecreaseLvl)
        {
            _enemyHPButtonText.text = "MAX";
            _enemyHPButton.enabled = false;
        }
        if (Local.Instance.GameSpeedLevel >= Local.MaxGameSpeedLevel)
        {
            _gameSpeedButtonText.text = "MAX";
            _gameSpeedButton.enabled = false;
        }
        //end

        // Update titles
        _essenceChangeInfoTitleText.text = "Essence Change(Lv." + Local.Instance.EssenceChangeLevel + ")";
        _goldDropInfoTitleText.text = "Gold Drop(Lv." + Local.Instance.GoldDropLevel + ")";
        _enemyHPInfoTitleText.text = "Enemy HP(Lv." + Local.Instance.EnemyHPDecreaseLevel + ")";
        _gameSpeedInfoTitleText.text = "Game Speed(Lv." + Local.Instance.GameSpeedLevel + ")";
        // end


        // Update Info
        _essenceChangeInfoText.text = "Current essence change <color=red>" + Local.Instance.EssenceChange * 100 + "%</color>";
        _goldDropInfoText.text = "Current gold drop <color=red>" + Local.Instance.GoldDrop + "</color>";
        _enemyHPInfoText.text = "Decreased enemy hp by <color=red>" + (100f - Local.Instance.EnemyHPDecreasePercent * 100f) + "%</color>";
        _gameSpeedInfoText.text = "Current game speed  <color=red>" + Local.Instance.GameSpeed + "X </color>";
        //end

    }

    public void OnClickEssenceChangeButton()
    {
        if (Local.Instance.RebornPoint >= Local.Instance.RebornPointCost(Local.Instance.EssenceChangeLevel))
        {
            Local.Instance.RebornPoint -= Local.Instance.RebornPointCost(Local.Instance.EssenceChangeLevel);
            Local.Instance.EssenceChangeLevel++;
            UpdateTexts();
        }
    }

    public void OnClickGoldDropButton()
    {
        if (Local.Instance.RebornPoint >= Local.Instance.RebornPointCost(Local.Instance.GoldDropLevel))
        {
            Local.Instance.RebornPoint -= Local.Instance.RebornPointCost(Local.Instance.GoldDropLevel);
            Local.Instance.GoldDropLevel++;
            UpdateTexts();
        }
    }

    public void OnClickEnemyHPButton()
    {
        if (Local.Instance.RebornPoint >= Local.Instance.RebornPointCost(Local.Instance.EnemyHPDecreaseLevel))
        {
            Local.Instance.RebornPoint -= Local.Instance.RebornPointCost(Local.Instance.EnemyHPDecreaseLevel);
            Local.Instance.EnemyHPDecreaseLevel++;
            UpdateTexts();
        }
    }

    public void OnClickGameSpeedButton()
    {
        if (Local.Instance.RebornPoint >= Local.Instance.RebornPointCost(Local.Instance.GameSpeedLevel))
        {
            Local.Instance.RebornPoint -= Local.Instance.RebornPointCost(Local.Instance.GameSpeedLevel);
            Local.Instance.GameSpeedLevel++;
            UpdateTexts();
        }
    }


    public void OnClickContractButton()
    {

        int currentEssence = Local.Instance.Essence;
        int currentRP = Local.Instance.RebornPoint + Local.Instance.CanEarnRP;
        
        int currentFireLevel = Local.Instance.FireLevel;
        int currentWaterLevel = Local.Instance.WaterLevel;
        int currentEarthLevel = Local.Instance.EarthLevel;
        int currentAirLevel = Local.Instance.AirLevel;

        int currentDamageLevel = Local.Instance.DamageLevel;
        int currentAttackSpeedLevel = Local.Instance.AttackSpeedLevel;
        int currentCriticalHitChangeLevel = Local.Instance.CriticalHitChangeLevel;
        int currentCriticalHitDamageLevel = Local.Instance.CriticalHitDamageLevel;
        int currentRangeLevel = Local.Instance.RangeLevel;

        int currentEssenceChangeLevel = Local.Instance.EssenceChangeLevel;
        int currentGoldDropLevel = Local.Instance.GoldDropLevel;
        int currentEnemyHPDecreaseLevel = Local.Instance.EnemyHPDecreaseLevel;
        int currentGameSpeedLevel = Local.Instance.GameSpeedLevel;


        Local.Instance.ResetPlayerPrefs();

        Tower.DestroyAllTowers();

        GameManager.Instance.ResetWave();


        Local.Instance.Essence = currentEssence;
        Local.Instance.RebornPoint = currentRP;

        Local.Instance.FireLevel = currentFireLevel;
        Local.Instance.WaterLevel = currentWaterLevel;
        Local.Instance.EarthLevel = currentEarthLevel;
        Local.Instance.AirLevel = currentAirLevel;

        Local.Instance.DamageLevel = currentDamageLevel;
        Local.Instance.AttackSpeedLevel = currentAttackSpeedLevel;
        Local.Instance.CriticalHitChangeLevel = currentCriticalHitChangeLevel;
        Local.Instance.CriticalHitDamageLevel = currentCriticalHitDamageLevel;
        Local.Instance.RangeLevel = currentRangeLevel;

        Local.Instance.EssenceChangeLevel = currentEssenceChangeLevel;
        Local.Instance.GoldDropLevel = currentGoldDropLevel;
        Local.Instance.EnemyHPDecreaseLevel = currentEnemyHPDecreaseLevel;
        Local.Instance.GameSpeedLevel = currentGameSpeedLevel;

        ScreenManager.Instance.ClosePopUp(this.name);
        UpdateTexts();
    }

    public override void Configurations()
    {
        AfterOpen = AfterOpen_;
        BeforeOpen = BeforeOpen_;

        //BeforeClose = BeforeClose_;
    }


    private IEnumerator BeforeOpen_()
    {
        UpdateTexts();
        //Debug.Log("before open start");
        transform.localScale = Vector3.zero;
        yield return null;
        //Debug.Log("before open over");
    }

    private IEnumerator AfterOpen_()
    {
        //Debug.Log("after open start");
        Tween _tweenerOpen = transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
        yield return new WaitForEndOfFrame();
        while (_tweenerOpen.IsPlaying())
        {
            yield return null;
        }
        //Debug.Log("after open over");
    }

    private IEnumerator BeforeClose_()
    {
        //Debug.Log("before close start");
        Tween _tweenerClose = transform.DOScale(Vector3.zero, 0.3f);
        yield return new WaitForEndOfFrame();
        while (_tweenerClose.IsPlaying())
        {
            yield return null;
        }
        //Debug.Log("before close over");
    }


}
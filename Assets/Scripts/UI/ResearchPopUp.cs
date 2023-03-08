using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ResearchPopUp : PopUp
{



    [SerializeField]
    private TextMeshProUGUI _damageInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _attackSpeedInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _criticalHitChanceInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _criticalHitDamageInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _rangeInfoTitleText;


    [SerializeField]
    private TextMeshProUGUI _damageInfoText;
    [SerializeField]
    private TextMeshProUGUI _attackSpeedInfoText;
    [SerializeField]
    private TextMeshProUGUI _criticalHitChanceInfoText;
    [SerializeField]
    private TextMeshProUGUI _criticalHitDamageInfoText;
    [SerializeField]
    private TextMeshProUGUI _rangeInfoText;


    [SerializeField]
    private TextMeshProUGUI _damageButtonText;
    [SerializeField]
    private TextMeshProUGUI _attackSpeedButtonText;
    [SerializeField]
    private TextMeshProUGUI _criticalHitChanceButtonText;
    [SerializeField]
    private TextMeshProUGUI _criticalHitDamageButtonText;
    [SerializeField]
    private TextMeshProUGUI _rangeButtonText;



    private void Start()
    {
        UpdateTexts();
    }




    private void UpdateTexts()
    {

        // Update Buttons
        _damageButtonText.text = "Essence(" + Local.Instance.ResearchCost(Local.Instance.DamageLevel) + ")";
        _attackSpeedButtonText.text = "Essence(" + Local.Instance.ResearchCost(Local.Instance.AttackSpeedLevel) + ")";
        _criticalHitChanceButtonText.text = "Essence(" + Local.Instance.ResearchCost(Local.Instance.CriticalHitChangeLevel) + ")";
        _criticalHitDamageButtonText.text = "Essence(" + Local.Instance.ResearchCost(Local.Instance.CriticalHitDamageLevel) + ")";
        _rangeButtonText.text = "Essence(" + Local.Instance.ResearchCost(Local.Instance.RangeLevel) + ")";
        //end


        // Update titles
        _damageInfoTitleText.text = "Damage(Lv." + Local.Instance.DamageLevel + ")";
        _attackSpeedInfoTitleText.text = "Attack Speed(Lv." + Local.Instance.AttackSpeedLevel + ")";
        _criticalHitChanceInfoTitleText.text = "Critical Hit Chance(Lv." + Local.Instance.CriticalHitChangeLevel + ")";
        _criticalHitDamageInfoTitleText.text = "Critical Hit Damage(Lv." + Local.Instance.CriticalHitDamageLevel + ")";
        _rangeInfoTitleText.text = "Range(Lv." + Local.Instance.RangeLevel + ")";

        // end


        // Update Info

        _damageInfoText.text = "Increased towers damage by <color=red>"+Local.Instance.Damage*100+"%</color>";
        _attackSpeedInfoText.text = "Increased towers attack speed by <color=red>" + Local.Instance.AttackSpeed*100+"%</color>";
        _criticalHitChanceInfoText.text = "Increased towers critical hit chance by <color=red>" + Local.Instance.CriticalHitChange*100+"%</color>";
        _criticalHitDamageInfoText.text = "Increased towers critical hit damage by <color=red>" + Local.Instance.CriticalHitDamage*100+"%</color>";
        _rangeInfoText.text = "Increased towers range by <color=red>" + Local.Instance.Range*100+"%</color>";


        //end



    }


    public void OnClickDamageButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.DamageLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.DamageLevel);
            Local.Instance.DamageLevel++;
            UpdateTexts();
        }
    }

    public void OnClickAttackSpeedButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.AttackSpeedLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.AttackSpeedLevel);
            Local.Instance.AttackSpeedLevel++;
            UpdateTexts();
        }
    }

    public void OnClickCriticalHitChanceButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.CriticalHitChangeLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.CriticalHitChangeLevel);
            Local.Instance.CriticalHitChangeLevel++;
            UpdateTexts();
        }
    }

    public void OnClickCriticalHitDamageButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.CriticalHitDamageLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.CriticalHitDamageLevel);
            Local.Instance.CriticalHitDamageLevel++;
            UpdateTexts();
        }
    }


    public void OnClickRangeButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ResearchCost(Local.Instance.RangeLevel))
        {
            Local.Instance.Essence -= Local.Instance.ResearchCost(Local.Instance.RangeLevel);
            Local.Instance.RangeLevel++;
            UpdateTexts();
        }
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
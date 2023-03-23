using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using ScreenManagerNameSpace;

public class SlotFullPopUp : PopUp
{

    private Slot _openerSlot;

    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _updateButtonText;
    [SerializeField]
    private TextMeshProUGUI _sellButtonText;

    [SerializeField]
    private TextMeshProUGUI _infoText;

    public void Open(Slot slot)
    {
        _openerSlot = slot;
        ScreenManager.Instance.CloseAllPopUpWithout("SlotFullPopUp");
        ScreenManager.Instance.OpenPopUp("SlotFullPopUp");


        UpdateTexts();

    }


    private void UpdateTexts()
    {

        _infoText.text = _openerSlot.Tower.GetTowerInfo();

        _title.text = _openerSlot.Tower.Name.ToUpperInvariant() + "(" + _openerSlot.Tower.CurrentLevel + ")";

        // Update Update Button
        if (_openerSlot.Tower.TowerType == TowerType.fire)
        {
            _updateButtonText.text = "Gold(" + Local.Instance.FireTowerUpdateCost(_openerSlot.Tower.CurrentLevel) + ")";
        }
        else if(_openerSlot.Tower.TowerType == TowerType.water)
        {
            _updateButtonText.text = "Gold(" + Local.Instance.WaterTowerUpdateCost(_openerSlot.Tower.CurrentLevel) + ")";
        }
        else if (_openerSlot.Tower.TowerType == TowerType.earth)
        {
            _updateButtonText.text = "Gold(" + Local.Instance.EarthTowerUpdateCost(_openerSlot.Tower.CurrentLevel) + ")";
        }
        else if (_openerSlot.Tower.TowerType == TowerType.air)
        {
            _updateButtonText.text = "Gold(" + Local.Instance.AirTowerUpdateCost(_openerSlot.Tower.CurrentLevel) + ")";
        }
        // end


        // Update Sell button
        if (_openerSlot.Tower.TowerType == TowerType.fire)
        {
            _sellButtonText.text = "Gold(" + Local.Instance.FireTowerSellPrice(_openerSlot.Tower.CurrentLevel) + ")";
        }
        else if (_openerSlot.Tower.TowerType == TowerType.water)
        {
            _sellButtonText.text = "Gold(" + Local.Instance.WaterTowerSellPrice(_openerSlot.Tower.CurrentLevel) + ")";
        }
        else if (_openerSlot.Tower.TowerType == TowerType.earth)
        {
            _sellButtonText.text = "Gold(" + Local.Instance.EarthTowerSellPrice(_openerSlot.Tower.CurrentLevel) + ")";
        }
        else if (_openerSlot.Tower.TowerType == TowerType.air)
        {
            _sellButtonText.text = "Gold(" + Local.Instance.AirTowerSellPrice(_openerSlot.Tower.CurrentLevel) + ")";
        }
        // end



    }



    public void OnClickUpdateButton()
    {
        int price = 0;

        if (_openerSlot.Tower.TowerType == TowerType.fire)
        {
            price = Local.Instance.FireTowerUpdateCost(_openerSlot.Tower.CurrentLevel);
        }
        else if (_openerSlot.Tower.TowerType == TowerType.water)
        {
            price = Local.Instance.WaterTowerUpdateCost(_openerSlot.Tower.CurrentLevel);
        }
        else if (_openerSlot.Tower.TowerType == TowerType.earth)
        {
            price = Local.Instance.EarthTowerUpdateCost(_openerSlot.Tower.CurrentLevel);
        }
        else if (_openerSlot.Tower.TowerType == TowerType.air)
        {
            price = Local.Instance.AirTowerUpdateCost(_openerSlot.Tower.CurrentLevel);
        }

        if (Local.Instance.Gold >= price)
        {
            _openerSlot.UpdateTower(price);
        }

        UpdateTexts();
    }


    public void OnClickSellButton()
    {


        int price = 0;

        if (_openerSlot.Tower.TowerType == TowerType.fire)
        {
            price = Local.Instance.FireTowerSellPrice(_openerSlot.Tower.CurrentLevel);
            Local.Instance.NumberOfFireTowers--;
        }
        else if (_openerSlot.Tower.TowerType == TowerType.water)
        {
            price = Local.Instance.WaterTowerSellPrice(_openerSlot.Tower.CurrentLevel);
            Local.Instance.NumberOfWaterTowers--;
        }
        else if (_openerSlot.Tower.TowerType == TowerType.earth)
        {
            price = Local.Instance.EarthTowerSellPrice(_openerSlot.Tower.CurrentLevel);
            Local.Instance.NumberOfEarthTowers--;
        }
        else if (_openerSlot.Tower.TowerType == TowerType.air)
        {
            price = Local.Instance.AirTowerSellPrice(_openerSlot.Tower.CurrentLevel);
            Local.Instance.NumberOfAirTowers--;
        }

        _openerSlot.DestroyTower();

        Local.Instance.Gold += price;


        ScreenManager.Instance.ClosePopUp("SlotFullPopUp");

    }



    public override void Configurations()
    {
        AfterOpen = AfterOpen_;
        BeforeOpen = BeforeOpen_;

        //BeforeClose = BeforeClose_;
    }


    private IEnumerator BeforeOpen_()
    {
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
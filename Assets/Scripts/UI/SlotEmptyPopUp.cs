using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using ScreenManagerNS;

public class SlotEmptyPopUp :PopUp
{

    private Slot _openerSlot;

    public Tower fireTower;
    public Tower waterTower;
    public Tower airTower;
    public Tower earthTower;

    public TextMeshProUGUI fireTowerCost;
    public TextMeshProUGUI waterTowerCost;
    public TextMeshProUGUI earthTowerCost;
    public TextMeshProUGUI airTowerCost;



    public void Open(Slot slot)
    {
        _openerSlot = slot;
        ScreenManager.Instance.CloseAllPopUpWithout("SlotEmptyPopUp");
        ScreenManager.Instance.OpenPopUp("SlotEmptyPopUp");

        fireTowerCost.text = "Gold(" +Local.Instance.FireTowerCost+ ")";
        waterTowerCost.text = "Gold(" +Local.Instance.WaterTowerCost + ")";
        earthTowerCost.text = "Gold(" +Local.Instance.EarthTowerCost + ")";
        airTowerCost.text = "Gold(" +Local.Instance.AirTowerCost + ")";

    }


    public void OnClickBuyFireTower()
    {
        if (Local.Instance.Gold >= Local.Instance.FireTowerCost)
        {
            _openerSlot.AddTower(fireTower);
            ScreenManager.Instance.ClosePopUp("SlotEmptyPopUp");
        }
    }

    public void OnClickBuyWaterTower()
    {
        if (Local.Instance.Gold >= Local.Instance.WaterTowerCost)
        {
            _openerSlot.AddTower(waterTower);
            ScreenManager.Instance.ClosePopUp("SlotEmptyPopUp");
        }
    }

    public void OnClickBuyEarthTower()
    {
        if (Local.Instance.Gold >= Local.Instance.EarthTowerCost)
        {
            _openerSlot.AddTower(earthTower);
            ScreenManager.Instance.ClosePopUp("SlotEmptyPopUp");
        }
    }

    public void OnClickBuyAirTower()
    {
        if (Local.Instance.Gold >= Local.Instance.AirTowerCost)
        {
            _openerSlot.AddTower(airTower);
            ScreenManager.Instance.ClosePopUp("SlotEmptyPopUp");
        }
    }



    public override void Configurations()
    {

    }




}
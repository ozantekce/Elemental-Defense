using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using ScreenManagerNameSpace;

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
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
    private Dictionary<TowerType, Tower> _towerTypeToTower = new Dictionary<TowerType, Tower>();

    public TextMeshProUGUI fireTowerCost;
    public TextMeshProUGUI waterTowerCost;
    public TextMeshProUGUI earthTowerCost;
    public TextMeshProUGUI airTowerCost;


    private void Start()
    {
        _towerTypeToTower[TowerType.Fire] = fireTower;
        _towerTypeToTower[TowerType.Water] = waterTower;
        _towerTypeToTower[TowerType.Air] = airTower;
        _towerTypeToTower[TowerType.Earth] = earthTower;

        ExtendedText.SetTextMethod("NewFireTowerButtonText", "Gold2", 
            () => Local.Instance.NewTowerCost(TowerType.Fire));
        ExtendedText.SetTextMethod("NewWaterTowerButtonText", "Gold2",
            () => Local.Instance.NewTowerCost(TowerType.Water));
        ExtendedText.SetTextMethod("NewEarthTowerButtonText", "Gold2",
            () => Local.Instance.NewTowerCost(TowerType.Earth));
        ExtendedText.SetTextMethod("NewAirTowerButtonText", "Gold2",
            () => Local.Instance.NewTowerCost(TowerType.Air));

    }

    public void Open(Slot slot)
    {
        _openerSlot = slot;
        //ScreenManager.Instance.CloseAllPopUpWithout("SlotEmptyPopUp");
        ScreenManager.Instance.OpenPopUp("SlotEmptyPopUp",0,true);

    }


    public void OnClickNewTowerButton(string towerType)
    {
        TowerType type = EnumHelper.StringToEnum<TowerType>(towerType);
        int price = Local.Instance.NewTowerCost(type);
        if (Local.Instance.Gold >= price)
        {
            _openerSlot.AddTower(_towerTypeToTower[type]);
            ScreenManager.Instance.ClosePopUp("SlotEmptyPopUp");
        }
    }
    



}
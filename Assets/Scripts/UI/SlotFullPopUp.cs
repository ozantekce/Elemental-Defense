using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using ScreenManagerNS;

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
        //ScreenManager.Instance.CloseAllPopUpWithout("SlotFullPopUp");
        "SlotFullPopUp".OpenPopUp(0,true);


        UpdateTexts();

    }


    private void UpdateTexts()
    {

        _infoText.text = _openerSlot.Tower.GetTowerInfo();

        _title.text = _openerSlot.Tower.Name.ToUpperInvariant() + "(" + _openerSlot.Tower.CurrentLevel + ")";

        
        _updateButtonText.text = "Gold2".ExecuteFormat(Local.Instance.TowerUpdateCost(_openerSlot.Tower.CurrentLevel));

        _sellButtonText.text = "Gold2".ExecuteFormat(Local.Instance.TowerSellPrice(_openerSlot.Tower.CurrentLevel));

    }



    public void OnClickUpdateButton()
    {
        int price = 0;

        price = Local.Instance.TowerUpdateCost(_openerSlot.Tower.CurrentLevel);

        if (Local.Instance.Gold >= price)
        {
            _openerSlot.UpdateTower(price);
        }

        UpdateTexts();
    }


    public void OnClickSellButton()
    {

        int price 
            = Local.Instance.TowerSellPrice(_openerSlot.Tower.CurrentLevel);

        Local.Instance.DecreaseNumberOfTower(_openerSlot.Tower.TowerType);

        _openerSlot.DestroyTower();

        Local.Instance.Gold += price;

        "SlotFullPopUp".ClosePopUp();
    }




}
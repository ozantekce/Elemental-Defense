using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using ScreenManagerNameSpace;

public class ElementsPopUp : PopUp
{


    [SerializeField]
    private TextMeshProUGUI _fireInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _waterInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _earthInfoTitleText;
    [SerializeField]
    private TextMeshProUGUI _airInfoTitleText;


    [SerializeField]
    private TextMeshProUGUI _fireInfoText;
    [SerializeField]
    private TextMeshProUGUI _waterInfoText;
    [SerializeField]
    private TextMeshProUGUI _earthInfoText;
    [SerializeField]
    private TextMeshProUGUI _airInfoText;


    [SerializeField]
    private TextMeshProUGUI _fireButtonText;
    [SerializeField]
    private TextMeshProUGUI _waterButtonText;
    [SerializeField]
    private TextMeshProUGUI _earthButtonText;
    [SerializeField]
    private TextMeshProUGUI _airButtonText;




    private void Start()
    {
        UpdateTexts();
    }




    private void UpdateTexts()
    {

        // Update Buttons
        _fireButtonText.text = "Essence(" + Local.Instance.ElementCost(Local.Instance.FireLevel) + ")";
        _waterButtonText.text = "Essence(" + Local.Instance.ElementCost(Local.Instance.WaterLevel) + ")";
        _earthButtonText.text = "Essence(" + Local.Instance.ElementCost(Local.Instance.EarthLevel) + ")";
        _airButtonText.text = "Essence(" + Local.Instance.ElementCost(Local.Instance.AirLevel) + ")";
        //end


        // Update titles
        _fireInfoTitleText.text = "Fire(Lv." + Local.Instance.FireLevel + ")";
        _waterInfoTitleText.text = "Water(Lv." + Local.Instance.WaterLevel + ")";
        _earthInfoTitleText.text = "Earth(Lv." + Local.Instance.EarthLevel + ")";
        _airInfoTitleText.text = "Air(Lv." + Local.Instance.AirLevel + ")";
        // end


        // Update Info

        _fireInfoText.text = "Increased fire towers damage by <color=red>" + Local.Instance.FireEffect*100 + "%</color>";
        _waterInfoText.text = "Increased water towers slow by <color=red>" + Local.Instance.WaterEffect*100 + "%</color>";
        _earthInfoText.text = "Increased earth towers stun change by <color=red>" + Local.Instance.EarthEffect*100 + "%</color>";
        _airInfoText.text = "Increased air towers messy attack rate by <color=red>" + Local.Instance.AirEffect*100 + "%</color>";
        //end



    }



    public void OnClickFireButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ElementCost(Local.Instance.FireLevel))
        {
            Local.Instance.Essence -= Local.Instance.ElementCost(Local.Instance.FireLevel);
            Local.Instance.FireLevel++;
            UpdateTexts();
        }
    }

    public void OnClickWaterButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ElementCost(Local.Instance.WaterLevel))
        {
            Local.Instance.Essence -= Local.Instance.ElementCost(Local.Instance.WaterLevel);
            Local.Instance.WaterLevel++;
            UpdateTexts();
        }
    }

    public void OnClickEarthButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ElementCost(Local.Instance.EarthLevel))
        {
            Local.Instance.Essence -= Local.Instance.ElementCost(Local.Instance.EarthLevel);
            Local.Instance.EarthLevel++;
            UpdateTexts();
        }
    }

    public void OnClickAirButton()
    {
        if (Local.Instance.Essence >= Local.Instance.ElementCost(Local.Instance.AirLevel))
        {
            Local.Instance.Essence -= Local.Instance.ElementCost(Local.Instance.AirLevel);
            Local.Instance.AirLevel++;
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

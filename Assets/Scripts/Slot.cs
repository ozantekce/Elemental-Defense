using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{

    [SerializeField]
    private bool _isEmpty = true;

    private Tower _tower;


    public SlotEmptyPopUp slotEmptyPopUp;
    public SlotFullPopUp slotFullPopUp;

    public int slotNumber;


    public Tower fireTowerPrefab;
    public Tower waterTowerPrefab;
    public Tower earthTowerPrefab;
    public Tower airTowerPrefab;


    private void Start()
    {
        LoadTowerData();
    }

    public void SaveTowerData()
    {
        if(_tower == null)
        {
            return;
        }

        TowerData towerData = new TowerData();

        towerData.type = _tower.TowerType;
        towerData.currentLevel = _tower.CurrentLevel;

        string jsonStr = JsonConvert.SerializeObject(towerData);
        PlayerPrefs.SetString("Slot"+slotNumber, jsonStr);

    }

    public void LoadTowerData()
    {
        string json = PlayerPrefs.GetString("Slot" + slotNumber);
        if (string.IsNullOrEmpty(json))
            return;

        TowerData towerData = JsonConvert.DeserializeObject<TowerData>(json);

        Debug.Log("Load Tower");
        Tower tower;

        if (towerData.type == TowerType.fire)
        {
            tower = fireTowerPrefab;
        }
        else if (towerData.type == TowerType.water)
        {
             tower = waterTowerPrefab;
        }
        else if (towerData.type == TowerType.earth)
        {
            tower = earthTowerPrefab;
        }
        else if (towerData.type == TowerType.air)
        {
            tower = airTowerPrefab;
        }
        else
        {
            Debug.Log("Error");
            return;
        }

        _tower = GameObject.Instantiate(tower);
        _tower.transform.SetParent(transform);
        _tower.transform.localPosition = Vector3.zero;

        _isEmpty = false;

        
    }



    private void OnMouseDown()
    {
        if (CursorOnUI)
        {
            return;
        }

        if (_isEmpty)
            slotEmptyPopUp.Open(this);
        else
            slotFullPopUp.Open(this);
    }

    public bool CursorOnUI
    {
        get
        {
#if UNITY_ANDROID && !UNITY_EDITOR
    if (EventSystem.current.IsPointerOverGameObject())
        return true;
 
    for (int touchIndex = 0; touchIndex < Input.touchCount; touchIndex++)
    {
        Touch touch = Input.GetTouch(touchIndex);
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return true;
    }
 
    return false;
#endif
            return EventSystem.current.IsPointerOverGameObject();
        }

    }


    public void UpdateTower()
    {
        Debug.Log("Update Tower");



        SaveTowerData();
    }

    public void AddTower(Tower tower)
    {
        Debug.Log("Add Tower");

        if(tower.TowerType == TowerType.fire)
        {
            Local.Instance.Gold -= Local.Instance.FireTowerCost;
            Local.Instance.NumberOfFireTowers++;
        }
        else if(tower.TowerType == TowerType.water)
        {
            Local.Instance.Gold -= Local.Instance.WaterTowerCost;
            Local.Instance.NumberOfWaterTowers++;
        }
        else if(tower.TowerType == TowerType.earth)
        {
            Local.Instance.Gold -= Local.Instance.EarthTowerCost;
            Local.Instance.NumberOfEarthTowers++;
        }
        else if(tower.TowerType == TowerType.air)
        {
            Local.Instance.Gold -= Local.Instance.AirTowerCost;
            Local.Instance.NumberOfAirTowers++;
        }

        _tower = GameObject.Instantiate(tower);
        _tower.transform.SetParent(transform);
        _tower.transform.localPosition = Vector3.zero;

        _isEmpty = false;
        SaveTowerData();
    }

    public void DestroyTower()
    {
        Debug.Log("Destroy Tower");




        SaveTowerData();

    }


}

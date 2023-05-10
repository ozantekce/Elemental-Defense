using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{

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

    public IEnumerator SaveTowerData()
    {

        yield return new WaitForEndOfFrame();

        if(_tower == null)
        {
            PlayerPrefs.DeleteKey("Slot" + slotNumber);
            yield break;
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

        //Debug.Log("Load Tower");
        Tower tower;

        if (towerData.type == TowerType.Fire)
        {
            tower = fireTowerPrefab;
        }
        else if (towerData.type == TowerType.Water)
        {
             tower = waterTowerPrefab;
        }
        else if (towerData.type == TowerType.Earth)
        {
            tower = earthTowerPrefab;
        }
        else if (towerData.type == TowerType.Air)
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

        _tower.CurrentLevel = towerData.currentLevel;

        
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

    private void OnMouseEnter()
    {
        if (CursorOnUI)
        {
            return;
        }

        if (!GameManager.Instance.RangeArea.active && _tower!=null)
        {
            _tower.ShowRange();
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.Instance.RangeArea.active && _tower!=null)
        {
            _tower.HideRange();
        }
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

    

    public void UpdateTower(int cost)
    {
        Debug.Log("Update Tower");

        Local.Instance.Gold -= cost;
        _tower.UpdateTower();

        StartCoroutine(SaveTowerData());
    }

    public void AddTower(Tower tower)
    {
        Debug.Log("Add Tower");

        Local.Instance.Gold -= Local.Instance.NewTowerCost(tower.TowerType);
        Local.Instance.IncreaseNumberOfTower(tower.TowerType);

        _tower = GameObject.Instantiate(tower);
        _tower.transform.SetParent(transform);
        _tower.transform.localPosition = Vector3.zero;

        StartCoroutine(SaveTowerData());
    }

    public void DestroyTower()
    {
        Debug.Log("Destroy Tower");

        Destroy(_tower.gameObject);

        _tower = null;
        StartCoroutine(SaveTowerData());

    }


    #region GetterSetter
    private bool _isEmpty{get { return _tower == null; }}
    public Tower Tower { get => _tower; set => _tower = value; }

    #endregion


}

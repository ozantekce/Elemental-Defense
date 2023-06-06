using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Tower : MonoBehaviour
{


    [SerializeField]
    private string _name;
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private Bullet _bulletPrefab;


    [SerializeField]
    private TowerType _towerType;


    private static List<Tower> AllTowers = new List<Tower>();


    #region BaseFeatureData
    private TowerFeatureData _towerBaseFeatureData;
    #endregion

    #region UpdateIncreaseFeatureData
    private TowerFeatureData _towerIncreaseFeatureData;
    #endregion

    #region CurrentFeatureData
    private TowerFeatureData _towerCurrentFeatureData;
    #endregion




    private int _currentLevel;

    private CooldownDynamic _attackCD;

    private void Awake()
    {
        _towerBaseFeatureData = Local.Instance.GetBaseFeatureData(this);
        _towerIncreaseFeatureData = Local.Instance.GetIncreaseFeatureData(this);
        _towerCurrentFeatureData = new TowerFeatureData();
        _nextLevelFeatureData = new TowerFeatureData();
        AllTowers.Add(this);
    }

    private void OnDestroy()
    {
        AllTowers.Remove(this);
    }

    private void Start()
    {
        _attackCD = new CooldownDynamic();
        UpdateFeatureData();
    }


    private Enemy _currentEnemy;
    private void Update()
    {

        //UpdateValues();
        
        if(_currentEnemy == null
            || DistanceSqrWithEnemy ()>= Range*Range)
            _currentEnemy = FindEnemy();

        if(_attackCD.Ready( (1000 / AttackPerSecond)/Local.Instance.GameSpeed) && _currentEnemy != null)
        {
            SendBullet(_currentEnemy);
        }

    }

    private float DistanceSqrWithEnemy()
    {

        Vector2 enemyVector;
        enemyVector.x = _currentEnemy.transform.position.x;
        enemyVector.y = _currentEnemy.transform.position.z;

        Vector2 towerVector;
        towerVector.x = transform.position.x;
        towerVector.y = transform.position.z;

        return Vector2.SqrMagnitude(towerVector - enemyVector);


    }


    private void UpdateFeatureData()
    {

        Range = (BaseRange + IncreaseRange * _currentLevel) * Local.Instance.ResearchEffect(Research.Range);
        AttackPower = (BaseAttackPower + IncreaseAttackPower * _currentLevel ) * Local.Instance.ResearchEffect(Research.Damage);
        if (TowerType == TowerType.Fire)
        {
            AttackPower *= (1f + Local.Instance.ElementEffect(Element.Fire));
        }

        AttackPerSecond = (BaseAttackPerSecond + IncreaseAttackPerSecond * _currentLevel) * Local.Instance.ResearchEffect(Research.AttackSpeed);
        CriticalChange = (BaseCriticalChange + IncreaseCriticalChange * _currentLevel) + Local.Instance.ResearchEffect(Research.CriticalHitChange);
        CriticalDamage = (BaseCriticalDamage + IncreaseCriticalDamage * _currentLevel) * Local.Instance.ResearchEffect(Research.CriticalHitDamage);

    }


    public static void UpdateAllTowersFeatureData()
    {
        for (int i = 0; i < AllTowers.Count; i++)
        {
            AllTowers[i].UpdateFeatureData();
        }
    }

    public static void DestroyAllTowers()
    {
        for (int i = 0; i < AllTowers.Count; i++)
        {
            Destroy(AllTowers[i].gameObject);
        }
    }

    private TowerFeatureData _nextLevelFeatureData;
    private TowerFeatureData NextLevelFeatureData()
    {

        _nextLevelFeatureData.range 
            = (BaseRange + IncreaseRange * (_currentLevel+1) ) * Local.Instance.ResearchEffect(Research.Range);
        _nextLevelFeatureData.attackPower = (BaseAttackPower + IncreaseAttackPower * (_currentLevel + 1)) * Local.Instance.ResearchEffect(Research.Damage);
        if (TowerType == TowerType.Fire)
        {
            _nextLevelFeatureData.attackPower *= (1f + Local.Instance.ElementEffect(Element.Fire));
        }

        _nextLevelFeatureData.attackPerSecond = (BaseAttackPerSecond + IncreaseAttackPerSecond * (_currentLevel + 1)) * Local.Instance.ResearchEffect(Research.AttackSpeed);
        _nextLevelFeatureData.criticalChange = (BaseCriticalChange + IncreaseCriticalChange * (_currentLevel + 1)) + Local.Instance.ResearchEffect(Research.CriticalHitChange);
        _nextLevelFeatureData.criticalDamage = (BaseCriticalDamage + IncreaseCriticalDamage * (_currentLevel + 1)) * Local.Instance.ResearchEffect(Research.CriticalHitDamage);

        return _nextLevelFeatureData;
    }


    private void SendBullet(Enemy target)
    {

        float damage = AttackPower;
        int r = Random.Range(0, 101);
        bool crit = false;
        if (r < CriticalChange)
        {
            // Critical Attack
            damage *= (1 + CriticalDamage);
            crit = true;
        }

        SoundManager.Instance.PlaySoundClip("TowerShot");

        Poolable poolable = Poolable.GetFromPool(_bulletPrefab);
        Bullet bullet = (Bullet)poolable;
        bullet.InitBullet(_bulletSpawnPoint.position,damage,crit,this,target);
        
    }


    private Enemy FindEnemy()
    {
        float minDistance = float.MaxValue;
        Enemy target = null;
        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if(dist<=Range &&dist < minDistance)
            {
                minDistance = dist;
                target = enemy;
            }
        }

        return target;
    }


    public void UpdateTower()
    {
        CurrentLevel++;
        UpdateFeatureData();

        SoundManager.Instance.PlaySoundClip("TowerLevelUp");

    }



    public void ShowRange()
    {
        if(GameManager.Instance.RangeArea != null)
        {

            GameManager.Instance.RangeArea.SetActive(true);
            Vector3 temp;
            temp.x = transform.position.x;
            temp.y = 2;
            temp.z = transform.position.z;
            GameManager.Instance.RangeArea.transform.position = temp;
            GameManager.Instance.RangeArea.transform.localScale = new Vector3(Range, Range, 0);
        }

    }

    public void HideRange()
    {
        if (GameManager.Instance.RangeArea != null)
        {

            GameManager.Instance.RangeArea.SetActive(false);
            //GameManager.Instance.RangeArea.transform.localScale = new Vector3(Range, Range, 0);
        }

    }


    public string GetTowerInfo()
    {
        TowerFeatureData nextLevel = NextLevelFeatureData();

        return "TowerInfo".ExecuteFormat(AttackPower, AttackPerSecond, CriticalChange, 100 * CriticalDamage, Range, nextLevel.attackPower, nextLevel.attackPerSecond, nextLevel.criticalChange, 100 * nextLevel.criticalDamage, nextLevel.range);
    }



    #region GetterSetter

    public float AttackPower { get => _towerCurrentFeatureData.attackPower; set => _towerCurrentFeatureData.attackPower = value; }
    public float Range { get => _towerCurrentFeatureData.range; set => _towerCurrentFeatureData.range = value; }
    public float AttackPerSecond { get => _towerCurrentFeatureData.attackPerSecond; set => _towerCurrentFeatureData.attackPerSecond = value; }
    public float CriticalChange { get => _towerCurrentFeatureData.criticalChange; set => _towerCurrentFeatureData.criticalChange = value; }
    public float CriticalDamage { get => _towerCurrentFeatureData.criticalDamage; set => _towerCurrentFeatureData.criticalDamage = value; }
    public TowerType TowerType { get => _towerType; set => _towerType = value; }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public string Name { get => _name; set => _name = value; }


    public float BaseRange { get => _towerBaseFeatureData.range; }
    public float BaseAttackPower { get => _towerBaseFeatureData.attackPower; }
    public float BaseAttackPerSecond { get => _towerBaseFeatureData.attackPerSecond; }
    public float BaseCriticalChange { get => _towerBaseFeatureData.criticalChange; }
    public float BaseCriticalDamage { get => _towerBaseFeatureData.criticalDamage; }


    public float IncreaseRange { get => _towerIncreaseFeatureData.range; }
    public float IncreaseAttackPower { get => _towerIncreaseFeatureData.attackPower; }
    public float IncreaseAttackPerSecond { get => _towerIncreaseFeatureData.attackPerSecond; }
    public float IncreaseCriticalChange { get => _towerIncreaseFeatureData.criticalChange; }
    public float IncreaseCriticalDamage { get => _towerIncreaseFeatureData.criticalDamage; }

    #endregion


}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{

    private static ObjectPoolManager _instance; 

    private Dictionary<string, Queue<Poolable>> _pools = new Dictionary<string, Queue<Poolable>>();

    private Dictionary<string,GameObject> _holders = new Dictionary<string, GameObject>();

    public static ObjectPoolManager Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        _instance = this;
    }

    public void AddToPool(Poolable poolable)
    {

        string key = poolable.Key;

        if (!_pools.ContainsKey(key))
            AddNewKey(key);

        poolable.MonoBehaviour.transform.SetParent(_holders[key].transform);
        poolable.MonoBehaviour.gameObject.SetActive(false);
        _pools[key].Enqueue(poolable);

        poolable.Pooled = true;


    }

    public Poolable GetFromPool(Poolable poolableInstance)
    {
        string key = poolableInstance.Key;

        if (!_pools.ContainsKey(key))
            AddNewKey(key);

        Poolable poolable = null;
        
        Queue < Poolable > pool = _pools[key];
        if (pool.Count==0)
        {
            GameObject gameObject = null;
            Debug.Log("new created "+key);
            gameObject = Instantiate(poolableInstance.MonoBehaviour.gameObject);
            AddToPool(gameObject.GetComponent<Poolable>());
        }

        poolable = pool.Dequeue();

        poolable.MonoBehaviour.transform.SetParent(null);
        poolable.MonoBehaviour.gameObject.SetActive(true);
        poolable.Pooled = false;

        return poolable;

    }


    private void AddNewKey(string key)
    {
        _pools[key] = new Queue<Poolable>();
        _holders[key] = new GameObject(key);
    }


}

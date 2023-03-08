using UnityEngine;

public interface Poolable
{

    public string Key { get; set; }
    public MonoBehaviour MonoBehaviour { get;}
    public bool Pooled { get; set; }
    public void SendToPool()
    {
        ObjectPoolManager.Instance.AddToPool(this);
        Pooled = true;
    }

}

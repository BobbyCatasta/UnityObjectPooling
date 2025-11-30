using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager<T> : Singleton<ObjectPoolManager<T>>
{
    [SerializeField] private List<PoolableObject> poolableItems;
    private Dictionary<string,Queue<T>> pools = new Dictionary<string, Queue<T>>();

    private void Start()
    {
        InitPools();
    }
    protected void InitPools()
    {
        for(int i = 0; i < poolableItems.Count; i++)
            pools.Add(poolableItems[i].key, PopulatePool(poolableItems[i]));
    }

    private Queue<T> PopulatePool(PoolableObject item)
    {
        Queue<T> pool = new Queue<T>();
        for (int i = 0; i < item.quantity; i++)
        {
            if (item.pooledObject is GameObject)
            {
                GameObject go = Instantiate(item.pooledObject as GameObject, transform);
                go.SetActive(false);
                dynamic obj = go;
                pool.Enqueue(obj);
            }
            else
                pool.Enqueue(item.pooledObject);
        }
        return pool;
    }

    public T GetInstanceOfObject(in string key)
    {
        if (pools.ContainsKey(key))
        {
            if (pools[key].Peek() is GameObject)
            {
                GameObject go = pools[key].Peek() as GameObject;
                if (!go.activeInHierarchy)
                    return GiveInstance(key);
                else
                    return ExpandPool(key);
            }
            // No GameObject
            if (pools[key].Count > 0)
                return pools[key].Dequeue();
            else
                ExpandPool(key);
            return default;
        }
        else
        {
            PoolableObject item = GetItemFromListPoolableObjects(key);
            if (item != null && item.key == key)
            {
                pools.Add(key,PopulatePool(item));
                return GiveInstance(key);
            }
            Debug.Log("An item with the given key doesn't exist");
            return default;
        }
    }

    private T ExpandPool(in string key)
    {
        PoolableObject item = GetItemFromListPoolableObjects(key);
        if (item.canGrow)
        {
            dynamic obj;
            if (item.pooledObject is GameObject)
            {
                GameObject go = Instantiate(item.pooledObject as GameObject, transform);
                go.SetActive(true);
                obj = go;
                pools[key].Enqueue(obj);
                return obj;
            }
            obj = pools[key].Dequeue();
            return obj;
        }
        Debug.LogWarning("Pool can't be enlarged");
        return default;
    }

    private PoolableObject GetItemFromListPoolableObjects(in string key)
    {
        foreach(var item in poolableItems)
        {
            if (key.ToUpper() == item.key.ToUpper())
                return item;
        }
        Debug.LogWarning("No item has been found in the PoolableItems");
        return default;
    }

    private T GiveInstance(in string key)
    {
        if (pools[key].Peek() is GameObject)
        {
            GameObject go = pools[key].Dequeue() as GameObject;
            go.SetActive(true);
            dynamic obj = go;
            pools[key].Enqueue(obj);
            return obj;
        }
        else
            return pools[key].Dequeue();
    }

    public void ClearAllNullObjects()
    {
        pools.Clear();
    }

    public List<string> GetListOfTags()
    {
        List<string> tags = new List<string>();
        foreach(var pool in pools)
            tags.Add(pool.Key);
        return tags;
    }


    [Serializable]
    private class PoolableObject
    {
        public string key;
        public T pooledObject;
        public int quantity;
        public bool canGrow;

    }
}




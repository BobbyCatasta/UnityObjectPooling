using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] private ScriptablePool scriptablePool;
    private List<Pool> pools = new List<Pool>();

    protected override void Awake()
    {
        base.Awake();
        foreach (var obj in scriptablePool.gameObjectsToPool)
            CreatePool(obj.gameObject, obj.quantity,obj.typePool);
    }
    private void CreatePool(GameObject gameObject, int quantity, TypePool poolType)
    {
        foreach(var pool in pools)
        {
            if(pool.prefabToSpawn == gameObject)
                return;
        }
        Pool newPool = new Pool(gameObject, quantity, poolType);
        pools.Add(newPool);
        InstantiatePool(newPool);
    }

    private void InstantiatePool(Pool pool)
    {
        GameObject go;
        for (int i = 0; i < pool.quantity; i++)
        {
            go = Instantiate(pool.prefabToSpawn);
            pool.instantiatedGameObjects.Enqueue(go);
            go.SetActive(false);
        }
    }

    public GameObject GetPoolObject(GameObject gameObject)
    {
        foreach(Pool pool in pools)
        {
            if(pool.prefabToSpawn == gameObject)
            {
                if (pool.instantiatedGameObjects.Count > 0)
                {
                    Debug.LogWarning("Object Dequeued correctly.");
                    return pool.instantiatedGameObjects.Dequeue();
                }
                else
                {
                    Debug.LogWarning("Not enough gameobjects in the pool.");
                    return null;
                }
            }
        }
        throw new NotImplementedException();
    }

    public void DeactivatePoolObject(GameObject gameObject, TypePool typePool)
    {
        gameObject.SetActive(false);
        foreach (Pool pool in pools)
        {
            if (pool.typePool == typePool)
            {
                pool.instantiatedGameObjects.Enqueue(gameObject);
                Debug.LogWarning("Object Enqueued correctly.");
            }
        }
    }
}

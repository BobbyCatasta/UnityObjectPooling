using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public GameObject prefabToSpawn;
    public Queue<GameObject> instantiatedGameObjects;
    public int quantity;

    public TypePool typePool;

    public Pool(GameObject prefabToSpawn, int quantity, TypePool typePool)
    {
        this.prefabToSpawn = prefabToSpawn;
        this.quantity = quantity;
        instantiatedGameObjects = new Queue<GameObject>();
        this.typePool = typePool;
    }
}

public interface IPoolable
{
    public void EnableObject();
    public void DisableObject();
}

public enum TypePool
{
    CUBE,
    SPHERE
}

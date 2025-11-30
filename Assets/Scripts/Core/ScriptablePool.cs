using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptablePool")]
public class ScriptablePool : ScriptableObject
{
    public PoolableObject[] gameObjectsToPool; 
}

[Serializable]
public struct PoolableObject
{
    [SerializeField] public GameObject gameObject;
    [SerializeField] public int quantity;
    [SerializeField] public TypePool typePool;
}

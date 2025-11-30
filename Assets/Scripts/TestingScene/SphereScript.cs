using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, IPoolable
{
    [SerializeField] private TypePool typePool;

    private void OnEnable()
    {
        StartCoroutine(DisableCoroutine());
    }
    public void DisableObject()
    {
        ObjectPool.Instance.DeactivatePoolObject(gameObject, typePool);
    }

    public void EnableObject()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0, 2));
        DisableObject();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour, IPoolable
{
    [SerializeField] private TypePool typeForPoolIndexing;
    float speed = 0.01f;
    Vector3 direction;
    public void DisableObject()
    {
        ObjectPool.Instance.DeactivatePoolObject(gameObject, typeForPoolIndexing);
    }

    private void OnEnable()
    {
        //StartCoroutine(DisableCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            gameObject.SetActive(false);
    }

    public void EnableObject()
    {
        gameObject.SetActive(true);
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] GameObject cube, sphere;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        GameObject go = null;
        if (Input.GetKeyDown(KeyCode.S))
        {
            go = ObjectPool.Instance.GetPoolObject(cube);
            if(go != null)
            {
                go.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                go.GetComponent<IPoolable>().EnableObject();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            go = ObjectPool.Instance.GetPoolObject(sphere);
            if (go != null)
            {
                go.GetComponent<IPoolable>().EnableObject();
            }
        }
    }
}

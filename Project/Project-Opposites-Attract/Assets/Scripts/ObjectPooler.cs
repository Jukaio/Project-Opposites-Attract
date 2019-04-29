using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject prefab;
    public int poolAmount;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ActivateObj();
        }
    }

    public void ActivateObj()
    {
        GameObject obj = GetObj();
        if (obj != null)
        {
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }
    }

    public GameObject GetObj()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        GameObject obj = Instantiate(prefab);
        pooledObjects.Add(obj);
        return obj;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject prefab;
    public int poolAmount;
    public Transform spawn;

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

    public void ActivateObj(Vector2 direction)
    {
        GameObject obj = GetObj();
        if (obj != null)
        {
            obj.transform.position = spawn.transform.position;
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

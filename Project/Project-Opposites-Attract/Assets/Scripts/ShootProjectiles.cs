﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectiles : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    Mechanics mechanics;
    Transform projectileSpawn;
    int poolAmount;


    private void Start()
    {
        mechanics = GetComponent<Mechanics>();
        poolAmount = mechanics.mechanic1poolAmount;
        projectileSpawn = mechanics.projectileSpawn;

        Debug.Log(mechanics.mechanic1poolAmount + " in class");
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(mechanics.mechanic1prefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            Debug.Log(i + "hello");
        }
    }

    public void ActivateObj(Vector2 direction)
    {
        GameObject obj = GetObj();
        if (obj != null)
        {
            obj.transform.position = projectileSpawn.transform.position;
            obj.GetComponent<Projectile>().MoveProjectile(direction);
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
        //GameObject obj = Instantiate(mechanics.mechanic1prefab);
        //pooledObjects.Add(obj);
        //return obj;
        return null;
    }

}

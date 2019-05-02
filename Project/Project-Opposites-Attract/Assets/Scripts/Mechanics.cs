﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mechanics : MonoBehaviour
{
    public float rangeDist;
    public float movementSpeed;
    public Vector2 throwHight;
    public Transform parent;

    public Tilemap tilemap;
    public Tile tile;

    public Vector2 tileSpawnPosition;

    public Transform projectileSpawn;

    //Mechanic 1
    public bool mechanic1;
    public GameObject mechanic1prefab;
    public int mechanic1poolAmount;
    ShootProjectiles Mechanic1;

    private void Start()
    {
        Debug.Log(mechanic1poolAmount + " in start");
        if (mechanic1)
        {
            Mechanic1 = gameObject.AddComponent<ShootProjectiles>();
        }

    }

    public void MoveLeft()
    {
        transform.Translate(new Vector2(-movementSpeed, 0f));
    }

    public void MoveRight()
    {
        transform.Translate(new Vector2(movementSpeed, 0f));
    }

    public void GrabAttach(GameObject obj1, GameObject obj2)
    {
        obj2.transform.parent = obj1.transform;
    }

    public void GrabDeattach(GameObject obj)
    {
        obj.transform.parent = parent;
    }

    public void Throw(GameObject obj2)
    {
        obj2.GetComponent<Rigidbody2D>().AddForce(throwHight, ForceMode2D.Impulse);
    }

    public bool InRange(GameObject obj1, GameObject obj2) //just range
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= rangeDist;
    }

    public void shootProjectile(Vector2 direction)
    {
        Mechanic1.ActivateObj(direction);
    }


}

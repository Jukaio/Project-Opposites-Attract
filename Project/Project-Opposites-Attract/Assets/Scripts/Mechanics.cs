﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mechanics : MonoBehaviour
{
    public float rangeDist;
    public float movementSpeed;
    public Vector2 throwHeight;
    public Transform parent;
    
    public Tilemap tilemap;
    public Tile tile;

    public Vector2 tileSpawnPosition;

    public Transform projectileSpawn;

    //Mechanic 1
    public bool mechanic1;
    public GameObject mechanic1prefab;
    public int mechanic1poolAmount;
    ShootProjectiles shootStuff;

    public CheckpointController checkPointController;

    private void Start()
    {
        Debug.Log(mechanic1poolAmount + " in start");
        if (mechanic1)
        {
            shootStuff = gameObject.AddComponent<ShootProjectiles>();
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
        if(obj2.transform.position.x >= transform.position.x)
            obj2.GetComponent<Rigidbody2D>().AddForce(throwHeight, ForceMode2D.Impulse);
        else if(obj2.transform.position.x <= transform.position.x)
            obj2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-throwHeight.x, throwHeight.y), ForceMode2D.Impulse);
    }

    public void ChargeThrow(GameObject obj2, float factor)
    {
        if (obj2.transform.position.x >= transform.position.x)
            obj2.GetComponent<Rigidbody2D>().AddForce(throwHeight * factor, ForceMode2D.Impulse);
        else if (obj2.transform.position.x <= transform.position.x)
            obj2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-throwHeight.x, throwHeight.y) * factor, ForceMode2D.Impulse);
    }

    public bool InRange(GameObject obj1, GameObject obj2) //just range
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= rangeDist;
    }

    public IEnumerator ShootProjectile(Vector2 direction)
    {
        shootStuff.ActivateObj(direction);
        print("start wait");
        yield return new WaitForSeconds(2f);
        print("end wait");
    }

    public void RespawnOnPosition()
    {
        transform.position = checkPointController.currentPoint;
    }
}

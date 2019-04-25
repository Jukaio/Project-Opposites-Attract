using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CreateTile : MonoBehaviour
{
    public GameObject tilemapGameObject;
    public Tile tile;

    Tilemap tilemap;

    void Start()
    {
        tilemap = tilemapGameObject.GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            CreateTiles();
        }
    }

    void CreateTiles()
    {
        Vector3 hitPosition = Vector3.zero;

        hitPosition.x = gameObject.transform.position.x - 0.01f * gameObject.transform.position.x + 2;
        hitPosition.y = gameObject.transform.position.y - 0.01f * gameObject.transform.position.y;

        tilemap.SetTile(tilemap.WorldToCell(hitPosition), tile);


    }
}

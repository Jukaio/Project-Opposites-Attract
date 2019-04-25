using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CreateTile : MonoBehaviour
{
    public GameObject tilemapGameObject;
    public Tile tile;

    public Vector2 position;
    Tilemap tilemap;

    void Start()
    {
        tilemap = tilemapGameObject.GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            CreateTiles(position);
        }
    }

    void CreateTiles(Vector2 pos)
    {
        Vector3 tilePosition = Vector3.zero;

        tilePosition.x = gameObject.transform.position.x - 0.01f * gameObject.transform.position.x + pos.x;
        tilePosition.y = gameObject.transform.position.y - 0.01f * gameObject.transform.position.y + pos.y;

        tilemap.SetTile(tilemap.WorldToCell(tilePosition), tile);
    }
}

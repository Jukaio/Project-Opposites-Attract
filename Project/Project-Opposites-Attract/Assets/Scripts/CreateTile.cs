using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CreateTile : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile;

    private Vector3 tilePos;
    public Vector2 position;

    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            CreateTiles(position);
        }
    }

    void CreateTiles(Vector2 position)
    {
        tilePos.x = gameObject.transform.position.x - 0.01f * gameObject.transform.position.x + position.x;
        tilePos.y = gameObject.transform.position.y - 0.01f * gameObject.transform.position.y + position.y;
        tilemap.SetTile(tilemap.WorldToCell(tilePos), tile);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mechanics : MonoBehaviour
{
    public float rangeDist;
    public float movementSpeed;
    public Vector2 throwHight;

    public Tilemap tilemap;
    public Tile tile;

    public Vector2 tileSpawnPosition;

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

    public void GrabDeattach(GameObject obj1, GameObject obj2)
    {
        obj2.transform.parent = obj1.transform.parent;
    }

    public void Throw(GameObject obj2)
    {
        obj2.GetComponent<Rigidbody2D>().AddForce(throwHight, ForceMode2D.Impulse);
    }

    public bool InRange(GameObject obj1, GameObject obj2) //just range
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= rangeDist;
    }

    void CreateTiles()
    {
        Vector3 tilePos = Vector3.zero;
        tilePos.x = gameObject.transform.position.x - 0.01f * gameObject.transform.position.x + tileSpawnPosition.x;
        tilePos.y = gameObject.transform.position.y - 0.01f * gameObject.transform.position.y + tileSpawnPosition.y;
        tilemap.SetTile(tilemap.WorldToCell(tilePos), tile);
    }

    void DestroyTiles(Collision2D collision)
    {
        Vector3 tilePos = Vector3.zero;
        foreach (ContactPoint2D collisionPoint in collision.contacts)
        {
            tilePos.x = collisionPoint.point.x - 0.01f * collisionPoint.normal.x;
            tilePos.y = collisionPoint.point.y - 0.01f * collisionPoint.normal.y;
            tilemap.SetTile(tilemap.WorldToCell(tilePos), null);
        }
    }
}

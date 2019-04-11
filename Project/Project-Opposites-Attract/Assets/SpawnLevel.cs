using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFrameNode
{
    GameObject WorldFrameObject;
    float x, y;


    public WorldFrameNode(string objectName, Transform parent, Vector2 position, Vector2 colliderSize)
    {
        GameObject obj = new GameObject(objectName);
        obj.AddComponent<BoxCollider2D>().size = colliderSize;
        obj.transform.parent = parent;
        obj.transform.position = position;
    }

}

public struct objectInformation
{
    public string name;
    public Vector2 cooridnates;
    public Vector2 colliderSize;

    public objectInformation(Vector2 position, Vector2 sizeCollider, string name)
    {
        this.name = name;
        cooridnates = position;
        colliderSize = sizeCollider;
    }
}



public class SpawnLevel : MonoBehaviour
{
    GameManager gameManager;


    public LinkedList<WorldFrameNode> worldFrameList = new LinkedList<WorldFrameNode>();
    public List<objectInformation> levelBuildData = new List<objectInformation>();

    public Camera mainCamera;

    void Start()
    {
        prepareInformation();

        levelBuildData = PrepareWorldFrameData(levelBuildData);

        buildWorldFrame();
    }




    void prepareInformation()
    {
        gameManager = transform.parent.gameObject.GetComponent<GameManager>();
        mainCamera = GameObjectExtensions.GetTInChildren<Camera>(gameManager.gameObject);
    }


    List<objectInformation> PrepareWorldFrameData(List<objectInformation> levelBuildData)
    {
        levelBuildData.Add(new objectInformation(new Vector2(transform.position.x, -12 + 0.25f),
            new Vector2(22 * 2f, 0.5f), "Ground"));
        levelBuildData.Add(new objectInformation(new Vector2(-22 - 0.25f, transform.position.x),
            new Vector2(0.5f, 12 * 2f), "Left"));
        levelBuildData.Add(new objectInformation(new Vector2(transform.position.x, 12 - 0.25f),
            new Vector2(22 * 2f, 0.5f), "Top"));
        levelBuildData.Add(new objectInformation(new Vector2(22 + 0.25f, transform.position.x),
            new Vector2(0.5f, 12 * 2f), "Right"));
        return levelBuildData;
    }

    void buildWorldFrame()
    {
        for (int i = 0; i < levelBuildData.Count; i++)
        {
            worldFrameList.AddLast(new WorldFrameNode(levelBuildData[i].name, transform, levelBuildData[i].cooridnates, levelBuildData[i].colliderSize));
        }
    }
    

}

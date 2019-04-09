using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions 
{
    public static GameObject GetChildGameObjectWithComponent<TComponent>(this GameObject gameObject)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            if (gameObject.transform.GetChild(i).GetComponent<TComponent>() != null)
                return gameObject.transform.GetChild(i).gameObject;

        return null;
    }

    public static T GetTInChildren<T>(GameObject gameObject)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<T>() != null)
                return gameObject.transform.GetChild(i).GetComponent<T>();
            else
                Debug.LogError("Can't find component in Object " + gameObject.transform.GetChild(i).name + " (GetTInChildren GameManager.cs Line 17)");
        }
        Debug.LogError("Can't find component in children (GetTInChildren GameManager.cs Line 17)");
        return default;
    }
}

public class GameManager : MonoBehaviour
{
    GameObject playerRed;
    GameObject playerBlue;
    GameObject playerParent;
    GameObject level;
    GameObject mainCamera;

    Camera mainCameraComp;

    void Start()
    {
        mainCamera = new GameObject("Main Camera");
        mainCamera.transform.parent = transform;
        mainCamera.AddComponent<Camera>();
        mainCamera.AddComponent<CameraSettings>();

        playerParent = new GameObject("Parent Player");
        playerParent.transform.parent = transform;
        playerParent.AddComponent<SpawnPlayers>();
        playerParent.AddComponent<PlayerController>();

        playerRed = new GameObject("Red Player");
        playerRed.AddComponent<Rigidbody2D>();
        playerRed.AddComponent<CapsuleCollider2D>();
        playerRed.transform.parent = playerParent.transform;

        playerBlue = new GameObject("Blue Player");
        playerBlue.AddComponent<Rigidbody2D>();
        playerBlue.AddComponent<CapsuleCollider2D>();
        playerBlue.transform.parent = playerParent.transform;


        mainCameraComp = GameObjectExtensions.GetTInChildren<Camera>(gameObject);

        level = new GameObject("Level");
        level.AddComponent<SpawnLevel>();
        level.transform.parent = transform;
    }

    void Update()
    {

    }

    public GameObject GetRedPlayer()
    {
        return playerRed;
    }

    public GameObject GetBluePlayer()
    {
        return playerBlue;
    }

    public GameObject CheckForCamera(GameObject assignTo)
    {
        GameObject child = gameObject.GetChildGameObjectWithComponent<Camera>();

        if (child != null)
        {
            return assignTo = child;
        }
        return null;
    }

    public Camera GetCamera()
    {
        return mainCameraComp;  
    }

    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerRed;
    public GameObject playerBlue;

    public GameManager gameManager;

    public Camera mainCamera;


    void Start()
    {
        gameManager = transform.parent.gameObject.GetComponent<GameManager>();

        mainCamera = gameManager.GetCamera();      


        playerRed = gameManager.GetComponent<GameManager>().GetRedPlayer();
        playerBlue = gameManager.GetComponent<GameManager>().GetBluePlayer();

        playerBlue.transform.position = new Vector2((mainCamera.orthographicSize * mainCamera.aspect) * (-8f / 10f), ((mainCamera.orthographicSize) * (-4f / 5f)));
        playerRed.transform.position = new Vector2((mainCamera.orthographicSize * mainCamera.aspect) * (-9f / 10f), ((mainCamera.orthographicSize) * (-4f / 5f)));
        Debug.Log(mainCamera.orthographicSize);
        Debug.Log(mainCamera.aspect);
    }

    void Update()
    {
        
    }
}

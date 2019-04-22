using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject playerRed;
    public GameObject playerBlue;

    public Movement moveBlue;
    public Movement moveRed;

    //grab shit 
    public bool inRange;
    public bool isGrabed;
    public bool isGrounded;
    public float checkDist;

    void Start()
    {
        playerRed.transform.position = new Vector2((mainCamera.orthographicSize * mainCamera.aspect) * (-9f / 10f), ((mainCamera.orthographicSize) * (-4f / 5f)));
        playerBlue.transform.position = new Vector2((mainCamera.orthographicSize * mainCamera.aspect) * (-8f / 10f), ((mainCamera.orthographicSize) * (-4f / 5f)));

        moveRed = playerRed.GetComponent<Movement>();
        moveBlue = playerBlue.GetComponent<Movement>();
    }

    private void Update()
    {
        inRange = InRange(playerRed, playerBlue, checkDist);

        DisableMovememnt(playerBlue, moveBlue.disableBlue && !moveBlue.disableRed);
        DisableMovememnt(playerRed, !moveRed.disableBlue && moveRed.disableRed);
        if (!isGrounded)
        {
            DisableMovememnt(playerBlue, false);
            DisableMovememnt(playerRed, false);
        }
    }

    public IEnumerator Grab(KeyCode obj1Code, GameObject obj1, GameObject obj2)
    { 
        if (!isGrabed && inRange)
        {
            isGrabed = true;
            DisableMovememnt(obj2, true);
            while (Input.GetKey(obj1Code))
            {
                obj2.transform.parent = obj1.transform;
                yield return new WaitForEndOfFrame();
            }
            isGrabed = false;
            DisableMovememnt(obj2, false);
            obj1.transform.DetachChildren();
            obj2.transform.parent = transform;
        }
    }

    public IEnumerator Throw(KeyCode obj1Code, GameObject obj2)
    {
        if (inRange)
        {
            while (Input.GetKey(obj1Code) && isGrounded)
            {
                obj2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 7f);
                isGrounded = false;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    bool InRange(GameObject obj1, GameObject obj2, float dist) //just range
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= dist;
    }

    void DisableMovememnt(GameObject obj, bool setToZero)
    {
        if (setToZero)
            obj.GetComponent<Movement>().speed = 0;
        else
            obj.GetComponent<Movement>().speed = obj.GetComponent<Movement>().normalSpeed;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform parent;
    PlayerController controller;

    KeyCode moveLeft;
    KeyCode moveRight;

    public SpawnLevel levelSpawner;

    bool inMovement;

    public float speed;
    public float normalSpeed;

    public bool disableBlue;
    public bool disableRed;

    private void Start()
    {
        parent = transform.parent;
        controller = parent.GetComponent<PlayerController>();

        for (int i = 0; i < transform.parent.parent.childCount; i++)
        {
            if (transform.parent.parent.GetChild(i).GetComponent<SpawnLevel>() != null)
            {
                levelSpawner = transform.parent.parent.GetChild(i).GetComponent<SpawnLevel>();
            }
        }

        if (GetComponent<InputHandlerBlue_base>() != null)
        {
            moveLeft = GetComponent<InputHandlerBlue_base>().moveLeft;
            moveRight = GetComponent<InputHandlerBlue_base>().moveRight;
        }
        else if(GetComponent<InputHandlerRed_base>() != null)
        {
            moveLeft = GetComponent<InputHandlerRed_base>().moveLeft;
            moveRight = GetComponent<InputHandlerRed_base>().moveRight;
        }
        else
        {
            Debug.LogError("NO INPUT COMPONENT FOUND!!");
        }


        speed = 1f / 10f;
        normalSpeed = speed;
        inMovement = false;
    }

    public IEnumerator MovePlayer(KeyCode direction)
    {
        if (!inMovement)
        {
            inMovement = true;
            while (Input.GetKey(direction))
            {
                if (direction == KeyCode.None)
                    break;

                else if (direction == moveLeft)
                {
                    if(!(transform.position.x <= -levelSpawner.levelHorSize + 1))
                        transform.Translate(new Vector2(-speed, 0));
                    if (Input.GetKey(moveRight))
                    {
                        break;
                    }
                }
                else if (direction == moveRight)
                {
                    if (!(transform.position.x >= levelSpawner.levelHorSize - 1))
                        transform.Translate(new Vector2(speed, 0));
                    if (Input.GetKey(moveLeft))
                    {
                        break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            inMovement = false;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            controller.isGrounded = true;
            disableBlue = false;
            disableRed = false;
        }
        else if (collision.gameObject.tag == "blueTile")
        {
            print("blue");
            disableBlue = false;
            disableRed = true;
            controller.isGrounded = true;
        }
        else if (collision.gameObject.tag == "redTile")
        {
            print("red");
            disableBlue = true;
            disableRed = false;
            controller.isGrounded = true;
        }
        else
        {
            disableBlue = false;
            disableRed = false;
            controller.isGrounded = true;
        }
    }
}

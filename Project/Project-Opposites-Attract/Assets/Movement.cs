using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform parent;
    PlayerController controller;
    public SpawnLevel levelSpawner;

    bool inMovement;

    public float speed;
    public float normalSpeed;

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
        speed = 1f / 10f;
        normalSpeed = speed;
        inMovement = false;
    }

    public IEnumerator MovePlayerBlue(KeyCode direction)
    {
        if (!inMovement)
        {
            inMovement = true;
            while (Input.GetKey(direction))
            {
                if (direction == KeyCode.None)
                    break;

                else if (direction == controller.moveLeftBlue)
                {
                    if(!(transform.position.x <= -levelSpawner.levelHorSize + 1))
                        transform.Translate(new Vector2(-speed, 0));
                    if (Input.GetKey(controller.moveRightBlue))
                    {
                        break;
                    }
                }
                else if (direction == controller.moveRightBlue)
                {
                    if (!(transform.position.x >= levelSpawner.levelHorSize - 1))
                        transform.Translate(new Vector2(speed, 0));
                    if (Input.GetKey(controller.moveLeftBlue))
                    {
                        break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            inMovement = false;
        }
    }

    public IEnumerator MovePlayerRed(KeyCode direction)
    {
        if (!inMovement)
        {
            inMovement = true;
            while (Input.GetKey(direction))
            {
                if (direction == KeyCode.None)
                    break;

                else if (direction == controller.moveLeftRed)
                {
                    if (!(transform.position.x <= -levelSpawner.levelHorSize + 1))
                        transform.Translate(new Vector2(-speed, 0));
                    if (Input.GetKey(controller.moveRightRed))
                    {
                        break;
                    }
                }
                else if (direction == controller.moveRightRed)
                {
                    if (!(transform.position.x >= levelSpawner.levelHorSize - 1))
                        transform.Translate(new Vector2(speed, 0));
                    if (Input.GetKey(controller.moveLeftRed))
                    {
                        break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            inMovement = false;
        }
    }

}

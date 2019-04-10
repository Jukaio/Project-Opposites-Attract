using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform parent;
    PlayerController controller;
    bool inMovement;

    private void Start()
    {
        parent = transform.parent;
        controller = parent.GetComponent<PlayerController>();
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
                    transform.Translate(new Vector2(-1f / 10f, 0));
                    if (Input.GetKey(controller.moveRightBlue))
                    {
                        break;
                    }
                }
                else if (direction == controller.moveRightBlue)
                {
                    transform.Translate(new Vector2(1f / 10f, 0));
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
                    transform.Translate(new Vector2(-1f / 10f, 0));
                    if (Input.GetKey(controller.moveRightRed))
                    {
                        break;
                    }
                }
                else if (direction == controller.moveRightRed)
                {
                    transform.Translate(new Vector2(1f / 10f, 0));
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

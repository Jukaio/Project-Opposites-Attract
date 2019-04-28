//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Movement : MonoBehaviour
//{
//    Transform parent;

//    KeyCode moveLeft;
//    KeyCode moveRight;

//    public SpawnLevel levelSpawner;

//    bool inMovement;

//    public float speed;
//    public float normalSpeed;

//    public bool disableBlue;
//    public bool disableRed;

//    private void Start()
//    {
//        parent = transform.parent;
        
//        for (int i = 0; i < transform.parent.parent.childCount; i++)
//        {
//            if (transform.parent.parent.GetChild(i).GetComponent<SpawnLevel>() != null)
//            {
//                levelSpawner = transform.parent.parent.GetChild(i).GetComponent<SpawnLevel>();
//            }
//        }

//        speed = 1f / 10f;
//        normalSpeed = speed;
//        inMovement = false;
//    }

//    public IEnumerator MovePlayer(KeyCode direction)
//    {
//        if (!inMovement)
//        {
//            inMovement = true;
//            while (Input.GetKey(direction))
//            {
//                if (direction == KeyCode.None)
//                    break;

//                else if (direction == moveLeft)
//                {
//                    if(!(transform.position.x <= -levelSpawner.levelHorSize + 1))
//                        transform.Translate(new Vector2(-speed, 0));
//                    if (Input.GetKey(moveRight))
//                    {
//                        break;
//                    }
//                }
//                else if (direction == moveRight)
//                {
//                    if (!(transform.position.x >= levelSpawner.levelHorSize - 1))
//                        transform.Translate(new Vector2(speed, 0));
//                    if (Input.GetKey(moveLeft))
//                    {
//                        break;
//                    }
//                }
//                yield return new WaitForEndOfFrame();
//            }
//            inMovement = false;
//        }
//    }


//}

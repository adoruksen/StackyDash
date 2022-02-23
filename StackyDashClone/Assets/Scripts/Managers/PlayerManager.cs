using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    float desiredSwipeValue = 20f;
    Vector2 touchDownPos;
    Vector2 touchUpPos ;

    public static Vector3 direction;

    public static bool isMoved;

    private void Start()
    {
        isMoved = false;
    }

    private void Update()
    {
        if (LevelManager.gameState == GameState.Normal || LevelManager.gameState == GameState.BeforeStart)
        {
            if (Input.touches.Length > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        touchUpPos = touch.position;
                        touchDownPos = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        touchDownPos = touch.position;
                        Detector();
                    }
                }
            }
        }
    }


    void Detector()
    {
        if ((VerticalMoveValue() > desiredSwipeValue || HorizontalMoveValue() > desiredSwipeValue) && !isMoved)
        {
            if (VerticalMoveValue() > HorizontalMoveValue())
            {
                if (touchDownPos.y - touchUpPos.y > 0)
                {
                    direction = Vector3.forward;
                }
                else if (touchDownPos.y - touchUpPos.y < 0)
                {
                    direction = Vector3.back;
                }
            }
            else
            {
                if (touchDownPos.x - touchUpPos.x > 0)
                {
                    direction = Vector3.right;
                }
                else if (touchDownPos.x - touchUpPos.x < 0)
                {
                    direction = Vector3.left;
                }
            }
            isMoved = true;
            touchUpPos = touchDownPos;
        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(touchDownPos.y - touchUpPos.y); //Absolute deðerler alýndý çünkü negatif transformlarý hesaba katmýyorum
    }
    float HorizontalMoveValue()
    {
        return Mathf.Abs(touchDownPos.x - touchUpPos.x);
    }

    
    public static Vector3 Direction
    {
        get
        {
            return direction;
        }
    }
}

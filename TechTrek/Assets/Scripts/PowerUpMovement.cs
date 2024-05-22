using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{

   public float moveSpeed = 2f;
    public float changeDirectionInterval = 1f;

    private Vector2 moveDirection;
    private float timer;
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width- 50, Screen.height - 50, mainCamera.transform.position.z));
        ChangeDirection();
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChangeDirection();
        }

        KeepWithinBounds();
    }

    void ChangeDirection()
    {
        int direction = (int) (0 + (3 - 0) * Generator.GetNextNumber());
        
        switch (direction)
        {
            case 0:
                moveDirection = Vector2.up;
                break;
            case 1:
                moveDirection = Vector2.down;
                break;
            case 2:
                moveDirection = Vector2.left;
                break;
            case 3:
                moveDirection = Vector2.right;
                break;
        }
        timer = changeDirectionInterval;
    }

    void KeepWithinBounds()
    {
        Vector3 pos = transform.position;
        if (pos.x > screenBounds.x)
        {
            pos.x = screenBounds.x;
            ChangeDirection();
        }
        else if (pos.x < -screenBounds.x)
        {
            pos.x = -screenBounds.x;
            ChangeDirection();
        }
        if (pos.y > screenBounds.y)
        {
            pos.y = screenBounds.y;
            ChangeDirection();
        }
        else if (pos.y < -screenBounds.y)
        {
            pos.y = -screenBounds.y;
            ChangeDirection();
        }
        transform.position = pos;
    }

}

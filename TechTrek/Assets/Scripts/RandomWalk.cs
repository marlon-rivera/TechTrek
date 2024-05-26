using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{

    public static Vector2 GetMovement()
    {
        int direction = (int)(0 + (3 - 0) * FindObjectOfType<Generator>().GetNextNumber());

        switch (direction)
        {
            case 0:
                return Vector2.left;
            case 1:
                return Vector2.down;
            case 2:
                return Vector2.right;
            default:
                return Vector2.up;
        }

    }
}

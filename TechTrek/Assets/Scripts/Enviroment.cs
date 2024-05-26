using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Transform playerPosition;
    public Boss boss;

    void Update()
    {
        if (playerPosition != null && boss != null)
        {
            boss.UpdateAction(playerPosition);
        }
    }

    
}

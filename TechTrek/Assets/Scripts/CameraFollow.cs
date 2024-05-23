using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    void FixedUpdate()
    {
        if (player != null)
        {

            Vector3 position = transform.position;
            position.x = player.transform.position.x;
            transform.position = position;
        }
    }
}

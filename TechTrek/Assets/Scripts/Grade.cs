using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float Speed;
    private Vector2 Direction;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void Destroy(){
        Destroy(gameObject);
    }
}

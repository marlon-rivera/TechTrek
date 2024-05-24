using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float Speed;
    private Vector2 Direction;
    public int damage;
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        if (damage == 1)
        {

            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.SetBool("destroy", true);
            
        }

    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void Destroy()
    {
        Animator animator = this.gameObject.GetComponent<Animator>();
        if (damage == 1)
        {

            animator.SetBool("destroy", true);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

        }else if(other.gameObject.CompareTag("boss")){
            Boss boss = other.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
   private Rigidbody2D Rigidbody2D;
   private float Horizontal;
   public float Speed;
   public float JumpForce;
   private bool Grounded;
   private Animator Animator;
    public float leftBound = -5.0f;
    private GameObject GradePrefab;
    private float LastShoot = 0.0f;


   void Start()
   {

        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        GradePrefab  = Resources.Load<GameObject>("prefabs/notas/Nota1");
   }

   void Update()
   {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if(Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            if (transform.position.x > leftBound)
            {
                Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
            }
            else
            {
                Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            }
        }
            
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        } 

        Animator.SetBool("running", Horizontal != 0.0f );
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.9f))
        {
            Grounded = true;
        }
        else Grounded = false;
        Animator.SetBool("jumping", !Grounded);
        if(Input.GetKeyDown(KeyCode.W) && Grounded){
            Jump();
        }
        if(Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
   }

    private void Shoot()
    {
        Vector3 Direction;
        if(transform.localScale.x == 1.0f) Direction = Vector2.right;
        else Direction = Vector2.left;
        GameObject grade = Instantiate(GradePrefab, transform.position + Direction * 0.9f, Quaternion.identity);
        grade.GetComponent<Grade>().SetDirection(Direction);
    }

   private void Jump()
   {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
   }

   private void FixedUpdate()
   {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
        
   }
}

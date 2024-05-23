using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
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
    private float[] luckOne;
    private float[] luckTwo;
    private float[] luckThree;
    private int lucky;

    public int health = 100;
    [SerializeField] Slider sliderLife;
    private CapsuleCollider2D boxCollider;
    public LayerMask capaSuelo;




    void Start()
    {

        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        luckOne = new float[] { 0.26f, 0.526f, 0.8f, 0.9f, 1.0f };
        luckTwo = new float[] { 0.1f, 0.37f, 0.64f, 0.91f, 1.0f };
        luckThree = new float[] { 0.1f, 0.2f, 0.47f, 0.74f, 1.0f };
        Generator.LoadData();
        lucky = (int)(1 + (3 - 1) * Generator.GetNextNumber());
        sliderLife.maxValue = health;
        sliderLife.value = sliderLife.maxValue;
        boxCollider = GetComponent<CapsuleCollider2D>();


    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f)
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

        Animator.SetBool("running", Horizontal != 0.0f);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.9f))
        {
            Grounded = true;
        }
        else Grounded = false;
        Animator.SetBool("jumping", !Grounded);
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        if (Generator.numbers.Count == 0)
        {

            Generator.LoadData();
        }

        float randomNumber = Generator.GetNextNumber();
        getPrefab(randomNumber);
        Vector3 Direction;
        if (transform.localScale.x == 1.0f) Direction = Vector2.right;
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

    void getPrefab(float randomNumber)
    {


        if (lucky == 1)
        {
            if (randomNumber <= luckOne[0])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota1");
            }
            else if (randomNumber <= luckOne[1])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota2");
            }
            else if (randomNumber <= luckOne[2])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota3");
            }
            else if (randomNumber <= luckOne[3])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota4");
            }
            else if (randomNumber <= luckOne[4])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota5");
            }
        }
        else if (lucky == 2)
        {
            if (randomNumber <= luckTwo[0])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota1");
            }
            else if (randomNumber <= luckTwo[1])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota2");
            }
            else if (randomNumber <= luckTwo[2])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota3");
            }
            else if (randomNumber <= luckTwo[3])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota4");
            }
            else if (randomNumber <= luckTwo[4])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota5");
            }
        }
        else if (lucky == 3)
        {
            if (randomNumber <= luckThree[0])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota1");
            }
            else if (randomNumber <= luckThree[1])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota2");
            }
            else if (randomNumber <= luckThree[2])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota3");
            }
            else if (randomNumber <= luckThree[3])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota4");
            }
            else if (randomNumber <= luckThree[4])
            {
                GradePrefab = Resources.Load<GameObject>("prefabs/notas/Nota5");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "PowerUp")
        {

            Destroy(collision.gameObject);

        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        sliderLife.value = health;
        if (health <= 0)
        {

            Destroy(this.gameObject);
        }
    }

    private bool puedeMoverse = true;
    public float fuerzaGolpe;
    private bool mirandoDerecha = true;

    public float velocidad;


    public void AplicarGolpe()
    {

        puedeMoverse = false;

        Vector2 direccionGolpe;

        if (Rigidbody2D.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else
        {
            direccionGolpe = new Vector2(1, 1);
        }

        Rigidbody2D.AddForce(direccionGolpe * fuerzaGolpe);

        StartCoroutine(EsperarYActivarMovimiento());
    }

    IEnumerator EsperarYActivarMovimiento()
    {
        // Esperamos antes de comprobar si esta en el suelo.
        yield return new WaitForSeconds(0.1f);

        while (!EstaEnSuelo())
        {
            // Esperamos al siguiente frame.
            yield return null;
        }

        // Si ya está en suelo activamos el movimiento.
        puedeMoverse = true;
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    void ProcesarMovimiento()
    {
        // Si no puede moverse, salimos de la funcion
        if (!puedeMoverse) return;

        // Lógica de movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");

        if (inputMovimiento != 0f)
        {
            Animator.SetBool("running", true);
        }
        else
        {
            Animator.SetBool("running", false);
        }

        Rigidbody2D.velocity = new Vector2(inputMovimiento * velocidad, Rigidbody2D.velocity.y);

        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        // Si se cumple condición
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            // Ejecutar código de volteado
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private int previousLucky;
    public int health = 150;
    [SerializeField] Slider sliderLife;
    private BoxCollider2D boxCollider;
    private bool unassailable = false;
    private PanelController panelController;
    private ManagerPopUps popUps;






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
        boxCollider = GetComponent<BoxCollider2D>();
        panelController = FindObjectOfType<PanelController>();
        popUps = FindObjectOfType<ManagerPopUps>();
        StartPresentation();
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
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.5f)
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
            if (collision.gameObject.name == "Festivo(Clone)" && !popUps.festivo)
            {
                GameManager.StopGame();
                panelController.ActiveFestivo();
                popUps.festivo = true;
            }
            else if (collision.gameObject.name == "Paro(Clone)" && !popUps.paro)
            {
                GameManager.StopGame();
                panelController.ActiveParo();
                popUps.paro = true;
            }
            else if (collision.gameObject.name == "Tutoria(Clone)" && !popUps.tutoria)
            {
                GameManager.StopGame();
                panelController.ActiveTutoria();
                popUps.tutoria = true;
            }
            SpawnPowerUps spawnScript = FindObjectOfType<SpawnPowerUps>();
            Debug.Log(spawnScript);
            if (spawnScript != null)
            {
                spawnScript.ShowPowerUp();
                string powerupType = spawnScript.GetPowerUpType();
                ApplyPowerUp(powerupType);
            }
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "kill")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Map");
        }
    }

    public void TakeDamage(int damage)
    {
        if (!unassailable)
        {
            health -= damage;
            sliderLife.value = health;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void ApplyPowerUp(string type)
    {
        Debug.Log(type);
        if (type == "tutoria")
        {
            previousLucky = lucky;
            if (lucky == 1)
            {
                lucky = 2;
            }
            else if (lucky == 2)
            {
                lucky = 3;
            }
        }
        else if (type == "paro")
        {
            unassailable = true;
        }
        else if (type == "festivo")
        {
            if (health < 150)
            {
                if (health < 150 && health > 130)
                {
                    health = 150;
                }
                else
                {
                    health += 20;
                }
                sliderLife.value = health;
            }
        }
    }

    public void RemovePowerUp(string type)
    {
        if (type == "tutoria")
        {
            lucky = previousLucky;

        }
        else if (type == "paro")
        {
            unassailable = false;
        }
    }

    private void StartPresentation(){       
        if(!popUps.presentacion){
            GameManager.StopGame();
            panelController.ActivePresentacion();
            popUps.presentacion = true;
        }
    }

}


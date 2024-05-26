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
    private float LastShoot = 0.0f;
    private int lucky;
    private int previousLucky;
    public int health = 150;
    [SerializeField] Slider sliderLife;
    private BoxCollider2D boxCollider;
    private bool unassailable = false;
    private PanelController panelController;
    public AudioManager audioManager;
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip failSound;
    public AudioClip damageSound;
    public AudioClip powerUpTakenSound;





    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        FindObjectOfType<Generator>().LoadData();
        lucky = (int)(1 + (3 - 1) * FindObjectOfType<Generator>().GetNextNumber());
        sliderLife.maxValue = health;
        sliderLife.value = sliderLife.maxValue;
        boxCollider = GetComponent<BoxCollider2D>();
        panelController = FindObjectOfType<PanelController>();
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
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        if (FindObjectOfType<Generator>().numbers.Count == 0)
        {
            FindObjectOfType<Generator>().LoadData();
        }

        float randomNumber = FindObjectOfType<Generator>().GetNextNumber();
        Vector3 Direction;
        if (transform.localScale.x == 1.0f) Direction = Vector2.right;
        else Direction = Vector2.left;
        GameObject grade = Instantiate(MonteCarlo.getPrefab(lucky), transform.position + Direction * 0.9f, Quaternion.identity);
        grade.GetComponent<Grade>().SetDirection(Direction);
        audioManager.PlaySound(attackSound);
    }

    private void Jump()
    {
        audioManager.PlaySound(jumpSound);
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            if (collision.gameObject.name == "Festivo(Clone)" && !ManagerPopUps.festivo)
            {
                GameManager.StopGame();
                panelController.ActiveFestivo();
                ManagerPopUps.festivo = true;
            }
            else if (collision.gameObject.name == "Paro(Clone)" && !ManagerPopUps.paro)
            {
                GameManager.StopGame();
                panelController.ActiveParo();
                ManagerPopUps.paro = true;
            }
            else if (collision.gameObject.name == "Tutoria(Clone)" && !ManagerPopUps.tutoria)
            {
                GameManager.StopGame();
                panelController.ActiveTutoria();
                ManagerPopUps.tutoria = true;
            }
            SpawnPowerUps spawnScript = FindObjectOfType<SpawnPowerUps>();
            Debug.Log(spawnScript);
            if (spawnScript != null)
            {
                spawnScript.ShowPowerUp();
                string powerupType = spawnScript.GetPowerUpType();
                ApplyPowerUp(powerupType);
            }
            ManagerPopUps.SaveData();
            audioManager.PlaySound(powerUpTakenSound);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "kill")
        {
            Destroy(gameObject);
            audioManager.PlaySound(failSound);
            GameManager.StopGame();
            panelController.ActiveFail();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!unassailable)
        {
            audioManager.PlaySound(damageSound);
            health -= damage;
            sliderLife.value = health;
            if (health <= 0)
            {
                Destroy(this.gameObject);
                audioManager.PlaySound(failSound);
                GameManager.StopGame();
                panelController.ActiveFail();
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

    private void StartPresentation()
    {
        Debug.Log("Presentacion: " + ManagerPopUps.presentacion);
        if (!ManagerPopUps.presentacion)
        {
            GameManager.StopGame();
            panelController.ActivePresentacion();
            ManagerPopUps.presentacion = true;
            ManagerPopUps.SaveData();
        }
        else
        {
            GameManager.ResumeGame();
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject[] bombs;
    private Rigidbody2D rgb2D;
    private string currentState;
    private GameObject bombPrefab;
    private float speed = 3f;
    private int health;
    [SerializeField] Slider slider;
    public Animator animator;
    private PanelController panelController;
    public AudioClip attackSound;
    public AudioManager audioManager;
    public AudioClip successSound;
    public AudioClip damageSound;

    void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        currentState = "expo";
        if (FindObjectOfType<Generator>().numbers.Count == 0)
        {
            FindObjectOfType<Generator>().LoadData();
        }
        health = 150;
        slider.maxValue = health;
        panelController = FindObjectOfType<PanelController>();

    }

    public void Shoot(Vector3 target)
    {
        if (FindObjectOfType<Generator>().numbers.Count == 0)
        {
            FindObjectOfType<Generator>().LoadData();
        }
        SelectBomb();
        Vector3 direction = (target - (Vector3)transform.position).normalized;

        GameObject bomb = Instantiate(bombPrefab, transform.position + direction * 0.9f, Quaternion.identity);
        bomb.GetComponent<Bomb>().SetDirection(direction);
        currentState = MarkovChain.GetNextState(currentState);
        audioManager.PlaySound(attackSound);

    }

    public void Follow(Vector3 target)
    {
        Vector3 direction = new Vector3(target.x - transform.position.x, 0, 0).normalized;
        rgb2D.velocity = new Vector2(direction.x * speed, rgb2D.velocity.y);
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = health;
        audioManager.PlaySound(damageSound);
        Debug.Log(health);
        if (health <= 0)
        {
            audioManager.PlaySound(successSound);
            Destroy(this.gameObject);
            GameManager.StopGame();
            panelController.ActiveSuccess();
        }
    }

    public void SelectBomb()
    {
        if (currentState == "expo")
        {
            bombPrefab = bombs[0];
        }
        else if (currentState == "taller")
        {
            bombPrefab = bombs[1];
        }
        else if (currentState == "parcial")
        {
            bombPrefab = bombs[2];
        }
        else
        {
            bombPrefab = bombs[3];
        }
    }

    public Animator GetAnimator()
    {
        return animator;
    }
    public float attackDistance = 5f;
    public float timeBetweenAttacks = 1f;
    private float timeNextAttack;


    public void UpdateAction(Transform playerPosition)
    {
        float distanceToplayerPosition = Vector2.Distance(this.transform.position, playerPosition.transform.position);
        if (distanceToplayerPosition <= attackDistance)
        {
            VerifyBoss();
            if (!GetAnimator().GetBool("attacking"))
            {
                if (CompareTag("Malex"))
                {
                   GetAnimator().SetBool("transform", true);
                }
                GetAnimator().SetBool("attacking", true);
                Debug.Log("Iniciando ataque: " + GetAnimator().GetBool("attacking"));
            }


            if (Time.time >= timeNextAttack)
            {
               Follow(playerPosition.transform.position);
                Shoot(playerPosition.transform.position);
                timeNextAttack = Time.time + timeBetweenAttacks;
            }
        }

        else
        {

            if (GetAnimator().GetBool("attacking"))
            {
                if (CompareTag("Malex"))
                {
                    GetAnimator().SetBool("transform", false);

                }
                GetAnimator().SetBool("attacking", false);
                Debug.Log("Terminando ataque: " + GetAnimator().GetBool("attacking"));
            }
        }
    }

    private void VerifyBoss()
    {
        if (panelController.panelErrorus != null)
        {
            if (!ManagerPopUps.errorus)
            {
                panelController.ActiveErrorus();
                ManagerPopUps.errorus = true;
                GameManager.StopGame();
            }
        }
        else if (panelController.panelDatus != null)
        {
            if (!ManagerPopUps.datus)
            {
                panelController.ActiveDatus();
                ManagerPopUps.datus = true;
                GameManager.StopGame();
            }
        }
        else if (panelController.panelCyber != null)
        {
            if (!ManagerPopUps.cyber)
            {
                panelController.ActiveCyber();
                ManagerPopUps.cyber = true;
                GameManager.StopGame();
            }
        }
        else if (panelController.panelMalex != null)
        {
            if (!ManagerPopUps.malex)
            {
                panelController.ActiveMalex();
                ManagerPopUps.malex = true;
                GameManager.StopGame();
            }
        }
        ManagerPopUps.SaveData();


    }
}

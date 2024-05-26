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
        Debug.Log(health);
        if (health <= 0)
        {
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


}

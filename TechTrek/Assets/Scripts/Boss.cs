using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject[] bombs;
    private Rigidbody2D rgb2D;
    private Dictionary<string, float[]> transitions;
    private string[] states = { "taller", "parcial", "expo", "proyecto" };
    private string currentState;
    private GameObject bombPrefab;
    private float speed = 3f;
    private int health;
    [SerializeField] Slider slider;
    public Animator animator;

    void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        currentState = "expo";
        transitions = new Dictionary<string, float[]>
        {
            { "taller", new float[] { 0.1f, 0.5f, 0.2f, 0.2f } },
            { "parcial", new float[] { 0.3f, 0.1f, 0.4f, 0.2f } },
            { "expo", new float[] { 0.2f, 0.3f, 0.1f, 0.4f } },
            { "proyecto", new float[] { 0.4f, 0.2f, 0.3f, 0.1f } }
        };
        if (Generator.numbers.Count == 0)
        {
            Generator.LoadData();
        }
        health = 150;
        slider.maxValue = health;
    }

    private void GetNextState()
    {
        float[] probs = transitions[currentState];
        float randomValue = Generator.GetNextNumber();
        float sum = 0;
        for (int i = 0; i < probs.Length; i++)
        {
            sum += probs[i];
            if (randomValue <= sum)
            {
                currentState = states[i];
                return;
            }
        }
    }

    public void Shoot(Vector3 target)
    {
        if (Generator.numbers.Count == 0)
        {
            Generator.LoadData();
        }
        SelectBomb();
        Vector3 direction = (target - (Vector3)transform.position).normalized;

        GameObject bomb = Instantiate(bombPrefab, transform.position + direction * 0.9f, Quaternion.identity);
        bomb.GetComponent<Bomb>().SetDirection(direction);
        GetNextState();

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

    public Animator GetAnimator(){
        return animator;
    }
}

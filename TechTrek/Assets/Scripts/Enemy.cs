using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rgb2D;

    public float velocityMovement;

    public LayerMask layerFront;
    public LayerMask layerBottom;

    public float distanceBottom;
    public float distanceFront;
    public Transform controlBottom;
    public Transform controlFront;
    public bool bottomInfo;
    public bool frontInfo;
    public bool lookingForward = true;
    private int health = 8;
    private int damage = 5;
    public LayerMask layerPlayer;
    public float detectionRadius = 0.3f;
    public float attackCooldown = 3f;

    private float lastAttackTime;


    private void Update()
    {
        rgb2D.velocity = new Vector2(velocityMovement, rgb2D.velocity.y);

        frontInfo = Physics2D.Raycast(controlFront.position, transform.right, distanceFront, layerFront);
        bottomInfo = Physics2D.Raycast(controlBottom.position, transform.up * -1, distanceBottom, layerBottom);
        if (frontInfo || !bottomInfo)
        {
            Flip();
        }
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerPlayer);
            foreach (Collider2D collider in colliders)
            {
                Player player = collider.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    private void Flip()
    {
        lookingForward = !lookingForward;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocityMovement *= -1;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controlBottom.transform.position, controlBottom.transform.position + transform.up * -1 * distanceBottom);
        Gizmos.DrawLine(controlFront.transform.position, controlFront.transform.position + transform.right * distanceFront);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}


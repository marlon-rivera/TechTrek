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
    private int health = 10;

    private void Update()
    {
        rgb2D.velocity = new Vector2(velocityMovement, rgb2D.velocity.y);

        frontInfo = Physics2D.Raycast(controlFront.position, transform.right, distanceFront, layerFront);
        bottomInfo = Physics2D.Raycast(controlBottom.position, transform.up * -1, distanceBottom, layerBottom);
        Debug.Log(bottomInfo);
        if (frontInfo || !bottomInfo)
        {
            Flip();
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
    }

    private int damage = 10;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                player.AplicarGolpe();
            }
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        Debug.Log("Salud: " + health);
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }
}


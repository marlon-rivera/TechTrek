using UnityEngine;

public class EnemyMovement : MonoBehaviour
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


    private void Update(){
         if (GetComponent<Rigidbody2D>() != null) {
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocityMovement, GetComponent<Rigidbody2D>().velocity.y);
        frontInfo = Physics2D.Raycast(controlFront.position, transform.right, distanceFront, layerFront);
        bottomInfo = Physics2D.Raycast(controlBottom.position, transform.up * -1, distanceBottom, layerBottom);

        if(frontInfo || bottomInfo){
            Flip();
        }
    }

    }

    private void Flip(){
        lookingForward = !lookingForward;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocityMovement *= -1;

    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controlBottom.transform.position, controlBottom.transform.position + transform.up * -1 * distanceBottom);
        Gizmos.DrawLine(controlFront.transform.position, controlFront.transform.position + transform.right * distanceFront);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limits : MonoBehaviour
{
    
    private Transform TheTransform;
    public Vector2 Hrange = Vector2.zero;
    public Vector2 Vrange = Vector2.zero;

    void LateUpdate()
    {
        TheTransform.position = new Vector3(
            Mathf.Clamp(transform.position.x, Vrange.x, Vrange.y),
            Mathf.Clamp(transform.position.y, Hrange.x, Hrange.y),
            transform.position.z
        );
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

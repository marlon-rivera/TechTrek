using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    public Transform[] points; // Lista de puntos por los que se moverá la plataforma
    public float speed = 2.0f;
    
    private int currentPointIndex = 0;

    void Start()
    {
        if (points.Length > 0)
        {
            // Inicialmente, la plataforma se mueve hacia el primer punto
            transform.position = points[0].position;
        }
    }

    void Update()
    {
        if (points.Length > 1)
        {
            // Mueve la plataforma hacia el siguiente punto en la lista
            transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].position, speed * Time.deltaTime);

            // Si la plataforma ha alcanzado el punto actual, cambia al siguiente
            if (Vector3.Distance(transform.position, points[currentPointIndex].position) < 0.1f)
            {
                currentPointIndex = (currentPointIndex + 1) % points.Length; // Avanza al siguiente punto en bucle
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{

    public static int CountEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        return enemies.Length;
    }
}

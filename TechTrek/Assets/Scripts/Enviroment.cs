using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour
{

    public GameObject player;
    public Boss boss;

    public float attackDistance = 5f;
    public float timeBetweenAttacks = 1f;
    private float timeNextAttack;

    void Update()
    {
        if(player != null && boss != null){
            float distanceToPlayer = Vector2.Distance(boss.transform.position, player.transform.position);
            if (distanceToPlayer <= attackDistance && Time.time >= timeNextAttack)
            {
                Debug.Log("Atacando ambiente");
                timeNextAttack = Time.time + timeBetweenAttacks;
                boss.Shoot(player.transform.position);
            }
        }        
    }
}

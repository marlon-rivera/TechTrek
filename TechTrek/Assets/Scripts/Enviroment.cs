using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject player;
    public Boss boss;

    public float attackDistance = 5f;
    public float timeBetweenAttacks = 1f;
    private float timeNextAttack;
    private PanelController panelController;
    private ManagerPopUps popUps;

    void Start()
    {
        panelController = FindObjectOfType<PanelController>();
        popUps = FindObjectOfType<ManagerPopUps>();
    }

    void Update()
    {
        if (player != null && boss != null)
        {
            float distanceToPlayer = Vector2.Distance(boss.transform.position, player.transform.position);
            if (distanceToPlayer <= attackDistance)
            {
                if (!popUps.errorus)
                {
                    panelController.ActiveErrorus();
                    popUps.errorus = true;
                    GameManager.StopGame();

                }
                else if (!popUps.datus)
                {
                    panelController.ActiveDatus();
                    popUps.datus = true;
                    GameManager.StopGame();
                }
                else if (!popUps.cyber)
                {
                    panelController.ActiveCyber();
                    popUps.cyber = true;
                    GameManager.StopGame();
                }
                else if (!popUps.malex)
                {
                    panelController.ActiveMalex();
                    popUps.malex = true;
                    GameManager.StopGame();
                }
                if (!boss.GetAnimator().GetBool("attacking"))
                {
                    if (boss.CompareTag("Malex"))
                    {
                        boss.GetAnimator().SetBool("transform", true);
                    }
                    boss.GetAnimator().SetBool("attacking", true);
                    Debug.Log("Iniciando ataque: " + boss.GetAnimator().GetBool("attacking"));
                }


                if (Time.time >= timeNextAttack)
                {
                    boss.Follow(player.transform.position);
                    boss.Shoot(player.transform.position);
                    timeNextAttack = Time.time + timeBetweenAttacks;
                }
            }

            else
            {

                if (boss.GetAnimator().GetBool("attacking"))
                {
                    if (boss.CompareTag("Malex"))
                    {
                        boss.GetAnimator().SetBool("transform", false);

                    }
                    boss.GetAnimator().SetBool("attacking", false);
                    Debug.Log("Terminando ataque: " + boss.GetAnimator().GetBool("attacking"));
                }
            }
        }
    }
}

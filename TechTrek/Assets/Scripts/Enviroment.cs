using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Transform playerPosition;
    public Boss boss;

    public float attackDistance = 5f;
    public float timeBetweenAttacks = 1f;
    private float timeNextAttack;
    private PanelController panelController;

    void Start()
    {
        panelController = FindObjectOfType<PanelController>();
    }

    void Update()
    {
        if (playerPosition != null && boss != null)
        {
            float distanceToplayerPosition = Vector2.Distance(boss.transform.position, playerPosition.transform.position);
            if (distanceToplayerPosition <= attackDistance)
            {
                VerifyBoss();
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
                    boss.Follow(playerPosition.transform.position);
                    boss.Shoot(playerPosition.transform.position);
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

    private void VerifyBoss()
    {
        if (panelController.panelErrorus != null)
        {
            if (!ManagerPopUps.errorus)
            {
                panelController.ActiveErrorus();
                ManagerPopUps.errorus = true;
                GameManager.StopGame();
            }
        }
        else if (panelController.panelDatus != null)
        {
            if (!ManagerPopUps.datus)
            {
                panelController.ActiveDatus();
                ManagerPopUps.datus = true;
                GameManager.StopGame();
            }
        }
        else if (panelController.panelCyber != null)
        {
            if (!ManagerPopUps.cyber)
            {
                panelController.ActiveCyber();
                ManagerPopUps.cyber = true;
                GameManager.StopGame();
            }
        }
        else if (panelController.panelMalex != null)
        {
            if (!ManagerPopUps.malex)
            {
                panelController.ActiveMalex();
                ManagerPopUps.malex = true;
                GameManager.StopGame();
            }
        }
        ManagerPopUps.SaveData();


    }
}

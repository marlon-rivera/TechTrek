using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelController : MonoBehaviour
{

    public GameObject panelFestivo;
    public GameObject panelParo;
    public GameObject panelTutoria;
    public GameObject panelPresentacion;
    public GameObject panelMalex;
    public GameObject panelDatus;
    public GameObject panelCyber;
    public GameObject panelErrorus;
    public GameObject panelSuccess;
    public GameObject panelFail;

    private int totalEnemies;
    void Start()
    {
        totalEnemies = EnemyCounter.CountEnemies();
    }

    public void ActiveFestivo()
    {
        panelFestivo.SetActive(true);
    }

    public void InactiveFestivo()
    {
        panelFestivo.SetActive(false);
    }

    public void ActiveParo()
    {
        panelParo.SetActive(true);
    }

    public void InactiveParo()
    {
        panelParo.SetActive(false);
    }

    public void ActiveTutoria()
    {
        panelTutoria.SetActive(true);
    }

    public void InactiveTutoria()
    {
        panelTutoria.SetActive(false);
    }

    public void ActivePresentacion()
    {
        panelPresentacion.SetActive(true);
    }

    public void InactivePresentacion()
    {
        panelPresentacion.SetActive(false);
    }

    public void ActiveMalex()
    {
        panelMalex.SetActive(true);
    }

    public void InactiveMalex()
    {
        panelMalex.SetActive(false);
    }

    public void ActiveDatus()
    {
        panelDatus.SetActive(true);
    }

    public void InactiveDatus()
    {
        panelDatus.SetActive(false);
    }

    public void ActiveCyber()
    {
        panelCyber.SetActive(true);
    }

    public void InactiveCyber()
    {
        panelCyber.SetActive(false);
    }

    public void ActiveErrorus()
    {
        panelErrorus.SetActive(true);
    }

    public void InactiveErrorus()
    {
        panelErrorus.SetActive(false);
    }

    public void ActiveSuccess()
    {
        Player player = FindObjectOfType<Player>();
        int enemiesKilled = EnemyCounter.CountEnemies();
        int starts = CalculateStarRating(player.health, enemiesKilled);
        //Debug.Log("Estrellas: " + starts + ", enemiesKilled: " + enemiesKilled + ", enemigos totales: " + totalEnemies+ ", health: " + player.health);
        panelSuccess.SetActive(true);
        Image[] images = panelSuccess.GetComponentsInChildren<Image>();

        foreach (Image img in images)
        {

                Debug.Log("Imagen encontrada: " + img.name);
            
        }
        for (int i = 0; i < starts; i++)
        {

        }
    }

    public void InactiveSuccess()
    {
        panelSuccess.SetActive(false);
    }

    public void ActiveFail()
    {

        panelFail.SetActive(true);
    }

    public void InactiveFail()
    {
        panelFail.SetActive(false);
    }

    public int CalculateStarRating(int currentLife, int enemiesKilled)
    {
        float lifePercentage = (float)currentLife / 150;
        float enemiesKilledPercentage = (float)enemiesKilled / totalEnemies;
        float overallScore = (lifePercentage + enemiesKilledPercentage) / 2;

        if (overallScore >= 0.8f)
        {
            return 3;
        }
        else if (overallScore >= 0.5f)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

}

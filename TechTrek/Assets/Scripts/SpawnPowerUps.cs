using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpawnPowerUps : MonoBehaviour
{
    private int[] arrivalTimeProb = new int[] { 0, 25, 50, 75 };
    private int[] serviceTimeProb = new int[] { 0, 20, 60, 85 };
    private int[] arrivalTime = new int[4];
    private int[] serviceTime = new int[4];
    private float minX, maxX, minY, maxY;
    public Transform[] points = new Transform[4];
    public GameObject[] powerups;
    private int[] arrivalTimeSum = new int[4];
    private int[] serviceTimeSum = new int[4];
    private int time = 0;
    private GameObject powerup = null;
    private int posArrival = 0;
    private int posService = 0;

    public Camera mainCamera;
    private string[] types = new string[] { "festivo", "paro", "tutoria" };
    private string actualType;
    public GameObject[] typesAssigned;

    private int powerRandom;

    void Start()
    {
        Generator.LoadData();
        mainCamera = Camera.main;

        for (int i = 0; i < 4; i++)
        {
            int number = (int)(0 + (99 - 0) * Generator.GetNextNumber());

            if (number <= arrivalTimeProb[1])
            {
                arrivalTime[i] = 16;
            }
            else if (number <= arrivalTimeProb[2])
            {
                arrivalTime[i] = 17;

            }
            else if (number <= arrivalTimeProb[3])
            {
                arrivalTime[i] = 18;
            }
            else
            {
                arrivalTime[i] = 19;
            }
            if (i == 0)
            {
                arrivalTimeSum[i] = arrivalTime[i];
            }
            else
            {
                arrivalTimeSum[i] = arrivalTimeSum[i - 1] + arrivalTime[i];
            }

        }

        for (int i = 0; i < 4; i++)
        {
            int number = (int)(0 + (99 - 0) * Generator.GetNextNumber());

            if (number <= serviceTimeProb[1])
            {
                serviceTime[i] = 4;
            }
            else if (number <= serviceTimeProb[2])
            {
                serviceTime[i] = 5;
            }
            else if (number <= serviceTimeProb[3])
            {
                serviceTime[i] = 6;
            }
            else
            {
                serviceTime[i] = 7;
            }
            serviceTimeSum[i] = arrivalTimeSum[i] + serviceTime[i];
        }

        typesAssigned[0].SetActive(false);
        typesAssigned[1].SetActive(false);
        typesAssigned[2].SetActive(false);

        InvokeRepeating("IncreaseTime", 1f, 1f);
    }




    void IncreaseTime()
    {
        time++;


        if (posArrival < 4)
        {
            if (time == arrivalTimeSum[posArrival])
            {
                Vector2 spawnPosition = new Vector2((int)(minX + (maxX - minX) * Generator.GetNextNumber() + 100), (int)(minY + (maxY - minY) * Generator.GetNextNumber()));
                powerRandom = (int)(0 + (powerups.Length - 0) * Generator.GetNextNumber());
                actualType = types[powerRandom];
                
                powerup = Instantiate(powerups[powerRandom], spawnPosition, Quaternion.identity);
                posArrival++;
            }
        }
        if (posService < 4)
        {
            if (time == serviceTimeSum[posService])
            {
                Destroy(powerup);
                Player player = FindObjectOfType<Player>();
                player.RemovePowerUp(actualType);
                typesAssigned[powerRandom].SetActive(false);
                posService++;
            }
        }
    }

    void LateUpdate()
    {
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        Vector3 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        points[0].position = bottomLeft;
        points[1].position = topLeft;
        points[2].position = topRight;
        points[3].position = bottomRight;

        maxX = points.Max(point => point.position.x);
        minX = points.Min(point => point.position.x);
        minY = points.Min(point => point.position.y);
        maxY = points.Max(point => point.position.y);
    }

    public void ShowPowerUp()
    {
        typesAssigned[powerRandom].SetActive(true);
    }

    public string GetPowerUpType()
    {
        return actualType;
    }
}

